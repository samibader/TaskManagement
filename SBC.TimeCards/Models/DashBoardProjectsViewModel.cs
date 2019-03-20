using SBC.TimeCards.Service.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SBC.TimeCards.Models
{
    public class DashBoardProjectsViewModel
    {
        public List<ProjectViewModel> ActiveProjects { get; set; }
        public List<ProjectViewModel> ArchivedProjects { get; set; }
        public List<ProjectViewModel> FavotirteProjects { get; set; }

    }
}