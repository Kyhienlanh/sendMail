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
using System.Reflection;

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
        public ActionResult SendEmail(HttpPostedFileBase file)
        {
            try
            {
                string MailTo = Request.Form.Get("mail");
                string content = Request.Form.Get("content");
                string subject = Request.Form.Get("subject");
                string from = Request.Form.Get("from");

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

                Attachment attachment = null;

                // Check if a file was uploaded
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                    file.SaveAs(filePath);

                    // Create an attachment object for the uploaded file
                    attachment = new Attachment(filePath);
                }

                using (var message = new MailMessage(fromAddress, toAddress))
                {
                    message.Subject = subject;
                    message.Body = body;

                    // Attach the file if it exists
                    if (attachment != null)
                    {
                        message.Attachments.Add(attachment);
                    }

                    smtp.Send(message);
                }

                ViewBag.Message = "Email sent successfully ";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error sending email: " + ex.Message;
                return View();
            }
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