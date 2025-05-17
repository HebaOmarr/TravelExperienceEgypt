using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.Repository
{
    public class GovernorateRepository : GenericRepository<Govermantate>, IGovernorateRepository
    {
        private readonly ApplicationDBContext applicationDBContext;
        public GovernorateRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }

    }
   
}
