namespace TestJob01.API.v1.TransferEndpoints;
public record struct DeleteTransferRequest(Guid TransferId, DateTimeOffset DeleteDate);