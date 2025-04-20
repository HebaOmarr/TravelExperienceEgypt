using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.Models;

namespace TravelExperienceEgypt.DataAccess.DTO.Posts
{
    public class GetFilterOptionsDTO
    {
        //I Will Give front list of IDs and name and return with Id user select
        public List<FilterCategoryDTO> Categories { get; set; }

        //I Will Give front list of IDs and name and return with Id user select
        public List<FilterGovermantateDTO> Govermantates { get; set; }
        public float Rate { get; set; }

        public decimal Price { get; set; }


    }
}
