using TestJob01.UI_BL.Models;

namespace TestJob01.UI_BL.Interfaces;
public interface ITransferService {
    Task<bool> Create(CreateTransferRequest transfer);
    Task<bool> Delete(DeleteTransferRequest request);

    Task<List<TransferHead>> ListHeadsOnly();
}
