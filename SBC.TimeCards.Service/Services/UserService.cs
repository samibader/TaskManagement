using AutoMapper;
using Microsoft.AspNet.Identity;
using SBC.TimeCards.Common;
using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using SBC.TimeCards.Data.Repositories;
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
        private readonly IRoleRepository _roleRepository;

        public UserService(IUnitOfWork unitOfWork, ApplicationUserManager userManager, IRoleRepository roleRepository)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager; }
        }

        public async Task<IdentityResult> Create(CreateUserViewModel viewModel)
        {
            //better to use mapper..
            var user = new User { UserName = viewModel.Username, Name = viewModel.Name, Email = viewModel.Email, CreateDate = GlobalSettings.CURRENT_DATETIME };
            var result = await _userManager.CreateAsync(user, viewModel.Password);

            // add user Role
            if (result.Succeeded)
            {

                var role = await _roleRepository.GetByIdAsync(viewModel.RoleId);
                _unitOfWork.UserRoles.Add(new UserRole { Role = role, User = user });
                _unitOfWork.SaveChanges();
            }
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
            //better to use mapper ..
            user.UserName = viewModel.Username;
            user.Name = viewModel.Name;
            user.Email = viewModel.Email;
           
            //update role 
            var userRole = _unitOfWork.UserRoles.GetBy(x => x.UserId == viewModel.Id).FirstOrDefault();
            //there is an issue updating current userRole as RoleId is a part of primary key => 
            //the old one should be deleted and a new one should be inserted. 
            if (userRole.RoleId != viewModel.RoleId)
            {
                _unitOfWork.UserRoles.Remove(userRole);
                var newUserRole = new UserRole { UserId = viewModel.Id, RoleId = viewModel.RoleId };
                _unitOfWork.UserRoles.Add(newUserRole);
            }
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public List<UserViewModel> GetAll()
        {
            return Mapper.Map<List<User>, List<UserViewModel>>(_unitOfWork.Users.GetAll().ToList());
        }

        public List<SelectListItem> GetAllAsSelectList(int selectedId = 0)
        {
            var users = _unitOfWork.Users.GetAll();
            var list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Selected = selectedId!=0,
                    Text = "",
                    Value = ""
                }
            };

            list.AddRange(users.Select(x => new SelectListItem()
            {
                Selected = x.Id == selectedId,
                Text = x.UserName+" ("+x.UserRoles.FirstOrDefault().Role.Name+")",
                Value = x.Id.ToString()
            }).ToList());
            return list;
        }

        public bool IsAdmin (int userId)
        {
            return _unitOfWork.UserRoles.GetBy(x => x.UserId == userId).Any(x => x.Role.Name == AppRoles.Administrator);
        }
        public async Task<UserViewModel> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            var userViewModel = Mapper.Map<User, UserViewModel>(user);
            var userRole = await _unitOfWork.UserRoles.GetSingleByAsync(x => x.UserId == id);
            return userViewModel;
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
        public async Task<List<SelectListItem>> GetAllAvailableRolesAsSelectList(int roleId = 0)
        {
            var roles = _roleRepository.GetAll()
                .Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString(), Selected = roleId == x.Id })
                .ToList();
            return await Task.FromResult(roles);

        }

        public string GetUserEmailAddress(int id)
        {
            return _unitOfWork.Users.GetById(id).Email;
        }
    }
}
