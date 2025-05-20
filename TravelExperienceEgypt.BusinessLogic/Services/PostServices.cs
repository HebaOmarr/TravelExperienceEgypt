using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.API.Results;
using TravelExperienceEgypt.DataAccess;
using TravelExperienceEgypt.DataAccess.DTO.Posts;
using TravelExperienceEgypt.DataAccess.Migrations;
using TravelExperienceEgypt.DataAccess.Models;
using TravelExperienceEgypt.DataAccess.Repository.Contract;
using TravelExperienceEgypt.DataAccess.UnitOfWork;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace TravelExperienceEgypt.BusinessLogic.Services
{
    public class PostServices
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment _env;
        public PostServices(RoleManager<ApplicationRole> roleManager,
          UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork,IWebHostEnvironment _env)
        {
            this.unitOfWork = unitOfWork;
            this._env = _env;
            this.roleManager = roleManager;
            this.userManager = userManager;
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
        /// <summary>
        /// I take much time to think in this what is better way to make this I see dubeg
        /// and results is not good enought
        /// </summary>
        /// 
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
        //            ImageUrl = imageUrl?.Url ?? "Not Found",
        //            Price = p.Price,
        //            PlaceName = Place?.Name ?? "Not Valid",
        //            GovermantateName = govermantate?.Name ?? "Not Valid",
        //            Description = p.Description,
        //            DatePosted = p.DatePosted,
        //            Rate = p.Rate,
        //            IsInWishList = (wishlist != null),
        //        };
        //    });

        //    result = (await Task.WhenAll(dtoTasks)).ToList();

        //    return result;
        //}



        public async Task<Result<List<PostDetailsDTO>>> Filter(string PostName="")
        {
            var posts = unitOfWork.Post
                .GetAllWithFilter(p => p.Name.Contains(PostName) && !p.IsDeleted)
                .Include(p => p.User)
                .Include(p => p.Place)
                .ThenInclude(x => x.category)
                .Select(p => new PostDetailsDTO()
                {
                    Id = p.ID,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Rate = p.Rate,
                    DatePosted = p.DatePosted,
                    UserFirstName = p.User.FirstName,
                    UserLastName = p.User.LastName,
                    PlaceName = p.Place.Name,
                    PlaceDescription = p.Place.Description,
                    CategoryName = p.Place.category.Name,
                })
                .AsNoTracking()
                .ToList();
            foreach (var item in posts)
            {
                item.ImageURLs =unitOfWork.ImageURL.GetAllWithFilter(i => i.PostId == item.Id).Select(i => i.Url).ToList();
            }
            if (posts == null || posts.Count == 0)
                return Result<List<PostDetailsDTO>>.Failure("No matching posts found.");

            return Result<List<PostDetailsDTO>>.Success(posts);
        }

        public async Task<Result<PostDetailsDTO>> GetPostDetails(int id)
        {
            var post = unitOfWork.Post.GetAllWithFilter(p => p.ID == id && !p.IsDeleted).FirstOrDefault();
            if (post == null)
            {
                return Result<PostDetailsDTO>.Failure("Post Not Exist");
            }

            var place = unitOfWork.Place.GetAllWithFilter(p=>p.ID==post.PlaceId).Select(p=>new { p.categoryId, p.Name, p.Description, p.ID, p.GovermantateId }).FirstOrDefault();
            if (place == null)
            {
                return Result<PostDetailsDTO>.Failure("place not found");
            }

            var govermantate = unitOfWork.Govermantate.GetAllWithFilter(g => g.ID == place.GovermantateId).Select(g => g.Name).FirstOrDefault();
            var category = unitOfWork.Category.GetAllWithFilter(g => g.ID == place.categoryId).Select(g => g.Name).FirstOrDefault();

            ApplicationUser? user =await userManager.FindByIdAsync(post.UserId.ToString());
            if (user == null)
            {
                return Result<PostDetailsDTO>.Failure("User not found.");
            }
            var postDetails = new PostDetailsDTO()
            {
                Name = post.Name,
                Description = post.Description,
                Price = post.Price,
                Rate = post.Rate,
                DatePosted = post.DatePosted,
                PlaceName = place.Name,
                PlaceDescription = place.Description,
                UserFirstName = user.FirstName,
                UserLastName = user.LastName,
                CategoryName = category
            };
            return Result<PostDetailsDTO>.Success(postDetails);
        }


        /// <summary>
        /// ////////////////////////Second way
        /// </summary>
        public async Task<Result<PostDetailsDTO>> PostDetails(int id)
        {
            var p = await unitOfWork.Post
                .GetAllWithFilter(x => x.ID == id && !x.IsDeleted)
                .Include(x => x.Place)
                    .ThenInclude(pl => pl.Govermantate)
                .Include(x => x.Place)
                    .ThenInclude(pl => pl.category)
               .Select(item => new
               {
                   NamePost = item.Name,
                   GovernamtateName = item.Place.Govermantate.Name,
                   categoryName = item.Place.category.Name,
                   PlaceName = item.Place.Name,
                   placeDesc = item.Place.Description,
                   item.Description,
                   item.Price,
                   item.Rate,
                   item.DatePosted,
                   item.UserId,
                   item.ID
               })
                .AsNoTracking()
                .FirstOrDefaultAsync();


            if (p == null)
                return Result<PostDetailsDTO>.Failure("Post Not Exist");

            var user = await userManager.FindByIdAsync(p.UserId.ToString());
            if (user == null)
                return Result<PostDetailsDTO>.Failure("User not found.");

            List<string> Images = unitOfWork.ImageURL.GetAllWithFilter(i => i.PostId == p.ID).Select(i => i.Url).ToList();

            var dto = new PostDetailsDTO
            {
                ImageURLs = Images,
                Name = p.NamePost,
                Description = p.Description,
                Price = p.Price,
                Rate = p.Rate,
                DatePosted = p.DatePosted,
                PlaceName = p.PlaceName,
                PlaceDescription = p.placeDesc,
                CategoryName = p.categoryName,
                UserFirstName = user.FirstName,
                UserLastName = user.LastName
            };

            return Result<PostDetailsDTO>.Success(dto);
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
