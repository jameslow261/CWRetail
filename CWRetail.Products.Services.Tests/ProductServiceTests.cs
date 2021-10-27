using CWRetail.Products.Data;
using CWRetail.Products.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CWRetail.Products.Services.Tests
{
    public class ProductServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task CreateProduct_EmptyName_ThrowsInvalidOperationException(string name)
        {
            var mockRepo = new Mock<IProductRepository>();

            var productService = new ProductService(mockRepo.Object);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async() => await productService.CreateProduct(new Product { Name = name }));

            Assert.Equal("Product name is empty", ex.Message);
        }

        [Fact]
        public async Task CreateProduct_NameLongerThan100Chars_ThrowsInvalidOperationException()
        {
            var mockRepo = new Mock<IProductRepository>();

            var productService = new ProductService(mockRepo.Object);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await productService.CreateProduct(new Product { Name = new string('a', 101) }));

            Assert.Equal("Product name is longer than 100 characters", ex.Message);
        }

        [Fact]
        public async Task CreateProduct_PriceBelowZero_ThrowsInvalidOperationException()
        {
            var mockRepo = new Mock<IProductRepository>();

            var productService = new ProductService(mockRepo.Object);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await productService.CreateProduct(new Product { Name = "name", Price = -1 }));

            Assert.Equal("Product price must be at least 0", ex.Message);
        }

        [Fact]
        public async Task CreateProduct_Valid_ReturnsCreatedProduct()
        {
            var mockRepo = new Mock<IProductRepository>();

            var newProduct = new Product { Name = "name", Price = 1 };

            mockRepo.Setup(repo => repo.CreateProduct(newProduct)).Returns(Task.FromResult(GetProduct()));

            var productService = new ProductService(mockRepo.Object);

            var createdProduct = await productService.CreateProduct(newProduct);

            Assert.NotNull(createdProduct);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task UpdateProduct_EmptyName_ThrowsInvalidOperationException(string name)
        {
            var mockRepo = new Mock<IProductRepository>();

            var productService = new ProductService(mockRepo.Object);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await productService.UpdateProduct(new Product { Name = name }));

            Assert.Equal("Product name is empty", ex.Message);
        }

        [Fact]
        public async Task UpdateProduct_NameLongerThan100Chars_ThrowsInvalidOperationException()
        {
            var mockRepo = new Mock<IProductRepository>();

            var productService = new ProductService(mockRepo.Object);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await productService.UpdateProduct(new Product { Name = new string('a', 101) }));

            Assert.Equal("Product name is longer than 100 characters", ex.Message);
        }

        [Fact]
        public async Task UpdateProduct_PriceBelowZero_ThrowsInvalidOperationException()
        {
            var mockRepo = new Mock<IProductRepository>();

            var productService = new ProductService(mockRepo.Object);

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await productService.UpdateProduct(new Product { Name = "name", Price = -1 }));

            Assert.Equal("Product price must be at least 0", ex.Message);
        }

        private Product GetProduct()
        {
            return new Product { Id = 1, Name = "name 1", Type = Models.Type.Books, Price = 1, IsActive = true };
        }
    }
}
