

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Models
{
    public class RestaurantContext : IdentityDbContext<User>
    {
        public RestaurantContext()
        {
            
        }

        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {
            
        }


        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<Resturant> Resturants { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RestaurantCateigory>().HasKey(r => new { r.CategoryId, r.RestaurantId });
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
