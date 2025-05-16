using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.UnitOfWork;
using TravelExperienceEgypt.DataAccess.DTO.GovernorateDTO;
using TravelExperienceEgypt.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace TravelExperienceEgypt.BusinessLogic.Services
{
    public class GovernorateService
    {
        UnitOfWork _unitOfWork;
        public GovernorateService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //get Govermantate that have places
        public async Task<IEnumerable<Govermantate>> GetGovermantateWithPlacesRequest()
        {
            return await _unitOfWork.Govermantate.GetAllWithFilter(g => g.Places.Any()).ToListAsync();
        }
        //get by name => unique name
        public async Task<Govermantate> GetGovermantateByNameRequest(string name)
        {
            return await _unitOfWork.Govermantate.GetItemAsync(g => g.Name == name);
        }
        //crud
        public async Task<Govermantate> GetGovermantateByIdRequest(int id)
        {
            return await _unitOfWork.Govermantate.GetItemAsync(g=>g.ID==id);
        }
        public async Task<IEnumerable<Govermantate>> GetAllGovermantateRequest()
        {
            return await _unitOfWork.Govermantate.GetAllAsync();
        }
        public async Task CreateGovermantateRequest(CreateGovernorateDto DTO)
        {
            Govermantate Govermantate = new Govermantate {
                Description=DTO.OverviewDescription,
                Image=DTO.ImageLink,
                Latitude=DTO.Lat,
                Name= DTO.Title,
                longitude= DTO.Lng
            };
            _unitOfWork.Govermantate.AddAsync(Govermantate);
        }
        public async Task<bool> UpdateGovermantateByIdRequest(UpdateGovernorateDto DTO)
        {
            Govermantate Govermantate = new Govermantate
            {
                Description = DTO.OverviewDescription,
                Image = DTO.ImageLink,
                Latitude = DTO.Lat,
                Name = DTO.Title,
                longitude = DTO.Lng
            };
           return await _unitOfWork.Govermantate.UpdateAsync(g=>g.ID== DTO.GovermantateId, Govermantate);
        }
        public async Task<bool> UpdateGovermantateByIdRequest(int Id )
        {
            return await _unitOfWork.Govermantate.Delete(g => g.ID == Id);
        }
        

    }
}
