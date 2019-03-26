using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBC.TimeCards.Data.Repositories
{
    public interface IDeviceTemplateRepository : IRepository<DeviceTemplate>
    {
    }
    internal class DeviceTemplateRepository: RepositoryBase<DeviceTemplate>,IDeviceTemplateRepository
    {
        public DeviceTemplateRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
