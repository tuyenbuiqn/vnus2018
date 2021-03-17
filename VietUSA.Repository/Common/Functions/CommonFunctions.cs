using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Enums;
using VietUSA.Repository.Common.Extensions;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Repository.Common.Functions
{
    public static class CommonFunctions
    {
        public const string VietNameCharacters = "AÁÀẢẠÃĂẮẰẲẶẴÂẦẤẨẬẪBCDĐEÉÈẺẸẼÊẾỀỂỆỄFGHIÍÌỈỊĨJKMLNOÓÒỎỌÕƠỚỜỞỢỠÔỐỒỔỘỖPQRSTUÚÙỦỤŨƯỨỪỬỰỮVXYÝỲỶỴỸZaáàảạãăắằẳặẵâấầẩậẫbcdđeéèẻẹẽêếềểệễfghiíìỉịĩjkmlnoóòỏọõơớờởợỡôốồổộỗpqrstuúùủụũưứừửựữvxyýỳỷỵỹz0123456789";
        public static string ConvertMessageCodeToMessage(string messageCode)
        {
            switch (messageCode)
            {
                case LoginConstant.Success:
                    return "Đăng nhập thành công";
                case LoginConstant.NotExisted:
                    return "Sai Username hoặc Password";
                case LoginConstant.NotAuthorized:
                    return "Tài khoản chưa được phân quyền, vui lòng liên hệ quản trị viên";
                default:
                    return string.Empty;
            }
        }
        public static int ConvertFromStringToInt(string input, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(input)) return defaultValue;
            try
            {
                int output;

                var isConverted = int.TryParse(input, out output);
                return isConverted ? output : defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }
        public static int ConvertFromObjectToInt(object input, int defaultValue = 0)
        {
            if (input == null) return defaultValue;
            return ConvertFromStringToInt(input.ToString(), defaultValue);
        }

        public static long ConvertFromStringToLong(string input, long defaultValue = 0)
        {
            if (string.IsNullOrEmpty(input)) return defaultValue;
            try
            {
                long output;

                var isConverted = long.TryParse(input, out output);
                return isConverted ? output : defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static long ConvertFromObjectToLong(object input, long defaultValue = 0)
        {
            if (input == null) return defaultValue;
            return ConvertFromStringToLong(input.ToString(), defaultValue);
        }
        public static string GetMd5Hash(string input)
        {
            var hash = new StringBuilder();
            var md5Provider = new MD5CryptoServiceProvider();
            var bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            foreach (var item in bytes)
            {
                hash.Append(item.ToString("x2"));
            }
            return hash.ToString();
        }

    
        public static string GetAdministrativeDivisionName(int? typeId)
        {
            switch (typeId)
            {
                case AdministrativeDivisionType.Nation:
                    return "Quốc gia";
                case AdministrativeDivisionType.Province:
                    return "Tỉnh/thành phố";
                case AdministrativeDivisionType.District:
                    return "Quận/huyện";
                case AdministrativeDivisionType.Ward:
                    return "Phường/xã";
                case AdministrativeDivisionType.Street:
                    return "Đường/phố";
                default:
                    return "Không xác định";
            }
        }


        public static string ReturnStyleOfElementInListElement(int element, int[] elements, string style, string defaultStyle)
        {
            if (elements.Contains(element))
                return style;
            else return defaultStyle;
        }




        public static string Sha512Encode(String sInp)
        {
            var strpass = "";
            var shaM = SHA512.Create();
            var enc = new ASCIIEncoding();
            int i;
            shaM.ComputeHash(enc.GetBytes(sInp));
            for (i = 0; i <= shaM.Hash.Length - 1; i++)
                strpass += shaM.Hash[i].ToString("X");
            return strpass;
        }

        #region Encrypt and Decrypt string in MD5 - DatLQ

        /// <summary>
        /// Mã hóa ký tự với kiểu mã hõa TripleDes - MD5
        /// </summary>
        /// <param name="key"></param>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public static string EncryptString(string key, string toEncrypt)
        {
            var toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            var tdes =
                new TripleDESCryptoServiceProvider { Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
            var cTransform = tdes.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(
                toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// Giải mã dữ liệu đã mã hóa TripleDes - MD5
        /// </summary>
        /// <param name="key"></param>
        /// <param name="toDecrypt"></param>
        /// <returns></returns>
        public static string DecryptString(string key, string toDecrypt)
        {
            var toEncryptArray = Convert.FromBase64String(toDecrypt);

            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(
            toEncryptArray, 0, toEncryptArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        #endregion Encrypt and Decrypt string in MD5 

        public static DateTime? ConvertStringToDateTime(string input, bool isGetDatetimeNow = true)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    if (isGetDatetimeNow)
                        return DateTime.Now;
                    return null;
                }
                var output = DateTime.ParseExact(input, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                return output;
            }
            catch (Exception exception)
            {
                return DateTime.Now;
            }
        }
        public static DateTime ConvertObjectToDateTime(object input, bool isGetDatetimeNow = true)
        {
            try
            {
                if (input == null) return (isGetDatetimeNow ? DateTime.Now : DateTime.MinValue);
                var output = DateTime.ParseExact(input.ToString(), "dd/MM/yyyy", null);
                return output;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
        public static string ConvertDateTimeToString(DateTime input)
        {
            try
            {
                return input.ToString("dd/MM/yyyy");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static bool ConvertObjectToBoolean(bool? input, bool defaultValue = false)
        {
            try
            {
                if (!input.HasValue) return defaultValue;
                return input.Value;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        public static string ConvertDateTimeToString(DateTime? input)
        {
            try
            {
                if (input.HasValue)
                    return input.Value.ToString("dd/MM/yyyy");
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string FormatNumber(object number)
        {
            if (number == null) return "";
            return string.Format(CultureInfo.CreateSpecificCulture("el-GR"), "{0:0,0}", number);
        }

        public static string GetGenderName(bool? gender)
        {
            if (!gender.HasValue) return string.Empty;
            return gender.Value ? "Nam" : "Nữ";
        }
        public static string GetHasTouristMarket(bool? gender)
        {
            if (!gender.HasValue) return string.Empty;
            return gender.Value ? "Có" : "Không";
        }
        public static ResultModel<DataTable> ListToDataTable<T>(IList<T> dataSource)
        {
            try
            {
                var properties =
                    TypeDescriptor.GetProperties(typeof(T));
                var table = new DataTable();
                foreach (PropertyDescriptor prop in properties)
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                foreach (var item in dataSource)
                {
                    var row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    table.Rows.Add(row);
                }

                return new ResultModel<DataTable>(false, CommonConstants.Success, table);
            }
            catch (Exception exception)
            {
                return new ResultModel<DataTable>(true, exception.Message, null);
            }
        }
        public static ResultModel<DataTable> ValidateColumnNames(DataTable dataSource, List<string> columnNames)
        {
            if (columnNames.Contains(CommonConstants.ExcelGetAll))
            {
                return new ResultModel<DataTable>(false, "", new DataTable());
            }
            var resultBase = new ResultModel<DataTable>();

            var countMatchedColumn = dataSource.Columns.Cast<DataColumn>().Count(column => columnNames.Any(columnName => column.ColumnName.ToLower().Equals(columnName.ToLower())));

            if (columnNames.Count != countMatchedColumn)
            {
                resultBase.IsError = true;
                resultBase.Message = "Tên cột cần lấy không đúng, vui lòng kiểm tra lại dữ liệu";
            }
            else
            {
                resultBase.IsError = false;
            }
            return resultBase;
        }

        public static ResultModel<DataTable> GetDataTableByColumnNames(DataTable dataSource, List<string> columnNames)
        {
            if (columnNames.Contains(CommonConstants.ExcelGetAll))
            {
                return new ResultModel<DataTable>(false, "", dataSource);
            }
            var resultBase = new ResultModel<DataTable>();
            var tempDataTable = new DataView(dataSource).ToTable(false, columnNames.ToArray());
            if (tempDataTable.Rows.Count > 0)
            {
                resultBase.IsError = false;
                resultBase.Data = tempDataTable;
            }
            else
            {
                resultBase.IsError = true;
            }
            return resultBase;
        }


        #region Enum function

       
        public static List<Select2Data> GroupCategoryEnumToList()
        {
            var result = (from GroupCategoryEnum item in Enum.GetValues(typeof(GroupCategoryEnum))
                          select new Select2Data()
                          {
                              Id = (int)item,
                              Text = item.ToDescription()
                          }).ToList();
            return result;
        }

        
        #endregion

        public static SqlParameter GetParameter(dynamic value, string name)
        {
            if (value is int)
            {
                return value != null ?
                    new SqlParameter(name, value) :
                    new SqlParameter(name, SqlInt32.Null);
            }
            if (value is long)
            {
                return value != null ?
                    new SqlParameter(name, value) :
                    new SqlParameter(name, SqlInt64.Null);
            }
            if (value is DateTime)
            {
                return value != null ?
                    new SqlParameter(name, value) :
                    new SqlParameter(name, SqlDateTime.Null);
            }
            if (value is double)
            {
                return value != null ?
                    new SqlParameter(name, value) :
                    new SqlParameter(name, SqlDouble.Null);
            }
            if (value is float)
            {
                return value != null ?
                    new SqlParameter(name, value) :
                    new SqlParameter(name, SqlDecimal.Null);
            }
            if (value is bool)
            {
                return value != null ?
                    new SqlParameter(name, value) :
                    new SqlParameter(name, SqlBoolean.Null);
            }
            return value != null ?
                new SqlParameter(name, value) :
                new SqlParameter(name, SqlString.Null);
        }

        public static string BoDauTiengViet(string TuGoc)
        {
            var vR = TuGoc;
            int i, cs;

            for (i = 0; i < vR.Length - 1; i++)
            {
                cs = VietNameCharacters.IndexOf(vR[i]);
                if (cs >= 0)
                {
                    vR = vR.Replace(VietNameCharacters[cs], VietNameCharacters[cs]);
                }
            }
            return vR;
        }
        public static bool CreateDirectory(string Dir)
        {
            if (Directory.Exists(Dir) == false)
            {
                try
                {
                    Directory.CreateDirectory(Dir);
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }

        public static string RegisterBodyEmail(UserModel parameter)
        {
            // Send email
            StringBuilder emailBody = new StringBuilder();
            emailBody.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"597px\" align=\"center\">");
            emailBody.Append("<tbody>");
            // Header Email
            emailBody.Append("<tr style=\"background:#2888c3\">");
            emailBody.Append("<td>");
            emailBody.Append("<h3 style=\"color:#ffffff;font-size:18px;font-weight:bold;text-align:center;\">Thông báo đăng ký từ hệ thống vnus2019.viasm.edu.vn</h3>");
            emailBody.AppendFormat("<p style=\"color:#ffffff;text-align:center;font-size:13px;font-weight:bold;font-style:italic;\">Ngày đăng ký: {0}</p>", parameter.SentTime);
            emailBody.Append("</td>");
            emailBody.Append("</tr>");
            // End Header Email

            // Body Email
            emailBody.Append("<tr>");
            emailBody.Append("<td style=\"background:#ffffff\">");

            emailBody.AppendFormat("<p style=\"padding-left:20px;margin-bottom:5px;\"><span style=\"color:#006c99\">Họ tên: </span><span style=\"font-weight:bold;\">{0}</span></p>", parameter.FullName);
            emailBody.AppendFormat("<p style=\"padding-left:20px;margin-bottom:5px;\"><span style=\"color:#006c99\">Giới tính: </span><span style=\"font-weight:bold;\">{0}</span></p>", parameter.Gender.HasValue ? (parameter.Gender.Value ? "Nam" : "Nữ") : "Nam");
            emailBody.AppendFormat("<p style=\"padding-left:20px;margin-bottom:5px;\"><span style=\"color:#006c99\">Đơn vị: </span><span style=\"font-weight:bold;\">{0}</span></p>", parameter.Organization);
            emailBody.AppendFormat("<p style=\"padding-left:20px;margin-bottom:5px;\"><span style=\"color:#006c99\">Địa chỉ: </span><span style=\"font-weight:bold;\">{0}</span></p>", parameter.Address);
            emailBody.AppendFormat("<p style=\"padding-left:20px;margin-bottom:5px;\"><span style=\"color:#006c99\">Email: </span><span style=\"font-weight:bold;\">{0}</span></p>", parameter.Email);
            emailBody.AppendFormat("<p style=\"padding-left:20px;margin-bottom:5px;\"><span style=\"color:#006c99\">Có đính kèm tệp tin: </span><span style=\"font-weight:bold;\">{0}</span></p>", parameter.HasAttachment ? "Có" : "Không");
            emailBody.AppendFormat("<p style=\"padding-left:20px;margin-bottom:5px;\"><span style=\"color:#006c99\">Ghi chú: </span><span style=\"font-weight:bold;\">{0}</span></p>", parameter.Note);

            emailBody.Append("</td>");
            emailBody.Append("</tr>");
            // End Body Email

            // Footer Email
            emailBody.Append("<tr>");
            emailBody.Append("<td style=\"background:#f5f5f5;border-top:1px solid #ccc;\">");

            emailBody.Append("<p style=\"padding-left: 20px;text-align: left;font-style: italic;\">Đây là thư tự động từ hệ thống. Vui lòng không trả lời thư này </p>");
            emailBody.Append("<p style=\"padding-left: 20px;text-align: left;font-style: italic;\">Vui lòng truy cập vào liên kết dưới đây để tới phần quản trị</p>");
            emailBody.Append("<p style=\"text-align:center;color:#15c;margin-top: 20px;margin-bottom: 30px;\"><a style=\"background: #15c;color: #fff;padding: 10px 20px;text-decoration: none;\" href=\"http://vnus2019.viasm.edu.vn/XPanel/Register\" target=\"_blank\">Liên kết</a></p>");

            emailBody.Append("</td>");
            emailBody.Append("</tr>");

            // Email Footer Email
            emailBody.Append("</tbody>");
            emailBody.Append("</table>");

            return emailBody.ToString();
        }
        public static string ContactBodyEmail(ContactModel parameter)
        {
            // Send email
            StringBuilder emailBody = new StringBuilder();
            emailBody.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"597px\" align=\"center\">");
            emailBody.Append("<tbody>");
            // Header Email
            emailBody.Append("<tr style=\"background:#2888c3\">");
            emailBody.Append("<td>");
            emailBody.Append("<h3 style=\"color:#ffffff;font-size:18px;font-weight:bold;text-align:center;\">Thông báo Liên hệ từ hệ thống vnus2019.viasm.edu.vn</h3>");
            emailBody.AppendFormat("<p style=\"color:#ffffff;text-align:center;font-size:13px;font-weight:bold;font-style:italic;\">Ngày liên hệ: {0}</p>", parameter.SentTime);
            emailBody.Append("</td>");
            emailBody.Append("</tr>");
            // End Header Email

            // Body Email
            emailBody.Append("<tr>");
            emailBody.Append("<td style=\"background:#ffffff\">");

            emailBody.AppendFormat("<p style=\"padding-left:20px;margin-bottom:5px;\"><span style=\"color:#006c99\">Tiêu đề: </span><span style=\"font-weight:bold;\">{0}</span></p>", parameter.Subject);
            emailBody.AppendFormat("<p style=\"padding-left:20px;margin-bottom:5px;\"><span style=\"color:#006c99\">Họ tên: </span><span style=\"font-weight:bold;\">{0}</span></p>", parameter.FullName);
            emailBody.AppendFormat("<p style=\"padding-left:20px;margin-bottom:5px;\"><span style=\"color:#006c99\">Email: </span><span style=\"font-weight:bold;\">{0}</span></p>", parameter.Email);
            emailBody.AppendFormat("<p style=\"padding-left:20px;margin-bottom:5px;\"><span style=\"color:#006c99\">Điện thoại: </span><span style=\"font-weight:bold;\">{0}</span></p>", parameter.PhoneNumber);
            emailBody.AppendFormat("<p style=\"padding-left:20px;margin-bottom:5px;\"><span style=\"color:#006c99\">Nội dung liên hệ: </span><span style=\"font-weight:bold;\">{0}</span></p>", parameter.Message);

            emailBody.Append("</td>");
            emailBody.Append("</tr>");
            // End Body Email

            // Footer Email
            emailBody.Append("<tr>");
            emailBody.Append("<td style=\"background:#f5f5f5;border-top:1px solid #ccc;\">");

            emailBody.Append("<p style=\"padding-left: 20px;text-align: left;font-style: italic;\">Đây là thư tự động từ hệ thống. Vui lòng không trả lời thư này </p>");
            emailBody.Append("<p style=\"padding-left: 20px;text-align: left;font-style: italic;\">Vui lòng truy cập vào liên kết dưới đây để tới phần quản trị</p>");
            emailBody.Append("<p style=\"text-align:center;color:#15c;margin-top: 20px;margin-bottom: 30px;\"><a style=\"background: #15c;color: #fff;padding: 10px 20px;text-decoration: none;\" href=\"http://vnus2019.viasm.edu.vn/XPanel/Contact\" target=\"_blank\">Liên kết</a></p>");

            emailBody.Append("</td>");
            emailBody.Append("</tr>");

            // Email Footer Email
            emailBody.Append("</tbody>");
            emailBody.Append("</table>");

            return emailBody.ToString();
        }
    }
}
