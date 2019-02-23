using Autofac;
using SBC.TimeCards.Data.Identity;
using SBC.TimeCards.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Data.Access.Autofac
{
    public class DataLayer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<UserStore>().As<IUserStore>().InstancePerRequest();
            builder.RegisterType<RoleStore>().As<IRoleStore>().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(DataLayer).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
        }
    }
}
