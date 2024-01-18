using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Data;
using PriceNegotiationApp.Exceptions;
using PriceNegotiationApp.Mappers;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services
{
    public interface IProductCatalogueService
    {
        Task<List<GetProductDto>> GetAllProducts();
        Task<GetProductDto> GetProductById(int id);
        Task<AddProductDto> AddProduct(AddProductDto newProduct);
        Task UpdateProduct(int id, UpdateProductDto updatedProduct);
        Task DeleteProduct(int id);

    }

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
        return _mapper.Map<List<GetProductDto>>(products);
    }

    public async Task<GetProductDto> GetProductById(int id)
    {
        var product = await _dbContext.Products.FindAsync(id);
        if (product is null)
        {
            throw new NotFoundException("Product id not found.");
        }
        return _mapper.Map<GetProductDto>(product);
    }

    public async Task<AddProductDto> AddProduct(AddProductDto dto)
    {
        _logger.LogInformation($"Added product {dto}.");

        var product = _mapper.Map<Product>(dto);
        if (product.ProductPrice <= 0)
        {
            throw new BadRequestException("Price cannot be 0 or negative.");
        }
        if (product.ProductName == null)
        {
            throw new BadRequestException("Product name cannot be null.");
        }
        if (product.ProductDescription == null)
        {
            throw new BadRequestException("Product description cannot be null.");

        }
        if (product.ProductCategory == null)
        {
            throw new BadRequestException("Product category cannot be null.");
        }
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<AddProductDto>(product);
    }

    public async Task UpdateProduct(int id, UpdateProductDto dto)
    {
        _logger.LogInformation($"Updated product with id: {id}.");

        var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        if (product is null)
        {
            throw new NotFoundException("Product id not found.");
        }
        product.ProductName = dto.ProductName;
        product.ProductDescription = dto.ProductDescription;
        product.ProductPrice = dto.ProductPrice;
        product.ProductCategory = dto.ProductCategory;
        product.IsAvailable = dto.IsAvailable;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteProduct(int id)
    {
        _logger.LogInformation($"Deleted product with id: {id}.");
       
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                throw new NotFoundException("Product id not found.");
            }
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
    }
    }
}