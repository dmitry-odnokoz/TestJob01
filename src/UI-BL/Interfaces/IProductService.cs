using TestJob01.UI_BL.Models;

namespace TestJob01.UI_BL.Interfaces;
public interface IProductService {
    Task<List<Product>> List();
}
