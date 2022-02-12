using TestJob01.UI_BL.Interfaces;
using TestJob01.UI_BL.Models;

namespace TestJob01.UI_BL.Services;
public class RemainderService: IRemainderService {
    public Task<List<Remainder>> ListByWarehouseAndDate(RemainderListByWarehouseAndDateRequest request) => throw new NotImplementedException();
    public Task<List<Remainder>> ListNonZeroByWarehouseResponse() => throw new NotImplementedException();
}
