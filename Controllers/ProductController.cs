using dotnet_search_mongo.Models;
using dotnet_search_mongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_search_mongo.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("products/populate")]
        public IActionResult Populate()
        {
            for (int i = 1; i <= 50; i++)
            {
                _productService.AddProduct(new Product
                {
                    Title = new Bogus.DataSets.Lorem().Word(),
                    Description = new Bogus.DataSets.Lorem().Paragraph(),
                    Image = new Bogus.DataSets.Images().LoremPixelUrl(),
                    Price = new Bogus.Randomizer().Number(10, 100),
                });
            }

            return Ok(new
            {
                message = "success"
            });
        }

        [HttpGet("products/frontend")]
        public IActionResult Frontend()
        {
            return Ok(_productService.All());
        }

        [HttpGet("products/backend")]
        public IActionResult Backend(
            [FromQuery(Name = "s")] string s,
            [FromQuery(Name = "sort")] string sort,
            [FromQuery(Name = "page")] int page
        )
        {
            return Ok(_productService.Query(s, sort, page));
        }
    }
}