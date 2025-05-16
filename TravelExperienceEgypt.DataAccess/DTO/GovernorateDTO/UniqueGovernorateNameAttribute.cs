using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.UnitOfWork;

namespace TravelExperienceEgypt.DataAccess.DTO.GovernorateDTO
{
    internal class UniqueGovernorateNameAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            IUnitOfWork _unitOfWork= (IUnitOfWork)validationContext.GetService(typeof(IUnitOfWork));
            string title =value.ToString();
            Govermantate existingGovermantate= _unitOfWork.Govermantate.GetItemAsync(g=>g.Name==title).GetAwaiter().GetResult();
            if (existingGovermantate != null)
            {
                return new ValidationResult("A governorate with this name already exists");
            }
            return ValidationResult.Success;
        }
        

       
    }
}
