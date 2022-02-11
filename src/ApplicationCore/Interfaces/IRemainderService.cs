using TestJob01.ApplicationCore.Entities;

namespace TestJob01.ApplicationCore.Interfaces;
public interface IRemainderService {
    Task<IEnumerable<Remainder>> GetRemainderByWarehouseWithNotZeroQuantityListAsync(Guid warehouseId);
    Task<IEnumerable<Remainder>> GetRemaindersByWarehouseAndDateListAsync(Guid warehouseId,
        DateTimeOffset reportDate);
}
