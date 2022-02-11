
using MinimalApi.Endpoint;

using TestJob01.ApplicationCore.Entities;
using TestJob01.ApplicationCore.Entities.TransferAggregate;
using TestJob01.ApplicationCore.Exceptions;
using TestJob01.ApplicationCore.Interfaces;
using TestJob01.ApplicationCore.Specifications;

namespace TestJob01.API.v1.TransferEndpoints;
public class CreateTransferEndpoint: IEndpoint<IResult, CreateTransferRequest> {
    private IReadRepository<Transfer> _transferRepository = default!;
    private IReadRepository<Product> _productRepository = default!;
    private IReadRepository<Warehouse> _warehouseRepository = default!;
    private ITransferService _transferService = default!;
    public CreateTransferEndpoint() {
    }

    public void AddRoute(IEndpointRouteBuilder app) {
        app.MapPost("api/v1/transfers/{transferId:guid}",
            async (CreateTransferRequest request,
                IReadRepository<Transfer> transferRepository,
                IReadRepository<Product> productRepository,
                IReadRepository<Warehouse> warehouseRepository,
                ITransferService transferService) => {
                    _transferRepository = transferRepository;
                    _productRepository = productRepository;
                    _warehouseRepository = warehouseRepository;
                    _transferService = transferService;
                    return await HandleAsync(request);
                })
            .Produces<DeleteTransferResponse>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .WithTags("Transfers");
    }

    public async Task<IResult> HandleAsync(CreateTransferRequest request) {
        var transferSpec = new TransferByIdSpecification(request.Id);
        var transfer = await _transferRepository.GetBySpecAsync(transferSpec);
        if (null != transfer)
            throw new EntityDuplicatedException(request.Id, nameof(Transfer));

        var shipperSpec = new WarehouseByIdSpecification(request.ShipperId);
        var shipper = await _warehouseRepository.GetBySpecAsync(shipperSpec);
        if (null == shipper)
            throw new EntityNotFoundException(request.ShipperId, "Shipper warehouse");

        var receiverSpec = new WarehouseByIdSpecification(request.ReceiverId);
        var receiver = await _warehouseRepository.GetBySpecAsync(receiverSpec);
        if (null == receiver)
            throw new EntityNotFoundException(request.ReceiverId, "Shipper warehouse");

        var newTransferItems = new List<TransferItem>();

        foreach (var item in request.Items) {
            var productSpec = new ProductByIdSpecification(item.ProductId);
            var product = await _productRepository.GetBySpecAsync(productSpec);
            if (null == product)
                throw new EntityNotFoundException(item.ProductId, nameof(Product));
            newTransferItems.Add(new TransferItem(item.Id, product, item.Quantity));
        }

        var response = new CreateTransferResponse();
        var result = await _transferService.CreateTransferAsync(
            new Transfer(request.Id, shipper, receiver, request.OperationDate, newTransferItems));
        response.Sussecced = result;
        return Results.Created("", response);
    }
}
