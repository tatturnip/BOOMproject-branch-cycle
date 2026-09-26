using Microsoft.EntityFrameworkCore;

namespace ProductAPI;

public class ProductDb : DbContext
{
    public ProductDb(DbContextOptions<ProductDb> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}