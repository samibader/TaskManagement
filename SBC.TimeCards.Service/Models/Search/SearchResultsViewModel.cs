using SBC.TimeCards.Service.Models.Attachments;
using SBC.TimeCards.Service.Models.Projects;
using SBC.TimeCards.Service.Models.Templates;
using SBC.TimeCards.Service.Models.Tickets;
using SBC.TimeCards.Service.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Search
{
    public class SearchResultsViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public List<TicketViewModel> Ticktes { get; set; }
        public List<ProjectViewModel> Projects { get; set; }
        public List<AttachmentViewModel> Attachments { get; set; }
        public List<TemplateSearchViewModel> Templates { get; set; }

    }
}
