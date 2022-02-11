using TestJob01.ApplicationCore.Entities;

namespace TestJob01.ApplicationCore.Interfaces;
public interface IWarehouseService {
    Task<IEnumerable<Warehouse>> GetWarehouseListAsync();
}
