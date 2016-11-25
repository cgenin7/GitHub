using APMPBilling.ViewModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace APMPBilling
{
    public class MyEmailService
    {
        private static readonly ILog _logger = LogManager.GetLogger("APMPLogger");

        public static async Task<bool> SendConfirmationEmail(string email, string url)
        {
            return await SendEmail(email, "APMP - Veuillez confirmer votre inscription", "<html><br/>" +
                 "<h2>Bienvenue parmi nous!</h2><br/> " +
                 "Vous faites maintenant partie de l'équipe. <br/>" +
                 "Veuillez confirmer votre inscription pour avoir accès au site: " +
                 "<a href='" + url + "'> activer mon compte. </a><br/><br/><br/>" + 
                 "<b>Pascal Monti</b><br/>" +
                 "<a href='mailto:p.monti@apmpelectrique.ca'>p.monti@apmpelectrique.ca</a><br/>" + 
                 "<a href='http://www.gestion.apmpelectrique.ca'>www.gestion.apmpelectrique.ca</a>" + 
                 "<br/>514-519-3110<br/><br/>" +
                 "<a href='http://www.apmpelectrique.ca'><img src='http://www.gestion.apmpelectrique.ca/Images/LogoAPMP.png' width='300px' /></a></html>");
        }

        public static async Task<bool> SendEmail(string email, string subject, string message)
        {
            try
            {
                var mailMessage = new MailMessage();
                mailMessage.To.Add(new MailAddress(email));  
                mailMessage.From = new MailAddress("pascal.monti@apmpelectrique.ca");  
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = message;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "postmaster@apmpelectrique.ca",  
                        Password = "ClaTest1234@"  
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "mail.apmpelectrique.ca";
                    smtp.Port = 8889;
                    smtp.EnableSsl = false;
                    await smtp.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Could not send email to {0}: {1}", email, Utils.GetExceptionMessage(ex)));
                return false;
            }
            return true;
        }

        internal static async Task<bool> SendNotificationEmail(string email, WorkHoursViewModel workHours)
        {
            return await SendEmail(email, string.Format("APMP - Nouvelles informations pour {0}", workHours.UserName),
                 string.Format("<html><br/> <h3>{0} - {1} - {2}</h3><br/> " +
                 "Temps travaillé: {3} heures.<br/>" +
                 "Info: {4}", workHours.UserName, workHours.SiteName, workHours.DisplayDay.ToString("dddd dd MMMM yyyy"),
                 workHours.NbHours, workHours.Info));
        }
    }
}