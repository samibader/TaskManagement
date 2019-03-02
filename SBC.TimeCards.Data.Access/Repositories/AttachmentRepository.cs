using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Data.Repositories
{
    public interface IAttachmentRepository : IRepository<Attachment>
    {
    }
    internal class AttachmentRepository: RepositoryBase<Attachment>,IAttachmentRepository
    {
        public AttachmentRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
