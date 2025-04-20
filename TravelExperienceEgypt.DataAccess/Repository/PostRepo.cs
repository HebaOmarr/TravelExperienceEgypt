using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.Repository
{
    internal class PostRepo : GenericRepository<Post>, IPostRepo
    {
        private readonly ApplicationDBContext applicationDBContext;
        public PostRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
    }
  
}
