using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBC.TimeCards.Data.Repositories
{
    public interface IServerNetworkTemplateRepository : IRepository<ServerNetworkTemplate>
    {
    }
    internal class ServerNetworkTemplateRepository: RepositoryBase<ServerNetworkTemplate>,IServerNetworkTemplateRepository
    {
        public ServerNetworkTemplateRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
