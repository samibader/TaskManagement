using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBC.TimeCards.Data.Repositories
{
    public interface ITicketRepository : IRepository<Ticket>
    {
    }
    internal class TicketRepository: RepositoryBase<Ticket>,ITicketRepository
    {
        public TicketRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
