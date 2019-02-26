﻿using SBC.TimeCards.Data.Repositories;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SBC.TimeCards.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        IProjectRepository Projects { get; }
        IUserRepository Users { get; }
        IUserRoleRepository UserRoles { get; }
        IRoleRepository Roles { get; }
        void SaveChanges();
        Task SaveChangesAsync();

        void Dispose();
    }

    internal class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory _dbFactory;
        private DbContext _dbContext;
        private DbContext Db => _dbContext ?? (_dbContext = _dbFactory.Init());

        public IProjectRepository Projects { get; private set; }

        public IUserRepository Users { get; private set; }
        public IRoleRepository Roles { get; private set; } 
        public IUserRoleRepository UserRoles { get; private set; }
        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
            Projects = new ProjectRepository(_dbFactory);
            Users = new UserRepository(_dbFactory);
            UserRoles = new UserRoleRepository(_dbFactory);
            Roles = new RoleRepository(_dbFactory);
        }
        
        public void SaveChanges()
        {
            Db.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbFactory.Dispose();
        }
    }
}
