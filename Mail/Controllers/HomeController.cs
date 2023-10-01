using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using Mail.Models;
using System.IO;

namespace Mail.Controllers
{
    public class HomeController : Controller
    {
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SendEmail()
        {

            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SendEmail(int id)
        {
            try
            {
                String MailTo = Request.Form.Get("mail");
                String content = Request.Form.Get("content");
                String subject = Request.Form.Get("subject");
                String from = Request.Form.Get("from");
                var fromAddress = new MailAddress("kyhienlanh1@gmail.com", from);
                var fromPassword = "gtzn icxt hcii qxnj";
                var toAddress = new MailAddress(MailTo);

                string body = content;

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })

                    smtp.Send(message);
                ViewBag.error = "successful";
            }
            catch (Exception ex)
            {
                ViewBag.error = "ERROR" + ex;
            }

            return View();
        }
       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}