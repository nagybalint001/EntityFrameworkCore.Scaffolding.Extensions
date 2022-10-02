namespace EntityFrameworkCore.Scaffolding.Extensions;

public class ColumnTypeMappingBuilder
{
    public Dictionary<DbColumn, Type> ColumnTypeMapping { get; }

    public string? SchemaName { get; internal set; } = "dbo";

    public string? TableName { get; internal set; }

    public ColumnTypeMappingBuilder(Dictionary<DbColumn, Type> columnTypeMapping)
    {
        ColumnTypeMapping = columnTypeMapping;
    }
}
