using TestJob01.ApplicationCore.Interfaces;

namespace TestJob01.ApplicationCore.Entities.TransferAggregate {
    public class TransferStorno: TransferBase, IAggregateRoot {
        public Guid OriginalTransferId { get; init; }
        public TransferStorno(Guid id, Transfer originalTransfer, DateTimeOffset operationDate)
            : base(
                 id,
                 TransferKid.Storno,
                 originalTransfer.Receiver,
                 originalTransfer.Shipper,
                 operationDate,
                 originalTransfer.TransferItems) {
            OriginalTransferId = originalTransfer.Id;
        }
        private TransferStorno() { } // For EF
    }
}
