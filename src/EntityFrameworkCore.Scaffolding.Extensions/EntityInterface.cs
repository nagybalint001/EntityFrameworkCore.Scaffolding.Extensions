using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFrameworkCore.Scaffolding.Extensions;

public class EntityInterface
{
    protected Func<IEntityType, bool> CheckFunc { get; }
    public Type Interface { get; }

    public EntityInterface(Type @interface, Func<IEntityType, bool> checkFunc)
    {
        Interface = @interface;
        CheckFunc = checkFunc;
    }

    public virtual bool IsImplementedBy(IEntityType entity)
    {
        return CheckFunc(entity);
    }

    public static EntityInterface Create<TInterface>(Func<IEntityType, bool> checkFunc)
    {
        return new EntityInterface(typeof(TInterface), checkFunc);
    }
}
