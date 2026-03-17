using Catalog.API.Products.GetProduct;
using Catalog.API.Products.GetProductById;
using Marten.Linq.QueryHandlers;
using System.Linq;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryIQuery(string category):IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    public class GetProductByCategoryHandler(IDocumentSession session, ILogger<GetProductByCategoryHandler> logger) 
        : IQueryHandler<GetProductByCategoryIQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryIQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryHandler.Handle called with {@Query}", query);

            var products = await session.Query<Product>()
                            .Where(p => p.Category.Contains(query.category))
                            .ToListAsync(cancellationToken);
            
            if (products is null) {
                //throw new ProductNotFoundException(query.category);
            }

            return new GetProductByCategoryResult(products);
        }
    }
}
