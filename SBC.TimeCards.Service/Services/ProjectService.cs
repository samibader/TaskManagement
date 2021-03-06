﻿using AutoMapper;
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
        private readonly IUserService _userService;

        public ProjectService(IUnitOfWork unitOfWork,UserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task Create(CreateProjectViewModel viewModel)
        {
            var project = new Project { Name = viewModel.Name, Timestamp = GlobalSettings.CURRENT_DATETIME, UserId = viewModel.UserId, Description = viewModel.Description, IsArchived = false, Color = "#90969b" };
            _unitOfWork.Projects.Add(project);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task Edit(EditProjectViewModel viewModel)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(viewModel.Id);
            if (project.IsArchived)
            {
                throw new InvalidOperationException("Can't Change Archived project");
            }
            project.Name = viewModel.Name;
            project.Description = viewModel.Description;

            _unitOfWork.Projects.Update(project);
            await _unitOfWork.SaveChangesAsync();
        }

        public List<ProjectViewModel> GetAll()
        {
            return Mapper.Map<List<Project>, List<ProjectViewModel>>(_unitOfWork.Projects.GetAll().ToList());
        }
        public List<ProjectViewModel> GetAllActiveProjects(int userId)
        {
            var isAdmin = _userService.IsAdmin(userId);
            return Mapper.Map<List<Project>, List<ProjectViewModel>>(_unitOfWork.Projects.GetBy(
                x => !x.IsArchived && (isAdmin || x.UserId == userId
                    || x.Tickets.Any(y => y.AssigneeId == userId && y.StateId != (int)TicketStates.Done) || x.Tickets.SelectMany(y=>y.SubTickets).Any(y => y.AssigneeId == userId && y.StateId != (int)TicketStates.Done)))
                .ToList());
        }
        public List<ProjectViewModel> GetArchivedProjects(int userId, bool getAll = false)
        {
            var isAdmin = _userService.IsAdmin(userId);
            var projects = _unitOfWork.Projects.GetBy(x =>
            (x.IsArchived && (isAdmin ||x.UserId == userId || x.Tickets.Any(y => y.AssigneeId == userId)))
            || (x.Tickets.Any(y => y.AssigneeId == userId) && !x.Tickets.Any(y => y.AssigneeId == userId && !(y.StateId == (int)TicketStates.Done)) && !x.Tickets.SelectMany(y=>y.SubTickets).Any(y => y.AssigneeId == userId && !(y.StateId == (int)TicketStates.Done))));
            if (!getAll)
            {
                projects = projects.OrderByDescending(x => x.ArchiveDate).Take(GlobalSettings.ArchivedProjectSize);
            }
            return Mapper.Map<List<Project>, List<ProjectViewModel>>(projects.ToList());
        }

        public async Task<ProjectViewModel> GetByIdAsync(int id)
        {
            return Mapper.Map<Project, ProjectViewModel>(await _unitOfWork.Projects.GetByIdAsync(id));
        }

        public async Task<EditProjectViewModel> GetProjectForEditAsync(int id)
        {
            return Mapper.Map<Project, EditProjectViewModel>(await _unitOfWork.Projects.GetByIdAsync(id));
        }

        public async Task<List<ProjectViewModel>> GetByUserId(int id)
        {
            var userProjects = (await _unitOfWork.Users.GetByIdAsync(id)).Projects;
            return Mapper.Map<List<Project>, List<ProjectViewModel>>(userProjects.ToList());
        }

        public List<SelectListItem> GetProjectUsersAsSelectList(int id)
        {
            var projectusers = _unitOfWork.Projects.GetById(id); //;.Users.Select(x=>x.Id);
            var users = _unitOfWork.Users.GetAll();
            return users.Select(x => new SelectListItem()
            {
                // Selected = projectusers.Contains(x.Id),
                Text = x.UserName,
                Value = x.UserName
            }).ToList();
        }

        public async Task Archive(int id)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            project.IsArchived = true;
            project.ArchiveDate = GlobalSettings.CURRENT_DATETIME;
            _unitOfWork.Projects.Update(project);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UnArchive(int id)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            project.IsArchived = false;
            project.ArchiveDate = null;
            _unitOfWork.Projects.Update(project);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task ChangeColor(int id, string color)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            project.Color = color;
            _unitOfWork.Projects.Update(project);
            await _unitOfWork.SaveChangesAsync();
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

        #region Favorite Management
        public void AddProjectToFavorite(int userId, int projectId)
        {
            var user = _unitOfWork.Users.GetById(userId);
            var project = _unitOfWork.Projects.GetById(projectId);
            user.FavoriteProjects.Add(project);
            project.UserFavorites.Add(user);
            _unitOfWork.SaveChanges();
        }
        public void RemoveProjectFromFavorite(int userId, int projectId)
        {
            var user = _unitOfWork.Users.GetById(userId);
            var project = _unitOfWork.Projects.GetById(projectId);
            user.FavoriteProjects.Remove(project);
            project.UserFavorites.Remove(user);
            _unitOfWork.SaveChanges();
        }
        public List<ProjectViewModel> GetUserFavoriteProjects(int userId)
        {
            return Mapper.Map<List<Project>, List<ProjectViewModel>>(_unitOfWork.Users.GetById(userId).FavoriteProjects.ToList());
        }
        #endregion
    }
}
