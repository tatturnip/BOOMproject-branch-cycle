using Microsoft.EntityFrameworkCore;
using ProductAPI;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5269");

builder.Services.AddDbContext<ProductDb>(options =>
    options.UseSqlite("Data Source=products.db"));

// Allow the Vite frontend to call the API
builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("frontend");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProductDb>();

    db.Database.EnsureCreated();

    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new Product
            {
                Name = "Blue Backpack",
                Price = 29.99m,
                Inventory = 12
            },
            new Product
            {
                Name = "Wireless Mouse",
                Price = 19.99m,
                Inventory = 8
            }
        );

        db.SaveChanges();
    }
}

app.MapGet("/productAPI", async (ProductDb db) =>
    await db.Products.ToListAsync());

app.Run();