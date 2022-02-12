namespace TestJob01.UI_BL.Models;
public record struct DeleteTransferRequest(Guid TransferId, DateTimeOffset DeleteDate);
