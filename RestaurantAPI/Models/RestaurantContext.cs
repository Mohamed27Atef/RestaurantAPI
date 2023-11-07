

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

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


        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<Resturant> Resturants { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Cateigory> Cateigorys { get; set; }
        public virtual DbSet<Copon> Copons { get; set; }
        public virtual DbSet<DeliveryMan> DeliveryMen { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<RecipeImage> RecipeImages { get; set; }
        public virtual DbSet<RecipeFeedback> RecipeFeedbacks { get; set; }
        public virtual DbSet<RestaurantCateigory> RestaurantCateigories { get; set; }
        public virtual DbSet<ResturantFeature> ResturantFeatures { get; set; }
        public virtual DbSet<ResturantFeedback> ResturantFeedbacks { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserTable> UserTables { get; set; }
        public virtual DbSet<ClosingDay> ClosingDays { get; set; }
        public virtual DbSet<RestaurantImage> RestaurantImages { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<RestaruantOrdersStatus> RestaruantOrdersStatuses { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RestaurantCateigory>().HasKey(r => new { r.CategoryId, r.RestaurantId });
            modelBuilder.Entity<ResturantFeature>().HasKey(r => new { r.FeatureId, r.ResturantId });
            modelBuilder.Entity<ClosingDay>().HasKey(r => new { r.restaurantId, r.day});
            modelBuilder.Entity<RestaurantImage>().HasKey(r => new { r.restaurantId, r.imageUrl});


            base.OnModelCreating(modelBuilder);
                    modelBuilder.Entity<RecipeImage>()
            .HasKey(ri => new { ri.RecipeId, ri.Image });


            modelBuilder.Entity<Recipe>()
                  .HasMany(t => t.RecipeFeedbacks)
                  .WithOne(t => t.Recipe)
                  .HasForeignKey(t => t.RecipeId)
                  .OnDelete(DeleteBehavior.NoAction);


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
