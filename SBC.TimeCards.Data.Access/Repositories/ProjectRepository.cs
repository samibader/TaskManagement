using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;

namespace SBC.TimeCards.Data.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        //IQueryable<Project> GetAllProjectsAsyncBySami();
    }

    internal class ProjectRepository : RepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        //public IQueryable<Project> GetAllProjectsAsyncBySami()
        //{
        //    return All();
        //}
        
    }
}
