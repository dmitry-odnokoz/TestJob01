using Ardalis.Specification;

using TestJob01.ApplicationCore.Entities;

namespace TestJob01.ApplicationCore.Specifications;
internal class RemaindersByWarehouseAndDateSpecification: Specification<Remainder> {
    public RemaindersByWarehouseAndDateSpecification(Guid warehouseId, DateTimeOffset reportDate) {
        Query.Include(x => x.Product).Include(x => x.Warehouse)
            .Where(x => x.Warehouse.Id == warehouseId)
            .Where(x => x.RemainderDate < reportDate)
            ;
    }
}
