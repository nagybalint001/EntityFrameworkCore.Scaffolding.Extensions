using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Scaffolding.Extensions;

public class ScaffoldingOptions
{
    public Type DbContextBase { get; set; } = typeof(DbContext);

    public Dictionary<DbColumn, Type> ColumnTypeMapping { get; } = new();

    public List<EntityInterface> EntityInterfaces { get; } = new();

    public List<string> ExcludedTables { get; } = new();
}
