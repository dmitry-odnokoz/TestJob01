using Ardalis.Specification;

using TestJob01.ApplicationCore.Entities.TransferAggregate;

namespace TestJob01.ApplicationCore.Specifications; 
public class TransferByIdSpecification: Specification<Transfer>, ISingleResultSpecification {
    public TransferByIdSpecification(Guid transferId) {
        Query.Where(x => x.Id == transferId);
    }
}
