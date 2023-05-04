using BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IFavoriteService
    {
        public Task<Favorite> AddAsync(Favorite item);
        public Task<IEnumerable<Company>> GetAll(string userId);
        Task<IEnumerable<Favorite>> GetByCompanyId(int companyId);
        Task<Favorite> GetByIdAsync(int id);
        Task<IEnumerable<Favorite>> GetByUserId(string userId);
        Task<Favorite> UpdateAsync(Favorite item);
        Task<IEnumerable<Infastructure.Models.OrderedProduct>> GetOrderedProduct(string userId);

        Task<bool> GetSameFavoritesCompanies(string userId, int companyId);

        Task<bool> DeleteFavorite(string userId, int companyId);
    }
}
