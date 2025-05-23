using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.Data;
using TravelExperienceEgypt.DataAccess.Models;
using TravelExperienceEgypt.DataAccess.Repository;
using TravelExperienceEgypt.DataAccess.Repository;
using TravelExperienceEgypt.DataAccess.Repository.Contract;

namespace TravelExperienceEgypt.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext applicationDBContext;
        public IPostRepo Post { get;  }
        public IPlaceRepository Place { get; private set; }
        public IWishlistRepo WishList { get;  }
        public IGovernorateRepository Govermantate { get; private set; }
        public ICategoryRepo Category { get; }
        public UnitOfWork(ApplicationDBContext applicationDBContext, IPostRepo postRepo,
            IWishlistRepo wishlistRepo, ICategoryRepo categoryRepo ,IGovernorateRepository governorateRepository)
        {
            this.applicationDBContext = applicationDBContext;
            Post = postRepo;
 
            WishList = wishlistRepo;
            Govermantate = governorateRepository;

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
