using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBC.TimeCards.Data.Repositories
{
    public interface ITicketSateRepository : IRepository<TicketState>
    {
    }
    internal class TicketSateRepository: RepositoryBase<TicketState>,ITicketSateRepository
    {
        public TicketSateRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
