using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.Repository
{
    internal class NotificationRepo : GenericRepository<Notification>, INotificationRepo
    {
        private readonly ApplicationDBContext applicationDBContext;
        public NotificationRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
    }    
    
}
