using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBC.TimeCards.Data.Repositories
{
    public interface INetworkTemplateRepository : IRepository<NetworkTemplate>
    {
    }
    internal class NetworkTemplateRepository: RepositoryBase<NetworkTemplate>,INetworkTemplateRepository
    {
        public NetworkTemplateRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
