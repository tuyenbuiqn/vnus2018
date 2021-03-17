using System;
using System.Collections.Generic;
using VietUSA.Business.Interfaces;
using VietUSA.Repository;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Interfaces;
using VietUSA.Repository.Models;
using System.Net.Mail;
using System.Net.Mime;

namespace VietUSA.Business
{
    public class CommonBusiness : BaseBusiness<ICommonRepository>, ICommonBusiness
    {
        public CommonBusiness(UserInfoModel userInfoModel) : base(new CommonRepository(userInfoModel))
        {
        }

        public ResultModel<bool> InsertLog(LogModel parameter)
        {
            try
            {
                return MainRepository.InsertLog(parameter);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<bool>(exception);
            }
        }

        public ResultModel<bool> SendEmail(EMailModel parameter)
        {
            try
            {
                MailMessage eMail = new MailMessage();
                SmtpClient client = new SmtpClient();
                client.Port = parameter.Port;
                client.Host = parameter.Host;
                client.Timeout = parameter.Timeout;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = parameter.UseDefaultCredentials;
                client.Credentials = new System.Net.NetworkCredential(parameter.FromEmail, parameter.FromEmailPassword);
                client.EnableSsl = true;
                eMail.From = new MailAddress(parameter.FromEmail);
                foreach (var item in parameter.ToEmails)
                {
                    eMail.To.Add(item);
                }

                eMail.Subject = parameter.Subject;
                eMail.Body = parameter.Body;
                eMail.IsBodyHtml = true;
                if (parameter.Attachments != null && parameter.Attachments.Count > 0)
                {
                    foreach (var item in parameter.Attachments)
                    {
                        Attachment data = new Attachment(item, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = data.ContentDisposition;
                        disposition.CreationDate = System.IO.File.GetCreationTime(item);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(item);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(item);
                        // Add the file attachment to this e-mail message.
                        eMail.Attachments.Add(data);
                    }
                }
                client.Send(eMail);

                return new ResultModel<bool>(false,"SUCCESS",true);

            }
            catch (Exception exception)
            {
                return new ResultModel<bool>(true, exception.Message, false);
            }
            //client.Host = "smtp.gmail.com";

        }

        public byte[] ExportToExcel<T>(List<T> data, ExcelModel parameter)
        {


            return new byte[1];
        }
    }
}
