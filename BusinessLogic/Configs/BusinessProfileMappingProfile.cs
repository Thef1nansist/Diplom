using AutoMapper;
using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Configs
{
    public class BusinessProfileMappingProfile : Profile
    {
        public BusinessProfileMappingProfile()
        {
            AppUser.CreateMaps(this);
            Company.CreateMaps(this);
            Favorite.CreateMaps(this);
            Product.CreateMaps(this);
        }
    }
}
