using Ardalis.Specification;

using TestJob01.ApplicationCore.Entities;

namespace TestJob01.ApplicationCore.Specifications;
public class ProductByIdSpecification: Specification<Product>, ISingleResultSpecification {
    public ProductByIdSpecification(Guid productId) {
        Query.Where(x => x.Id == productId);
    }
}
