namespace Catalog.API.Products.GetProduct
{
    public record GetProductsRequest(int? PageNumber, int? PageSize);
    public record GetProductsResponse(IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery(request.PageNumber, request.PageSize));
                var response = result.Adapt<GetProductsResponse>();
                return Results.Ok( response);
            })
                .WithName("Products")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Products"); ;
        }
    }
}
