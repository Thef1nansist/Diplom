using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdYou.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemsController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public ProductItemsController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public IEnumerable<string> Get() => new string[] { "value1", "value2" };

        [HttpGet("{id}")]
        public string Get(int id) => "value";

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        [HttpGet("sellProduct/{id}/{userId}")]
        public async Task<IActionResult> SellProduct(int id, string userId)
        {
            await _companyService.SellProductAsync(userId, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
