using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC.TimeCards.Common
{
    public static class GlobalSettings
    {
        public static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }
        public static DateTime CURRENT_DATETIME
        {
            get
            {
                return DateTime.Now;
            }
        }
        public static int ArchivedProjectSize = 10;
        public static string DATE_FORMAT = "dd/MM/yyyy";
        public static string UPLOADS_PATH = "/uploads/";
        public static string LONG_TIME_PATTERN = "hh:mm:ss";
        public static string DATETIME_FORMAT = "dd/MM/yyyy hh:mm:ss";
        public static string CURRENT_DATETIME_AS_STRING
        {
            get
            {
                return CURRENT_DATETIME.ToString(DATETIME_FORMAT, System.Globalization.CultureInfo.InvariantCulture);
            }

        }
        public static string CURRENT_DATE_AS_STRING = CURRENT_DATETIME.ToString(DATE_FORMAT, System.Globalization.CultureInfo.InvariantCulture);
        public static string ConvertDateTimeToString(DateTime value)
        {
            return value.ToString(DATETIME_FORMAT, System.Globalization.CultureInfo.InvariantCulture);
        }
        public static string ConvertDateToString(DateTime value)
        {
            return value.ToString(DATE_FORMAT, System.Globalization.CultureInfo.InvariantCulture);
        }
    }

    public enum TicketStates
    {
        Active = 1, Done = 2, Delayed = 3
    }
    public enum TemplateTypes
    {
        ServerTemplate = 1, DeviceTemplate = 3, NetworkTemplate = 4, UserTemplate = 2
    }
}
