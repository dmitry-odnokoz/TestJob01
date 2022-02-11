using TestJob01.ApplicationCore.Entities.TransferAggregate;

namespace TestJob01.ApplicationCore.Interfaces;
public interface ITransferService {
    Task<IEnumerable<Transfer>> GetTransferListAsync();
    Task<bool> CreateTransferAsync(Transfer transfer);
    Task<bool> DeleteTransferAsync(Transfer transfer, DateTimeOffset deleteDate);
}
