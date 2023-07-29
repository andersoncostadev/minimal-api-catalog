using Microsoft.EntityFrameworkCore;
using MinimalAPICatalogo.Domain;

namespace MinimalAPICatalogo.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);
            modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Category>().Property(c => c.Description).HasMaxLength(150).IsRequired();

            modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Description).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Image).HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(14, 2);

            //relationship
            modelBuilder.Entity<Product>().HasOne<Category>(c => c.Category).WithMany(p => p.Products).HasForeignKey(c => c.CategoryId);
        }
    }
}
