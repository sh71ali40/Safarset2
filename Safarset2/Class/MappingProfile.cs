using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Safarset.Datalayer.Context.Entities;
using Safarset.ViewModel.Entities;

namespace Safarset2.Class
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<SlideDto, Slide>().ReverseMap();
            CreateMap<CityDto, City>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            

        }
    }
}
