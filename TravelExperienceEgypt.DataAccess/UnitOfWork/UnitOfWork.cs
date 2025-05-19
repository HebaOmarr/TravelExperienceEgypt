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
        public IPostRepo Post { get; private set; }
        public IPlaceRepository Place { get; private set; }
        public IWishlistRepo WishList { get; private set; }
        public IIMageURLRepo ImageURL { get; private set; }
        public IGovernorateRepository Govermantate { get; private set; }
        public ICategoryRepo Category { get; private set; }

        public UnitOfWork(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;

            this.Post = new PostRepo(applicationDBContext);
            this.WishList = new WishlistRepo(applicationDBContext);
            this.Place = new PlaceRepository(applicationDBContext);
            this.ImageURL = new IMageURLRepo(applicationDBContext);
            this.Govermantate = new GovernorateRepository(applicationDBContext);
            this.Category = new CategoryRepo(applicationDBContext);
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
