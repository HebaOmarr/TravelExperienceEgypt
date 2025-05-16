using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.Repository.Contract;

namespace TravelExperienceEgypt.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepo Post{ get; }
        IPlaceRepository Place { get; }
        IWishlistRepo WishList { get; }
        IGovernorateRepository Govermantate { get; }
        Task Save();  
    }
}
