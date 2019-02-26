using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SBC.TimeCards.Data.EF;
using SBC.TimeCards.Data.Models;
using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Service.Models.Users;
using SBC.TimeCards.Common;
using SBC.TimeCards.Service.Identity;
using Microsoft.AspNet.Identity;
using SBC.TimeCards.Service.Interfaces;
using SBC.TimeCards.Service.Services;
using AutoMapper;
using DataTables.AspNet.Core;
using System.ComponentModel;
using DataTables.AspNet.Mvc5;

namespace SBC.TimeCards.Controllers
{
    [Authorize(Roles =AppRoles.Administrator)]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET: Users
        public ActionResult Index()
        {
            return View(_userService.GetAll());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _userService.GetByIdAsync(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.RoleId = await _userService.GetAllAvailableRolesAsSelectList();
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Create(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            ViewBag.RoleId = await _userService.GetAllAvailableRolesAsSelectList();

            return View(model);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EditUserViewModel user = Mapper.Map<UserViewModel, EditUserViewModel>(await _userService.GetByIdAsync(id.Value));
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = await _userService.GetAllAvailableRolesAsSelectList(user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                await _userService.Edit(user);
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = await _userService.GetAllAvailableRolesAsSelectList(user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserViewModel user = await _userService.GetByIdAsync(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _userService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            var data = _userService.GetAll();
            var filteredData = data.Where(_item => _item.Name.ToLower().Contains(request.Search.Value.ToLower())).AsEnumerable();
            var sortColumn = request.Columns.FirstOrDefault(s => s.Sort != null);
            if (sortColumn != null)
            {
                PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(UserViewModel)).Find(sortColumn.Field, true);
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


        #region Private Methods
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        #endregion


        #region Validation API
        [HttpPost]
        public async Task<JsonResult> CheckUniqueEmail(string Email, int? Id)
        {
            try
            {
                var isEmailUnique = true;

                if (Email != null)
                {
                    isEmailUnique = await _userService.IsEmailUnique(Email,Id);
                }

                if (isEmailUnique)
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

        [HttpPost]
        public async Task<JsonResult> CheckUniqueUsername(string Username, int? Id)
        {
            try
            {
                var isUsernameUnique = true;

                if (Username != null)
                {
                    isUsernameUnique = await _userService.IsUsernameUnique(Username,Id);
                }

                if (isUsernameUnique)
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
