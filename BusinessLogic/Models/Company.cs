using AutoMapper;
using DA = Infastructure.Models;

namespace BusinessLogic.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }

        public string ImageSource { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }

        public string Description { get; set; }


        public IEnumerable<Product> Products { get; set; }
        internal static void CreateMaps(Profile profile)
        {
            profile.CreateMap<DA.Company, Company>()
                .ForMember(x => x.Products, opt => opt.MapFrom(s => s.Products))
                .ReverseMap()
                .ForMember(d => d.Products, opt => opt.MapFrom(s => s.Products.ToList()));

        }
    }
}
