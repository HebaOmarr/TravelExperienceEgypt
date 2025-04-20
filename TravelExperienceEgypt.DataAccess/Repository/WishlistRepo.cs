using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.Repository
{
    internal class WishlistRepo : GenericRepository<Wishlist>, IWishlistRepo
    {
        private readonly ApplicationDBContext applicationDBContext;
        public WishlistRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
    }
  
}
