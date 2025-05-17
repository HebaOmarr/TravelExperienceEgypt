using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.DTO.GovernorateDTO
{
    public class GetGovernorateByIdDto
    {
        public int GovermantateId { get; set; }
        public string Title { get; set; } = null!;
        public string ImageLink { get; set; } = string.Empty;
        public string OverviewDescription { get; set; } = string.Empty;
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
