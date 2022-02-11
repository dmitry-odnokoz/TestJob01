using AutoMapper;

using MinimalApi.Endpoint;

using TestJob01.ApplicationCore.Interfaces;

namespace TestJob01.API.v1.WarehouseEndpoints;
public class WarehouseListEndpoint: IEndpoint<IResult> {
    private IWarehouseService _warehouseService = default!;
    private readonly IMapper _mapper;
    public WarehouseListEndpoint(IMapper mapper) {
        _mapper = mapper;
    }
    public void AddRoute(IEndpointRouteBuilder app) {
        app.MapGet("api/v1/warehouses",
        async (IWarehouseService warehouseService) => {
            _warehouseService = warehouseService;
            return await HandleAsync();
        })
        .Produces<ListWarehousesResponse>()
        .WithTags("Warehouses");
    }
    public async Task<IResult> HandleAsync() {
        var items = await _warehouseService.GetWarehouseListAsync();
        var response = new ListWarehousesResponse(_mapper.Map<IEnumerable<WarehouseDto>>(items)
            .OrderBy(x => x.Name));
        return Results.Ok(response);
    }
}
