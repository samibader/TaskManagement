using SBC.TimeCards.Service.Models.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Interfaces
{
    public interface ITemplatesService
    {
        void UpdateTemplate(TemplateViewModel template);
        TemplateViewModel Init(TemplateViewModel template);
        List<TemplateViewModel> GetTemplates(int ticketId);
        void Delete(int ticketTmplateId);
        ServerDiskTemplateViewModel InitDisk(int id);
        ServerNetworkTemplateViewModel InitNetwork(int id);
        int GetDisksCount(int id);
        int GetNetworkCount(int id);
        void DeleteDisk(int id);
        void DeleteNetwork(int id);
    }
}
