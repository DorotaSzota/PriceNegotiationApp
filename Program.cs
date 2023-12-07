global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Http;
using MediatR;
using System.Reflection;
using PriceNegotiationApp.Services;
using PriceNegotiationApp.Data;
using Microsoft.AspNetCore.Builder;
using System.Reflection.Metadata;
using FluentValidation;
using NLog.Web;
using PriceNegotiationApp.Middleware;
using PriceNegotiationApp.Models;
using PriceNegotiationApp.Models.Validators;
using Microsoft.IdentityModel.Tokens;
using PriceNegotiationApp;
using System.Text;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseNLog();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

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
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCors(option =>
{
    option.AddPolicy("FrontEndClient", cfg =>
    {
        cfg.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin(); //TODO: change to specific origin (WithOrigins()) when deploying and add the origin to the appsettings.json
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("FrontEndClient");
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.UseAuthorization();
app.Run();

