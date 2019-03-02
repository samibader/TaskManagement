using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SBC.TimeCards.Data.Models;
using SBC.TimeCards.Service.Models.Projects;
using SBC.TimeCards.Service.Models.Users;
using SBC.TimeCards.Service.Models.Attachments;
using SBC.TimeCards.Common;

namespace SBC.TimeCards.Service.AutoMapping
{
    public static class ViewModelMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                // ENTITY TO DTO
                #region ENTITY TO DTO
                cfg.CreateMap<User, UserViewModel>()
                .ForMember(x=>x.RoleName,opt=>opt.MapFrom(x=>x.UserRoles.FirstOrDefault().Role.Name))
                .ForMember(x=>x.RoleId,opt=>opt.MapFrom(x=>x.UserRoles.FirstOrDefault().Role.Id));
                cfg.CreateMap<User, DetailedUserViewModel>();
                //  .ForMember(d => d.Projects,
                //  opt => opt.MapFrom(s => Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(s.Projects))); ;

                cfg.CreateMap<Project, ProjectViewModel>();
                cfg.CreateMap<Project, DetailedProjectViewModel>();
                cfg.CreateMap<Project, EditProjectViewModel>();
                // .ForMember(d => d.Users,
                //opt => opt.MapFrom(s => Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(s.Users))); ;
                cfg.CreateMap<Attachment, AttachmentViewModel>()
                .ForMember(x => x.Url, opt => opt.MapFrom(x => GlobalSettings.UPLOADS_PATH + x.FileName))
               .ForMember(x => x.Type, opt => opt.MapFrom(x => x.FileName.Substring(x.FileName.LastIndexOf('.'))));
                cfg.CreateMap<Attachment, EditAttachmentViewModel>()
                .ForMember(x=>x.FileName,opt=>opt.MapFrom(x=>GlobalSettings.UPLOADS_PATH+ x.FileName));

                #endregion

                // DTO TO ENTITY
                #region DTO TO ENTTY
                cfg.CreateMap<UserViewModel, User>();
                #endregion

                #region Dot to Dto
                cfg.CreateMap<UserViewModel, EditUserViewModel>();
                #endregion
            });

        }
    }
}
