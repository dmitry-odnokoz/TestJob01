using TestJob01.UI_BL.Models;

namespace TestJob01.UI_BL.Interfaces;
public interface IWarehouseService {
    Task<List<Warehouse>> List();
}
