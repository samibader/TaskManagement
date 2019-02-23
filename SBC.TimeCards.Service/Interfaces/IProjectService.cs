﻿using Microsoft.AspNet.Identity;
using SBC.TimeCards.Service.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SBC.TimeCards.Service.Interfaces
{
    public interface IProjectService
    {
        Task Create(CreateProjectViewModel viewModel);
        Task Edit(EditProjectViewModel viewModel);
        Task Delete(int id);
        Task<DetailedProjectViewModel> GetByIdAsync(int id);
        //Task<DetailedUserViewModel> GetDetailedByIdAsync(int id);
        //Task<bool> IsEmailUnique(string email, int? Id);
        Task<bool> IsProjectNameUnique(string projectName, int? id);
        List<DetailedProjectViewModel> GetAll();
        List<SelectListItem> GetProjectUsersAsSelectList(int id);
        Task<List<ProjectViewModel>> GetByUserId(int id);
        IEnumerable<ProjectViewModel> GetDropDownValues(string searchTerm, int pageNum, int pageSize, int userId, string role, out int total);
    }
}
