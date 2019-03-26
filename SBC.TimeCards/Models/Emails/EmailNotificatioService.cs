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
        public static void NotifyNewProject(int projectId)
        {
            // Prepare Postal classes to work outside of ASP.NET request
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));

            var emailService = new EmailService(engines);

            

            // Get comment and send a notification.
            //using (var db = new MailerDbContext())
            //{
                //var comment = db.Comments.Find(commentId);

                var email = new NewProjectEmail
                {
                    To = "yourmail@example.com",
                    UserName = "sami",
                    ProjectName = "test project"
                };

            //emailService.Send(email);
            //}

            var message = emailService.CreateMailMessage(email);

            SmtpClient client = new SmtpClient();
            client.SendMailAsync(message);
        }
    }
}