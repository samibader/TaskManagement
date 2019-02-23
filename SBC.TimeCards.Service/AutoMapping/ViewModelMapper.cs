using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SBC.TimeCards.Data.Models;
using SBC.TimeCards.Service.Models.Projects;
using SBC.TimeCards.Service.Models.Users;

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
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<User, DetailedUserViewModel>()
                    .ForMember(d => d.Projects,
                 opt => opt.MapFrom(s => Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(s.Projects))); ;

                cfg.CreateMap<Project, ProjectViewModel>();
                cfg.CreateMap<Project, DetailedProjectViewModel>()
                    .ForMember(d => d.Users,
                 opt => opt.MapFrom(s => Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(s.Users))); ;



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
