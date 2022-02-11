using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using MinimalApi.Endpoint;

using TestJob01.ApplicationCore.Interfaces;

namespace TestJob01.API.v1.RemainderEndpoints;
/// <summary>
/// Получить список продуктов на складе с ненулевым остатком наскладе
/// </summary>
public class RemainderListByWarehouseAndDateEndpoint: IEndpoint<IResult, RemainderListByWarehouseAndDateRequest> {
    private IRemainderService _remainderService = default!;
    private readonly IMapper _mapper;
    public RemainderListByWarehouseAndDateEndpoint(IMapper mapper) {
        _mapper = mapper;
    }
    public void AddRoute(IEndpointRouteBuilder app) {
        app.MapGet("api/v1/remainders/by-warehouse/{warehouseid:guid}",
        async (Guid warehouseid, [FromQuery(Name = "date")] DateTimeOffset reportDate,
        IRemainderService remainderService) => {
            _remainderService = remainderService;
            return await HandleAsync(new RemainderListByWarehouseAndDateRequest(warehouseid, reportDate));
        })
        .Produces<RemainderListByWarehouseAndDateResponse>()
        .WithTags("Remainders");
    }
    public async Task<IResult> HandleAsync(RemainderListByWarehouseAndDateRequest request) {
        var items = await _remainderService.GetRemaindersByWarehouseAndDateListAsync(
            request.WarehouseId,
            request.ReportDate);
        var response = new RemainderListByWarehouseAndDateResponse(
            _mapper.Map<IEnumerable<RemainderDto>>(items)
            .OrderBy(x => x.ProductName)
        );
        return Results.Ok(response);
    }
}
