

namespace Catalog.API.Products.GetProductByCategory
{
    //public record GetProductByCategoryRequest(string category);
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);

    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var products = await sender.Send(new GetProductByCategoryIQuery(category));
                var results = products.Adapt<GetProductByCategoryResponse>();

                return Results.Ok(results);
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("Get Product By Category")
            .WithSummary("Get Product By Category");
        }
    }
}
