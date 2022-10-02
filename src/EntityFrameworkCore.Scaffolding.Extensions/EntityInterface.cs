using Microsoft.EntityFrameworkCore.Metadata;

namespace EntityFrameworkCore.Scaffolding.Extensions;

public abstract class EntityInterface
{
    public Type Interface { get; set; }

    public abstract bool Check(IEntityType entity);
}
