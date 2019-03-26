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
using SBC.TimeCards.Service.Models.Tickets;
using SBC.TimeCards.Service.Models.Comments;
using SBC.TimeCards.Service.Models.Templates;

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
                .ForMember(x => x.RoleName, opt => opt.MapFrom(x => x.UserRoles.FirstOrDefault().Role.Name))
                .ForMember(x => x.RoleId, opt => opt.MapFrom(x => x.UserRoles.FirstOrDefault().Role.Id));
                cfg.CreateMap<User, DetailedUserViewModel>();
                //  .ForMember(d => d.Projects,
                //  opt => opt.MapFrom(s => Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(s.Projects))); ;

                cfg.CreateMap<Project, ProjectViewModel>()
                 .ForMember(d => d.Owner, opt => opt.MapFrom(s => Mapper.Map<User, UserViewModel>(s.User)))
                 .ForMember(d => d.IsFavorite, opt => opt.MapFrom(x => x.User.FavoriteProjects.Contains(x)));
                cfg.CreateMap<Project, DetailedProjectViewModel>();
                cfg.CreateMap<Project, EditProjectViewModel>();
                // .ForMember(d => d.Users,
                //opt => opt.MapFrom(s => Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(s.Users))); ;
                cfg.CreateMap<Attachment, AttachmentViewModel>()
                .ForMember(x => x.Url, opt => opt.MapFrom(x => GlobalSettings.UPLOADS_PATH + x.FileName))
                .ForMember(x => x.Type, opt => opt.MapFrom(x => x.FileName.Substring(x.FileName.LastIndexOf('.')).ToLower()))
                .ForMember(x => x.SizeAsString, opt => opt.MapFrom(x => GlobalSettings.BytesToString(x.Size)))
                ;
                cfg.CreateMap<Attachment, EditAttachmentViewModel>()
                .ForMember(x => x.FileName, opt => opt.MapFrom(x => GlobalSettings.UPLOADS_PATH + x.FileName));

                cfg.CreateMap<Ticket, EditTicketViewModel>()
                    .ForMember(x => x.ProjectInfo, opt => opt.MapFrom(x => Mapper.Map<Project, ProjectViewModel>(x.ParentTicketId.HasValue ? x.Parent.Project : x.Project)))
                    .ForMember(x => x.IsSubTask, opt => opt.MapFrom(x => x.ParentTicketId.HasValue))
                    .ForMember(x => x.DueDate, opt => opt.MapFrom(x => x.DueDate.HasValue ? x.DueDate.Value.ToShortDateString() : ""))
                    .ForMember(x => x.ParentTicketInfo, opt => opt.MapFrom(x => (x.ParentTicketId.HasValue ? Mapper.Map<Ticket, TicketViewModel>(x.Parent) : new TicketViewModel())));

                cfg.CreateMap<Ticket, TicketViewModel>()
                .ForMember(x => x.Title, opt => opt.MapFrom(x => String.IsNullOrEmpty(x.Title) ? "New Ticket-" + x.Id.ToString() : x.Title))
                .ForMember(x => x.CommentsCount, opt => opt.MapFrom(x => x.Comments.Any() ? x.Comments.Count : 0))
                .ForMember(x => x.Assignee, opt => opt.MapFrom(x => x.AssigneeId.HasValue ? Mapper.Map<User, UserViewModel>(x.Assignee) : null))
                .ForMember(x => x.ProjectInfo, opt => opt.MapFrom(x => Mapper.Map<Project, ProjectViewModel>(x.ParentTicketId.HasValue ? x.Parent.Project : x.Project)))
                ;

                cfg.CreateMap<NetworkTemplate, TemplateViewModel>()
                    .ForMember(x => x.NetworkIp, opt => opt.MapFrom(x => x.Ip)).ReverseMap();
                cfg.CreateMap<DeviceTemplate, TemplateViewModel>()
                    .ForMember(x => x.DeviceIp, opt => opt.MapFrom(x => x.Ip)).ReverseMap();
                cfg.CreateMap<UserTemplate, TemplateViewModel>().ReverseMap();
                cfg.CreateMap<ServerTemplate, TemplateViewModel>()
                .ForMember(x=>x.ServerName,opt=>opt.MapFrom(x=>x.Name))
                .ForMember(x => x.Disks, opt => opt.MapFrom(x => Mapper.Map<List<ServerDiskTemplate>, List<ServerDiskTemplateViewModel>>(x.ServerDiskTemplates.ToList())))
                .ForMember(x => x.Networks, opt => opt.MapFrom(x => Mapper.Map<List<ServerNetworkTemplate>, List<ServerNetworkTemplateViewModel>>(x.ServerNetworkTemplates.ToList())))
                .ReverseMap();
                cfg.CreateMap<ServerDiskTemplate, ServerDiskTemplateViewModel>().ReverseMap();
                cfg.CreateMap<ServerNetworkTemplate, ServerNetworkTemplateViewModel>().ReverseMap();
                //cfg.CreateMap<Ticket, TicketKanabanViewModel>()
                //.ForMember(x=>x.ActiveTickets,opt=>opt.MapFrom(x=>x.SubTickets.Where(t=>t.StateId == (int)TicketStates.Active).ToList()))
                //.ForMember(x => x.DoneTickets, opt => opt.MapFrom(x => x.SubTickets.Where(t => t.StateId == (int)TicketStates.Done).ToList()))
                //.ForMember(x => x.DelayedTickets, opt => opt.MapFrom(x => x.SubTickets.Where(t => t.StateId == (int)TicketStates.Delayed).ToList()));
                cfg.CreateMap<Comment, CommentViewModel>()
                .ForMember(d => d.User, opt => opt.MapFrom(s => Mapper.Map<User, UserViewModel>(s.User)));
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
