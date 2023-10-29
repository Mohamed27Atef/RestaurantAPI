using RestaurantAPI.Interfaces;
using RestaurantAPI.Repository.ProductRepository;
using RestaurantAPI.Repository.ResturantRepository;
using RestaurantAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;
using RestaurantAPI.Repository.OrderRepository;
using RestaurantAPI.Repository.CartRepository;

namespace RestaurantAPI.Services
{
    public static class RegisterServices
    {
        public static WebApplicationBuilder registerAllService(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRecipeRepository, RecipetRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IResturanrRepo, ResturantRepo>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ImageService, ImageService>();
            builder.Services.AddScoped<IToken, TokenService>();
            builder.Services.AddScoped<ITableRepository, TableRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITableUserRepository, TableUserRepository>();
            builder.Services.AddScoped<ICartUserRrepository, CartUserRrepository>();
            builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
            builder.Services.AddScoped<IMenuRepository, MenuRepository>();

            return builder;
        }

        public static WebApplicationBuilder registerTokenService(this WebApplicationBuilder builder) {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudiance"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:srcret"]))
                };

            });

            return builder;
        }

        public static WebApplicationBuilder registerDBAndIdentityService(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<RestaurantContext>(options =>
            options
            //.UseLazyLoadingProxies()
            .UseSqlServer(builder.Configuration.GetConnectionString("DB")));
            builder.Services.AddIdentity<ApplicationIdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<RestaurantContext>();

            return builder;
        }   
        
        public static WebApplicationBuilder addCorseConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("myCorse", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            return builder;
        }



       
    }
}
