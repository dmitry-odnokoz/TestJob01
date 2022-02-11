using TestJob01.UI_BL.Interfaces;
using TestJob01.UI_BL.Models;

namespace TestJob01.UI_BL.Services;
public class WarehouseService: IWarehouseService {
    private readonly HttpService _httpService;
    private readonly ILogger<WarehouseService> _logger;

    public WarehouseService(HttpService httpService,
        ILogger<WarehouseService> logger) {
        _httpService = httpService;
        _logger = logger;
    }
    public async Task<List<Warehouse>> List() {
        _logger.LogInformation("Fetching warehouses from API.");

        var result = await _httpService.HttpGet<ListWarehousesResponse>($"v1/warehouses");
        return result?.Warehouses ?? new();
    }
}
