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
                Mapper.Map<TemplateViewModel, NetworkTemplate>(template, ticketTemplate.NetworkTemplate);
            }
            else if (template.TemplateTypeId == (int)TemplateTypes.ServerTemplate)
            {
                Mapper.Map<TemplateViewModel, ServerTemplate>(template, ticketTemplate.ServerTemplate);
                foreach (var item in template.Disks)
                {
                    var disk = ticketTemplate.ServerTemplate.ServerDiskTemplates.FirstOrDefault(x => x.Id == item.Id);
                    disk.Value = item.Value;
                }
                foreach (var item in template.Networks)
                {
                    var network = ticketTemplate.ServerTemplate.ServerNetworkTemplates.FirstOrDefault(x => x.Id == item.Id);
                    network.Ip = item.Ip;
                    network.Subnet = item.Subnet;
                    network.DefaultGateway = item.DefaultGateway;
                    network.Zone = item.Zone;
                }
            }
            else if (template.TemplateTypeId == (int)TemplateTypes.DeviceTemplate)
            {
                Mapper.Map<TemplateViewModel, DeviceTemplate>(template, ticketTemplate.DeviceTemplate);

            }
            else if (template.TemplateTypeId == (int)TemplateTypes.UserTemplate)
            {
                Mapper.Map<TemplateViewModel, UserTemplate>(template, ticketTemplate.UserTemplate);

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
                tt.ServerTemplate.ServerDiskTemplates.Add(new ServerDiskTemplate());
                tt.ServerTemplate.ServerNetworkTemplates.Add(new ServerNetworkTemplate());
            }
            else if (tt.TemplateTypeId == (int)TemplateTypes.DeviceTemplate)
            {
                tt.DeviceTemplate = new DeviceTemplate();
            }
            else if (tt.TemplateTypeId == (int)TemplateTypes.UserTemplate)
            {
                tt.UserTemplate = new UserTemplate();
            }
            else
            {
                throw new InvalidOperationException("Type Error");
            }
            _unitOfWork.TicketTemplates.Add(tt);
            _unitOfWork.SaveChanges();
            template.Id = tt.Id;
            //Fix missing data for servertemplate
            if (tt.TemplateTypeId == (int)TemplateTypes.ServerTemplate)
            {
                Mapper.Map<ServerTemplate, TemplateViewModel>(tt.ServerTemplate, template);
            }
            return template;
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
                    Mapper.Map<NetworkTemplate, TemplateViewModel>(item.NetworkTemplate, t);
                }
                else if (item.TemplateTypeId == (int)TemplateTypes.ServerTemplate)
                {
                    Mapper.Map<ServerTemplate, TemplateViewModel>(item.ServerTemplate, t);
                }
                else if (item.TemplateTypeId == (int)TemplateTypes.DeviceTemplate)
                {
                    Mapper.Map<DeviceTemplate, TemplateViewModel>(item.DeviceTemplate, t);
                }
                else if (item.TemplateTypeId == (int)TemplateTypes.UserTemplate)
                {
                    Mapper.Map<UserTemplate, TemplateViewModel>(item.UserTemplate, t);
                }

                res.Add(t);
            }
            return res;
        }

        public void Delete(int ticketTmplateId)
        {
            var ticketTemplate = _unitOfWork.TicketTemplates.GetById(ticketTmplateId);
            ticketTemplate.NetworkTemplate = null;
            _unitOfWork.TicketTemplates.Remove(ticketTemplate);
            _unitOfWork.SaveChanges();
        }

        public ServerDiskTemplateViewModel InitDisk(int id)
        {
            var ticketTemplate = _unitOfWork.TicketTemplates.GetById(id);
            var res = new ServerDiskTemplate();
            ticketTemplate.ServerTemplate.ServerDiskTemplates.Add(res);
            _unitOfWork.TicketTemplates.Update(ticketTemplate);
            _unitOfWork.SaveChanges();
            return Mapper.Map<ServerDiskTemplate, ServerDiskTemplateViewModel>(res);
        }

        public ServerNetworkTemplateViewModel InitNetwork(int id)
        {
            var ticketTemplate = _unitOfWork.TicketTemplates.GetById(id);
            var res = new ServerNetworkTemplate();
            ticketTemplate.ServerTemplate.ServerNetworkTemplates.Add(res);
            _unitOfWork.TicketTemplates.Update(ticketTemplate);
            _unitOfWork.SaveChanges();
            return Mapper.Map<ServerNetworkTemplate, ServerNetworkTemplateViewModel>(res);
        }

        public int GetDisksCount(int id)
        {
            return _unitOfWork.ServerDiskTemplates.GetBy(x => x.ServerTemplateId == id).Count();
        }

        public int GetNetworkCount(int id)
        {
            return _unitOfWork.ServerNetworkTemplates.GetBy(x => x.ServerTemplateId == id).Count();

        }
    }
}
