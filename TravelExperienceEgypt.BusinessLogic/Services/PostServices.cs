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
        IIMageURLRepo ImageURLRepo;
        IGovermantateRepo GovermantateRepo;

        public PostServices(IPostRepo postRepo)
        {
            this.postRepo = postRepo;
        }
        public GetFilterOptionsDTO GetFilterOptions()
        {
            GetFilterOptionsDTO getFilterDto = new GetFilterOptionsDTO();

            getFilterDto.Govermantates = GovermantateRepo.GetAllWithFilter(a => a.IsDeleted != true)
                .Select(g => new FilterGovermantateDTO { Id = g.ID, Name = g.Name }).ToList();

            getFilterDto.Categories = CategoryRepo.GetAllWithFilter(a => !a.IsDeleted)
            .Select(g => new FilterCategoryDTO { Id = g.ID, Name = g.Name }).ToList();

            return getFilterDto;
        } 
        //public async List<FilterResposeDTO> FilterRequest(FilterRequestDTO requestDTO)
        //{
        //    List<FilterResposeDTO> result=new();

        //    if (requestDTO.Price != null) {

        //        var Url = (await ImageURLRepo.GetItemAsync(I => I.PostId == 4)).Url;

        //        result = postRepo.GetAllWithFilter(c => c.Price <= requestDTO.Price)
        //            .Select(p => new FilterResposeDTO
        //            {
        //                ImageUrl = (await ImageURLRepo.GetItemAsync(I => I.PostId == p.ID)).Url

        //                // Rate, Price, PlaceName, GovermantateName, Description, DatePosted, IsInWishList
        //            });
        //    }
        //    return result;
        //}
    }
}
