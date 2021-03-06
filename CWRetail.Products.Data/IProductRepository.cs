using CWRetail.Products.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWRetail.Products.Data
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProduct(int id);

        Task<Product> CreateProduct(Product product);

        Task UpdateProduct(Product product);

        Task DeleteProduct(int id);
    }
}