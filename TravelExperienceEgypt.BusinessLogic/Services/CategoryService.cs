using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.BusinessLogic.Services.Contract;
using TravelExperienceEgypt.DataAccess.DTO.Category;
using TravelExperienceEgypt.DataAccess.Models;
using TravelExperienceEgypt.DataAccess.UnitOfWork;

namespace TravelExperienceEgypt.BusinessLogic.Services
{
    public class CategoryService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
        }

        public  async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            IEnumerable<Category> categories = await _unitOfWork.Category.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);

        }

        public async Task<CategoryDTO> GetByIDAsync(int id)
        {
            Category category = await _unitOfWork.Category.GetItemAsync(c => c.ID == id);

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task CreateAsync(CategoryDTO CategoryDto)
        {
            Category Category = _mapper.Map<Category>(CategoryDto);
            await _unitOfWork.Category.AddAsync(Category);
            await _unitOfWork.Save();

        }
        public async Task UpdateAsync(CategoryDTO categoryDto)
        {
            Category category = _mapper.Map<Category>(categoryDto);
            await _unitOfWork.Category.UpdateAsync(x => x.ID == categoryDto.ID, category);
            await _unitOfWork.Save();
        }

        public async Task DeleteAsync(int id)
        {
            
            await _unitOfWork.Category.Delete(c => c.ID == id);
            await _unitOfWork.Save();

        }


    }
}
