
using TestJob01.ApplicationCore.Entities;
using TestJob01.ApplicationCore.Entities.TransferAggregate;
using TestJob01.ApplicationCore.Exceptions;
using TestJob01.ApplicationCore.Interfaces;
using TestJob01.ApplicationCore.Specifications;

namespace TestJob01.ApplicationCore.Services;
public class TransferService: ITransferService {
    private readonly IRepository<Transfer> _transferRepository;
    private readonly IRepository<TransferStorno> _transferStornoRepository;
    private readonly IRepository<Remainder> _remainderRepository;

    public TransferService(IRepository<Transfer> transferRepository,
        IRepository<TransferStorno> transferStornoRepository,
        IRepository<Remainder> remainderRepository) {
        _transferRepository = transferRepository;
        _transferStornoRepository = transferStornoRepository;
        _remainderRepository = remainderRepository;
    }
    public async Task<bool> CreateTransferAsync(Transfer transfer) {
        foreach (var transferItem in transfer.TransferItems) {
            var shipperRemainderSpec = new ActualRemaiderByWarehouseAndProductSpecification(
                transfer.Shipper, transferItem.Product);
            var oldShipperRemainder = await _remainderRepository.GetByIdAsync(shipperRemainderSpec);
            if (null == oldShipperRemainder)
                throw new RemainderTooSmallException(transfer.Shipper, transferItem.Product);
            var newShipperRemainder = oldShipperRemainder.Take(transferItem.Quantity, transfer.OperationDate);

            var receiverRemainderSpec = new ActualRemaiderByWarehouseAndProductSpecification(
                transfer.Receiver, transferItem.Product);
            var oldReceiverRemainder = await _remainderRepository.GetByIdAsync(receiverRemainderSpec);
            var newReceiverRemainder = oldReceiverRemainder?.Add(transferItem.Quantity, transfer.OperationDate)
                ?? new Remainder(Guid.NewGuid(), transfer.Receiver,
                transferItem.Product, transferItem.Quantity.Value, transfer.OperationDate);

            await _remainderRepository.AddAsync(newShipperRemainder);
            await _remainderRepository.AddAsync(newReceiverRemainder);
        }
        await _transferRepository.AddAsync(transfer);
        return true;
    }

    public async Task<bool> DeleteTransferAsync(Transfer transfer, DateTimeOffset deleteDate) {
        foreach (var transferItem in transfer.TransferItems) {
            var shipperRemainderSpec = new ActualRemaiderByWarehouseAndProductSpecification(
                transfer.Shipper, transferItem.Product);
            var oldShipperRemainder = await _remainderRepository.GetByIdAsync(shipperRemainderSpec);
            var newShipperRemainder = oldShipperRemainder!.Add(transferItem.Quantity, deleteDate);

            var receiverRemainderSpec = new ActualRemaiderByWarehouseAndProductSpecification(
                transfer.Receiver, transferItem.Product);
            var oldReceiverRemainder = await _remainderRepository.GetByIdAsync(receiverRemainderSpec);
            var newReceiverRemainder = oldReceiverRemainder!.Take(transferItem.Quantity, deleteDate);

            await _remainderRepository.AddAsync(newShipperRemainder);
            await _remainderRepository.AddAsync(newReceiverRemainder);
        }
        await _transferStornoRepository.AddAsync(new TransferStorno(Guid.NewGuid(), transfer, deleteDate));
        return true;
    }

    public async Task<IEnumerable<Transfer>> GetTransferListAsync() {
        var spec = new TransferIncludeSpecification();
        return await _transferRepository.ListAsync(spec);
    }
}
