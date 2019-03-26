using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBC.TimeCards.Data.Repositories
{
    public interface ITemplateTypeRepository : IRepository<TemplateType>
    {
    }
    internal class TemplateTypeRepository: RepositoryBase<TemplateType>,ITemplateTypeRepository
    {
        public TemplateTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
