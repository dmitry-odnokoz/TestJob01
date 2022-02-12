namespace TestJob01.UI_BL.Models;
public record struct RemainderListByWarehouseAndDateRequest(Guid WarehouseId, DateTimeOffset ReportDate);
