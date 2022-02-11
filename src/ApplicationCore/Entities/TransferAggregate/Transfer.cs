
using TestJob01.ApplicationCore.Interfaces;

namespace TestJob01.ApplicationCore.Entities.TransferAggregate;
/// <summary>
/// Операция перемещения
/// </summary>
public class Transfer: TransferBase, IAggregateRoot {
    public Transfer(
        Guid id,
        Warehouse shipper,
        Warehouse receiver,
        DateTimeOffset operationDate,
        IEnumerable<TransferItem> items)
        : base(id, TransferKid.Transfer, shipper, receiver, operationDate, items) { }
    private Transfer() : base() { } // required by EF
}
