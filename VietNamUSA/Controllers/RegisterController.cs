using System.Linq;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using VietNamUSA.Areas.XPanel.Controllers;
using VietNamUSA.Common;
using VietUSA.Business;
using VietUSA.Business.Interfaces;
using VietUSA.Repository.Common.Enums;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;
using VietUSA.Repository.Common.Constants;
using System.Text;
using System;
using VietUSA.Repository.Common.Functions;
using System.Collections.Generic;

namespace VietNamUSA.Controllers
{
    public class RegisterController : BaseController
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IDocumentBusiness _documentBusiness;
        private readonly ICommonBusiness _commonBussiness;
        // GET: Register

        public RegisterController()
        {
            _userBusiness = new UserBusiness(FrontEndUser);
            _documentBusiness = new DocumentBusiness(FrontEndUser);
            _commonBussiness = new CommonBusiness(FrontEndUser);
        }

        public ActionResult Index()
        {
            return View(new UserModel());
        }
        [HttpPost]
        public JsonResult Register(UserModel parameter)
        {
            var result = new ResultModel<bool>(false, "", true);
            var validateCaptcha = ValidateCaptcha(Request["g-recaptcha-response"]);
            if (validateCaptcha.IsError)
            {
                return Json(validateCaptcha, JsonRequestBehavior.AllowGet);
            }

            var validateFileUpload = WebCommonFunctions.ValidateFileUpload(Request.Files);
            if (validateFileUpload.IsError)
            {
                return Json(validateFileUpload, JsonRequestBehavior.AllowGet);
            }
            var currentDatetime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            var dataToInsert = new UserModel
            {
                Address = parameter.Address,
                IsSuperAdministrator = false,
                Email = parameter.Email,
                IsEmailed = false,
                Organization = parameter.Organization,
                Gender = parameter.Gender,
                FullName = parameter.FullName,
                Ip = Request.UserHostAddress,
                Note = parameter.Note,
                UserTypeId = (int)UserTypeEnum.AnonymousUser,
                UserType = UserTypeEnum.AnonymousUser.ToString(),
                HasAttachment = Request.Files.Count > 0,
                SentTime = currentDatetime
            };

            var insertResult = _userBusiness.UpdateOrInsert(dataToInsert);
            if (insertResult.IsError)
            {
                result.IsError = true;
                result.Message = "There is error when submiting data, please try again later!";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            // Insert to Media
            List<string> Attachments = new List<string>();
            if (Request.Files.Count > 0)
            {
                foreach (var item in Request.Files)
                {
                    var fileItem = Request.Files[item.ToString()];
                    var document = new DocumentModel()
                    {
                        FullFilePath = Server.MapPath("~/Uploads/AbstractSubmission"),
                        FilePath = "/Uploads/AbstractSubmission"
                    };

                    var physicalFileResult = WebCommonFunctions.CreateFile(document, fileItem);
                    if (!physicalFileResult.IsError)
                    {
                        document = physicalFileResult.Data;
                        document.CreatedBy = insertResult.Data;
                        document.ObjectTypeId = (int)ObjectTypeEnum.AbstractSubmission;
                        document.ObjectTypeName = ObjectTypeEnum.AbstractSubmission.ToString();
                        document.ObjectId = insertResult.Data;
                        _documentBusiness.UpdateOrInsert(document);

                        Attachments.Add(Server.MapPath("~" + document.FilePath));
                    }
                }
            }
            // Send email
           
            var emailBody = CommonFunctions.RegisterBodyEmail(dataToInsert);
            var email = new EMailModel()
            {
                Subject = "[vnus2019.viasm.edu.vn]-[Thông tin đăng ký]: " + dataToInsert.FullName + "(" + currentDatetime + ")",
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
                Body = emailBody.ToString(),
                Attachments = Attachments
            };

            var sendEmailResult = _commonBussiness.SendEmail(email);
            if (!sendEmailResult.IsError)
            {
                // Update là đã Email
                dataToInsert.UserId = insertResult.Data;
                dataToInsert.IsEmailed = true;
                _userBusiness.UpdateOrInsert(dataToInsert);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ResultModel<bool> ValidateCaptcha(string response)
        {
            var secret = System.Web.Configuration.WebConfigurationManager.AppSettings["recaptchaPrivateKey"];
            var client = new WebClient();
            var jsonResult = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            var result = JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult.ToString());
            return result.Success ? new ResultModel<bool>(false, "", true) : new ResultModel<bool>(true, result.ErrorMessage.FirstOrDefault(), false);
        }
    }
}