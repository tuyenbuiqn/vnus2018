using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using OfficeOpenXml.Style;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Repository.Common.Functions
{
    public static class CustomizeExcelExportFunctions
    {
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

        public static CUserExcelFormat DefaultHeaderFormat()
        {
            return new CUserExcelFormat()
            {
                FontBold = true,
                FontItalic = false,
                FontColor = "#000000",
                FontSize = 13,
                BackgroundColor = "#b8cce4",
                BorderColor = "#000000",
                BorderStyle = (int)ExcelBorderStyle.Thin,
                VerticalAlignment = (int)ExcelVerticalAlignment.Top,
                HorizontalAlignment = (int)ExcelHorizontalAlignment.Center
            };
        }
        public static CUserExcelFormat DefaultHeaderFormat(int objectTypeId, string objectTypeName, int userId, int formatStyleId)
        {
            return new CUserExcelFormat()
            {
                FontBold = true,
                FontItalic = false,
                FontColor = "#000000",
                FontSize = 13,
                BackgroundColor = "#b8cce4",
                BorderColor = "#000000",
                BorderStyle = (int)ExcelBorderStyle.Thin,
                VerticalAlignment = (int)ExcelVerticalAlignment.Top,
                HorizontalAlignment = (int)ExcelHorizontalAlignment.Center,
                ObjectTypeId = objectTypeId,
                ObjectTypeName = objectTypeName,
                UserId = userId,
                FormatStyleId = formatStyleId
            };
        }
        public static CUserExcelFormat DefaultBodyFormat()
        {
            return new CUserExcelFormat()
            {
                FontBold = false,
                FontItalic = false,
                FontColor = "#000000",
                FontSize = 12,
                BackgroundColor = "#ffffff",
                BorderColor = "#000000",
                BorderStyle = (int)ExcelBorderStyle.Thin,
                VerticalAlignment = (int)ExcelVerticalAlignment.Top,
                HorizontalAlignment = (int)ExcelHorizontalAlignment.Left
            };
        }

        public static CUserExcelFormat DefaultBodyFormat(int objectTypeId, string objectTypeName, int userId, int formatStyleId)
        {
            return new CUserExcelFormat()
            {
                FontBold = false,
                FontItalic = false,
                FontColor = "#000000",
                FontSize = 12,
                BackgroundColor = "#ffffff",
                BorderColor = "#000000",
                BorderStyle = (int)ExcelBorderStyle.Thin,
                VerticalAlignment = (int)ExcelVerticalAlignment.Top,
                HorizontalAlignment = (int)ExcelHorizontalAlignment.Left,
                ObjectTypeId = objectTypeId,
                ObjectTypeName = objectTypeName,
                UserId = userId,
                FormatStyleId = formatStyleId
            };
        }

    }
}
