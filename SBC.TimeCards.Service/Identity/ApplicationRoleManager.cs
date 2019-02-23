using Microsoft.AspNet.Identity;
using SBC.TimeCards.Data.Identity;
using SBC.TimeCards.Data.Models;

namespace SBC.TimeCards.Service.Identity
{
    public class ApplicationRoleManager : RoleManager<Role, int>
    {
        public ApplicationRoleManager(IRoleStore store) : base(store)
        {
        }
    }
}
