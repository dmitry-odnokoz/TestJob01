namespace TestJob01.API.v1.ProductEndpoints;

public record struct ListProductsResponse(IEnumerable<ProductDto> Products);