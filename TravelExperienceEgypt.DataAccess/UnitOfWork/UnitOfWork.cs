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
        private IPlaceRepository placeRepository;
        private IGovernorateRepository govermantateRepository;
        private ICommentRepo comment;

        public IPostRepo Post { get; }
        public IWishlistRepo WishList { get; }
        public ICategoryRepo Category { get; }
        public ICommentRepo Comment
        {
            get
            {
                if (comment is null)
                {
                    comment = new CommentRepo(this.applicationDBContext);
                }
                return comment;
            }
        }

        public UnitOfWork(ApplicationDBContext applicationDBContext, IPostRepo postRepo,
            IWishlistRepo wishlistRepo, ICategoryRepo categoryRepo)
        {
            this.applicationDBContext = applicationDBContext;
            Post = postRepo;

            WishList = wishlistRepo;

        }
        public IGovernorateRepository Govermantate
        {
            get
            {
                if (govermantateRepository is null)
                {
                    govermantateRepository = new GovernorateRepository(this.applicationDBContext);
                }
                return govermantateRepository;
            }
        }
        public IPlaceRepository Place
        {
            get
            {
                if (placeRepository is null)
                {
                    placeRepository = new PlaceRepository(this.applicationDBContext);
                }
                return placeRepository;
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
