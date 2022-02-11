namespace TestJob01.API.v1.WarehouseEndpoints;
public record struct ListWarehousesResponse(IEnumerable<WarehouseDto> Warehouses);
