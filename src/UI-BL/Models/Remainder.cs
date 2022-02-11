namespace TestJob01.UI_BL.Models;
public record struct Remainder {
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }

    public Guid WarehouseId { get; init; }
    public string WarehouseName { get; init; }

    public int Quantity { get; init; }
    public DateTimeOffset RemainderDate { get; init; }
}
