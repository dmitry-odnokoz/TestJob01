using TestJob01.UI_BL.Interfaces;
using TestJob01.UI_BL.Models;

namespace TestJob01.UI_BL.Services;
public class ProductService: IProductService {
    private readonly HttpService _httpService;
    private readonly ILogger<ProductService> _logger;

    public ProductService(HttpService httpService,
        ILogger<ProductService> logger) {
        _httpService = httpService;
        _logger = logger;
    }
    public async Task<List<Product>> List() {
        _logger.LogInformation("Fetching products from API.");

        var result = await _httpService.HttpGet<ListProductsResponse>($"v1/products");
        return result?.Products ?? new();
    }
}
