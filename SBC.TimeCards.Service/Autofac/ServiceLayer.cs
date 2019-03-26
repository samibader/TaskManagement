using Autofac;
using Microsoft.Owin.Security.DataProtection;
using SBC.TimeCards.Data.Identity;
using SBC.TimeCards.Service.Identity;
using SBC.TimeCards.Service.Interfaces;
using SBC.TimeCards.Service.Services;

namespace SBC.TimeCards.Service.Autofac
{
    public class ServiceLayer : Module
    {
        private readonly string _absoluteUploadPath;
        public ServiceLayer(string absoluteUploadPath)
        {
            _absoluteUploadPath = absoluteUploadPath;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => ApplicationUserManager.Create(c.Resolve<IUserStore>(), c.Resolve<IDataProtectionProvider>())).AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<UserService>().AsSelf().InstancePerRequest();
            builder.RegisterType<ProjectService>().AsSelf().InstancePerRequest();
            builder.RegisterType<AttachmentsService>().AsSelf().InstancePerRequest().WithParameter("uploadPath",_absoluteUploadPath);
            builder.RegisterType<TicketsService>().AsSelf().InstancePerRequest();
            builder.RegisterType<SearchService>().AsSelf().InstancePerRequest();
            builder.RegisterType<TemplatesService>().AsSelf().InstancePerRequest();
        }
    }
}
