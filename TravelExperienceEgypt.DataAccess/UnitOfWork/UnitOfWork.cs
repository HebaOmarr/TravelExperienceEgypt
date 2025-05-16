using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.Data;
<<<<<<< HEAD
using TravelExperienceEgypt.DataAccess.Models;
using TravelExperienceEgypt.DataAccess.Repository;
=======
using TravelExperienceEgypt.DataAccess.Repository;
using TravelExperienceEgypt.DataAccess.Repository.Contract;
>>>>>>> 91811c5c4cc536ad1922520700f27000cfe47358

namespace TravelExperienceEgypt.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext applicationDBContext;
        public IPostRepo Post { get;  }
<<<<<<< HEAD
        public IPlaceRepository Place { get; private set; }
        public IWishlistRepo WishList { get;  }
        public IGovernorateRepository Govermantate { get; private set; }
        public UnitOfWork(ApplicationDBContext applicationDBContext, IPostRepo postRepo,IWishlistRepo wishlistRepo)
=======
        public IPlaceRepo Place { get;  }
        public IGenericRepository<Category> Category { get; }
        public IWishlistRepo WishList { get; }
        public UnitOfWork(ApplicationDBContext applicationDBContext, IPostRepo postRepo,IPlaceRepo placeRepo,
            IWishlistRepo wishlistRepo, IGenericRepository<Category> categoryRepo)
>>>>>>> 91811c5c4cc536ad1922520700f27000cfe47358
        {
            this.applicationDBContext = applicationDBContext;
            Post = postRepo;
            
            WishList = wishlistRepo;
<<<<<<< HEAD
           
        }
        public IGovernorateRepository GovermantateRepository
        {
            get
            {
                if (Govermantate == null) {
                    Govermantate = new GovernorateRepository(applicationDBContext);
                }
                return Govermantate;
            }
        }
        public IPlaceRepository PlaceRepository
        {
            get
            {
                if (Place == null)
                {
                    Place = new PlaceRepository(applicationDBContext);
                }
                return Place;
            }
=======
            Category = categoryRepo;
>>>>>>> 91811c5c4cc536ad1922520700f27000cfe47358
        }


        public async void Dispose()
        {
            await applicationDBContext.DisposeAsync();
        }
        public async Task Save()
        {
            await applicationDBContext.SaveChangesAsync();
        }
    }
}
