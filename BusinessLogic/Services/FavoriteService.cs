using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Infastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAM = Infastructure.Models;

namespace BusinessLogic.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IDbContextFactory<AddYouContext> _contextFactory;
        private readonly IAppUserService _appUserService;

        private readonly IMapper _mapper;

        public FavoriteService(IDbContextFactory<AddYouContext> contextFactory, IMapper mapper, UserManager<DAM.AppUser> userManager, IAppUserService appUserService)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
            _appUserService = appUserService;
        }

        public async Task<Favorite> AddAsync(Favorite item)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var entities = context.Favorites
                    .Where( x => x.UserId == item.UserId  && x.CompanyId == item.CompanyId)
                    .ToList();
                if (entities.Count() > 0)
                {
                    return null;
                }
                var addModel = _mapper.Map<DAM.Favorite>(item);
                var entityDa = await context.Favorites
                    .AddAsync(addModel)
                    .ConfigureAwait(false);
                await context.SaveChangesAsync().ConfigureAwait(false);
                return _mapper.Map<Favorite>(entityDa.Entity);
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
        public async Task<Favorite> UpdateAsync(Favorite item)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.Favorites
                .FirstOrDefaultAsync(i => i.Id == item.Id)
                .ConfigureAwait(false);

            if (entity != null)
            {
                var itemNew = _mapper.Map(item, entity);

                context.Favorites.Update(itemNew);
            }

            return _mapper.Map<Favorite>(entity);
        }
        public async Task<Favorite> GetByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.Favorites
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id)
                .ConfigureAwait(false);

            return _mapper.Map<Favorite>(entity);
        }
        public async Task<IEnumerable<Favorite>> GetByUserId(string userId)
        {
            using var context = _contextFactory.CreateDbContext();

            var entities = await context.Favorites
                .Where(item => item.UserId == userId)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);

            return _mapper.Map<IEnumerable<Favorite>>(entities);
        }
        public async Task<IEnumerable<Favorite>> GetByCompanyId(int companyId)
        {
            using var context = _contextFactory.CreateDbContext();

            var entities = await context.Favorites
                .Where(item => item.CompanyId == companyId)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);

            return _mapper.Map<IEnumerable<Favorite>>(entities);
        }
        public async Task<IEnumerable<Company>> GetAll(string userId)
        {
            using var context = _contextFactory.CreateDbContext();

            var entities = await context.Favorites
                //.Include(x => x.Company)
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.CompanyId)
                .ToListAsync()
                .ConfigureAwait(false);

            var companies = await context.Companies
                    .Where(x => entities.Contains(x.Id))
                    .ToListAsync();



            return _mapper.Map<IEnumerable<Company>>(companies);
        }
        public async Task<IEnumerable<Infastructure.Models.OrderedProduct>> GetOrderedProduct(string userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var items = await context.OrderedProducts
                .Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.Id)
                .ToListAsync();
            return items;
        }
        public async Task<bool> GetSameFavoritesCompanies(string userId, int companyId)
        {
            using var context = _contextFactory.CreateDbContext();
            var items = await context.Favorites
                .Where(x => x.UserId == userId)
                .Where(x => x.CompanyId == companyId)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
            return items.Count > 0;
        }

        public async Task<bool> DeleteFavorite(string userId, int companyId)
        {
            using var context = _contextFactory.CreateDbContext();
            var entity = await context.Favorites
                .FirstOrDefaultAsync(x => x.UserId == userId && x.CompanyId == companyId)
                .ConfigureAwait(false);

            if (entity == null)
                return false;

             context.Favorites
                .Remove(entity);

            await context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
    }
}
