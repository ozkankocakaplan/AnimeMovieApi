using System;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace AnimeMovie.API
{
    public static class Handler
    {
        public static int UserID(HttpContext httpContext)
        {
            var identity = (ClaimsIdentity)httpContext.User.Identity;
            int? id = Convert.ToInt32(identity.Claims.Where(x => x.Type == "ID").Select(x => x.Value).SingleOrDefault());
            if (id != null)
                return (int)id;
            return 0;
        }
        public static string RolType(HttpContext httpContext)
        {
            var identity = (ClaimsIdentity)httpContext.User.Identity;
            string id = identity.Claims.Where(x => x.Type == "Role").Select(x => x.Value).SingleOrDefault();
            if (id != null)
                return (string)id;
            return "";
        }
        public static string RandomData()
        {
            Random rnd = new Random();
            string data = "";
            int number, min = 65;
            char _char;
            for (int i = 0; i < 2; i++)
            {
                number = rnd.Next(min, min + 25);
                _char = Convert.ToChar(number);
                data += _char;
            }
            return data;
        }
        public static string createData()
        {

            Random rnd = new Random();
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomData());
            builder.Append(rnd.Next(100, 999));
            builder.Append(RandomData());
            return builder.ToString();
        }
        public static string EmailTemplate(IWebHostEnvironment webHost,string head,string body)
        {
            string template = String.Empty;
            using (var reader = new System.IO.StreamReader(webHost.WebRootPath + "/emailTemplate.txt"))
            {
                string readFile = reader.ReadToEnd();
                string content = readFile;
                content = content.Replace("HEAD", head);
                content = content.Replace("BODY", body);
                template = content.ToString();
            }
            return template;
        }
        public static void SendEmail(IWebHostEnvironment webHostEnvironment,string email,string head,string body,string subject)
        {
            MailMessage mail = new MailMessage();
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = Handler.EmailTemplate(webHostEnvironment, head,body);
            mail.From = new MailAddress("info@lycorisa.com", "ANİME");
            mail.To.Add(new MailAddress(email));
            SmtpClient smtp = new SmtpClient("srvm09.trwww.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("info@lycorisa.com", "7vLHchT2");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(mail);
        }
    }
}

