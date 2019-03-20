using SBC.TimeCards.Service.Identity;
using SBC.TimeCards.Service.Interfaces;
using SBC.TimeCards.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SBC.TimeCards.Controllers
{
    public class SearchController : BaseController
    {
        private readonly ISearchService _searchService;
        public SearchController(SearchService searchService, ApplicationUserManager userManager):base(userManager)
        {
            _searchService = searchService;
        }
        // GET: Search
        public ActionResult Index(string query)
        {
            var res = _searchService.Search(query);
            return View(res);
        }
        public ActionResult FullResults(string query)
        {
            var res = _searchService.Search(query,full:true);
            return View(res);
        }
    }
}