using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.Data;
using TravelExperienceEgypt.DataAccess.Repository;
using TravelExperienceEgypt.DataAccess.Repository.Contract;

namespace TravelExperienceEgypt.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext applicationDBContext;
        public IPostRepo Post { get;  }
        public IPlaceRepo Place { get;  }
        public IGenericRepository<Category> Category { get; }
        public IWishlistRepo WishList { get; }
        public UnitOfWork(ApplicationDBContext applicationDBContext, IPostRepo postRepo,IPlaceRepo placeRepo,
            IWishlistRepo wishlistRepo, IGenericRepository<Category> categoryRepo)
        {
            this.applicationDBContext = applicationDBContext;
            Post = postRepo;
            Place = placeRepo;
            WishList = wishlistRepo;
            Category = categoryRepo;
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
