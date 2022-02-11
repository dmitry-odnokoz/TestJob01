using TestJob01.ApplicationCore.Interfaces;

namespace TestJob01.ApplicationCore.Entities;
/// <summary>
/// Склад
/// </summary>
public class Warehouse: BaseEntity, IAggregateRoot {
    public string Name { get; init; } = default!;
    public Warehouse(Guid id, string name) {
        Id = id;
        Name = name;
    }
    private Warehouse() { } // For EF
}
