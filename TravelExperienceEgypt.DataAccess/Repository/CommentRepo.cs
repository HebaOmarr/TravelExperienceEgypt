using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TravelExperienceEgypt.DataAccess.Repository
{
    internal class CommentRepo : GenericRepository<Comment>, ICommentRepo
    {
        private readonly ApplicationDBContext applicationDBContext;
        public CommentRepo(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
    }
    
}
