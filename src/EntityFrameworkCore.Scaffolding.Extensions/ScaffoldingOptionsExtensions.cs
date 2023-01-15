using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFrameworkCore.Scaffolding.Extensions;

public static class ScaffoldingOptionsExtensions
{
    public static ColumnTypeMappingBuilder UseSchema(this ScaffoldingOptions options, string schemaName = "dbo")
    {
        return new ColumnTypeMappingBuilder(options.ColumnTypeMapping)
        {
            SchemaName = schemaName,
        };
    }

    public static ColumnTypeMappingBuilder UseSchema(this ColumnTypeMappingBuilder builder, string schemaName = "dbo")
    {
        builder.SchemaName = schemaName;
        builder.TableName = null;

        return builder;
    }

    public static ColumnTypeMappingBuilder UseTable(this ScaffoldingOptions options, string tableName)
    {
        return new ColumnTypeMappingBuilder(options.ColumnTypeMapping)
        {
            TableName = tableName,
        };
    }

    public static ColumnTypeMappingBuilder UseTable(this ColumnTypeMappingBuilder builder, string tableName)
    {
        builder.TableName = tableName;

        return builder;
    }

    public static ColumnTypeMappingBuilder AddEnumColumn<TEnum>(this ColumnTypeMappingBuilder builder, string columnName)
        where TEnum : Enum
    {
        if (builder.SchemaName == null)
        {
            throw new InvalidOperationException("Schema name was not specified.");
        }

        if (builder.TableName == null)
        {
            throw new InvalidOperationException("Table name was not specified.");
        }

        builder.ColumnTypeMapping.Add(new DbColumn(builder.SchemaName, builder.TableName, columnName), typeof(TEnum));

        return builder;
    }

    public static ScaffoldingOptions AddInterface(this ScaffoldingOptions options, EntityInterface entityInterface)
    {
        options.EntityInterfaces.Add(entityInterface);

        return options;
    }

    public static ScaffoldingOptions AddInterface<TInterface>(this ScaffoldingOptions options, Func<IEntityType, bool> checkFunc)
    {
        return options.AddInterface(EntityInterface.Create<TInterface>(checkFunc));
    }

    public static ScaffoldingOptions AddInterface<TInterface>(this ScaffoldingOptions options, params string[] propertyNames)
    {
        return options.AddInterface<TInterface>(entity => entity.FindProperties(propertyNames) != null);
    }

    public static ScaffoldingOptions ExcludeTable(this ScaffoldingOptions options, string tableName)
    {
        options.ExcludedTables.Add(tableName);

        return options;
    }
}
