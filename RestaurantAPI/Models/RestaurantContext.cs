

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Models
{
    public class RestaurantContext : IdentityDbContext<User>
    {
            public DbSet<RecipeImage> RecipeImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipeImage>()
            .HasKey(ri => new { ri.RecipeId, ri.Image });

        modelBuilder.Entity<RecipeImage>()
            .HasOne(ri => ri.Recipe)
            .WithMany()
            .HasForeignKey(ri => ri.RecipeId);
    }
        public RestaurantContext()
        {
            
        }

        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {
            
        }


        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<Resturant> Resturants { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
