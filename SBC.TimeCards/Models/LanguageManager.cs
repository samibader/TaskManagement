using SBC.TimeCards.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace SBC.TimeCards.Models
{
    public static class LanguageManager
    {
        public static List<Language> AvailableLanguages = new List<Language> {
            new Language { LanguageFullName = "English", LanguageCultureName = "en"},
            new Language { LanguageFullName = "عربي", LanguageCultureName = "ar"},
        };
        public static bool IsRTL
        {
            get { return Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft; }
        }

        public static Language CurrentLanguage
        {
            get
            {
                return AvailableLanguages.FirstOrDefault(x => x.LanguageCultureName == Thread.CurrentThread.CurrentCulture.Name);
            }
        }
        public static bool IsLanguageAvailable(string lang)
        {
            return AvailableLanguages.Where(a => a.LanguageCultureName.Equals(lang)).FirstOrDefault() != null ? true : false;
        }
        public static string GetDefaultLanguage()
        {
            return AvailableLanguages[0].LanguageCultureName;
        }
        public static void SetLanguage(string lang)
        {
            try
            {

                if (!IsLanguageAvailable(lang)) lang = GetDefaultLanguage();
                var cultureInfo = new CultureInfo(lang);
                cultureInfo.DateTimeFormat.ShortDatePattern = GlobalSettings.DATE_FORMAT;
                cultureInfo.DateTimeFormat.LongTimePattern = GlobalSettings.LONG_TIME_PATTERN;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                HttpCookie langCookie = new HttpCookie("culture", lang);
                langCookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(langCookie);
            }
            catch (Exception e) { }
        }
    }

    public class Language
    {
        public string LanguageFullName { get; set; }
        public string LanguageCultureName { get; set; }
    }
}