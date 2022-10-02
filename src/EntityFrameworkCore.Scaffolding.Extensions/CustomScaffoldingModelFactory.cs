using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace EntityFrameworkCore.Scaffolding.Extensions;

public class CustomScaffoldingModelFactory : RelationalScaffoldingModelFactory
{
    private readonly ScaffoldingOptions _scaffoldingOptions;

    public CustomScaffoldingModelFactory(
        IOperationReporter reporter,
        ICandidateNamingService candidateNamingService,
        IPluralizer pluralizer,
        ICSharpUtilities cSharpUtilities,
        IScaffoldingTypeMapper scaffoldingTypeMapper,
        IModelRuntimeInitializer modelRuntimeInitializer,
        ScaffoldingOptions scaffoldingOptions)
        : base(reporter, candidateNamingService, pluralizer, cSharpUtilities, scaffoldingTypeMapper, modelRuntimeInitializer)
    {
        _scaffoldingOptions = scaffoldingOptions;
    }

    protected override TypeScaffoldingInfo? GetTypeScaffoldingInfo(DatabaseColumn column)
    {
        var enumType = _scaffoldingOptions
            .ColumnTypeMapping
            .GetValueOrDefault(new DbColumn(column.Table.Schema, column.Table.Name, column.Name));

        if (enumType != null)
        {
            return new TypeScaffoldingInfo(enumType, false, null, null, null, null, null);
        }

        return base.GetTypeScaffoldingInfo(column);
    }
}
