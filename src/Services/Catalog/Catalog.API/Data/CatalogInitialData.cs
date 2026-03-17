using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync()) {
                return;
            }

            session.Store<Product>(GetPreconfigureProducts());
            await session.SaveChangesAsync();

        }

        private static IEnumerable<Product> GetPreconfigureProducts() => new List<Product>
        {
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Description = "Description",
                Category = ["Cate1","Cat2"],
                ImageFile= "ImageFile",
                Price = 10000
            }
        };
    }

    
}
