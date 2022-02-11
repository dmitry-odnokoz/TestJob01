using Microsoft.Extensions.Logging;

using TestJob01.ApplicationCore.Entities;
using TestJob01.ApplicationCore.Interfaces;

namespace TestJob01.ApplicationCore.Services;
public class WarehouseService: IWarehouseService {
    private readonly IReadRepository<Warehouse> _warehouseRepository;
    private readonly ILogger<WarehouseService> _logger;

    public WarehouseService(IReadRepository<Warehouse> warehouseRepository, ILogger<WarehouseService> logger) {
        _warehouseRepository = warehouseRepository;
        _logger = logger;
    }
    public async Task<IEnumerable<Warehouse>> GetWarehouseListAsync() => await _warehouseRepository.ListAsync();
}
