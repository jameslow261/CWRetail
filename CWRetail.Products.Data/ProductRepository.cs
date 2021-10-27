using CWRetail.Products.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWRetail.Products.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _productDbContext;

        public ProductRepository(ProductDbContext productDbContext) => _productDbContext = productDbContext;

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productDbContext.Set<Product>().ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _productDbContext.Set<Product>().FindAsync(id);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _productDbContext.Set<Product>().AddAsync(product);

            await _productDbContext.SaveChangesAsync();

            return product;
        }

        public async Task UpdateProduct(Product product)
        {
            var productToUpdate = await GetProduct(product.Id);

            productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;
            productToUpdate.Type = product.Type;
            productToUpdate.IsActive = product.IsActive;

            _productDbContext.Set<Product>().Update(productToUpdate);

            await _productDbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var productToDelete = await GetProduct(id);

            _productDbContext.Set<Product>().Remove(productToDelete);

            await _productDbContext.SaveChangesAsync();
        }
    }
}
