using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using VietNamUSA.Areas.XPanel.Controllers;
using VietUSA.Business;
using VietUSA.Business.Interfaces;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Common.Constants;
using System.Collections.Generic;
using System;

namespace VietNamUSA.Controllers
{
    public class ContactController : BaseController
    {
        private readonly IContactBusiness _contactBusiness;
        // GET: Register
        private readonly ICommonBusiness _commonBussiness;
        public ContactController()
        {
             _contactBusiness = new ContactBusiness(FrontEndUser);
            _commonBussiness = new CommonBusiness(FrontEndUser);
        }

        public ActionResult Index()
        {
            return View(new ContactModel());
        }
        [HttpPost]
        public JsonResult Register(ContactModel parameter)
        {
            var result = new ResultModel<bool>(false, "", true);
            var validateCaptcha = ValidateCaptcha(Request["g-recaptcha-response"]);
            if (validateCaptcha.IsError)
            {
                return Json(validateCaptcha, JsonRequestBehavior.AllowGet);
            }
            var currentDatetime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            var dataToInsert = new ContactModel()
            {
                Subject = parameter.Subject,
                FullName = parameter.FullName,
                Email = parameter.Email,
                PhoneNumber = parameter.PhoneNumber,
                Message = parameter.Message,
                IsProcessed = false,
                HasAttachment = false,
                SentTime = currentDatetime
            };

            var insertResult = _contactBusiness.InsertOrUpdate(dataToInsert);
            if (insertResult.IsError)
            {
                result.IsError = true;
                result.Message = "There is error when submiting data, please try again later!";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            // Send email

            var emailBody = CommonFunctions.ContactBodyEmail(dataToInsert);
            var email = new EMailModel()
            {
                Subject = "[vnus2019.viasm.edu.vn]-[Thông tin Liên hệ]: " + dataToInsert.FullName + "(" + currentDatetime + ")",
                FromEmail = EmailConstant.FromEmail,
                FromEmailPassword = EmailConstant.FromEmailPassword,
                Host = EmailConstant.Host,
                Port = EmailConstant.Port,
                Timeout = EmailConstant.Timeout,
                UseDefaultCredentials = EmailConstant.UseDefaultCredentials,
                ToEmails = new List<System.Net.Mail.MailAddress>()
                {
                    new System.Net.Mail.MailAddress(EmailConstant.ToEmail1)
                },
                Body = emailBody.ToString()
            };

            var sendEmailResult = _commonBussiness.SendEmail(email);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ResultModel<bool> ValidateCaptcha(string response)
        {
            var secret = System.Web.Configuration.WebConfigurationManager.AppSettings["recaptchaPrivateKey"];
            var client = new WebClient();
            var jsonResult = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            var result = JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult);
            return result.Success ? new ResultModel<bool>(false, "", true) : new ResultModel<bool>(true, result.ErrorMessage.FirstOrDefault(), false);
        }
    }
}