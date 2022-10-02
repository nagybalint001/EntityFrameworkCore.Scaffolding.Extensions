using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFrameworkCore.Scaffolding.Extensions;

public class PropertyNameBasedEntityInterface : EntityInterface
{
    protected string[] PropertyNames;

    public PropertyNameBasedEntityInterface(Type interfaceType, params string[] propertyNames)
    {
        Interface = interfaceType;
        PropertyNames = propertyNames;
    }

    public override bool Check(IEntityType entity)
    {
        return entity.FindProperties(PropertyNames) != null;
    }
}
