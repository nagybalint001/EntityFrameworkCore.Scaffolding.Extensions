namespace EntityFrameworkCore.Scaffolding.Extensions.CommonInterfaces;

public static class ScaffoldingOptionsExtensions
{
    public static ScaffoldingOptions AddAuditableInterface<TDateTime, TUser>(this ScaffoldingOptions options)
    {
        options.AddInterface<IAuditableEntity<TDateTime, TUser>>("CreatedAt", "CreatedBy", "ModifiedAt", "ModifiedBy");

        return options;
    }

    public static ScaffoldingOptions AddSoftDeletableInterface(this ScaffoldingOptions options)
    {
        options.AddInterface<ISoftDeletableEntity>("IsDeleted");

        return options;
    }
}
