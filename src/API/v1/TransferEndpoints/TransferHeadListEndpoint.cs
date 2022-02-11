using AutoMapper;

using MinimalApi.Endpoint;

using TestJob01.ApplicationCore.Interfaces;

namespace TestJob01.API.v1.TransferEndpoints;
public class TransferHeadListEndpoint: IEndpoint<IResult> {
    private ITransferService _transferService = default!;
    private readonly IMapper _mapper;
    public TransferHeadListEndpoint(IMapper mapper) {
        _mapper = mapper;
    }
    public void AddRoute(IEndpointRouteBuilder app) {
        app.MapGet("api/v1/transfers/head-only",
        async (ITransferService transferService) => {
            _transferService = transferService;
            return await HandleAsync();
        })
        .Produces<ListTransferHeadssResponse>()
        .WithTags("Transfers");
    }

    public async Task<IResult> HandleAsync() {
        var items = await _transferService.GetTransferListAsync();
        var response = new ListTransferHeadssResponse(
            _mapper.Map<List<TransferHeadDto>>(items)
            //.OrderBy(x => x.OperationDate)
            //.ThenBy(x => x.ShipperName)
            //.ThenBy(x => x.ReceiverName)
            );
        return Results.Ok(response);
    }
}