using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess;
using TravelExperienceEgypt.DataAccess.DTO.Posts;
using TravelExperienceEgypt.DataAccess.Models;
using TravelExperienceEgypt.DataAccess.Repository.Contract;


namespace TravelExperienceEgypt.BusinessLogic.Services
{
    public class PostServices
    {
        IPostRepo postRepo;
        ICategoryRepo CategoryRepo;
        IPlaceRepo PlaceRepo;
        IIMageURLRepo ImageURLRepo;
        IGovermantateRepo GovermantateRepo;
        IWishlistRepo WishlistRepo;

        public PostServices(IPostRepo postRepo,IGovermantateRepo govermantateRepo,ICategoryRepo categoryRepo,
                            IIMageURLRepo imageURLRepo, IPlaceRepo placeRepo,IWishlistRepo wishlistRepo)
        {
            this.postRepo = postRepo;
            this.GovermantateRepo = govermantateRepo;
            this.CategoryRepo = categoryRepo;
            this.ImageURLRepo = imageURLRepo;
            this.PlaceRepo = placeRepo;
            this.WishlistRepo = wishlistRepo;
        }
        public GetFilterOptionsDTO GetFilterOptions()
        {
            GetFilterOptionsDTO getFilterDto = new GetFilterOptionsDTO();

            getFilterDto.Govermantates = GovermantateRepo.GetAllWithFilter(a => a.IsDeleted != true)
                ?.Select(g => new FilterGovermantateDTO { Id = g.ID, Name = g.Name?? string.Empty })
                .ToList()??new List<FilterGovermantateDTO>();

            getFilterDto.Categories = CategoryRepo.GetAllWithFilter(a => !a.IsDeleted)
            .Select(g => new FilterCategoryDTO { Id = g.ID, Name = g.Name }).ToList();

            return getFilterDto;
        }
        public async Task<List<FilterResposeDTO>> FilterRequestAsync(FilterRequestDTO requestDTO)
        {
            var result = new List<FilterResposeDTO>();

            var placeIdsQuery = PlaceRepo.GetAllWithFilter(p => !p.IsDeleted &&
                (requestDTO.GovermantateId == null || p.GovermantateId == requestDTO.GovermantateId) &&
                (requestDTO.CategoryId == null || p.categoryId == requestDTO.CategoryId))
                                         .Select(p => p.ID);

            var filteredPosts = postRepo.GetAllWithFilter(c =>
                c.Price <= requestDTO.Price &&
                c.Rate <= requestDTO.Rate &&
                placeIdsQuery.Contains(c.PlaceId)).ToList();

            var dtoTasks = filteredPosts.Select(async p =>
            {

                var imageUrl = await ImageURLRepo.GetItemAsync(i => i.PostId == p.ID);
                var Place = await PlaceRepo.GetItemAsync(i => i.ID == p.PlaceId);
                var govermantate = await GovermantateRepo.GetItemAsync(i => i.ID == Place.GovermantateId);
                var wishlist = await WishlistRepo.GetItemAsync(i => i.PostId == p.ID);

                return new FilterResposeDTO
                {
                    ImageUrl = imageUrl.Url,
                    Price = p.Price,
                    PlaceName = Place.Name,
                    GovermantateName = govermantate.Name,
                    Description = p.Description,
                    DatePosted = p.DatePosted,
                    Rate = p.Rate,
                    IsInWishList = (wishlist != null),
                };
            });

            result = (await Task.WhenAll(dtoTasks)).ToList();

            return result;
        }
  
    
        public async Task<> AppearMap()
        {

        }
    
    }
}
