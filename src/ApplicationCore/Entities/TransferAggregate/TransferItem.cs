using TestJob01.ApplicationCore.ValueObjects;

namespace TestJob01.ApplicationCore.Entities.TransferAggregate;
public class TransferItem: BaseEntity {
    public Product Product { get; init; } = default!;
    public TransferQuantity Quantity { get; init; } = default!;
    private TransferItem() { } // For ET
    public TransferItem(Guid id, Product product, int quantity) {
        Id = id;
        Product = product;
        Quantity = new TransferQuantity(quantity);
    }
}