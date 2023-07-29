using Microsoft.EntityFrameworkCore;
using MinimalAPICatalogo.Context;
using MinimalAPICatalogo.Domain;

namespace MinimalAPICatalogo.ApiEndpoints
{
    public static class ProductsEndpoints
    {
        public static void MapProductsEndpoints( this WebApplication application)
        {
            application.MapPost("/products", async (Product product, AppDbContext context) =>
            {
                context.Products.Add(product);
                await context.SaveChangesAsync();

                return Results.Created($"/products/{product.ProductId}", product);
            });

            application.MapGet("/products", async (AppDbContext context) => await context.Products.ToListAsync()).WithTags("Products").RequireAuthorization();

            application.MapGet("/products/{id:int}", async (int id, AppDbContext context) => await context.Products.FindAsync(id) is Product product ? Results.Ok(product) : Results.NotFound());

            application.MapPut("/products/{id:int}", async (int id, Product product, AppDbContext conntext) =>
            {
                if (product.ProductId != id) return Results.NotFound();

                var productDb = await conntext.Products.FindAsync(id);

                if (productDb is null) return Results.NotFound();

                productDb.Name = product.Name;
                productDb.Description = product.Description;
                productDb.PurchaseDate = product.PurchaseDate;
                productDb.Price = product.Price;
                productDb.Image = product.Image;
                productDb.PurchaseDate = product.PurchaseDate;
                productDb.Stock = product.Stock;
                productDb.CategoryId = product.CategoryId;

                await conntext.SaveChangesAsync();

                return Results.Ok(product);
            });

            application.MapDelete("/products/{id:int}", async (int id, AppDbContext context) =>
            {
                var product = await context.Products.FindAsync(id);

                if (product is null) return Results.NotFound();

                context.Products.Remove(product);
                await context.SaveChangesAsync();

                return Results.NoContent();
            });

        }
    }
}
