using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Data;
using PriceNegotiationApp.Exceptions;
using PriceNegotiationApp.Mappers;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public class ProductCatalogueService : IProductCatalogueService
{
    private readonly IMapper _mapper;
    private readonly PriceNegotiationDbContext _dbContext;
    private readonly ILogger<ProductCatalogueService> _logger;

    public ProductCatalogueService(IMapper mapper,PriceNegotiationDbContext dbContext, ILogger<ProductCatalogueService> logger)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<List<GetProductDto>> GetAllProducts()
    {
        
        var products = await _dbContext.Products.ToListAsync();
        return products.Select(ProductMapper.MapProductToGetProductDto).ToList();

    }

    public async Task<GetProductDto> GetProductById(int id)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if (product is null)
        {
            throw new NotFoundException("Product id not found.");
        }
        return ProductMapper.MapProductToGetProductDto(product);
        
    }

    public async Task<ServiceResponse<AddProductDto>> AddProduct(AddProductDto newProduct)
{
    var serviceResponse = new ServiceResponse<AddProductDto>();
    _logger.LogInformation($"Added product {newProduct}.");

        if (newProduct.ProductPrice <= 0)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "The price cannot be 0.";
            return serviceResponse;
        }

        var product = new Product
        {
            ProductName = newProduct.ProductName,
            ProductCategory = newProduct.ProductCategory,
            ProductDescription = newProduct.ProductDescription,
            ProductPrice = newProduct.ProductPrice,
            IsAvailable = newProduct.IsAvailable
        };

        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        serviceResponse.Data = newProduct;
        serviceResponse.Message = "Product added successfully.";
        return serviceResponse;
}

    public async Task DeleteProduct(int id)
    {
        _logger.LogInformation($"Deleted product with id: {id}.");
       
            var product = await _dbContext.Products.FirstAsync(p => p.Id == id);
            if (product is null)
            {
                throw new NotFoundException("Product id not found.");
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
    }
}