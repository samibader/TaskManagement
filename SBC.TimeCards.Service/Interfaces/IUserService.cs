using Microsoft.AspNet.Identity;
using SBC.TimeCards.Service.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SBC.TimeCards.Service.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> Create(CreateUserViewModel viewModel);
        Task Edit(EditUserViewModel viewModel);
        Task Delete(int id);
        Task<UserViewModel> GetByIdAsync(int id);
        Task<DetailedUserViewModel> GetDetailedByIdAsync(int id);
        Task<bool> IsEmailUnique(string email, int? Id);
        Task<bool> IsUsernameUnique(string username, int? Id);
        List<UserViewModel> GetAll();
        List<SelectListItem> GetAllAsSelectList(int selectedId = 0);
        /// <summary>
        /// forms a select list for all available roles.
        /// </summary>
        /// <param name="roleId"> the selected role id the default value is 0 which mean no selected roles</param>
        /// <returns></returns>
        Task<List<SelectListItem>> GetAllAvailableRolesAsSelectList(int roleId = 0 );
        bool IsAdmin(int userId);
    }
}
