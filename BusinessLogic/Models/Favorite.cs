using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;
using DA = Infastructure.Models;

namespace BusinessLogic.Models
{
    public class Favorite
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        //public AppUser User { get; set; }
        public int CompanyId { get; set; }
        //public Company Company { get; set; }

        public static void CreateMaps(Profile profile)
        {
            profile.CreateMap<DA.Favorite, Favorite>()
                .ReverseMap();
        }
    }
}
