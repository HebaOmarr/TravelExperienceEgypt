using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.Data;
using TravelExperienceEgypt.DataAccess.Models;
using TravelExperienceEgypt.DataAccess.Repository.Contract;

namespace TravelExperienceEgypt.DataAccess.Repository
{
    internal class CategoryRepo : GenericRepository<Category>, ICategoryRepo
    {
        private readonly ApplicationDBContext applicationDBContext;

        public CategoryRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
    }
}
