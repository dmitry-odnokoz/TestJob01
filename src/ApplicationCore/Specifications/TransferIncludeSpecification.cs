using Ardalis.Specification;

using TestJob01.ApplicationCore.Entities.TransferAggregate;

namespace TestJob01.ApplicationCore.Specifications;
internal class TransferIncludeSpecification: Specification<Transfer> {
    public TransferIncludeSpecification() {
        Query.Include(x => x.Shipper).Include(x => x.Receiver);
    }
}
