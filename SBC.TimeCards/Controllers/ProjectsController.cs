using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using Microsoft.AspNet.Identity;
using SBC.TimeCards.Common;
using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using SBC.TimeCards.Service.Identity;
using SBC.TimeCards.Service.Interfaces;
using SBC.TimeCards.Service.Models.Projects;
using SBC.TimeCards.Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SBC.TimeCards.Controllers
{
    
    public class ProjectsController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;

        public ProjectsController(ProjectService projectService, UserService userService, ApplicationUserManager userManager) : base(userManager)
        {
            _projectService = projectService;
            _userService = userService;
        }

        [Authorize(Roles = AppRoles.Administrator)]
        // GET: Projects
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = AppRoles.Administrator)]

        public ActionResult PageData(IDataTablesRequest request)
        {
            var data = _projectService.GetArchivedProjects(GetCurrentUserId(),true);
            var filteredData = data.Where(_item => _item.Name.ToLower().Contains(request.Search.Value.ToLower())).AsEnumerable();
            var sortColumn = request.Columns.FirstOrDefault(s => s.Sort != null);
            if (sortColumn != null)
            {
                PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(ProjectViewModel)).Find(sortColumn.Field, true);
                if (sortColumn.Sort.Direction == SortDirection.Descending)
                {
                    filteredData = filteredData.OrderByDescending(c => prop.GetValue(c));
                }
                else
                {
                    filteredData = filteredData.OrderBy(c => prop.GetValue(c));
                }
            }

            var dataPage = filteredData.Skip(request.Start).Take(request.Length).ToList();

            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = AppRoles.Administrator)]
        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = await _projectService.GetByIdAsync(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [Authorize(Roles = AppRoles.Administrator)]
        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = AppRoles.Administrator)]
        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Int32.Parse(User.Identity.GetUserId());
                await _projectService.Create(model);
                return RedirectToAction("Index","Home");
            }
            return View(model);
        }

        [Authorize(Roles = AppRoles.Administrator)]
        // GET: Projects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var project = await _projectService.GetProjectForEditAsync(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [Authorize(Roles = AppRoles.Administrator)]
        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                await _projectService.Edit(project);
                return RedirectToAction("Index","Home");
            }

           // project.Users = _projectService.GetProjectUsersAsSelectList(project.Id);

            return View(project);
        }

        [Authorize(Roles = AppRoles.Administrator)]
        // GET: Projects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectViewModel project = await _projectService.GetByIdAsync(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [HttpPost]
        public async Task<ActionResult> Archive(int id)
        {
            await _projectService.Archive(id);
            return Json(new { success = true ,url=Url.Action("Index","Home")});
        }

        [HttpPost]
        public async Task<ActionResult> ChangeColor(int id,string color)
        {
            //if (color == "#2b78dc")
            //    throw new Exception("errorrrrrrrrrrrr!");
            await _projectService.ChangeColor(id, color);
            return Json(new { success = true, message="Project color changed" });
        }
        [Authorize(Roles = AppRoles.Administrator)]
        // POST: TimeRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _projectService.Delete(id);
            return RedirectToAction("Index","Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetValues(string searchTerm, int pageSize, int pageNum)
        {
            //Getting items
            int total;
            var userId =  GetCurrentUserId();
            var results =  _projectService.GetDropDownValues(searchTerm, pageNum, pageSize, userId, GetCurrentUserRole(), out total);
            var projects = new List<ProjectViewModel>();
            foreach (var item in results)
            {
                projects.Add(new ProjectViewModel()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            //Creating new object for return.
            var result = new
            {
                Total = total,
                Results = projects
            };

            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public async Task<ActionResult> Manage(int id)
        {
             var model = await _projectService.GetByIdAsync(id);
            return View(model);
        }

        #region Favorites
        
        public ActionResult AddToFavorite(int id)
        {
            _projectService.AddProjectToFavorite(GetCurrentUserId(), id);
            return Json(new { success = true });
        }
        public ActionResult RemoveFromFavorite(int id)
        {
            _projectService.RemoveProjectFromFavorite(GetCurrentUserId(), id);
            return Json(new { success = true });
        }
        
        public ActionResult GetFavoriteProjects()
        {
            var projects = _projectService.GetUserFavoriteProjects(GetCurrentUserId());
            return Json(new { data = projects },JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult SidebarFavorites()
        {
            var model = _projectService.GetUserFavoriteProjects(GetCurrentUserId());
            return PartialView("_SidebarFavorites",model);
        }
        #endregion
        [Authorize(Roles = AppRoles.Administrator)]
        #region Validation API
        [HttpPost]
        public async Task<JsonResult> CheckUniqueProjectName(string Name, int? Id)
        {
            try
            {
                var isNameUnique = true;

                if (Name != null)
                {
                    isNameUnique = await _projectService.IsProjectNameUnique(Name, Id);
                }

                if (isNameUnique)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
