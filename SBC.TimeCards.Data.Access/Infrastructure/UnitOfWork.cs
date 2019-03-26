using SBC.TimeCards.Data.Repositories;
using System.Data.Entity;
using System.Threading.Tasks;
using System;

namespace SBC.TimeCards.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        IProjectRepository Projects { get; }
        IUserRepository Users { get; }
        IUserRoleRepository UserRoles { get; }
        IRoleRepository Roles { get; }
        IAttachmentRepository Attachments { get; }
        ITicketRepository Tickets { get; }
        ICommentRepository Comments { get; }
        ITicketTemplateRepository TicketTemplates { get; }
        ITemplateTypeRepository TemplateTypes { get; }
        IServerTemplateRepository ServerTemplates { get; }
        INetworkTemplateRepository NetworkTemplates { get; }
        IDeviceTemplateRepository DeviceTemplates { get; }
        IUserTemplateRepository UserTemplates { get; }
        IServerDiskTemplateRepository ServerDiskTemplates { get; }
        IServerNetworkTemplateRepository ServerNetworkTemplates { get; }
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
        public IAttachmentRepository Attachments { get; private set; }
        public ITicketRepository Tickets { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public ITicketSateRepository TicketStates { get; private set; }

        public ITicketTemplateRepository TicketTemplates
        { get; private set; }

        public ITemplateTypeRepository TemplateTypes
        { get; private set; }

        public IServerTemplateRepository ServerTemplates
        { get; private set; }

        public INetworkTemplateRepository NetworkTemplates
        { get; private set; }

        public IDeviceTemplateRepository DeviceTemplates
        { get; private set; }

        public IUserTemplateRepository UserTemplates
        { get; private set; }

        public IServerDiskTemplateRepository ServerDiskTemplates
        { get; private set; }

        public IServerNetworkTemplateRepository ServerNetworkTemplates
        { get; private set; }

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
            Projects = new ProjectRepository(_dbFactory);
            Users = new UserRepository(_dbFactory);
            UserRoles = new UserRoleRepository(_dbFactory);
            Roles = new RoleRepository(_dbFactory);
            Attachments = new AttachmentRepository(_dbFactory);
            Tickets = new TicketRepository(_dbFactory);
            TicketStates = new TicketSateRepository(_dbFactory);
            Comments = new CommentRepository(_dbFactory);
            TicketTemplates = new TicketTemplateRepository(_dbFactory);
            TemplateTypes = new TemplateTypeRepository(_dbFactory);
            ServerTemplates = new ServerTemplateRepository(_dbFactory);
            DeviceTemplates = new DeviceTemplateRepository(_dbFactory);
            NetworkTemplates = new NetworkTemplateRepository(_dbFactory);
            UserTemplates = new UserTemplateRepository(_dbFactory);
            ServerNetworkTemplates = new ServerNetworkTemplateRepository(_dbFactory);
            ServerDiskTemplates = new ServerDiskTemplateRepository(_dbFactory);
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
