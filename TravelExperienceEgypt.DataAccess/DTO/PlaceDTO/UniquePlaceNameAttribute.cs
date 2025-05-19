using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.UnitOfWork;

namespace TravelExperienceEgypt.DataAccess.DTO.PlaceDTO
{
    internal class UniquePlaceNameAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IUnitOfWork _unitOfWork = (IUnitOfWork)validationContext.GetService(typeof(IUnitOfWork));
            string title = value.ToString();
            Place existingGovermantate = _unitOfWork.Place.GetItemAsync(g => g.Name == title).GetAwaiter().GetResult();
            if (existingGovermantate != null)
            {
                return new ValidationResult("A place with this name already exists");
            }
            return ValidationResult.Success;
        }
    }
}
