namespace TestJob01.API.v1.RemainderEndpoints;
public record struct RemainderListNonZeroByWarehouseResponse(IEnumerable<RemainderDto> Remainders);
