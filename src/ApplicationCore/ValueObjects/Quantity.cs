
using TestJob01.ApplicationCore.Exceptions;

namespace TestJob01.ApplicationCore.ValueObjects;
public abstract record Quantity {
    protected Quantity(int quantity, int min, int max) {
        if ((quantity < min) || (quantity > max))
            throw new QuantityOutOfRangeException(quantity, min, max);
        Value = quantity;
    }

    public int Value { get; init; }
    protected Quantity() { } // For EF
}
