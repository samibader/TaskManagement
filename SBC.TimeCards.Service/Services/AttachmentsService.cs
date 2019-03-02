using SBC.TimeCards.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBC.TimeCards.Service.Models.Attachments;
using SBC.TimeCards.Data.Models;
using SBC.TimeCards.Common;
using SBC.TimeCards.Data.Infrastructure;
using AutoMapper;
using System.IO;

namespace SBC.TimeCards.Service.Services
{
    public class AttachmentsService : IAttachmentsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _uploadPath;
        public AttachmentsService(IUnitOfWork unitOfWork, string uploadPath)
        {
            _unitOfWork = unitOfWork;
            _uploadPath = uploadPath;
        }
        public void Create(CreateAttachmentViewModel model)
        {
            var attachment = new Attachment
            {
                Title = model.Title,
                Description = model.Description,
                ProjectId = model.Id,
                UploadDate = GlobalSettings.CURRENT_DATETIME,
                FileName = model.FileName
            };
            attachment.Size = (int)new FileInfo(_uploadPath + model.FileName).Length; 
            _unitOfWork.Attachments.Add(attachment);
            _unitOfWork.SaveChanges();
        }
        public EditAttachmentViewModel GetAttachmentForEdit(int id)
        {
            return Mapper.Map<Attachment, EditAttachmentViewModel>(_unitOfWork.Attachments.GetById(id));
        }
        public string GetAttachmentFileName(int id)
        {
            return _unitOfWork.Attachments.GetById(id).FileName;
        }
        public void Edit(EditAttachmentViewModel model)
        {
            var attachment = _unitOfWork.Attachments.GetById(model.Id);
            if (!String.IsNullOrEmpty(model.FileName))
            {
                File.Delete(_uploadPath + attachment.FileName);
                attachment.FileName = model.FileName;
                attachment.Size = (int)new FileInfo(_uploadPath + model.FileName).Length;
            }
            attachment.Title = model.Title;
            attachment.Description = model.Description;
            _unitOfWork.Attachments.Update(attachment);
            _unitOfWork.SaveChanges();
        }
        public List<AttachmentViewModel> GetProjectAttachments(int projectId)
        {
            return Mapper.Map<List<Attachment>, List<AttachmentViewModel>>(_unitOfWork.Attachments.GetBy(x => x.ProjectId == projectId).ToList());
        }
        public void Delete(int id)
        {
            var attachement = _unitOfWork.Attachments.GetById(id);
            File.Delete(_uploadPath + attachement.FileName);
            _unitOfWork.Attachments.Remove(attachement);
            _unitOfWork.SaveChanges();
        }
    }
}
