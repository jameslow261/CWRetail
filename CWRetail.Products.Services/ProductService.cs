using CWRetail.Products.Data;
using CWRetail.Products.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWRetail.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _productRepository.GetProduct(id);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            ValidateProduct(product);

            RoundPrice(product);

            var createdProduct = await _productRepository.CreateProduct(product);

            return createdProduct;
        }

        public async Task UpdateProduct(Product product)
        {
            ValidateProduct(product);

            RoundPrice(product);

            await _productRepository.UpdateProduct(product);
        }

        public async Task DeleteProduct(int id)
        {
            await _productRepository.DeleteProduct(id);
        }

        private static void ValidateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new InvalidOperationException("Product name is empty");
            }

            if (product.Name.Length > 100)
            {
                throw new InvalidOperationException("Product name is longer than 100 characters");
            }

            if (product.Price < 0)
            {
                throw new InvalidOperationException("Product price must be at least 0");
            }
        }

        private static void RoundPrice(Product product)
        {
            Math.Round(product.Price, 2);
        }
    }
}
