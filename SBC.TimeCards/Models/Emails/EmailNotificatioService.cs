using Postal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace SBC.TimeCards.Models.Emails
{
    public static class EmailNotificatioService
    {
        public static string SMTP_HOST = "mail.wassatah.com";
        public static int SMTP_PORT = 26;
        public static string SMTP_UN = "romazonia@wassatah.com";
        public static string SMTP_PW = "Radwan12@";
        public static string AutomatedEmail = "task-no-reply@redf.dcportl.gov.sa";
       

        public static void SendNotificationEmail(string Subject, string To, string MessageBody)
        {
            string Path = HostingEnvironment.MapPath("~/template.html");
            string TemplateContent = System.IO.File.ReadAllText(Path);
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(AutomatedEmail);
            mailMessage.To.Add(To);
            mailMessage.Subject = Subject;
            mailMessage.Body = TemplateContent.Replace("{{0}}", MessageBody);
            mailMessage.IsBodyHtml = true;
            AlternateView htmlview = default(AlternateView);
            htmlview = AlternateView.CreateAlternateViewFromString(mailMessage.Body, null, "text/html");
            LinkedResource imageResourceEs = new LinkedResource(HostingEnvironment.MapPath("~/logo.png"));
            imageResourceEs.ContentId = "photo";
            imageResourceEs.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
            htmlview.LinkedResources.Add(imageResourceEs);
            mailMessage.AlternateViews.Add(htmlview);
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Host = SMTP_HOST;
            client.Port = SMTP_PORT;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SMTP_UN, SMTP_PW);
            client.EnableSsl = false;
            //client.Timeout = 10000;
            client.Send(mailMessage);
        }
    }
}