using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietNamUSA.Common
{
    public static class WebCommonFunctions
    {
        public static bool ClientCheckPermission(int[] permissions)
        {
            var user = HttpContext.Current.Session[SessionKeys.UserInfo] as UserInfoModel;
            if (user == null)
                return false;
            if (user.IsSuperAdministrator || permissions == null)
            {
                return true;
            }
            return user.Permissions != null && permissions.Any(t => user.Permissions.Contains(t));
        }

        public static string ExcelValidateColumnErrorMessage(int row, int column, string columnAddress, string errorMessage)
        {
            return string.Format("Dòng {0} cột {1}({2}): {3} </br>", row, column, columnAddress, errorMessage);
        }

        public static ResultModel<string> ValidateExcelDataType(int row, int column, string columnAddress, string errorMessage, string input, string dataType)
        {
            switch (dataType)
            {
                case "int":
                    int outNumber;
                    var isNumber = int.TryParse(input, out outNumber);
                    if (!isNumber)
                    {
                        return new ResultModel<string>(true,
                            string.Format("Dòng {0} cột {1}({2}): {3} </br>", row, column, columnAddress, errorMessage), input);
                    }
                    return new ResultModel<string>(false, string.Empty, input);
                case "float":
                    float outFloat;
                    var isFloat = float.TryParse(input, out outFloat);
                    if (!isFloat)
                    {
                        return new ResultModel<string>(true,
                            string.Format("Dòng {0} cột {1}({2}): {3} </br>", row, column, columnAddress, errorMessage), input);
                    }
                    return new ResultModel<string>(false, string.Empty, input);
            }
            return new ResultModel<string>();
        }

        public static ResultModel<bool> ValidateFileUpload(HttpFileCollectionBase files)
        {
            var result = new ResultModel<bool>(false, "", false);
            if (files.Count <= 0)
            {
                result.IsError = false;
                return result;
            }
            const int maxFileSize = 20480 * 1024; // 20MB
            //var allowedExtension = new[] { ".jpg", ".jpeg", ".png", ".doc", ".docx", ".xls", ".xlsx", ".pdf", ".txt" };
            var blacklistExtension = new[] {".asp",".aspx",".asa",".aSP",".aSpx",".aSa",".asp%20%20%20",".aspx%20%20%20",".asa%20%20%20",".aSP%20%20%20",".aSpx%20%20%20",".aSa%20%20%20",".asp......",".aspx......",".asa......",".aSP......",".aSpx......",".aSa......",".asp%20%20%20...%20.%20..",".aspx%20%20%20...%20.%20..",".asa%20%20%20...%20.%20..",".aSP%20%20%20...%20.%20.."
                ,".aSpx%20%20%20...%20.%20..",".aSa%20%20%20...%20.%20..",".asp%00",".aspx%00",".asa%00",".aSp%00",".aSpx%00",".aSa%00",".ani",".asf",".asp",".avi",".bas",".bat",".bin",".chm",".cmd",".com",".cpl",".cur",".eml",".exe",".hta",".ico",".js",".jse",".midi",".mp3",".mpa",".mpe",".mpeg",".mpg",".msc",".msp",".pcd",".pif",".reg",".scr",".sct",".vb",".vbs",".wma",".wmf",".wmv",".wsc",".wsf"};
            var totalFileSize = 0;
            foreach (var file in files)
            {
                var item = files[file.ToString()];
                if (item == null) continue;
                if (item.FileName.Length <= 0 || item.FileName.Length >= 256)
                {
                    result.IsError = true;
                    result.Message = "File name is too long or empty!";
                    return result;
                }
                var fileExtension = item.FileName.Substring(item.FileName.LastIndexOf('.'));
                if (blacklistExtension.Contains(fileExtension))
                {
                    result.IsError = true;
                    result.Message = "File type is not allowed, please contact to admin for this case";
                    return result;
                }
                totalFileSize += item.ContentLength;
            }
            if (totalFileSize < maxFileSize) return result;
            result.IsError = true;
            result.Message = "Upload size is only 20MB limited!";
            return result;
        }

        public static ResultModel<DocumentModel> CreateFile(DocumentModel parameter, HttpPostedFileBase file)
        {
            if (file.ContentLength <= 0)
                return new ResultModel<DocumentModel>
                {
                    IsError = true,
                    Message = string.Empty,
                    Data = new DocumentModel()
                    {
                        FileName = string.Empty,
                        FilePath = string.Empty
                    }
                };
            if (!Directory.Exists(parameter.FullFilePath))
            {
                CommonFunctions.CreateDirectory(parameter.FullFilePath);
            }
            var fileName = file.FileName.Replace(' ', '_');
            var arrFileName = fileName.Split('\\');
            fileName = arrFileName[arrFileName.Length - 1];
            fileName = fileName.Replace("  ", " ");
            fileName = CommonFunctions.BoDauTiengViet(fileName);
            arrFileName = fileName.Split('.');
            if (arrFileName.Length >= 2)
            {
                fileName = string.Format("{0}.{1}", arrFileName[0], arrFileName[arrFileName.Length - 1]);
            }
            fileName = string.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMdd_HHmmss"), fileName);
            file.SaveAs(string.Format("{0}\\{1}", parameter.FullFilePath, fileName));

            parameter.FileName = fileName;
            parameter.FilePath = parameter.FilePath + "/" + fileName;

            return new ResultModel<DocumentModel>()
            {
                IsError = false,
                Message = CommonConstants.Success,
                Data = parameter
            };
        }
    }
}