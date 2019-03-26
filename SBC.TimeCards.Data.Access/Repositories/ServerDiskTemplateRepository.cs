using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBC.TimeCards.Data.Repositories
{
    public interface IServerDiskTemplateRepository : IRepository<ServerDiskTemplate>
    {
    }
    internal class ServerDiskTemplateRepository: RepositoryBase<ServerDiskTemplate>,IServerDiskTemplateRepository
    {
        public ServerDiskTemplateRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
