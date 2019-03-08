﻿using SBC.TimeCards.Data.Infrastructure;
using SBC.TimeCards.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Services
{
    public class TicketsService :ITicketsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
