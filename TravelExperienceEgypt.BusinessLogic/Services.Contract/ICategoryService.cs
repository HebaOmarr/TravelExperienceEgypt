using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.DTO.Category;

namespace TravelExperienceEgypt.BusinessLogic.Services.Contract
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDTO>> GetAllAsync();
        public Task<CategoryDTO> GetByIDAsync(int id);
        public Task CreateAsync(CategoryDTO CategoryDto);
        public Task UpdateAsync(CategoryDTO categoryDto);
        public Task DeleteAsync(int id);




    }
}
