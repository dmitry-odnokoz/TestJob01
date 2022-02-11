namespace TestJob01.API.v1.TransferEndpoints;
public record CreateTransferRequest {
    public Guid Id { get; set; }
    public Guid ShipperId { get; set; }
    public Guid ReceiverId { get; set; }
    public DateTimeOffset OperationDate { get; set; }
    public List<CreateTransferItem> Items { get; set; } = new();
}

public record struct CreateTransferItem(Guid Id, Guid ProductId, int Quantity);