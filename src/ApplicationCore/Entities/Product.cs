using TestJob01.ApplicationCore.Interfaces;

namespace TestJob01.ApplicationCore.Entities;
/// <summary>
/// Продукт
/// </summary>
public class Product: BaseEntity, IAggregateRoot {
    /// <summary>
    /// Название продукта
    /// </summary>
    public string Name { get; init; } = default!;
    public Product(Guid id, string name) {
        Id = id;
        Name = name;
    }
    private Product() { }// For EF
}
