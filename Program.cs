global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Http;
using MediatR;
using System.Reflection;
using PriceNegotiationApp.Services;
using PriceNegotiationApp.Data;
using Microsoft.AspNetCore.Builder;
using System.Reflection.Metadata;
using NLog.Web;
using PriceNegotiationApp.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseNLog();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PriceNegotiationDbContext>(options =>
       options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<PriceNegotiationSeeder>(sp =>
{
    var dbContext = sp.GetRequiredService<PriceNegotiationDbContext>();
    return new PriceNegotiationSeeder(dbContext);
});

builder.Services.AddScoped<IProductCatalogueService, ProductCatalogueService>();
builder.Services.AddScoped<IPriceNegotiationService, PriceNegotiationService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.UseAuthorization();
app.Run();

