
using TestJob01.ApplicationCore.Constants;

namespace TestJob01.ApplicationCore.ValueObjects;
public record TransferQuantity: Quantity {
    public TransferQuantity(int quantity) : base(quantity, 1, QuantityConstants.MAX_TRANSFER_QUANTITY) { }
    private TransferQuantity() { } // For EF
}
