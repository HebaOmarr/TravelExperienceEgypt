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
<<<<<<< HEAD
        IPlaceRepository Place { get; }
=======
        IPlaceRepo Place { get; }
        IGenericRepository<Category> Category { get; }

>>>>>>> 91811c5c4cc536ad1922520700f27000cfe47358
        IWishlistRepo WishList { get; }
        IGovernorateRepository Govermantate { get; }
        Task Save();  
    }
}
