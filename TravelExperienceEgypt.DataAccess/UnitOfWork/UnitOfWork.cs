using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.Data;

namespace TravelExperienceEgypt.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext applicationDBContext;
        public UnitOfWork(ApplicationDBContext applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
        public async void Dispose()
        {
            await applicationDBContext.DisposeAsync();
        }
        public async Task Save()
        {
            await applicationDBContext.SaveChangesAsync();
        }
    }
}
