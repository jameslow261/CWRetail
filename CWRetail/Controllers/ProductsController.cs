using CWRetail.Products.Models;
using CWRetail.Products.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWRetail.Products.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            var createdProduct = default(Product);

            try
            {
                createdProduct = await _productService.CreateProduct(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (id != product.Id)
            {
                return BadRequest("Product ids mismatch");
            }

            var productToUpdate = await _productService.GetProduct(id);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            try
            {
                await _productService.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var productToDelete = await _productService.GetProduct(id);

            if (productToDelete == null)
            {
                return NotFound();
            }

            try
            {
                await _productService.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return NoContent();
        }
    }
}
