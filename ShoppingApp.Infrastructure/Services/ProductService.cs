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
        public async Task AddProductAsync(ProductRequestDto req)
        {
            var product = _mappper.Map<Product>(req);
            product.DateAdded = DateTime.Now;
            product.DateModified = DateTime.Now;

            await _productRepo.AddProduct(product);

        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepo.CheckIfProductExist(id);

            if (product == null)
            {
                return false;
            }

            product.Isdeleted = true;
            product.IsDisabled = true;

            await _productRepo.DeleteProductAsync(product);
            return true;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _productRepo.GetAllProducts();
            var result =  _mappper.Map<IEnumerable<ProductResponseDto>>(products);

            return result;
        }

        public async Task<bool> UpdateProductAsync(ProductRequestDto req, int id)
        {
            var product = await _productRepo.CheckIfProductExist(id);
            
            if (product == null)
            {
                return false;
            }

            product.Name = req.Name;
            product.Brand = req.Brand;
            product.Color = req.Color;
            product.Description = req.Description;
            product.Price = req.Price;
            product.Quantity = req.Quantity;
            product.Size = req.Size;
            product.DateModified = DateTime.Now;

            await _productRepo.UpdateProductAsync(product);
            return true;
        }
    }
}
