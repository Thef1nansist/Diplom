using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICompanyService
    {
        Task<Company> AddAsync(Company item);

        Task<Product> AddAsyncProduct(Product product);
        Task<IEnumerable<Company>> GetAllAsync();
        Task<IEnumerable<Company>> GetAllAsync(string adminId);
        Task<IEnumerable<Company>> GetByCompanyId(int companyId);
        Task<Company> GetByIdAsync(int id);

        Task<bool> DeleteProduct(int id);
        Task<Company> UpdateAsync(Company item);
        Task SellProductAsync(string userId, int id);
        Task<IEnumerable<Company>> GetPopularCompanies();

        Task<List<Product>> GetProductByUserAsync(string userId);

        Task<List<Product>> GetSearchProducts(string productName);

        Task<IEnumerable<Company>> GetByCompaniesIdUser(string userId);

        Task<Product> GetProductById(int productId);
        
    }
}
