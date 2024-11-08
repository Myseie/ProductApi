using Microsoft.AspNetCore.Mvc;
using ProductApi.Model;
using ProductApi.Service;


namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<List<Product>> Get() => _productService.GetAllProducts();

        [HttpGet("{id}")]
        public ActionResult<Product> Get(string id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();

            }
            return product;
        }

        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {

            _productService.CreateProduct(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id.ToString() }, product);
        }

        [HttpPut("{id}")]

        public IActionResult Update(string id,Product product)
        {
            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();

            }

            _productService.UpdateProduct(id, product);
            return NoContent();
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(string id)
        {
            var product = _productService.GetProductById(id);
            if(product == null)
            {
                return NotFound();
            }

            _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
