using AutoMapper;
using BusinessLogic.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA = Infastructure.Models;

namespace BusinessLogic.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        
        public int CompanyId { get; set; }
        public int SoldCounter { get; set; }
        public string Description { get; set; }
        public string ImageSource { get; set; }

        public static void CreateMaps(Profile profile)
        {
            profile.CreateMap<DA.Product, Product>()
                .ReverseMap();
        }
    }
}
