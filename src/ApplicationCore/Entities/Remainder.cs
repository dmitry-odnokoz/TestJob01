using TestJob01.ApplicationCore.Exceptions;
using TestJob01.ApplicationCore.Interfaces;
using TestJob01.ApplicationCore.ValueObjects;

namespace TestJob01.ApplicationCore.Entities;
/// <summary>
/// Остаток на складе
/// </summary>
public class Remainder: BaseEntity, IAggregateRoot {
    /// <summary>
    /// Склад
    /// </summary>
    public Warehouse Warehouse { get; init; } = default!;
    /// <summary>
    /// Номенклатура
    /// </summary>
    public Product Product { get; init; } = default!;
    /// <summary>
    /// Кол-во
    /// </summary>
    public RemainderQuantity Quantity { get; init; } = default!;
    /// <summary>
    /// Дата остатка
    /// </summary>
    public DateTimeOffset RemainderDate { get; init; } = default!;
    public Remainder(Guid id, Warehouse warehouse, Product product, int quantity, DateTimeOffset remainderDate) :
        this(id, warehouse, product, new RemainderQuantity(quantity), remainderDate) { }
    public Remainder(Guid id, Warehouse warehouse, Product product, RemainderQuantity quantity,
        DateTimeOffset remainderDate) {
        Id = id;
        Warehouse = warehouse;
        Product = product;
        Quantity = quantity;
        RemainderDate = remainderDate;
    }
    private Remainder() { } // For EF

    public Remainder Add(TransferQuantity quantity, DateTimeOffset remainderDate) =>
        new(Guid.NewGuid(), Warehouse, Product, Quantity.Add(quantity), remainderDate);
    public Remainder Take(TransferQuantity quantity, DateTimeOffset remainderDate) {
        if (quantity.Value > Quantity.Value)
            throw new RemainderTooSmallException(Warehouse, Product, quantity.Value, Quantity.Value);
        return new Remainder(Guid.NewGuid(), Warehouse, Product, Quantity.Take(quantity), remainderDate);
    }
}
