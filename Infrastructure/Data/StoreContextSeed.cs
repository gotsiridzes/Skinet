using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(
            StoreContext context,
            ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            try
            {
                if (!context.ProductBrands.Any())
                {
                    logger.LogInformation("Seeding brands data.");
                    var brandsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                        await context.ProductBrands.AddAsync(item);

                    await context.SaveChangesAsync();
                    logger.LogInformation("Brands data seeded.");
                }

                if (!context.ProductTypes.Any())
                {
                    logger.LogInformation("Seeding types data.");
                    var typesData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                        await context.ProductTypes.AddAsync(item);

                    await context.SaveChangesAsync();
                    logger.LogInformation("Types data seeded.");
                }

                if (!context.Products.Any())
                {
                    logger.LogInformation("Seeding products data.");
                    var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                        await context.Products.AddAsync(item);

                    await context.SaveChangesAsync();
                 
                    logger.LogInformation("Products data seeded.");
                }
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, "Error occured while seeding data.");
            }
        }
    }
}