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

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> Create(
               [FromBody] CreateProductRequest request,
               CancellationToken cancellationToken)
        {
            var product = await _productService.CreateAsync(
                request,
                cancellationToken);

            return Created(
                $"/api/products/{product.Id}",
                product);
        }



        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductResponse>> Get(Guid id)
        {
            var product = await _productService.GetAsync(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

    }
}
