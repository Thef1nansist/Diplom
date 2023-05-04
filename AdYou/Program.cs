using Infastructure.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AdYou
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<Infastructure.Contexts.AddYouContext>()
            .UseSqlite("DataSource=company.db")
               .Options;
            var context = new Infastructure.Contexts.AddYouContext(options);
            //context.Database.EnsureDeleted();
            if (context.Database.EnsureCreated())
            {
                var users = context.Users.ToList();
                var a = users.ToArray();
                var us = new AppUser();
                us.IsAdmin = false;
                us.UserName = "vlad12";
                us.PasswordHash = "sadsadasd";
                context.Users.Add(us);
                var us2 = new AppUser();
                us.IsAdmin = true;
                us.UserName = "vlad";
                us.PasswordHash = "sadsadaasdasdasdsd";
                context.Users.Add(us2);
                context.SaveChanges();
                var companies = context.Companies.ToList();
                var b = companies.ToArray();
            }


            //var users = context.Users.ToList();
            //var user = users[0];
            //var admin = users[1];
            //var coff = new Company();
            //var coffeeHouse = new Company
            //{
            //    CreatorId = admin.Id,
            //    Name = "Pure",
            //    Address = "city Amuera",
            //    Creator = admin,
            //    ImageSource = "asdasdasdasd",
            //    Products = new List<Product>
            //        {
            //            new Product
            //            {
            //                Name = "Coffee",
            //                Price = 10,
            //                SoldCounter = 5
            //            },
            //            new Product
            //            {
            //                Name = "Tea",
            //                Price = 15,
            //                SoldCounter = 2
            //            },
            //            new Product
            //            {
            //                Name = "Cola",
            //                Price = 7,
            //                SoldCounter = 15
            //            },
            //        }
            //};
            //context.Companies.Add(coffeeHouse);
            //context.SaveChanges();
            //var orderedCoffee = new OrderedProduct
            //{
            //    UserId = user.Id,
            //    User = user,
            //    Product = coffeeHouse.Products.Last(),
            //    ProductId = coffeeHouse.Products.Last().Id
            //};
            //context.OrderedProducts.Add(orderedCoffee);
            //context.SaveChanges();

            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}