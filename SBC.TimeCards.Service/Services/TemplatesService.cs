using SBC.TimeCards.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBC.TimeCards.Service.Models.Templates;
using SBC.TimeCards.Common;
using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using AutoMapper;

namespace SBC.TimeCards.Service.Services
{
    public class TemplatesService : ITemplatesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TemplatesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void UpdateTemplate(TemplateViewModel template)
        {
            var ticketTemplate = _unitOfWork.TicketTemplates.GetById(template.Id);

            if (template.TemplateTypeId == (int)TemplateTypes.NetworkTemplate)
            {
                ticketTemplate.NetworkTemplate.Ip = template.Ip;
            }
            else if (template.TemplateTypeId == (int)TemplateTypes.ServerTemplate)
            {
                ticketTemplate.ServerTemplate.Name = template.ServerName;

            }
            _unitOfWork.TicketTemplates.Update(ticketTemplate);
            _unitOfWork.SaveChanges();
        }

        public TemplateViewModel Init(TemplateViewModel template)
        {
            var tt = new TicketTemplate
            {
                TicketId = template.TicketId,
                TemplateTypeId = template.TemplateTypeId
            };
            if (tt.TemplateTypeId == (int)TemplateTypes.NetworkTemplate)
            {
                tt.NetworkTemplate = new NetworkTemplate();
            }
            else if (tt.TemplateTypeId == (int)TemplateTypes.ServerTemplate)
            {
                tt.ServerTemplate = new ServerTemplate();
            }
            _unitOfWork.TicketTemplates.Add(tt);
            _unitOfWork.SaveChanges();
            template.Id = tt.Id;
            return template;
            //if (template.TemplateTypeId == (int)TemplateTypes.NetworkTemplate)
            //{
            //    NetworkTemplateViewModel x = new NetworkTemplateViewModel
            //    {
            //        TicketId = template.TicketId,
            //        Id = tt.Id,
            //        TemplateTypeId = template.TemplateTypeId
            //    };
            //    return x;
            //}
            //if (template.TemplateTypeId == (int)TemplateTypes.ServerTemplate)
            //{
            //    ServerTemplateViewModel x = new ServerTemplateViewModel
            //    {
            //        TicketId = template.TicketId,
            //        Id = tt.Id,
            //        TemplateTypeId = template.TemplateTypeId
            //    };
            //    return x;
            //}
            //  return template;
        }

        public List<TemplateViewModel> GetTemplates(int ticketId)
        {
            var templates = _unitOfWork.Tickets.GetById(ticketId);
            var res = new List<TemplateViewModel>();
            var src = templates.TicketTemplates.ToList();
            foreach (var item in src)
            {
                var t = new TemplateViewModel
                {
                    Id = item.Id,
                    TicketId = item.TicketId,
                    TemplateTypeId = item.TemplateTypeId

                };
                if (item.TemplateTypeId == (int)TemplateTypes.NetworkTemplate)
                {
                    t.Ip = item.NetworkTemplate.Ip;
                }
                else if (item.TemplateTypeId == (int)TemplateTypes.ServerTemplate)
                {
                    t.ServerName = item.ServerTemplate.Name;
                }

                res.Add(t);
            }
            return res;
        }
    }
}
