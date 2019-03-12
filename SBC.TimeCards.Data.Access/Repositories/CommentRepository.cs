using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBC.TimeCards.Data.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
    }
    internal class CommentRepository: RepositoryBase<Comment>,ICommentRepository
    {
        public CommentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
