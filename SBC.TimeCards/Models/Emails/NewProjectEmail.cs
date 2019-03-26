using Postal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SBC.TimeCards.Models.Emails
{
    public class NewProjectEmail : Email
    {
        public string To { get; set; }
        public string UserName { get; set; }
        public string ProjectName { get; set; }
    }
}