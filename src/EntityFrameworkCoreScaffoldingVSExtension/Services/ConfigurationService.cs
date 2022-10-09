using EntityFrameworkCoreScaffoldingVSExtension.Models;

using Newtonsoft.Json;

using System.IO;
using System.Threading.Tasks;

namespace EntityFrameworkCoreScaffoldingVSExtension.Services;

public class ConfigurationService
{
    public const string SettingsFileName = "scaffoldingsettings.json";

    public static async Task<ScaffoldingSettings> LoadSettingsAsync()
    {
        var settingsPath = await GetSettingsPathAsync();

        if (!File.Exists(settingsPath))
            return null;

        var json = File.ReadAllText(settingsPath);
        return JsonConvert.DeserializeObject<ScaffoldingSettings>(json);
    }

    public static async Task SaveSettingsAsync(ScaffoldingSettings model)
    {
        var settingsPath = await GetSettingsPathAsync();

        var json = JsonConvert.SerializeObject(model, Formatting.Indented);
        File.WriteAllText(settingsPath, json);
    }

    public static ScaffoldingSettings GetInitialModel()
    {
        return new ScaffoldingSettings()
        {
            DataSource = "Data Source=.\\SQLEXPRESS; Initial Catalog=MyDatabase; Integrated Security=True",
            Provider = "Microsoft.EntityFrameworkCore.SqlServer",
            ContextName = "MyDbContext",
            ContextDirectory = ".",
            OutputDirectory = "Entities",
            ContextNamespace = null,
            Namespace = null,
            Schemas = null,
            Tables = null,
            ProjectDirectory = ".",
            StartupProjectDirectory = ".",
            TargetFramework = null,
            Configuration = null,
            TargetRuntime = null,
            DataAnnotations = false,
            UseDatabaseNames = false,
            NoOnConfiguring = true,
            NoPluralize = true,
            Force = true,
            NoBuild = false,
            Verbose = false,
        };
    }

    private static async Task<string> GetSettingsPathAsync()
    {
        var project = await VS.Solutions.GetActiveProjectAsync();
        var projectDirectory = Path.GetDirectoryName(project.FullPath);
        return Path.Combine(projectDirectory, SettingsFileName);
    }
}
