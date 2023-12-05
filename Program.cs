global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Services;
using Microsoft.AspNetCore.Builder;
using PriceNegotiationApp.Data;
using System.Reflection.Metadata;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.AddScoped< ProductCatalogueService>();
builder.Services.AddScoped<IProductCatalogueService, ProductCatalogueService>();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.UseAuthorization(); 
app.Run();

