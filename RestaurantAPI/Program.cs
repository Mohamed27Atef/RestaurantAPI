using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;
using RestaurantAPI.Repository.ProductRepository;
using RestaurantAPI.Repository.ResturantRepository;
using RestaurantAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RestaurantContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DB")));

builder.Services.AddScoped<IRecipeRepository, RecipetRepository>();
builder.Services.AddScoped<IResturanrRepo, ResturantRepo>();
builder.Services.AddScoped<ImageService, ImageService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
