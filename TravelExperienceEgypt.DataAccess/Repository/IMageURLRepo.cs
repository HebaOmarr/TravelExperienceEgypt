using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TravelExperienceEgypt.DataAccess.Repository
{
    internal class IMageURLRepo : GenericRepository<ImageUrl>, IIMageURLRepo
    {
        private readonly ApplicationDBContext applicationDBContext;
        public IMageURLRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
    }
   
}
