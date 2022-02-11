using AutoMapper;

using MinimalApi.Endpoint;

using TestJob01.ApplicationCore.Interfaces;

namespace TestJob01.API.v1.ProductEndpoints;
public class ProductListEndpoint: IEndpoint<IResult> {
    private IProductService _productService = default!;
    private readonly IMapper _mapper;
    public ProductListEndpoint(IMapper mapper) {
        _mapper = mapper;
    }
    public void AddRoute(IEndpointRouteBuilder app) {
        app.MapGet("api/v1/products",
        async (IProductService productService) => {
            _productService = productService;
            return await HandleAsync();
        })
        .Produces<ListProductsResponse>()
        .WithTags("Products");
    }
    public async Task<IResult> HandleAsync() {
        var items = await _productService.GetProductListAsync();
        var response = new ListProductsResponse(_mapper.Map<IEnumerable<ProductDto>>(items)
            .OrderBy(x => x.Name));
        return Results.Ok(response);
    }
}
