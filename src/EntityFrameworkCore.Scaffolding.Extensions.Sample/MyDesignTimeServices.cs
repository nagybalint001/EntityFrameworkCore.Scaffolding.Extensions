using EntityFrameworkCore.Scaffolding.Extensions.CommonInterfaces;
using EntityFrameworkCore.Scaffolding.Extensions.Sample.Enums;
using EntityFrameworkCore.Scaffolding.Extensions.Sample.Interfaces;

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFrameworkCore.Scaffolding.Extensions.Sample;

public class MyDesignTimeServices : IDesignTimeServices
{
    public void ConfigureDesignTimeServices(IServiceCollection services)
    {
        services.AddScaffoldingExtensions(options =>
        {
            options.ExcludeTable("HideThis");

            options
                .UseTable("Tree")
                    .AddEnumColumn<TreeType>("Type")
                .UseTable("Apple")
                    .AddEnumColumn<AppleStatus>("Status");

            options
                .AddAuditableInterface<DateTimeOffset, Guid>()
                .AddSoftDeletableInterface()
                .AddInterface<MyInterface>(entity => entity.Name == "Apple");
        });
    }
}
