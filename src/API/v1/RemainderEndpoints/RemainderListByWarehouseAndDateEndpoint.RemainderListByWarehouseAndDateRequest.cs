namespace TestJob01.API.v1.RemainderEndpoints;
public record struct RemainderListByWarehouseAndDateRequest(Guid WarehouseId, DateTimeOffset ReportDate);
