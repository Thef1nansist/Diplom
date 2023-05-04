using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdYou.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }


        [HttpGet("GetPopularCompanies")]
        public async Task<IActionResult> GetPopularCoffeeHouses() =>
            Ok(await _companyService.GetPopularCompanies());

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _companyService.GetAllAsync());

        [HttpPut]
        public async Task<IActionResult> Get([FromBody] GetCompaniesByAdmin command) =>
            Ok(await _companyService.GetAllAsync(command.AdminId));

        [HttpGet("GetProductByUserAsync")]
        public async Task<IActionResult> GetProductByUserAsync([FromQuery] string idUser) =>
            Ok(await _companyService.GetProductByUserAsync(idUser));

        [HttpGet("GetByCompaniesIdUser")]
        public async Task<IActionResult> GetByCompaniesIdUser([FromQuery] string userId) =>
            Ok(await _companyService.GetByCompaniesIdUser(userId));

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById([FromQuery] int productId) =>
            Ok(await _companyService.GetProductById(productId));

        [HttpGet("GetByCompanyId")]
        public async Task<IActionResult> GetByCompanyId([FromQuery] int companyId) =>
            Ok(await _companyService.GetByCompanyId(companyId));

        [HttpGet("getSearchProducts")]
        public async Task<IActionResult> GetSearchProducts([FromQuery] string productName) =>
            Ok(await _companyService.GetSearchProducts(productName));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) =>
            Ok(await _companyService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Company value) =>
            Ok(await _companyService.AddAsync(value));

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProductAsync([FromBody] Product product) =>
            Ok(await _companyService.AddAsyncProduct(product));

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Company value) =>
            Ok(await _companyService.UpdateAsync(value));

        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            Ok(await _companyService.DeleteProduct(id));
        }
    }
}
