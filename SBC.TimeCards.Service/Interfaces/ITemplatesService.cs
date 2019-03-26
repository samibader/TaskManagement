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
    }
}
