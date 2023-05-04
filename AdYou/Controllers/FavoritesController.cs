using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdYou.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> Get([FromQuery] string userId) =>
          Ok(await _favoriteService.GetAll(userId));

        [HttpGet("{userId}/{companyId}")]
        public async Task<IActionResult> Get(string userId, int companyId) =>
            Ok(await _favoriteService.GetSameFavoritesCompanies(userId, companyId));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _favoriteService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Favorite value)
        {
            var result = await _favoriteService
                .AddAsync(value)
                .ConfigureAwait(false);
            return CreatedAtAction(nameof(Post), result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Favorite value) =>
            Ok(await _favoriteService.UpdateAsync(value));

        [HttpDelete("{userId}/{companyId}")]
        public async Task<IActionResult> Delete(string userId, int companyId)
        {
            try
            {
               return Ok(await _favoriteService.DeleteFavorite( userId, companyId));
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }
    }
}
