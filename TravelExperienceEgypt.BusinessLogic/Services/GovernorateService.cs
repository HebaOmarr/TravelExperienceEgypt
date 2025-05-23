using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.UnitOfWork;
using TravelExperienceEgypt.DataAccess.DTO.GovernorateDTO;
using TravelExperienceEgypt.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using TravelExperienceEgypt.BusinessLogic.Services.Contract;

namespace TravelExperienceEgypt.BusinessLogic.Services
{
    public class GovernorateService 
    {
        IUnitOfWork _unitOfWork;
        public GovernorateService(IUnitOfWork unitOfWork)
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
            Govermantate govermantate = new Govermantate {
                Description=DTO.OverviewDescription,
                Image=DTO.ImageLink,
                Latitude=DTO.Lat,
                Name= DTO.Title,
                longitude= DTO.Lng
            };
           await _unitOfWork.Govermantate.AddAsync(govermantate);
            await _unitOfWork.Save();
        }
        public async Task<bool> UpdateGovermantateByIdRequest(UpdateGovernorateDto DTO, int id)
        {
            Govermantate Govermantate = new Govermantate
            {
                ID =id,
                Description = DTO.OverviewDescription,
                Image = DTO.ImageLink,
                Latitude = DTO.Lat,
                Name = DTO.Title,
                longitude = DTO.Lng
            };
           bool res= await _unitOfWork.Govermantate.UpdateAsync(g=>g.ID== id, Govermantate);
            if (res)
            {
                await _unitOfWork.Save();
                return true;
            }
            return false;

        }
        public async Task<bool> DeleteGovermantateByIdRequest(int Id )
        {

            bool res = await _unitOfWork.Govermantate.Delete(g => g.ID == Id);
            if (res)
            {
                await _unitOfWork.Save();
                return true;
            }
            return false;

        }


    }
}
