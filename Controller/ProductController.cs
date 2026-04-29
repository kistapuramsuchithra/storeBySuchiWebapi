using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApisforstorebySuchi.Interface;
using WebApisforstorebySuchi.Common.Objects;

namespace WebApisforstorebySuchi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _manager;

        public ProductController(IProductManager manager)
        {
            _manager = manager;
        }

        //  GET ALL PRODUCTS
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _manager.GetAllProducts();
            return Ok(products);
        }

        //  CREATE PRODUCT
        [HttpPost]
        public IActionResult Create([FromBody] ProductDTO product)
        {
            if (product == null)
                return BadRequest("Invalid product data");

            var id = _manager.AddProduct(product);
            return Ok(new { ProductId = id });
        }

        //  UPDATE PRODUCT
        [HttpPut]
        public IActionResult Update([FromBody] ProductDTO product)
        {
            if (product == null || product.Id <= 0)
                return BadRequest("Invalid product data");

            var result = _manager.UpdateProduct(product);

            if (!result)
                return StatusCode(500, "Failed to update product");

            return Ok("Product updated successfully");
        }

        //  DELETE PRODUCT
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid product id");

            var result = _manager.DeleteProduct(id);

            if (!result)
                return StatusCode(500, "Failed to delete product");

            return Ok("Product deleted successfully");
        }
    }
}