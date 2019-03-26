using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBC.TimeCards.Data.Repositories
{
    public interface ITicketTemplateRepository : IRepository<TicketTemplate>
    {
    }
    internal class TicketTemplateRepository: RepositoryBase<TicketTemplate>,ITicketTemplateRepository
    {
        public TicketTemplateRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
