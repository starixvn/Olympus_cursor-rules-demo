using CafeShopV2.Core.Interfaces;
using CafeShopV2.Infrastructure.Data;
using CafeShopV2.Infrastructure.Repositories;
using CafeShopV2.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
var connectionString = "Server=127.0.0.1;Port=3306;Database=cafeshopv2;User=root;Password=1234aa;";
builder.Services.AddDbContext<CafeShopDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add Repositories
builder.Services.AddScoped<IMenuRepository, MenuRepository>();

// Add Services
builder.Services.AddScoped<IMenuService, MenuService>();

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

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CafeShopDbContext>();
    context.Database.EnsureCreated();
}

app.Run(); 