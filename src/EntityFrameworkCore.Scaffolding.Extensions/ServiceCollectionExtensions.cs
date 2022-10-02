using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFrameworkCore.Scaffolding.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScaffoldingExtensions(this IServiceCollection services, Action<ScaffoldingOptions> configureOptions)
    {
        var options = new ScaffoldingOptions();
        configureOptions(options);
        services.AddSingleton(options);

        services.AddSingleton<IModelCodeGenerator, CustomTextTemplatingModelGenerator>();
        services.AddSingleton<IScaffoldingModelFactory, CustomScaffoldingModelFactory>();
        services.AddSingleton<EntityTypeHelper>();

        return services;
    }
}
