using Microsoft.Extensions.Logging;

using TestJob01.ApplicationCore.Entities;
using TestJob01.ApplicationCore.Interfaces;

namespace TestJob01.ApplicationCore.Services;
public class ProductService: IProductService {
    private readonly IReadRepository<Product> _productRepository;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IReadRepository<Product> productRepository, ILogger<ProductService> logger) {
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Product>> GetProductListAsync() => await _productRepository.ListAsync();
}
