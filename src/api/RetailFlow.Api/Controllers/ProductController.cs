using Microsoft.AspNetCore.Mvc;
using RetailFlow.Application.DTOs.Products;
using RetailFlow.Application.Interfaces;


namespace RetailFlow.Api.Controllers
{

    //[Route("api/[controller]")]
    [Route("api/products")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;



        // POST api/<ProductController>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public ActionResult<ProductResponse> Create(CreateProductRequest request)
        {
            var product = _productService.Create(request);

            return Created($"/api/products/{product.Id}", product);
        }

    }
}
