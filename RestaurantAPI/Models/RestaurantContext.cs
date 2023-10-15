

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Models
{
    public class RestaurantContext : IdentityDbContext<ApplicationIdentityUser>
    {
        public RestaurantContext()
        {
            
        }

        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {
            
        }


        public virtual DbSet<Recipe> Prodcuts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
