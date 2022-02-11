using TestJob01.ApplicationCore.Interfaces;
using TestJob01.ApplicationCore.Services;
using TestJob01.Infrastructure.Data;

namespace TestJob01.API.Configuration;
internal static class ConfigureCoreServices {
    public static IServiceCollection AddCoreServices(this IServiceCollection services,
            IConfiguration configuration) {
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IRemainderService, RemainderService>();
        services.AddScoped<ITransferService, TransferService>();
        services.AddScoped<IWarehouseService, WarehouseService>();

        return services;
    }
}
