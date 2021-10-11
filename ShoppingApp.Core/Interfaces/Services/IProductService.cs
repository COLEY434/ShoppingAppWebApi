using ShoppingApp.Core.DTOs.Request;
using ShoppingApp.Core.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task AddProductAsync(ProductRequestDto req);
        Task<bool> UpdateProductAsync(ProductRequestDto req, int id);
        Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
        Task<bool> DeleteProductAsync(int id);
    }
}
