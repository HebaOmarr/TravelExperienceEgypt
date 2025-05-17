using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.DTO.GovernorateDTO;
using TravelExperienceEgypt.DataAccess.Models;

namespace TravelExperienceEgypt.DataAccess.Repository.Contract
{
    public interface IGovernorateRepository:IGenericRepository<Govermantate>
    {
       //  Task<IEnumerable<GetAllGovermantateDto>> GetAllGovermantateRequest();
    }
}
