
using Microsoft.AspNetCore.Mvc;

using MinimalApi.Endpoint;

using TestJob01.ApplicationCore.Entities.TransferAggregate;
using TestJob01.ApplicationCore.Exceptions;
using TestJob01.ApplicationCore.Interfaces;
using TestJob01.ApplicationCore.Specifications;

namespace TestJob01.API.v1.TransferEndpoints;
public class DeleteTransferEndpoint: IEndpoint<IResult, DeleteTransferRequest> {
    private IReadRepository<Transfer> _transferRepository = default!;
    private IReadRepository<TransferStorno> _transferStornoRepository = default!;
    private ITransferService _transferService = default!;
    public DeleteTransferEndpoint() { }

    public void AddRoute(IEndpointRouteBuilder app) {
        app.MapDelete("api/v1/transfers/{transferId:guid}",
            async (Guid transferId, [FromQuery(Name = "Date")] DateTimeOffset deleteDate,
                IReadRepository<Transfer> transferRepository,
                IReadRepository<TransferStorno> transferStornoRepository,
                ITransferService transferService) => {
                    _transferRepository = transferRepository;
                    _transferStornoRepository = transferStornoRepository;
                    _transferService = transferService;
                    return await HandleAsync(new DeleteTransferRequest(transferId, deleteDate));
                })
            .Produces<DeleteTransferResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Transfers");
    }

    public async Task<IResult> HandleAsync(DeleteTransferRequest request) {
        var transferSpec = new TransferByIdWithItemsSpecification(request.TransferId);
        var transfer = await _transferRepository.GetBySpecAsync(transferSpec);
        if (null == transfer)
            throw new EntityNotFoundException(request.TransferId, nameof(Transfer));

        var transferStornoSpec = new TransferStornoByTransferIdSpecification(request.TransferId);
        var transferStorno = await _transferStornoRepository.GetBySpecAsync(transferStornoSpec);
        if (null != transferStorno)
            throw new EntityDuplicatedException(request.TransferId, "Transfer storno");

        var response = new DeleteTransferResponse();
        var result = await _transferService.DeleteTransferAsync(transfer, request.DeleteDate);
        response.Sussecced = result;
        return Results.Ok(response);
    }
}
