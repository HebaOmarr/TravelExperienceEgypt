using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.API.Results;
using TravelExperienceEgypt.DataAccess;
using TravelExperienceEgypt.DataAccess.DTO.Posts;
using TravelExperienceEgypt.DataAccess.Models;
using TravelExperienceEgypt.DataAccess.Repository.Contract;
using TravelExperienceEgypt.DataAccess.UnitOfWork;


namespace TravelExperienceEgypt.BusinessLogic.Services
{
    public class PostServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment _env;
        public PostServices(IUnitOfWork unitOfWork,IWebHostEnvironment _env)
        {
            this.unitOfWork = unitOfWork;
            this._env = _env;
            Debug.Assert(_env != null);
        }
        public GetFilterOptionsDTO GetFilterOptions()
        {
            GetFilterOptionsDTO getFilterDto = new GetFilterOptionsDTO();

            getFilterDto.Govermantates = unitOfWork.Govermantate.GetAllWithFilter(a => a.IsDeleted != true)
                ?.Select(g => new FilterGovermantateDTO { Id = g.ID, Name = g.Name ?? string.Empty })
                .ToList() ?? new List<FilterGovermantateDTO>();

            getFilterDto.Categories = unitOfWork.Category.GetAllWithFilter(a => !a.IsDeleted)
            .Select(g => new FilterCategoryDTO { Id = g.ID, Name = g.Name }).ToList();

            return getFilterDto;
        }
      
        //public async Task<List<FilterResposeDTO>> FilterRequestAsync(FilterRequestDTO requestDTO)
        //{
        //    var result = new List<FilterResposeDTO>();
        //    IQueryable<Post> query;

        //    var placeIdsQuery = unitOfWork.Place.GetAllWithFilter(p => !p.IsDeleted &&
        //        (requestDTO.GovermantateId == 0 || p.GovermantateId == requestDTO.GovermantateId) &&
        //        (requestDTO.CategoryId == 0 || p.categoryId == requestDTO.CategoryId))
        //                                 .Select(p => p.ID);

            
        //    var filteredPosts = unitOfWork.Post.GetAllWithFilter(c =>
        //(requestDTO.Price == 0 || c.Price <= requestDTO.Price) &&
        //(requestDTO.Rate == 0 || c.Rate <= requestDTO.Rate) &&
        //placeIdsQuery.Contains(c.PlaceId)).ToList();

        //    var dtoTasks = filteredPosts.Select(async p =>
        //    {

        //        var imageUrl = await unitOfWork.ImageURL.GetItemAsync(i => i.PostId == p.ID);
        //        var Place = await unitOfWork.Place.GetItemAsync(i => i.ID == p.PlaceId);
        //        var govermantate = await unitOfWork.Govermantate.GetItemAsync(i => i.ID == Place.GovermantateId);
        //        var wishlist = await unitOfWork.WishList.GetItemAsync(i => i.PostId == p.ID);

        //        return new FilterResposeDTO
        //        {
        //            ImageUrl = imageUrl?.Url??"Not Found",
        //            Price = p.Price,
        //            PlaceName = Place?.Name??"Not Valid",
        //            GovermantateName = govermantate?.Name??"Not Valid",
        //            Description = p.Description,
        //            DatePosted = p.DatePosted,
        //            Rate = p.Rate,
        //            IsInWishList = (wishlist != null),
        //        };
        //    });

        //    result = (await Task.WhenAll(dtoTasks)).ToList();

        //    return result;
        //}


        public List<FilterResposeDTO> Filter(string PostName)
        {
            var Places = unitOfWork.Place.GetAllWithFilter(p => !p.IsDeleted).Select(p => p.ID);

            var posts = unitOfWork.Post.GetAllWithFilter(p => p.Name.Contains(PostName) && Places.Contains(p.PlaceId) && !p.IsDeleted);

            var result = unitOfWork.Post.GetAllWithFilter(p => p.Name.Contains(PostName) && !p.IsDeleted)
    .Join(
        unitOfWork.Place.GetAllWithFilter(pl => !pl.IsDeleted),
        post => post.PlaceId,
        place => place.ID,
        (post, place) => new FilterResposeDTO
        {
            Id = post.ID,
            ImageUrl = unitOfWork.ImageURL.GetAllWithFilter(i => i.PostId == post.ID).Select(i => i.Url).FirstOrDefault() ?? "not Valid",
            Rate = post.Rate,
            Price = post.Price,
            PlaceName = place.Name,
            Description = post.Description,
            DatePosted = post.DatePosted,
        }
    ).AsNoTracking().ToList();

            return result;
        }

        public async Task<Result<string>> AddPost(AddPostDTO dto, IFormFile imageFile,int UserId)
        {
            try
            {
                var place = unitOfWork.Place
                   .GetAllWithFilter(p => p.Name.Contains(dto.PlaceName) && !p.IsDeleted).Select(p => p.ID).FirstOrDefault();

                if (place == 0)
                {
                    return Result<string>.Failure("This Place not Exist");
                }

                var post = new Post
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Price = dto.Price,
                    Rate = dto.Rate,
                    DatePosted = dto.DatePosted,
                    PlaceId = place,
                    UserId = UserId
                };


                //var uploads = Path.Combine(_env.WebRootPath, "uploads");
                var uploads = Path.Combine( _env.ContentRootPath, "wwwroot", "uploads");

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                var imageUrl = new ImageUrl
                {
                    Url = $"/uploads/{fileName}",
                    PostId = post.ID
                };

                await unitOfWork.Post.AddAsync(post);
                await unitOfWork.Save();

                var PostID = unitOfWork.Post
                   .GetAllWithFilter(p => p.Name.Contains(post.Name) && !p.IsDeleted&& p.UserId==UserId).Select(p => p.ID).FirstOrDefault();
                
                imageUrl.PostId = PostID;
                await unitOfWork.ImageURL.AddAsync(imageUrl);
                await unitOfWork.Save();
                return Result<string>.Success("Posted Added");
            }
            catch
            {
                return Result<string>.Failure("This Place not Exist");
            }
        }


        public async Task<Result<string>> UpdatePostAsync(int PostId,EditPostDTO dto,int UserId)
        {
            var post = unitOfWork.Post.GetAllWithFilter(p => p.ID == PostId).FirstOrDefault();

            if (post == null) return Result<string>.Failure("Post Not Found");

            if (post.UserId != UserId)
                return Result<string>.Failure("Unauthorize to edit This Post");


            if (!string.IsNullOrEmpty(dto.Name)) post.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.Description)) post.Description = dto.Description;
            if (dto.Price.HasValue) post.Price = dto.Price.Value;
            if (dto.Rate.HasValue) post.Rate = dto.Rate.Value;
            if (!string.IsNullOrEmpty(dto.PlaceName))
            {
                var placeId = unitOfWork.Place
                   .GetAllWithFilter(p => p.Name.Contains(dto.PlaceName) && !p.IsDeleted).Select(p => p.ID).FirstOrDefault();

                if (placeId != 0)
                post.PlaceId = placeId;
            }

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                var uploads = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads");

                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.ImageFile.FileName)}";
                var path = Path.Combine(uploads, fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await dto.ImageFile.CopyToAsync(stream);

                // احذف الصورة القديمة إن أردت:
                var imageUrl = unitOfWork.ImageURL.GetAllWithFilter(i => i.PostId == PostId).FirstOrDefault();
                if (imageUrl != null)
                {
                    var oldPath = Path.Combine(_env.ContentRootPath, "wwwroot", imageUrl.Url.TrimStart('/'));
                    if (File.Exists(oldPath)) File.Delete(oldPath);

                    imageUrl.Url = $"/uploads/{fileName}";
                }
            }

            await unitOfWork.Save();
            return Result<string>.Success("Post Edited");
        }


        public async Task<Result<string>> DeletePostAsync(int postId, int currentUserId)
        {
            try
            {
                var IsDeleted = await unitOfWork.Post.Delete(p => p.ID == postId);

                if (IsDeleted && await unitOfWork.ImageURL.Delete(p => p.PostId == postId))
                {
                    await unitOfWork.Save();
                    return Result<string>.Success("Deleted Successfuly");
                }
                return Result<string>.Failure("Post Not Found");
            }
            catch
            {
                return Result<string>.Failure("Something error happen please try later");
            }
        }

    }
}
