using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TestJob01.Infrastructure.Data;

namespace TestJob01.Infrastructure;
public static class Dependencies {
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services) {
        //services.AddDbContext<WarehouseKeeperContext>(c =>
        //   c.UseInMemoryDatabase("WarehouseKeeper"));
        services.AddDbContext<TestJob01Context>(c =>
            c.UseSqlServer(configuration.GetConnectionString("TestJob01Connection")));

    }
}
