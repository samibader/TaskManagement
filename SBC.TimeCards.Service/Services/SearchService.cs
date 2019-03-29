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
using SBC.TimeCards.Service.Models.Templates;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace SBC.TimeCards.Service.Services
{
    public class SearchService : ISearchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITemplatesService _templateService;
        public SearchService(IUnitOfWork unitOfWork, TemplatesService templateService)
        {
            _unitOfWork = unitOfWork;
            _templateService = templateService;
        }
        public SearchResultsViewModel Search(string searchString, bool full = false)
        {
            var res = new SearchResultsViewModel
            {
                Attachments = Mapper.Map<List<Attachment>, List<AttachmentViewModel>>(_unitOfWork.Attachments.GetBy(x => x.Title.ToLower().Contains(searchString.ToLower())).Take(full ? Int32.MaxValue : 3).ToList()),
                Users = Mapper.Map<List<User>, List<UserViewModel>>(_unitOfWork.Users.GetBy(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList()),
                Projects = Mapper.Map<List<Project>, List<ProjectViewModel>>(_unitOfWork.Projects.GetBy(x => x.Name.ToLower().Contains(searchString.ToLower()) || x.Description.ToLower().Contains(searchString.ToLower())).Take(full ? Int32.MaxValue : 3).ToList()),
                Ticktes = Mapper.Map<List<Ticket>, List<TicketViewModel>>(_unitOfWork.Tickets.GetBy(x => x.Title.ToLower().Contains(searchString.ToLower()) || x.Description.ToLower().Contains(searchString.ToLower())).Take(full ? Int32.MaxValue : 3).ToList()),

            };
            var jsonSer = new JavaScriptSerializer();
            res.Templates = new List<TemplateSearchViewModel>();
            foreach (var id in _unitOfWork.Tickets.GetAll().Select(x => x.Id).ToList())
            {
                var templates = _templateService.GetTemplates(id);
                //var tempres = new List<TemplateViewModel>();
                foreach (var item in templates)
                {
                    var json = JToken.Parse(jsonSer.Serialize(item));
                    var fieldsCollector = new JsonFieldsCollector(json);
                    if (fieldsCollector.GetAllFields().Any(x => x.Value.ToString().ToLower().Contains(searchString.ToLower())))
                    {
                        res.Templates.Add(new TemplateSearchViewModel
                        {
                            Template = item,
                            Ticket = Mapper.Map<Ticket, TicketViewModel>(_unitOfWork.Tickets.GetById(id))
                        });
                    }
                }

                //var tempres = templates.Where(x => x.GetType().GetProperties().Where(pi => pi.PropertyType == typeof(string) && pi.GetValue(x, null)!=null).Any(y => y.GetValue(x, null).ToString().Contains(searchString)));
                //foreach (var item in tempres)
                //{
                //    res.Templates.Add(new TemplateSearchViewModel
                //    {
                //        Template = item,
                //        Ticket = Mapper.Map<Ticket, TicketViewModel>(_unitOfWork.Tickets.GetById(id))
                //    });
                //}
            }
            return res;
        }
    }
}
public class JsonFieldsCollector
{
    private readonly Dictionary<string, JValue> fields;

    public JsonFieldsCollector(JToken token)
    {
        fields = new Dictionary<string, JValue>();
        CollectFields(token);
    }

    private void CollectFields(JToken jToken)
    {
        switch (jToken.Type)
        {
            case JTokenType.Object:
                foreach (var child in jToken.Children<JProperty>())
                    CollectFields(child);
                break;
            case JTokenType.Array:
                foreach (var child in jToken.Children())
                    CollectFields(child);
                break;
            case JTokenType.Property:
                CollectFields(((JProperty)jToken).Value);
                break;
            default:
                fields.Add(jToken.Path, (JValue)jToken);
                break;
        }
    }

    public IEnumerable<KeyValuePair<string, JValue>> GetAllFields() => fields;
}