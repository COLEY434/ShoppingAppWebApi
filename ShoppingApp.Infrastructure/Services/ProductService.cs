using AutoMapper;
using ShoppingApp.Core.DTOs.Request;
using ShoppingApp.Core.DTOs.Response;
using ShoppingApp.Core.Entities;
using ShoppingApp.Core.Interfaces;
using ShoppingApp.Core.Interfaces.Repositries;
using ShoppingApp.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mappper;
        private readonly IProductRepository _productRepo;

        public ProductService(IMapper mapper, IProductRepository productRepo)
        {
            _mappper = mapper;
            _productRepo = productRepo;
        }
        public async Task<bool> AddProductAsync(ProductRequestDto req)
        {
            var product = _mappper.Map<Product>(req);

            var result = await _productRepo.AddProduct(product);
           
            return result;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _productRepo.GetAllProducts();
            var result =  _mappper.Map<IEnumerable<ProductResponseDto>>(products);

            return result;
        }
    }
}
