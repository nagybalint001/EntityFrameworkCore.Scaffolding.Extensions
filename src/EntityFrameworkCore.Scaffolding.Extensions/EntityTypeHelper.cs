using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFrameworkCore.Scaffolding.Extensions;

public class EntityTypeHelper
{
    private readonly ScaffoldingOptions _options;

    public EntityTypeHelper(ScaffoldingOptions options)
    {
        _options = options;
    }

    public List<Type> GetInterfaces(IEntityType entityType)
    {
        return _options.EntityInterfaces
            .Where(i => i.IsImplementedBy(entityType))
            .Select(i => i.Interface)
            .ToList();
    }
}
