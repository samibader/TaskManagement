using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBC.TimeCards.Data.Repositories
{
    public interface IServerTemplateRepository : IRepository<ServerTemplate>
    {
    }
    internal class ServerTemplateRepository: RepositoryBase<ServerTemplate>,IServerTemplateRepository
    {
        public ServerTemplateRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
