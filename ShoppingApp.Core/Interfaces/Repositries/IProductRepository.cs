using ShoppingApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Core.Interfaces.Repositries
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task AddProduct(Product product);
        Task<Product> CheckIfProductExist(int id);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
    } 
}
