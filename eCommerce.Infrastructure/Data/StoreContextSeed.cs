using eCommerce.Core.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace eCommerce.Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrand.Any())
                {
                    var brandsData = 
                        File.ReadAllText("../eCommerce.Infrastructure/Data/SeedData/brands.json"); //because we're running the method from Program.cs

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    await context.ProductBrand.AddRangeAsync(brands);

                    await context.SaveChangesAsync();
                }

                if (!context.ProductType.Any())
                {
                    var typesData =
                        File.ReadAllText("../eCommerce.Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    await context.ProductType.AddRangeAsync(types);

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData =
                        File.ReadAllText("../eCommerce.Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    await context.Products.AddRangeAsync(products);

                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
