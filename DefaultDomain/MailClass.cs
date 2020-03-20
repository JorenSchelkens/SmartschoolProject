using System;
using System.Net;
using System.Net.Mail;

namespace DefaultDomain
{
    public class MailClass
    {
        private const string EmailSchool = "no-reply@bazandpoort.be";
        private const string EmailSchoolPassword = "BAZandpoort";

        public MailClass()
        {

        }

        public bool SendMail(string emailGebruiker, string voorEnAchterNaam, string secret)
        {
            try
            {
                string href = "https://localhost:5001/inschrijving/";
                href += $"{voorEnAchterNaam}/";
                href += secret;

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(EmailSchool);
                    mail.To.Add(emailGebruiker);
                    mail.Body = $"<div><img src = 'https://www.busleydenatheneum.be/sites/default/files/thumbnails/image/BA_bol.jpg' style = 'width: 100px;float: left;'><h1 style = 'padding-left: 110px;padding-top: 31px;'> bevestigen schoolbal </h1></div><div style = 'margin-top: 50px;' href =\"{href}\"><p> Klik op de link hieronder om je inschrijving te bevestigen</p><a href=\"{href}\"> Bevestigen </a></div>";

                    mail.Subject = "Bedankt voor de inschrijving!";
                    //mail.Body = $"<h1>Klik op de link hieronder om je inschrijving te bevestigen</h1><a href=\"{href}\">Bevestigen</a>";
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(EmailSchool, EmailSchoolPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}