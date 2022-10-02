namespace EntityFrameworkCore.Scaffolding.Extensions;

public class DbColumn : IEquatable<DbColumn?>
{
    public string SchemaName { get; }

    public string TableName { get; }

    public string ColumnName { get; }

    public DbColumn(string schemaName, string tableName, string columnName)
    {
        SchemaName = schemaName;
        TableName = tableName;
        ColumnName = columnName;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as DbColumn);
    }

    public bool Equals(DbColumn? other)
    {
        return other is not null &&
            SchemaName == other.SchemaName &&
            TableName == other.TableName &&
            ColumnName == other.ColumnName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(SchemaName, TableName, ColumnName);
    }
}
