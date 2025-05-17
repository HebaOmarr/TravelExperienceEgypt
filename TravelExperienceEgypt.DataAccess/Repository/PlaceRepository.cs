using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.Repository
{
    public class PlaceRepository : GenericRepository<Place>, IPlaceRepository
    {
        private readonly ApplicationDBContext applicationDBContext;
        public PlaceRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
    }
   
}
