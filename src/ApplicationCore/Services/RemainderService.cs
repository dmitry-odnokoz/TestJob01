using Ardalis.Specification;

using TestJob01.ApplicationCore.Entities;
using TestJob01.ApplicationCore.Interfaces;
using TestJob01.ApplicationCore.Specifications;

namespace TestJob01.ApplicationCore.Services;
public class RemainderService: IRemainderService {
    private readonly IReadRepository<Remainder> _remainderRepository;

    public RemainderService(IReadRepository<Remainder> remainderRepository) {
        _remainderRepository = remainderRepository;
    }

    public async Task<IEnumerable<Remainder>> GetRemaindersByWarehouseAndDateListAsync(Guid warehouseId,
        DateTimeOffset reportDate) {
        var spec = new RemaindersByWarehouseAndDateSpecification(warehouseId, reportDate);
        return await GetLastDateOnlyListAsync(spec);
    }

    public async Task<IEnumerable<Remainder>> GetRemainderByWarehouseWithNotZeroQuantityListAsync(Guid warehouseId) {
        var spec = new RemaindersByWarehouseWithNotZeroSpecification(warehouseId);
        return await GetLastDateOnlyListAsync(spec);
    }

    // TODO Нужно перенести на SQL Server
    private async Task<List<Remainder>> GetLastDateOnlyListAsync(Specification<Remainder> spec) {
        var itemsWithDublicates = await _remainderRepository.ListAsync(spec);
        var resultItems = new List<Remainder>();
        foreach (var groupItem in itemsWithDublicates.GroupBy(x => x.Product.Id)) {
            var item = groupItem.OrderByDescending(x => x.RemainderDate).First();
            resultItems.Add(item);
        }
        return resultItems;
    }
}
