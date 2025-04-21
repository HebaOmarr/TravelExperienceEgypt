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
        IPlaceRepo Place { get; }
        IWishlistRepo WishList { get; }
        Task Save();  
    }
}
