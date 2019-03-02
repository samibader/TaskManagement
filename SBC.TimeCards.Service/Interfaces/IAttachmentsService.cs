using SBC.TimeCards.Service.Models.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Interfaces
{
    public interface IAttachmentsService
    {
        void Create(CreateAttachmentViewModel model);
        EditAttachmentViewModel GetAttachmentForEdit(int id);
        string GetAttachmentFileName(int id);
        void Edit(EditAttachmentViewModel model);
        List<AttachmentViewModel> GetProjectAttachments(int projectId);
        void Delete(int id);
    }
}
