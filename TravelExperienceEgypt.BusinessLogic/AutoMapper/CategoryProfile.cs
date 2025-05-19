using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.DTO.Category;
using TravelExperienceEgypt.DataAccess.Models;

namespace TravelExperienceEgypt.BusinessLogic.AutoMapper
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category,CategoryDTO>().ReverseMap();
        }
    }
}
