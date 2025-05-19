using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.Repository;
using TravelExperienceEgypt.DataAccess.Repository.Contract;

namespace TravelExperienceEgypt.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepo Post{ get; }
        IPlaceRepository Place { get; }
        ICategoryRepo Category { get; }
        IWishlistRepo WishList { get; }
        IGovernorateRepository Govermantate { get; }
        ICommentRepo Comment {get;}
        Task Save();  
    }
}
