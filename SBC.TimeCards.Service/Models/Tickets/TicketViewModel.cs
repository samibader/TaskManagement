using SBC.TimeCards.Common;
using SBC.TimeCards.Service.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Service.Models.Tickets
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CommentsCount { get; set; }
        public UserViewModel Assignee { get; set; }
        public DateTime? DueDate { get; set; }

        public string DueDateAsString
        {
            get
            {
                if (DueDate.HasValue)
                {
                    if(DueDate.Value.Date>=GlobalSettings.CURRENT_DATETIME.Date)
                    {
                        if (DueDate.Value.Date <= GlobalSettings.CURRENT_DATETIME.Date.AddDays(7))
                            return (DueDate.Value.Date - GlobalSettings.CURRENT_DATETIME.Date).Days.ToString() + " Days";
                        else
                            return DueDate.Value.Date.ToString("MMM dd", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        if (DueDate.Value.Date.AddDays(7) >= GlobalSettings.CURRENT_DATETIME.Date)
                            return "Delayed " + (GlobalSettings.CURRENT_DATETIME.Date - DueDate.Value.Date).Days.ToString() + " Days";
                        else
                            return DueDate.Value.Date.ToString("MMM dd", System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                return "Not Specified";
            }
        }
    }
}
