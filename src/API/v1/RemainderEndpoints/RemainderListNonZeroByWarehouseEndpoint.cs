using AutoMapper;

using MinimalApi.Endpoint;

using TestJob01.ApplicationCore.Interfaces;

namespace TestJob01.API.v1.RemainderEndpoints;
public class RemainderListNonZeroByWarehouseEndpoint: IEndpoint<IResult, Guid> {
    private IRemainderService _remainderService = default!;
    private readonly IMapper _mapper;
    public RemainderListNonZeroByWarehouseEndpoint(IMapper mapper) {
        _mapper = mapper;
    }
    public void AddRoute(IEndpointRouteBuilder app) {
        app.MapGet("api/v1/remainders/by-warehouse/notzero/{warehouseid:guid}",
        async (Guid warehouseid, IRemainderService remainderService) => {
            _remainderService = remainderService;
            return await HandleAsync(warehouseid);
        })
        .Produces<RemainderListNonZeroByWarehouseResponse>()
        .WithTags("Remainders");
    }
    public async Task<IResult> HandleAsync(Guid warehouseid) {
        var items = await _remainderService
            .GetRemainderByWarehouseWithNotZeroQuantityListAsync(warehouseid);
        var response = new RemainderListNonZeroByWarehouseResponse(
            _mapper.Map<IEnumerable<RemainderDto>>(items)
            .OrderBy(x => x.ProductName)
            .ThenByDescending(x => x.RemainderDate)
        );
        return Results.Ok(response);
    }
}
