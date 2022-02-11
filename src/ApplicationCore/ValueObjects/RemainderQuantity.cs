
using TestJob01.ApplicationCore.Constants;

namespace TestJob01.ApplicationCore.ValueObjects;
public record RemainderQuantity: Quantity {
    public RemainderQuantity(int quantity) : base(quantity, 1, QuantityConstants.MAX_REMAINDER_QUANTITY) { }
    private RemainderQuantity() { } // For EF

    public RemainderQuantity Add(TransferQuantity quantity) => new(Value + quantity.Value);

    public RemainderQuantity Take(TransferQuantity quantity) => new(Value - quantity.Value);
}
