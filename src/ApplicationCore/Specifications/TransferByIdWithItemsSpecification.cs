using Ardalis.Specification;

using TestJob01.ApplicationCore.Entities.TransferAggregate;

namespace TestJob01.ApplicationCore.Specifications;
public class TransferByIdWithItemsSpecification: Specification<Transfer>, ISingleResultSpecification {
    public TransferByIdWithItemsSpecification(Guid transferId) {
        Query
            .Include(x => x.TransferItems)
            .Where(x => x.Id == transferId);
    }
}
