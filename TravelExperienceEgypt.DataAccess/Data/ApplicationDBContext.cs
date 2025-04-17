using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
