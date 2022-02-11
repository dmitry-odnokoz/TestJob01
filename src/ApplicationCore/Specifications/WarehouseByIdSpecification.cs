using Ardalis.Specification;

using TestJob01.ApplicationCore.Entities;

namespace TestJob01.ApplicationCore.Specifications;
public class WarehouseByIdSpecification: Specification<Warehouse>, ISingleResultSpecification {
    public WarehouseByIdSpecification(Guid warehouseId) {
        Query.Where(x => x.Id == warehouseId);
    }
}
