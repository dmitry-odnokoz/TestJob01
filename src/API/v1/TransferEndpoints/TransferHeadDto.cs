namespace TestJob01.API.v1.TransferEndpoints;
public record TransferHeadDto {
    public Guid Id { get; init; }
    public Guid ShipperId { get; init; }
    public string ShipperName { get; init; } = default!;
    public Guid ReceiverId { get; init; }
    public string ReceiverName { get; init; } = default!;
    public DateTimeOffset OperationDate { get; init; }
}