using Ardalis.Specification;

using TestJob01.ApplicationCore.Entities;

namespace TestJob01.ApplicationCore.Specifications;
internal class RemaindersByWarehouseWithNotZeroSpecification: Specification<Remainder> {
    public RemaindersByWarehouseWithNotZeroSpecification(Guid warehouseId) {
        Query.Include(x => x.Product).Include(x => x.Warehouse)
            .Where(x => x.Warehouse.Id == warehouseId)
            .Where(x => x.Quantity.Value > 0)
            ;
    }
}
