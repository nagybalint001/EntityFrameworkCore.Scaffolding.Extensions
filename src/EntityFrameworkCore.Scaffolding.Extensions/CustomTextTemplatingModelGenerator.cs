using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;

using Mono.TextTemplating;

using System.CodeDom.Compiler;

using System.Text;

namespace EntityFrameworkCore.Scaffolding.Extensions;

public class CustomTextTemplatingModelGenerator : TextTemplatingModelGenerator
{
    private const string DbContextTemplate = "DbContext.t4";
    private const string EntityTypeTemplate = "EntityType.t4";
    private const string EntityTypeConfigurationTemplate = "EntityTypeConfiguration.t4";

    private readonly IOperationReporter _reporter;
    private readonly IServiceProvider _serviceProvider;
    private readonly EntityTypeHelper _entityTypeHelper;
    private readonly ScaffoldingOptions _scaffoldingOptions;

    public CustomTextTemplatingModelGenerator(
        ModelCodeGeneratorDependencies dependencies,
        IOperationReporter reporter,
        IServiceProvider serviceProvider,
        EntityTypeHelper entityTypeHelper,
        ScaffoldingOptions scaffoldingOptions)
        : base(dependencies, reporter, serviceProvider)
    {
        _reporter = reporter;
        _serviceProvider = serviceProvider;
        _entityTypeHelper = entityTypeHelper;
        _scaffoldingOptions = scaffoldingOptions;
    }

    public override ScaffoldedModel GenerateModel(IModel model, ModelCodeGenerationOptions options)
    {
        if (options.ContextName == null)
        {
            throw new ArgumentException(
                CoreStrings.ArgumentPropertyNull(nameof(options.ContextName), nameof(options)), nameof(options));
        }

        if (options.ConnectionString == null)
        {
            throw new ArgumentException(
                CoreStrings.ArgumentPropertyNull(nameof(options.ConnectionString), nameof(options)), nameof(options));
        }

        var host = new TextTemplatingEngineHost(_serviceProvider)
        {
            Session =
            {
                { "Model", model },
                { "Options", options },
                { "NamespaceHint", options.ContextNamespace ?? options.ModelNamespace },
                { "ProjectDefaultNamespace", options.RootNamespace },
                { "DbContextBase", _scaffoldingOptions.DbContextBase }
            }
        };
        var contextTemplate = Path.Combine(options.ProjectDir!, TemplatesDirectory, DbContextTemplate);

        string generatedCode;
        if (File.Exists(contextTemplate))
        {
            host.TemplateFile = contextTemplate;

            generatedCode = Engine.ProcessTemplate(File.ReadAllText(contextTemplate), host);
            CheckEncoding(host.OutputEncoding);
            HandleErrors(host);
        }
        else
        {
            if (!string.Equals(options.Language, "C#", StringComparison.OrdinalIgnoreCase))
            {
                throw new OperationException(DesignStrings.NoContextTemplate);
            }

            var defaultContextTemplate = new CSharpDbContextGenerator { Host = host, Session = host.Session };
            defaultContextTemplate.Initialize();

            generatedCode = defaultContextTemplate.TransformText();

            foreach (CompilerError error in defaultContextTemplate.Errors)
            {
                _reporter.Write(error);
            }

            if (defaultContextTemplate.Errors.HasErrors)
            {
                throw new OperationException(DesignStrings.ErrorGeneratingOutput(defaultContextTemplate.GetType().Name));
            }
        }

        var dbContextFileName = options.ContextName + host.Extension;
        var resultingFiles = new ScaffoldedModel
        {
            ContextFile = new ScaffoldedFile
            {
                Path = options.ContextDir != null
                    ? Path.Combine(options.ContextDir, dbContextFileName)
                    : dbContextFileName,
                Code = generatedCode
            }
        };

        var entityTypeTemplate = Path.Combine(options.ProjectDir!, TemplatesDirectory, EntityTypeTemplate);
        if (File.Exists(entityTypeTemplate))
        {
            host.TemplateFile = entityTypeTemplate;

            CompiledTemplate? compiledEntityTypeTemplate = null;
            string? entityTypeExtension = null;
            try
            {
                foreach (var entityType in model.GetEntityTypes())
                {
                    var interfaceTypes = _entityTypeHelper.GetInterfaces(entityType);

                    host.Initialize();
                    host.Session.Add("EntityType", entityType);
                    host.Session.Add("Options", options);
                    host.Session.Add("NamespaceHint", options.ModelNamespace);
                    host.Session.Add("ProjectDefaultNamespace", options.RootNamespace);
                    host.Session.Add("Interfaces", interfaceTypes);

                    if (compiledEntityTypeTemplate is null)
                    {
                        compiledEntityTypeTemplate = Engine.CompileTemplate(File.ReadAllText(entityTypeTemplate), host);
                        entityTypeExtension = host.Extension;
                        CheckEncoding(host.OutputEncoding);
                    }

                    generatedCode = compiledEntityTypeTemplate.Process();
                    HandleErrors(host);

                    if (string.IsNullOrWhiteSpace(generatedCode))
                    {
                        continue;
                    }

                    var entityTypeFileName = entityType.Name + entityTypeExtension;
                    resultingFiles.AdditionalFiles.Add(
                        new ScaffoldedFile { Path = entityTypeFileName, Code = generatedCode });
                }
            }
            finally
            {
                compiledEntityTypeTemplate?.Dispose();
            }
        }

        var configurationTemplate = Path.Combine(options.ProjectDir!, TemplatesDirectory, EntityTypeConfigurationTemplate);
        if (File.Exists(configurationTemplate))
        {
            host.TemplateFile = configurationTemplate;

            CompiledTemplate? compiledConfigurationTemplate = null;
            string? configurationExtension = null;
            try
            {
                foreach (var entityType in model.GetEntityTypes())
                {
                    host.Initialize();
                    host.Session.Add("EntityType", entityType);
                    host.Session.Add("Options", options);
                    host.Session.Add("NamespaceHint", options.ContextNamespace ?? options.ModelNamespace);
                    host.Session.Add("ProjectDefaultNamespace", options.RootNamespace);

                    if (compiledConfigurationTemplate is null)
                    {
                        compiledConfigurationTemplate = Engine.CompileTemplate(File.ReadAllText(configurationTemplate), host);
                        configurationExtension = host.Extension;
                        CheckEncoding(host.OutputEncoding);
                    }

                    generatedCode = compiledConfigurationTemplate.Process();
                    HandleErrors(host);

                    if (string.IsNullOrWhiteSpace(generatedCode))
                    {
                        continue;
                    }

                    var configurationFileName = entityType.Name + "Configuration" + configurationExtension;
                    resultingFiles.AdditionalFiles.Add(
                        new ScaffoldedFile
                        {
                            Path = options.ContextDir != null
                                ? Path.Combine(options.ContextDir, configurationFileName)
                                : configurationFileName,
                            Code = generatedCode
                        });
                }
            }
            finally
            {
                compiledConfigurationTemplate?.Dispose();
            }
        }

        return resultingFiles;
    }

    private void CheckEncoding(Encoding outputEncoding)
    {
        if (outputEncoding != Encoding.UTF8)
        {
            _reporter.WriteWarning(DesignStrings.EncodingIgnored(outputEncoding.WebName));
        }
    }

    private void HandleErrors(TextTemplatingEngineHost host)
    {
        foreach (CompilerError error in host.Errors)
        {
            _reporter.Write(error);
        }

        if (host.Errors.HasErrors)
        {
            throw new OperationException(DesignStrings.ErrorGeneratingOutput(host.TemplateFile));
        }
    }
}
