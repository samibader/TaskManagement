using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBC.TimeCards.Data.Repositories
{
    public interface IUserTemplateRepository : IRepository<UserTemplate>
    {
    }
    internal class UserTemplateRepository: RepositoryBase<UserTemplate>,IUserTemplateRepository
    {
        public UserTemplateRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
