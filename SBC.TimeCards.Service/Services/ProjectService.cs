using AutoMapper;
using Microsoft.AspNet.Identity;
using SBC.TimeCards.Common;
using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using SBC.TimeCards.Service.Identity;
using SBC.TimeCards.Service.Interfaces;
using SBC.TimeCards.Service.Models.Projects;
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
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(CreateProjectViewModel viewModel)
        {
            var project = new Project { Name = viewModel.Name, Timestamp = GlobalSettings.CURRENT_DATETIME };
            _unitOfWork.Projects.Add(project);
            foreach (var userItem in viewModel.SelectedUsers)
            {
                var user = await _unitOfWork.Users.FindByNameAsync(userItem);
                project.Users.Add(user);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Edit(EditProjectViewModel viewModel)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(viewModel.Id);
            project.Name = viewModel.Name;
            project.Users.Clear();

            foreach (var userItem in viewModel.SelectedUsers)
            {
                var user = await _unitOfWork.Users.FindByNameAsync(userItem);
                project.Users.Add(user);
            }
            _unitOfWork.Projects.Update(project);
            await _unitOfWork.SaveChangesAsync();
        }

        public List<DetailedProjectViewModel> GetAll()
        {
            return Mapper.Map<List<Project>, List<DetailedProjectViewModel>>(_unitOfWork.Projects.GetAll().ToList());
        }

        public async Task<DetailedProjectViewModel> GetByIdAsync(int id)
        {
            return Mapper.Map<Project, DetailedProjectViewModel>(await _unitOfWork.Projects.GetByIdAsync(id));
        }

        public async Task<List<ProjectViewModel>> GetByUserId(int id)
        {
            var userProjects = (await _unitOfWork.Users.GetByIdAsync(id)).Projects;
            return Mapper.Map<List<Project>, List<ProjectViewModel>>(userProjects.ToList());
        }

        public List<SelectListItem> GetProjectUsersAsSelectList(int id)
        {
            var projectusers = _unitOfWork.Projects.GetById(id).Users.Select(x=>x.Id);
            var users = _unitOfWork.Users.GetAll();
            return users.Select(x => new SelectListItem()
            {
                Selected = projectusers.Contains(x.Id),
                Text = x.UserName,
                Value = x.UserName
            }).ToList();
        }

        public IEnumerable<ProjectViewModel> GetDropDownValues(string searchTerm, int pageNum, int pageSize, int userId, string role, out int total)
        {
            var itemList = new List<Project>();
            if (role == AppRoles.Administrator)
                itemList = _unitOfWork.Projects.GetAll().Where(p => string.IsNullOrEmpty(searchTerm) || p.Name.Contains(searchTerm)).ToList();
            else
            {
                itemList = _unitOfWork.Users.GetById(userId).Projects.Where(p => string.IsNullOrEmpty(searchTerm) || p.Name.Contains(searchTerm)).ToList();
            }
                
            var results = itemList.Skip((pageNum * pageSize) - 100).Take(pageSize);
            total = itemList.Count;
            return Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(results);

        }

        public async Task<bool> IsProjectNameUnique(string projectName, int? id)
        {
            if (id.HasValue)
            {
                var project = _unitOfWork.Projects.GetSingleBy(c => c.Name.ToLower() == projectName.ToLower());
                if (project != null && project.Id != id.Value)
                    return false;
                return true;
            }
            return await _unitOfWork.Projects.GetSingleByAsync(c => c.Name.ToLower() == projectName.ToLower()) == null;
        }

        public async Task Delete(int id)
        {
            Project project = await _unitOfWork.Projects.GetByIdAsync(id);
            _unitOfWork.Projects.Remove(project);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
