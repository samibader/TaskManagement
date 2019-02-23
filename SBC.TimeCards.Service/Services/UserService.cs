using AutoMapper;
using Microsoft.AspNet.Identity;
using SBC.TimeCards.Common;
using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using SBC.TimeCards.Service.Identity;
using SBC.TimeCards.Service.Interfaces;
using SBC.TimeCards.Service.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SBC.TimeCards.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationUserManager _userManager;

        public UserService(IUnitOfWork unitOfWork, ApplicationUserManager userManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager; }
        }

        public async Task<IdentityResult> Create(CreateUserViewModel viewModel)
        {
            var user = new User { UserName = viewModel.Username, Email = viewModel.Email, CreateDate = GlobalSettings.CURRENT_DATETIME };
            var result = await _userManager.CreateAsync(user, viewModel.Password);
            return result;
        }

        public async Task Delete(int id)
        {
            User user = await _unitOfWork.Users.GetByIdAsync(id);
            _unitOfWork.Users.Remove(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Edit(EditUserViewModel viewModel)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(viewModel.Id);
            user.UserName = viewModel.Username;
            user.Email = viewModel.Email;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public List<UserViewModel> GetAll()
        {
            return Mapper.Map<List<User>, List<UserViewModel>>(_unitOfWork.Users.GetAll().ToList());
        }

        public List<SelectListItem> GetAllAsSelectList()
        {
            var users = _unitOfWork.Users.GetAll();
            return users.Select(x => new SelectListItem()
            {
                Selected = false,
                Text = x.UserName,
                Value = x.UserName
            }).ToList();
        }

        public async Task<UserViewModel> GetByIdAsync(int id)
        {
            return Mapper.Map<User, UserViewModel>(await _unitOfWork.Users.GetByIdAsync(id));
        }

        public async Task<DetailedUserViewModel> GetDetailedByIdAsync(int id)
        {
            return Mapper.Map<User, DetailedUserViewModel>(await _unitOfWork.Users.GetByIdAsync(id));
        }

        public async Task<bool> IsEmailUnique(string email, int? id)
        {
            if (id.HasValue)
            {
                var user = await _unitOfWork.Users.FindByEmailAsync(email);
                if (user != null && user.Id != id.Value)
                    return false;
                return true;
            }
            return await _unitOfWork.Users.FindByEmailAsync(email) == null;
        }

        public async Task<bool> IsUsernameUnique(string username, int? id)
        {
            if (id.HasValue)
            {
                var user = await _unitOfWork.Users.FindByNameAsync(username);
                if (user != null && user.Id != id.Value)
                    return false;
                return true;
            }
            return await _unitOfWork.Users.FindByNameAsync(username) == null;
        }
    }
}
