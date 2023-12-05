﻿using Microsoft.AspNetCore.Mvc;
using PriceNegotiationApp.Data;
using PriceNegotiationApp.Mappers;
using PriceNegotiationApp.Models;

namespace PriceNegotiationApp.Services;

public class ProductCatalogueService : IProductCatalogueService
{
    private readonly PriceNegotiationDbContext _dbContext;
    private readonly ServiceResponse<AddProductDto> _serviceResponse = new ServiceResponse<AddProductDto>();

    public ProductCatalogueService(PriceNegotiationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
   
    public async Task<List<GetProductDto>> GetAllProducts()
    {
        
        var products = await _dbContext.Products.ToListAsync();
        return products.Select(ProductMapper.MapProductToGetProductDto).ToList();

    }

    public async Task<GetProductDto> GetProductById(int id)
    {
        var product = await _dbContext.Products.FindAsync(id);
        return ProductMapper.MapProductToGetProductDto(product);
    }

    public async Task<ServiceResponse<AddProductDto>> AddProduct(AddProductDto newProduct)
{
    var serviceResponse = new ServiceResponse<AddProductDto>();
    
        if (newProduct.ProductPrice <= 0)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Cena produktu musi być większa od zera.";
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
    
        return serviceResponse;
}
}