using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdYou.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderedProductController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public OrderedProductController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] string userId) =>
            Ok(await _favoriteService.GetOrderedProduct(userId));
    }
}
