using TestJob01.ApplicationCore.Entities;

namespace TestJob01.ApplicationCore.Interfaces;
public interface IProductService {
    Task<IEnumerable<Product>> GetProductListAsync();
}
