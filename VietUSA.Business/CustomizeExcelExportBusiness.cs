using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using VietUSA.Business.Interfaces;
using VietUSA.Repository;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Enums;
using VietUSA.Repository.Common.Extensions;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Business
{
    public class CustomizeExcelExportBusiness : BaseBusiness<CustomizeExcelExportRepository>, ICustomizeExcelExportBusiness
    {
        private readonly ICommonBusiness _commonBusiness;
        public CustomizeExcelExportBusiness(UserInfoModel userInfoModel) : base(new CustomizeExcelExportRepository(userInfoModel))
        {
            CurrentUser = userInfoModel;
            _commonBusiness = new CommonBusiness(CurrentUser);
        }

        public ResultModel<byte[]> ExportCustomizeExcelData<T>(List<T> data, CExportExcelInputModel excelParameter)
        {
            if (!data.Any()) return new ResultModel<byte[]>(true, CommonConstants.DataNull, null);
            var convertedDatatable = CustomizeExcelExportFunctions.ListToDataTable(data);
            if (convertedDatatable.IsError) return new ResultModel<byte[]>(true, convertedDatatable.Message, null);

            var userConfigurationData = GetUserExportExcel(excelParameter);
            if (userConfigurationData.IsError || (!userConfigurationData.IsError && userConfigurationData.Data == null)) return new ResultModel<byte[]>(true, userConfigurationData.Message, null);
            if (userConfigurationData.Data.UserExcelFile == null || (userConfigurationData.Data.UserExcelFile != null && userConfigurationData.Data.UserExcelFile.CExcelFileId <= 0))
                return new ResultModel<byte[]>(true, userConfigurationData.Message, null);
            if ((userConfigurationData.Data.UserExcelColumns == null) || (userConfigurationData.Data.UserExcelColumns != null && userConfigurationData.Data.UserExcelColumns.Count <= 0))
                return new ResultModel<byte[]>(true, userConfigurationData.Message, null);
            // Fix template file name
            userConfigurationData.Data.UserExcelFile.TemplateFileName = excelParameter.TemplateFileName;
            var columnNames = userConfigurationData.Data.UserExcelColumns.Select(item => item.ColumnName).ToList();
            var validateColumnNames = CustomizeExcelExportFunctions.ValidateColumnNames(convertedDatatable.Data, columnNames);
            if (validateColumnNames.IsError) return new ResultModel<byte[]>(true, validateColumnNames.Message, null);
            var dataToExport = CustomizeExcelExportFunctions.GetDataTableByColumnNames(convertedDatatable.Data, columnNames);
            if (dataToExport.IsError) return new ResultModel<byte[]>(true, dataToExport.Message, null);

            dataToExport.Data.SetColumnsOrder(columnNames.ToArray());
            byte[] fileXls;
            using (var p = new ExcelPackage(new FileInfo(userConfigurationData.Data.UserExcelFile.TemplateFileName), true))
            {
                var rowIndex = 0;
                var ws = p.Workbook.Worksheets[userConfigurationData.Data.UserExcelFile.SheetIndex ?? 1];
                // Format header
                using (var excelRange = ws.Cells[(userConfigurationData.Data.UserExcelFile.StartRow ?? 1), (userConfigurationData.Data.UserExcelFile.StartColumn ?? 1), (userConfigurationData.Data.UserExcelFile.StartRow ?? 1) + 1, dataToExport.Data.Columns.Count + (userConfigurationData.Data.UserExcelFile.StartColumn ?? 1)])
                {
                    var headerBorderColor = ColorTranslator.FromHtml(userConfigurationData.Data.HeaderFormat.BorderColor);
                    var headerFontColor = ColorTranslator.FromHtml(userConfigurationData.Data.HeaderFormat.FontColor);
                    var headerBackgroundColor = ColorTranslator.FromHtml(userConfigurationData.Data.HeaderFormat.BackgroundColor);

                    // Header Border
                    if (userConfigurationData.Data.HeaderFormat.ExcelBorderStyle != ExcelBorderStyle.None)
                    {
                        excelRange.Style.Border.Top.Style = userConfigurationData.Data.HeaderFormat.ExcelBorderStyle;
                        excelRange.Style.Border.Top.Color.SetColor(headerBorderColor);
                        excelRange.Style.Border.Right.Style = userConfigurationData.Data.HeaderFormat.ExcelBorderStyle;
                        excelRange.Style.Border.Right.Color.SetColor(headerBorderColor);
                        excelRange.Style.Border.Bottom.Style = userConfigurationData.Data.HeaderFormat.ExcelBorderStyle;
                        excelRange.Style.Border.Bottom.Color.SetColor(headerBorderColor);
                        excelRange.Style.Border.Left.Style = userConfigurationData.Data.HeaderFormat.ExcelBorderStyle;
                        excelRange.Style.Border.Left.Color.SetColor(headerBorderColor);
                    }
                    //excelRange.Style.WrapText = false;
                    // Header Font Color
                    excelRange.Style.Font.Color.SetColor(headerFontColor);
                    excelRange.Style.Font.Bold = userConfigurationData.Data.HeaderFormat.FontBold ?? true;
                    excelRange.Style.Font.Size = userConfigurationData.Data.HeaderFormat.FontSize ?? 13;
                    excelRange.Style.Font.Italic = userConfigurationData.Data.HeaderFormat.FontItalic ?? false;
                    excelRange.Style.VerticalAlignment = userConfigurationData.Data.HeaderFormat.ExcelVerticalAlignment;
                    excelRange.Style.HorizontalAlignment = userConfigurationData.Data.HeaderFormat.ExcelHorizontalAlignment;
                    // Header Background Color
                    excelRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    excelRange.Style.Fill.BackgroundColor.SetColor(headerBackgroundColor);

                    excelRange.AutoFitColumns();
                }

                // Format Body
                using (var excelRange = ws.Cells[(userConfigurationData.Data.UserExcelFile.StartRow ?? 1) + 1, (userConfigurationData.Data.UserExcelFile.StartColumn ?? 1), dataToExport.Data.Rows.Count + 1 + (userConfigurationData.Data.UserExcelFile.StartRow ?? 1), dataToExport.Data.Columns.Count + (userConfigurationData.Data.UserExcelFile.StartColumn ?? 1)])
                {
                    var bodyBorderColor = ColorTranslator.FromHtml(userConfigurationData.Data.BodyFormat.BorderColor);
                    var bodyFontColor = ColorTranslator.FromHtml(userConfigurationData.Data.BodyFormat.FontColor);
                    var bodyBackgroundColor = ColorTranslator.FromHtml(userConfigurationData.Data.BodyFormat.BackgroundColor);

                    // Body Border
                    if (userConfigurationData.Data.BodyFormat.ExcelBorderStyle != ExcelBorderStyle.None)
                    {
                        excelRange.Style.Border.Top.Style = userConfigurationData.Data.BodyFormat.ExcelBorderStyle;
                        excelRange.Style.Border.Top.Color.SetColor(bodyBorderColor);
                        excelRange.Style.Border.Right.Style = userConfigurationData.Data.BodyFormat.ExcelBorderStyle;
                        excelRange.Style.Border.Right.Color.SetColor(bodyBorderColor);
                        excelRange.Style.Border.Bottom.Style = userConfigurationData.Data.BodyFormat.ExcelBorderStyle;
                        excelRange.Style.Border.Bottom.Color.SetColor(bodyBorderColor);
                        excelRange.Style.Border.Left.Style = userConfigurationData.Data.BodyFormat.ExcelBorderStyle;
                        excelRange.Style.Border.Left.Color.SetColor(bodyBorderColor);
                    }
                    // Body Font Color
                    excelRange.Style.Font.Color.SetColor(bodyFontColor);
                    excelRange.Style.Font.Bold = userConfigurationData.Data.BodyFormat.FontBold ?? false;
                    excelRange.Style.Font.Size = userConfigurationData.Data.BodyFormat.FontSize ?? 12;
                    excelRange.Style.Font.Italic = userConfigurationData.Data.BodyFormat.FontItalic ?? false;
                    excelRange.Style.VerticalAlignment = userConfigurationData.Data.BodyFormat.ExcelVerticalAlignment;
                    excelRange.Style.HorizontalAlignment = userConfigurationData.Data.BodyFormat.ExcelHorizontalAlignment;
                    //excelRange.Style.WrapText = false;
                    // Body Background Color
                    excelRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    excelRange.Style.Fill.BackgroundColor.SetColor(bodyBackgroundColor);

                    excelRange.AutoFitColumns();
                }

                for (var i = 0; i < (dataToExport.Data.Columns.Count); i++)
                {
                    ws.Cells[(userConfigurationData.Data.UserExcelFile.StartRow ?? 1)  + rowIndex, (userConfigurationData.Data.UserExcelFile.StartColumn ?? 1) + i].Value = userConfigurationData.Data.UserExcelColumns[i].DisplayName;
                    ws.Cells[(userConfigurationData.Data.UserExcelFile.StartRow ?? 1)  + rowIndex, (userConfigurationData.Data.UserExcelFile.StartColumn ?? 1) + i].AutoFitColumns();
                }
                for (var i = 0; i < dataToExport.Data.Rows.Count; i++)
                {
                    for (var j = 0; j < dataToExport.Data.Columns.Count; j++)
                    {
                        ws.Cells[((userConfigurationData.Data.UserExcelFile.StartRow ?? 1)) + 1 + rowIndex, (userConfigurationData.Data.UserExcelFile.StartColumn ?? 1) + j].Value = dataToExport.Data.Rows[i][j];
                        ws.Cells[((userConfigurationData.Data.UserExcelFile.StartRow ?? 1)) + 1 + rowIndex, (userConfigurationData.Data.UserExcelFile.StartColumn ?? 1) + j].AutoFitColumns();
                    }
                    rowIndex++;
                }
                fileXls = p.GetAsByteArray();
            }
            return new ResultModel<byte[]>(false, CommonConstants.Success, fileXls);
        }
        public ResultModel<byte[]> ExportSimpleExcelTemplate<T>(List<T> data, CustomizeExportExcelModel excelParameter)
        {
            try
            {
                if (!data.Any()) return new ResultModel<byte[]>(true, CommonConstants.DataNull, null);
                var convertedDatatable = CustomizeExcelExportFunctions.ListToDataTable(data);
                if (convertedDatatable.IsError) return new ResultModel<byte[]>(true, convertedDatatable.Message, null);
                var validateColumnNames = CustomizeExcelExportFunctions.ValidateColumnNames(convertedDatatable.Data, excelParameter.ColumnNames);
                if (validateColumnNames.IsError) return new ResultModel<byte[]>(true, validateColumnNames.Message, null);
                var dataToExport = CustomizeExcelExportFunctions.GetDataTableByColumnNames(convertedDatatable.Data, excelParameter.ColumnNames);
                if (dataToExport.IsError) return new ResultModel<byte[]>(true, dataToExport.Message, null);
                byte[] fileXls;
                using (var p = new ExcelPackage(new FileInfo(excelParameter.UserExcelFile.TemplateFileName), true))
                {
                    var rowIndex = 0;
                    var ws = p.Workbook.Worksheets[excelParameter.UserExcelFile.SheetIndex ?? 1];
                    // Format header
                    using (var excelRange = ws.Cells[excelParameter.UserExcelFile.StartRow ?? 1, excelParameter.UserExcelFile.StartColumn ?? 1, excelParameter.UserExcelFile.StartRow ?? 1 + 1, dataToExport.Data.Columns.Count + excelParameter.UserExcelFile.StartColumn ?? 1])
                    {
                        var headerBorderColor = ColorTranslator.FromHtml(excelParameter.HeaderFormat.BorderColor);
                        var headerFontColor = ColorTranslator.FromHtml(excelParameter.HeaderFormat.FontColor);
                        var headerBackgroundColor = ColorTranslator.FromHtml(excelParameter.HeaderFormat.BackgroundColor);

                        // Header Border
                        if (excelParameter.HeaderFormat.ExcelBorderStyle != ExcelBorderStyle.None)
                        {
                            excelRange.Style.Border.Top.Style = excelParameter.HeaderFormat.ExcelBorderStyle;
                            excelRange.Style.Border.Top.Color.SetColor(headerBorderColor);
                            excelRange.Style.Border.Right.Style = excelParameter.HeaderFormat.ExcelBorderStyle;
                            excelRange.Style.Border.Right.Color.SetColor(headerBorderColor);
                            excelRange.Style.Border.Bottom.Style = excelParameter.HeaderFormat.ExcelBorderStyle;
                            excelRange.Style.Border.Bottom.Color.SetColor(headerBorderColor);
                            excelRange.Style.Border.Left.Style = excelParameter.HeaderFormat.ExcelBorderStyle;
                            excelRange.Style.Border.Left.Color.SetColor(headerBorderColor);
                        }
                        //excelRange.Style.WrapText = false;
                        // Header Font Color
                        excelRange.Style.Font.Color.SetColor(headerFontColor);
                        excelRange.Style.Font.Bold = excelParameter.HeaderFormat.FontBold ?? true;
                        excelRange.Style.Font.Size = excelParameter.HeaderFormat.FontSize ?? 13;
                        excelRange.Style.Font.Italic = excelParameter.HeaderFormat.FontItalic ?? false;
                        excelRange.Style.VerticalAlignment = excelParameter.HeaderFormat.ExcelVerticalAlignment;
                        excelRange.Style.HorizontalAlignment = excelParameter.HeaderFormat.ExcelHorizontalAlignment;
                        // Header Background Color
                        excelRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        excelRange.Style.Fill.BackgroundColor.SetColor(headerBackgroundColor);

                        excelRange.AutoFitColumns();
                    }

                    // Format Body
                    using (var excelRange = ws.Cells[excelParameter.UserExcelFile.StartRow ?? 1 + 1, excelParameter.UserExcelFile.StartColumn ?? 1, dataToExport.Data.Rows.Count + 1 + excelParameter.UserExcelFile.StartRow ?? 1, dataToExport.Data.Columns.Count + excelParameter.UserExcelFile.StartColumn ?? 1])
                    {
                        var bodyBorderColor = ColorTranslator.FromHtml(excelParameter.BodyFormat.BorderColor);
                        var bodyFontColor = ColorTranslator.FromHtml(excelParameter.BodyFormat.FontColor);
                        var bodyBackgroundColor = ColorTranslator.FromHtml(excelParameter.BodyFormat.BackgroundColor);

                        // Body Border
                        if (excelParameter.BodyFormat.ExcelBorderStyle != ExcelBorderStyle.None)
                        {
                            excelRange.Style.Border.Top.Style = excelParameter.BodyFormat.ExcelBorderStyle;
                            excelRange.Style.Border.Top.Color.SetColor(bodyBorderColor);
                            excelRange.Style.Border.Right.Style = excelParameter.BodyFormat.ExcelBorderStyle;
                            excelRange.Style.Border.Right.Color.SetColor(bodyBorderColor);
                            excelRange.Style.Border.Bottom.Style = excelParameter.BodyFormat.ExcelBorderStyle;
                            excelRange.Style.Border.Bottom.Color.SetColor(bodyBorderColor);
                            excelRange.Style.Border.Left.Style = excelParameter.BodyFormat.ExcelBorderStyle;
                            excelRange.Style.Border.Left.Color.SetColor(bodyBorderColor);
                        }
                        // Body Font Color
                        excelRange.Style.Font.Color.SetColor(bodyFontColor);
                        excelRange.Style.Font.Bold = excelParameter.BodyFormat.FontBold ?? false;
                        excelRange.Style.Font.Size = excelParameter.BodyFormat.FontSize ?? 12;
                        excelRange.Style.Font.Italic = excelParameter.BodyFormat.FontItalic ?? false;
                        excelRange.Style.VerticalAlignment = excelParameter.BodyFormat.ExcelVerticalAlignment;
                        excelRange.Style.HorizontalAlignment = excelParameter.BodyFormat.ExcelHorizontalAlignment;
                        //excelRange.Style.WrapText = false;
                        // Body Background Color
                        excelRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        excelRange.Style.Fill.BackgroundColor.SetColor(bodyBackgroundColor);

                        excelRange.AutoFitColumns();
                    }

                    for (var i = 0; i < dataToExport.Data.Rows.Count; i++)
                    {
                        for (var j = 0; j < dataToExport.Data.Columns.Count; j++)
                        {
                            ws.Cells[excelParameter.UserExcelFile.StartRow ?? 1 + 1 + rowIndex, excelParameter.UserExcelFile.StartColumn ?? 1 + j].Value = dataToExport.Data.Rows[i][j];
                            ws.Cells[excelParameter.UserExcelFile.StartRow ?? 1 + 1 + rowIndex, excelParameter.UserExcelFile.StartColumn ?? 1 + j].AutoFitColumns();
                        }
                        rowIndex++;
                    }
                    fileXls = p.GetAsByteArray();
                }
                return new ResultModel<byte[]>(false, CommonConstants.Success, fileXls);
            }
            catch (Exception exception)
            {
                return new ResultModel<byte[]>(true, exception.Message, null);
            }
        }
        public ResultModel<bool> UpdateExcelFormat(CUserExcelFormat parameter)
        {
            try
            {
                var data = MainRepository.UpdateExcelFormat(parameter);

                _commonBusiness.InsertLog(new LogModel
                {
                    ModuleId = (int)ObjectTypeEnum.UserExcelFile,
                    Action = "Update User Excel Format",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = LogType.Update,
                    LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(parameter)
                });
                return data;
            }
            catch (Exception exception)
            {
                return new ResultModel<bool>(true, exception.Message, false);
            }
        }
        public ResultModel<bool> UpdateExcelFile(CUserExcelFile parameter)
        {
            try
            {
                var data = MainRepository.UpdateExcelFile(parameter);

                _commonBusiness.InsertLog(new LogModel
                {
                    ModuleId = (int)ObjectTypeEnum.UserExcelFile,
                    Action = "Update User Excel File",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = LogType.Update,
                    LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(parameter)
                });
                return data;
            }
            catch (Exception exception)
            {
                return new ResultModel<bool>(true, exception.Message, false);
            }
        }
        public ResultModel<CustomizeExportExcelModel> GetUserExportExcel(CExportExcelInputModel parameter)
        {
            if (parameter == null || ((parameter.UserId <= 0 || parameter.ObjectTypeId <= 0)))
            {
                return new ResultModel<CustomizeExportExcelModel>()
                {
                    Data = new CustomizeExportExcelModel(),
                    IsError = true,
                    Message = CommonConstants.InvalidInput + "(UserId/ObjectTypeId)!"
                };
            }
            var result = new ResultModel<CustomizeExportExcelModel>()
            {
                Data = new CustomizeExportExcelModel()
                {
                    UserExcelFile = new CUserExcelFile(),
                    HeaderFormat = new CUserExcelFormat(),
                    BodyFormat = new CUserExcelFormat(),
                    ExportExcelInputModel = parameter
                }
            };

            // 1. UserExcelFile
            var userExcelFileData = MainRepository.GetUserExcelFile(new CUserExcelFile()
            {
                UserId = parameter.UserId,
                ObjectTypeId = parameter.ObjectTypeId
            });

            if (userExcelFileData.IsError)
            {
                result.IsError = true;
                result.Message = userExcelFileData.Message;
                return result;
            }
            result.Data.UserExcelFile = userExcelFileData.Data;
            // 2. Format
            // 2.1. Header Format
            var headerFormatData = MainRepository.GetUserExcelFormat(new CUserExcelFormat()
            {
                FormatStyleId = (int)CExcelFormatTypeEnum.Header,
                UserId = parameter.UserId,
                ObjectTypeId = parameter.ObjectTypeId,
                CExcelFileId = userExcelFileData.Data.CExcelFileId
            });
            if (headerFormatData.IsError || headerFormatData.Data == null ||
                (headerFormatData.Data != null && headerFormatData.Data.CExcelFormatId < 0))
            {
                var defaultHeaderFormat = CustomizeExcelExportFunctions.DefaultHeaderFormat(parameter.ObjectTypeId,
                    parameter.ObjectTypeName, parameter.UserId, (int)CExcelFormatTypeEnum.Header);
                result.Data.HeaderFormat = defaultHeaderFormat;
            }
            else
            {
                result.Data.HeaderFormat = headerFormatData.Data;
            }
            // 2.2. Body Format
            var bodyFormatData = MainRepository.GetUserExcelFormat(new CUserExcelFormat()
            {
                FormatStyleId = (int)CExcelFormatTypeEnum.Body,
                UserId = parameter.UserId,
                ObjectTypeId = parameter.ObjectTypeId,
                CExcelFileId = userExcelFileData.Data.CExcelFileId
            });
            if (bodyFormatData.IsError || bodyFormatData.Data == null ||
                (bodyFormatData.Data != null && bodyFormatData.Data.CExcelFormatId < 0))
            {
                var defaultbodyFormat = CustomizeExcelExportFunctions.DefaultHeaderFormat(parameter.ObjectTypeId,
                    parameter.ObjectTypeName, parameter.UserId, (int)CExcelFormatTypeEnum.Body);
                result.Data.BodyFormat = defaultbodyFormat;
            }
            else
            {
                result.Data.BodyFormat = bodyFormatData.Data;
            }
            // 3. Excel Column
            var excelColumnData = MainRepository.GetUserExcelColumn(new CUserExcelColumn()
            {
                ObjectTypeId = parameter.ObjectTypeId,
                UserId = parameter.UserId,
                CExcelFileId = userExcelFileData.Data.CExcelFileId
            });
            if (excelColumnData.IsError || excelColumnData.Data == null ||
                (excelColumnData.Data != null && excelColumnData.Data.Count < 0))
            {

            }
            else
            {
                result.Data.UserExcelColumns = excelColumnData.Data;
            }

            // 4. Unseleted Excel Column
            var unselectedExcelColumnData = MainRepository.GetUserExcelUnselectedColumn(new CUserExcelColumn()
            {
                ObjectTypeId = parameter.ObjectTypeId,
                UserId = parameter.UserId,
                CExcelFileId = userExcelFileData.Data.CExcelFileId
            });
            if (unselectedExcelColumnData.IsError || unselectedExcelColumnData.Data == null ||
                (unselectedExcelColumnData.Data != null && unselectedExcelColumnData.Data.Count < 0))
            {

            }
            else
            {
                result.Data.UserExcelUnSelectedColumns = unselectedExcelColumnData.Data;
            }
            return result;
        }

        public ResultModel<CustomizeExportExcelModel> UpdateExcelColumn(CUserExcelColumn parameter)
        {
            var result = new ResultModel<CustomizeExportExcelModel>()
            {
                Data = new CustomizeExportExcelModel()
                {
                    UserExcelColumns = new List<CUserExcelColumn>(),
                    UserExcelUnSelectedColumns = new List<CUserExcelColumn>()
                }
            };
            var updateResult = MainRepository.UpdateExcelColumn(parameter);

            result.IsError = updateResult.IsError;
            result.Message = updateResult.Message;

            // 1. UserExcelFile
            var userExcelFileData = MainRepository.GetUserExcelFile(new CUserExcelFile()
            {
                UserId = parameter.UserId,
                ObjectTypeId = parameter.ObjectTypeId
            });

            if (userExcelFileData.IsError)
            {
                result.IsError = true;
                result.Message = userExcelFileData.Message;
                return result;
            }
            result.Data.UserExcelFile = userExcelFileData.Data;

            // 3. Excel Column
            var excelColumnData = MainRepository.GetUserExcelColumn(new CUserExcelColumn()
            {
                ObjectTypeId = parameter.ObjectTypeId,
                UserId = parameter.UserId,
                CExcelFileId = parameter.CExcelFileId
            });
            if (excelColumnData.IsError || excelColumnData.Data == null ||
                (excelColumnData.Data != null && excelColumnData.Data.Count < 0))
            {
            }
            else
            {
                result.Data.UserExcelColumns = excelColumnData.Data;
            }

            // 4. Unseleted Excel Column
            var unselectedExcelColumnData = MainRepository.GetUserExcelUnselectedColumn(new CUserExcelColumn()
            {
                ObjectTypeId = parameter.ObjectTypeId,
                UserId = parameter.UserId,
                CExcelFileId = parameter.CExcelFileId
            });
            if (unselectedExcelColumnData.IsError || unselectedExcelColumnData.Data == null ||
                (unselectedExcelColumnData.Data != null && unselectedExcelColumnData.Data.Count < 0))
            {
            }
            else
            {
                result.Data.UserExcelUnSelectedColumns = unselectedExcelColumnData.Data;
            }

            return result;
        }

        public ResultModel<CustomizeExportExcelModel> UpdateExcelColumnOrder(CExportUpdateExcelColumnParameter parameter)
        {
            var result = new ResultModel<CustomizeExportExcelModel>()
            {
                Data = new CustomizeExportExcelModel()
                {
                    UserExcelColumns = new List<CUserExcelColumn>(),
                    UserExcelUnSelectedColumns = new List<CUserExcelColumn>()
                }
            };
            // New DataTable columns
            parameter.SelectedData = CommonFunctions.ListToDataTable(parameter.SelectedColumns).Data;
            parameter.UnSelectedData = CommonFunctions.ListToDataTable(parameter.UnSelectedColumns).Data;

            var updateResult = MainRepository.UpdateExcelColumnOrder(parameter);

            result.IsError = updateResult.IsError;
            result.Message = updateResult.Message;

            // 1. UserExcelFile
            var userExcelFileData = MainRepository.GetUserExcelFile(new CUserExcelFile()
            {
                UserId = parameter.UserId,
                ObjectTypeId = parameter.ObjectTypeId
            });

            if (userExcelFileData.IsError)
            {
                result.IsError = true;
                result.Message = userExcelFileData.Message;
                return result;
            }
            result.Data.UserExcelFile = userExcelFileData.Data;

            // 3. Excel Column
            var excelColumnData = MainRepository.GetUserExcelColumn(new CUserExcelColumn()
            {
                ObjectTypeId = parameter.ObjectTypeId,
                UserId = parameter.UserId,
                CExcelFileId = parameter.CExcelFileId
            });
            if (excelColumnData.IsError || excelColumnData.Data == null ||
                (excelColumnData.Data != null && excelColumnData.Data.Count < 0))
            {
            }
            else
            {
                result.Data.UserExcelColumns = excelColumnData.Data;
            }

            // 4. Unseleted Excel Column
            var unselectedExcelColumnData = MainRepository.GetUserExcelUnselectedColumn(new CUserExcelColumn()
            {
                ObjectTypeId = parameter.ObjectTypeId,
                UserId = parameter.UserId,
                CExcelFileId = parameter.CExcelFileId
            });
            if (unselectedExcelColumnData.IsError || unselectedExcelColumnData.Data == null ||
                (unselectedExcelColumnData.Data != null && unselectedExcelColumnData.Data.Count < 0))
            {
            }
            else
            {
                result.Data.UserExcelUnSelectedColumns = unselectedExcelColumnData.Data;
            }

            return result;
        }
    }
}
