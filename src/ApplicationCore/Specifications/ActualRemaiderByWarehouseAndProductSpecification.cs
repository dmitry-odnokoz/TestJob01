using Ardalis.Specification;
using TestJob01.ApplicationCore.Entities;

namespace TestJob01.ApplicationCore.Specifications; 
internal class ActualRemaiderByWarehouseAndProductSpecification: Specification<Remainder>, ISingleResultSpecification {
    public ActualRemaiderByWarehouseAndProductSpecification(
        Warehouse wareHouse, Product product) {
        Query
            .Where(x => x.Warehouse.Id == wareHouse.Id && x.Product.Id == product.Id)
            .OrderByDescending(x => x.RemainderDate);
    }
}
