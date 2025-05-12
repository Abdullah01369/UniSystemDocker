using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniSystem.Core.Services;

namespace UniSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]

    public class ProductController : CustomBaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;

        }
        [HttpGet]
        public async Task<IActionResult> ListOfProduct()
        {
            var val = await _productService.ProductList();
            return ActionResultInstance(val);
        }
    }
}
