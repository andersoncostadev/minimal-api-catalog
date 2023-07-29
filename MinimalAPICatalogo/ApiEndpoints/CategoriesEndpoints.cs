using Microsoft.EntityFrameworkCore;
using MinimalAPICatalogo.Context;
using MinimalAPICatalogo.Domain;

namespace MinimalAPICatalogo.ApiEndpoints
{
    public static class CategoriesEndpoints
    {
        public static void MapCategoriesEndpoints(this WebApplication application)
        {
            application.MapPost("/categories", async (Category category, AppDbContext context) =>
            {
                context.Categories.Add(category);
                await context.SaveChangesAsync();

                return Results.Created($"/categories/{category.CategoryId}", category);
            });

            application.MapGet("/categories", async (AppDbContext context) => await context.Categories.ToListAsync()).WithTags("Categories").RequireAuthorization();

            application.MapGet("/categories/{id:int}", async (int id, AppDbContext context) => await context.Categories.FindAsync(id) is Category category ? Results.Ok(category) : Results.NotFound());

            application.MapPut("/categories/{id:int}", async (int id, Category category, AppDbContext context) =>
            {
                if (category.CategoryId != id) return Results.NotFound();

                var categoryDb = await context.Categories.FindAsync(id);

                if (categoryDb is null) return Results.NotFound();

                categoryDb.Name = category.Name;
                categoryDb.Description = category.Description;

                await context.SaveChangesAsync();

                return Results.Ok(category);
            });

            application.MapDelete("/categories/{id:int}", async (int id, AppDbContext context) =>
            {
                var category = await context.Categories.FindAsync(id);

                if (category is null) return Results.NotFound();

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Results.NoContent();

            });
        }
    }
}
