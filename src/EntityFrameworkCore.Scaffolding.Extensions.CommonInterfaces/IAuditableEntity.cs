namespace EntityFrameworkCore.Scaffolding.Extensions.CommonInterfaces;

public interface IAuditableEntity<TDateTime, TUser>
{
    public TDateTime CreatedAt { get; set; }

    public TUser CreatedBy { get; set; }

    public TDateTime ModifiedAt { get; set; }

    public TUser ModifiedBy { get; set; }
}
