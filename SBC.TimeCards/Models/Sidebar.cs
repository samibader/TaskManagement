using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SBC.TimeCards.Models
{
    public class SidebarItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool IsParent { get; set; }
        public int ParentId { get; set; }
        public string IconClass { get; set; }
    }

    public static class Sidebar
    {

        public static List<SidebarItem> AdminItems = new List<SidebarItem>  {
            // Dashboard
            new SidebarItem() { Id = 1, Name = "Dashboard", Controller = "Home", Action = "Index", IsParent=false, ParentId=0 , IconClass="icon-home"},
            new SidebarItem() { Id = 8, Name = "My Tasks", Controller = "Tickets", Action = "MyTickets", IsParent=false, ParentId=0 , IconClass="icon-check"},
            // Projects
            new SidebarItem() { Id = 2, Name = "Projects", Controller = "Projects", IsParent=true, ParentId=0, IconClass="icon-list" },
            new SidebarItem() { Id = 3, Name = "Archived Projects", Controller = "Projects", Action = "Index",IsParent=false, ParentId=2 },
            new SidebarItem() { Id = 4, Name = "Create Project", Controller = "Projects", Action = "Create",IsParent=false, ParentId=2 },
            // Users
            new SidebarItem() { Id = 5, Name = "Users", Controller = "Users", IsParent=true, ParentId=0 , IconClass="icon-user"},
            new SidebarItem() { Id = 6, Name = "View Users", Controller = "Users", Action = "Index", IsParent=false, ParentId=5 },
            new SidebarItem() { Id = 7, Name = "Create User", Controller = "Users", Action = "Create", IsParent=false, ParentId=5 }
        };
        public static List<SidebarItem> UserItems = new List<SidebarItem>  {
            // Dashboard
            new SidebarItem() { Id = 1, Name = "Dashboard", Controller = "Home", Action = "Index", IsParent=false, ParentId=0 , IconClass="icon-home"},
            new SidebarItem() { Id = 4, Name = "My Tasks", Controller = "Tickets", Action = "MyTickets", IsParent=false, ParentId=0 , IconClass="icon-check"},
            new SidebarItem() { Id = 2, Name = "Projects", Controller = "Projects", IsParent=true, ParentId=0, IconClass="icon-list" },
            new SidebarItem() { Id = 3, Name = "Archived Projects", Controller = "Projects", Action = "Index",IsParent=false, ParentId=2 }

        };
    }
}