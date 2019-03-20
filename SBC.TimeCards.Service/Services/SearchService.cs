using SBC.TimeCards.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBC.TimeCards.Service.Models.Search;
using SBC.TimeCards.Data.Infrastructure;
using AutoMapper;
using SBC.TimeCards.Data.Models;
using SBC.TimeCards.Service.Models.Attachments;
using SBC.TimeCards.Service.Models.Users;
using SBC.TimeCards.Service.Models.Projects;
using SBC.TimeCards.Service.Models.Tickets;

namespace SBC.TimeCards.Service.Services
{
    public class SearchService : ISearchService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public SearchResultsViewModel Search(string searchString, bool full = false)
        {
            var res = new SearchResultsViewModel
            {
                Attachments = Mapper.Map<List<Attachment>, List<AttachmentViewModel>>(_unitOfWork.Attachments.GetBy(x => x.Title.Contains(searchString)).Take(full?Int32.MaxValue:3).ToList()),
                Users = Mapper.Map<List<User>, List<UserViewModel>>(_unitOfWork.Users.GetBy(x => x.Name.Contains(searchString)).ToList()),
                Projects = Mapper.Map<List<Project>, List<ProjectViewModel>>(_unitOfWork.Projects.GetBy(x => x.Name.Contains(searchString) || x.Description.Contains(searchString)).Take(full ? Int32.MaxValue : 3).ToList()),
                Ticktes = Mapper.Map<List<Ticket>, List<TicketViewModel>>(_unitOfWork.Tickets.GetBy(x => x.Title.Contains(searchString)||x.Description.Contains(searchString)).Take(full ? Int32.MaxValue : 3).ToList()),

            };
            return res;
        }
    }
}
