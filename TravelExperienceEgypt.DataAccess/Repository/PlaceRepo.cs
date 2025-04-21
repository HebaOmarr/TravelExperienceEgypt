using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.Repository
{
    public class PlaceRepo : GenericRepository<Place>, IPlaceRepo
    {
        private readonly ApplicationDBContext applicationDBContext;
        public PlaceRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
    }
   
}
