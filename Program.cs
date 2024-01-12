global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Http;
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
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseNLog();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {

    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme

    {
        Description = """Standard Authorization header using the Bearer scheme. Example: "bearer {token}" """,
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF32.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateTokenReplay = true

    };
    options.IncludeErrorDetails = true;
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

