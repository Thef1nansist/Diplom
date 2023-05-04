using AutoMapper;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Infastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAM = Infastructure.Models;

namespace BusinessLogic.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IDbContextFactory<AddYouContext> _contextFactory;
        private readonly IMapper _mapper;

        public CompanyService(IDbContextFactory<AddYouContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }
        public async Task<Company> AddAsync(Company item)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var map = _mapper.Map<DAM.Company>(item);
                var entityDa = await context.Companies
                    .AddAsync(map)
                    .ConfigureAwait(false);

                await context.SaveChangesAsync().ConfigureAwait(false);

                return _mapper.Map<Company>(entityDa.Entity);
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }

        public async Task<Product> AddAsyncProduct(Product item)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var map = _mapper.Map<DAM.Product>(item);
                var entityDa = await context.Products
                    .AddAsync(map)
                    .ConfigureAwait(false);

                await context.SaveChangesAsync().ConfigureAwait(false);

                return _mapper.Map<Product>(entityDa.Entity);
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
        public async Task<List<Product>> GetProductByUserAsync(string userId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var products = await context.OrderedProducts
                    .Include(x => x.Product)
                    .Where(x => x.UserId == userId)
                    .Select(x => x.Product)
                    .ToListAsync()
                    .ConfigureAwait(false);

                return _mapper.Map<List<Product>>(products);
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }
        public async Task<Company> UpdateAsync(Company item)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.Companies
                .FirstOrDefaultAsync(i => i.Id == item.Id)
                .ConfigureAwait(false);

            if (entity != null)
            {
                var itemNew = _mapper.Map(item, entity);

                context.Companies.Update(itemNew);
            }
            await context.SaveChangesAsync().ConfigureAwait(false);

            return _mapper.Map<Company>(entity);
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var entity = await context.Companies
                .AsNoTracking()
                .Include(x => x.Products)
                .FirstOrDefaultAsync(i => i.Id == id)
                .ConfigureAwait(false);

            return _mapper.Map<Company>(entity);
        }
        public async Task<IEnumerable<Company>> GetByCompanyId(int companyId)
        {
            using var context = _contextFactory.CreateDbContext();

            var entities = await context.Companies
                .Where(item => item.Id == companyId)
                .Include(x => x.Products)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);

            return _mapper.Map<IEnumerable<Company>>(entities);
        }

        public async Task<IEnumerable<Company>> GetByCompaniesIdUser(string userId)
        {
            using var context = _contextFactory.CreateDbContext();

            var entities = await context.Companies
                .Where(x => x.CreatorId == userId)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
            return _mapper.Map<IEnumerable<Company>>(entities);
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext(); //для работы с любыми данными юзать using

            var entities = await context.Companies
                .AsNoTracking()
                .Include(x => x.Products)
                .ToListAsync()
                .ConfigureAwait(false);
            return _mapper.Map<IEnumerable<Company>>(entities);
        }
        public async Task<IEnumerable<Company>> GetAllAsync(string adminId)
        {
            using var context = _contextFactory.CreateDbContext(); //для работы с любыми данными юзать using

            var entities = await context.Companies
                .AsNoTracking()
                .Include(x => x.Products)
                .Where(x => x.CreatorId == adminId)
                .ToListAsync()
                .ConfigureAwait(false);
            return _mapper.Map<IEnumerable<Company>>(entities);
        }
        public async Task SellProductAsync(string userId, int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var productItem = await context.Products
                .FirstOrDefaultAsync(c => c.Id == id)
                .ConfigureAwait(false);

            productItem.SoldCounter++;

            context.Products.Update(productItem);
            await context.SaveChangesAsync().ConfigureAwait(false);

            var user = context.Users.First(u => u.Id == userId);
            context.OrderedProducts.Add(new DAM.OrderedProduct
            {
                ProductId = productItem.Id,
                UserId = user.Id,
                Product = productItem,
                User = user
            });

            await context.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task<IEnumerable<Company>> GetPopularCompanies()
        {
            using var context = _contextFactory.CreateDbContext();
            var items = await context.Companies
                .Include(x => x.Products)
                .Where(x => x.Products.Any(x => x.SoldCounter > 10))
                .OrderBy(x => x.Name)
                .Take(10)
                .ToListAsync()
                .ConfigureAwait(false);
            return _mapper.Map<IEnumerable<Company>>(items);
        }

        public async Task<Product> GetProductById(int productId)
        {
            using var context = _contextFactory.CreateDbContext();
            var item = await context.Products
                    .FirstOrDefaultAsync(x => x.Id == productId)
                     .ConfigureAwait(false);

            return _mapper.Map<Product>(item);
        }

        public async Task<List<Product>> GetSearchProducts(string productName)
        {
            try
            {
                var decodeFilter = HttpUtility.UrlDecode(productName);
                productName = productName.ToLower();

                using var context = _contextFactory.CreateDbContext();

                var prod = await context.Products.ToListAsync();
                
                var products = await context.Products
                    .Where(x => (x.Name.ToLower().Contains(productName)) || (x.Description.ToLower().Contains(productName)) || (x.Name == productName))
                    .ToListAsync()
                    .ConfigureAwait(false);
                var products2 = await context.Products
                    .Where(x => (x.Name.ToLower().IndexOf(decodeFilter.ToLower()) >= 0) || (x.Description.ToLower().IndexOf(decodeFilter.ToLower())) >= 0)
                    .ToListAsync()
                    .ConfigureAwait(false);

                return _mapper.Map<List<Product>>(products);
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var product = await context.Products
                    .FirstOrDefaultAsync(x => x.Id == id)
                    .ConfigureAwait(false);
                if (product == null)
                {
                    return false;
                }

                context.Products.Remove(product);
                await context.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
    }
}
