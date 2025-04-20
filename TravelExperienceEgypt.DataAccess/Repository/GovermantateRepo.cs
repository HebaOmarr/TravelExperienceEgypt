using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.Repository
{
    internal class GovermantateRepo : GenericRepository<Govermantate>, IGovermantateRepo
    {
        private readonly ApplicationDBContext applicationDBContext;
        public GovermantateRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
    }
   
}
