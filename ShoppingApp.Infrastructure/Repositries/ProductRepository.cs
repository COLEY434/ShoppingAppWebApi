using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Entities;
using ShoppingApp.Core.Interfaces.Repositries;
using ShoppingApp.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp.Infrastructure.Repositries
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _dbContext.Products.AsNoTracking().ToListAsync();

            return products;
        }

        public async Task AddProduct(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> CheckIfProductExist(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task UpdateProductAsync(Product product)
        {

             _dbContext.Products.Update(product);
             await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
