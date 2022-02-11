using Ardalis.Specification;

using TestJob01.ApplicationCore.Entities.TransferAggregate;

namespace TestJob01.ApplicationCore.Specifications;
public class TransferStornoByTransferIdSpecification: Specification<TransferStorno>, ISingleResultSpecification {
    public TransferStornoByTransferIdSpecification(Guid transferId) {
        Query.Where(x => x.OriginalTransferId == transferId);
    }
}
