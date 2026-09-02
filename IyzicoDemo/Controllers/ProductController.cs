using System.Threading.Tasks;
using IyzicoDemo.Entity;
using IyzicoDemo.Models;
using IyzicoDemo.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IyzicoDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet("{id}")]
        public async Task<List<Product>> Get(Guid id)
        {
            return await _productService.GetProductByIdAsync(id);
        }

        // POST api/<ProductController>
        [HttpGet("get-all-category")]
        public async Task<List<Category>> GetAllCategory()
        {

            return await _productService.GettAllCategoryAsync();

        }
        [HttpGet("get-all-product")]
        public async Task<List<Product>> GetAllProduct()
        {

            return await _productService.GettAllProductAsync();

        }
        [HttpGet("get-all-ProductToCategory")]
        public async Task<List<Category>> GettAllProductToCategory()
        {

            return await _productService.GettAllProductToCategoryAsync();

        }
        [HttpPost("create-product")]
        public async Task<IActionResult> Post([FromForm] CreateProductRequest model)
        {
            try
            {
                await _productService.CreateProductAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Controller error. " + ex.Message);

            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] CreateProductRequest value)
        {
            await _productService.UpdateProductAsync(id, value);
            return Ok();
        }
        [HttpPut("update-category/{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromForm] CreateCategoryRequest value)
        {
            await _productService.UpdateCategoryAsync(id, value);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            try
            {
                _productService.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Controller error. " + ex.Message);
            }
        }
        [HttpPost("add-category")]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryRequest model)
        {
            try
            {
                await _productService.CreateCategoryAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Controller error. " + ex.Message);

            }
        }
        [HttpDelete("category-delete-{id}")]
        public void DeleteCategory(Guid id)
        {
            try
            {
                _productService.DeleteCategory(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Controller error. " + ex.Message);
            }
        }

        [HttpDelete("delete-product-image-{id}")]
        public void DeleteProductImage(Guid id)
        {
            try
            {
                _productService.DeleteProductImage(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Controller error. " + ex.Message);
            }
        }
    }
}
