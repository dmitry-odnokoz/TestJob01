using TestJob01.UI_BL.Interfaces;
using TestJob01.UI_BL.Services;

namespace TestJob01.UI_BL;
public static class ServicesConfiguration {
    public static IServiceCollection AddBlazorServices(this IServiceCollection services) {
        services.AddScoped<ITransferHeadService, TransferHeadService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IWarehouseService, WarehouseService>();

        return services;
    }
}
