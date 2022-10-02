namespace EntityFrameworkCore.Scaffolding.Extensions.CommonInterfaces;

public interface ISoftDeletableEntity
{
    public bool IsDeleted { get; set; }
}
