using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelExperienceEgypt.DataAccess.Models;
using TravelExperienceEgypt.DataAccess.DTO.PlaceDTO;
using TravelExperienceEgypt.DataAccess.UnitOfWork;
using TravelExperienceEgypt.BusinessLogic.Services.Contract;

namespace TravelExperienceEgypt.BusinessLogic.Services
{
    public class PlaceService
    {
        private readonly UnitOfWork _unitOfWork;

        public PlaceService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Get all places
        public async Task<IEnumerable<Place>> GetAllPlacesRequest()
        {
            return await _unitOfWork.Place.GetAllAsync();
        }

        // Get place by Id
        public async Task<Place> GetPlaceByIdRequest(int id)
        {
            return await _unitOfWork.Place.GetItemAsync(p => p.ID == id);
        }

        // Get places by GovernorateId
        public async Task<IEnumerable<Place>> GetPlacesByGovernorateIdRequest(int governorateId)
        {
            return await _unitOfWork.Place.GetAllWithFilter(p => p.GovermantateId == governorateId).ToListAsync();
        }

        // Create new place
        public async Task CreatePlaceRequest(CreatePlaceDTO dto)
        {
            Place place = new Place
            {
                Name = dto.Title,
                Description = dto.OverviewDescription,
                Latitude = dto.Lat,
                Longitude = dto.Lng,
                Image = dto.ImageLink,
                categoryId = dto.CategoryId,
                GovermantateId = dto.GovernorateId
            };

            await _unitOfWork.Place.AddAsync(place);
        }

        // Update place by Id
        public async Task<bool> UpdatePlaceByIdRequest(UpdatePlaceDTO dto)
        {
            Place updatedPlace = new Place
            {
                Name = dto.Title,
                Description = dto.OverviewDescription,
                Latitude = dto.Lat,
                Longitude = dto.Lng,
                Image = dto.ImageLink,
                categoryId = dto.CategoryId,
                GovermantateId = dto.GovernorateId
            };

            return await _unitOfWork.Place.UpdateAsync(p => p.ID == dto.Id, updatedPlace);
        }

        // Delete place by Id
        public async Task<bool> DeletePlaceByIdRequest(int id)
        {
            return await _unitOfWork.Place.Delete(p => p.ID == id);
        }
    }
}
