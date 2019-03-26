using AutoMapper;
using Microsoft.AspNet.Identity;
using SBC.TimeCards.Common;
using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Data.Models;
using SBC.TimeCards.Service.Identity;
using SBC.TimeCards.Service.Interfaces;
using SBC.TimeCards.Service.Models.Projects;
using SBC.TimeCards.Service.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SBC.TimeCards.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public EmailService(IUnitOfWork unitOfWork,UserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        //public async Task SomeTask()
        //{
            
        //}
    }
}
