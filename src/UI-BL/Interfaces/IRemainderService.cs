using TestJob01.UI_BL.Models;

namespace TestJob01.UI_BL.Interfaces;
public interface IRemainderService {
    Task<List<Remainder>> ListByWarehouseAndDate(RemainderListByWarehouseAndDateRequest request);
    Task<List<Remainder>> ListNonZeroByWarehouseResponse();
}
