using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Common
{
    public static class AppRoles
    {
        public const string Administrator = "Administrator";
        public const string Manager = "Manager";
        public const string Network  = "Network";
        public const string System = "System";
        public const string DataCenter = "DataCenter";
        public const string Tichnician = "Network,System,DataCenter";

    }
}
