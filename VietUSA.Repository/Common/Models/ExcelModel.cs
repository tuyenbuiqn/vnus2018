using System.Collections.Generic;

namespace VietUSA.Repository.Common.Models
{
    public class ExcelModel
    {
        public int StartRow { get; set; }
        public int StartColumn { get; set; }
        public int TotalColumns { get; set; }
        public string TemplateFileName { get; set; }
        public string FileName { get; set; }
        public List<string> ColumnNames { get; set; }

        public bool IsUseEmptyTemplate { get; set; }
        public int SheetIndex { get; set; }

        public ExcelModel()
        {
            ColumnNames = new List<string>();
            SheetIndex = 1;
            StartRow = 1;
            StartColumn = 3;
        }

        public dynamic Data { get; set; }
    }

    //public class ExportExcelModel
    //{
    //     public int StartRow { get; set; }
    //    public int StartColumn { get; set; }
    //    public int TotalColumns { get; set; }
    //}
    public class ExportMultipleSheetExcelModel
    {
        public ExcelModel ExcelModel { get; set; }
        public List<ExcelModel> ExcelModels { get; set; }
        public int TotalSheet { get; set; }
    }

    /*Customize Excel Export*/
    /*
     1 sheet
     Thong tin ve Header
     - Ten hien thi
     - Order
     - Format(font-size, font-weight, font-color, background-color, border:none/has border, border-size, border-color)
     - Start Row, Start Column
     */

    //public class CUserExcelFile
    //{
    //    public string TemplateFileName { get; set; }
    //    public int StartRow { get; set; }
    //    public int StartColumn { get; set; }
    //    public string FileName { get; set; }
    //    public int SheetIndex { get; set; }
    //    public string SheetName { get; set; }

    //    public CUserExcelFile()
    //    {
    //        StartRow = 1;
    //        StartColumn = 1;
    //        FileName = string.Format("_{0}", DateTime.Now.ToString("yyyyMMdd_hhmmss"));
    //        SheetIndex = 1;
    //        SheetName = "Sheet";
    //    }
    //}
    //public class CExcelFormat
    //{
    //    public int ObjectTypeId { get; set; }
    //    public string ObjectTypeName { get; set; }
    //    /// <summary>
    //    /// Header Format/Body Format/Footer Format
    //    /// </summary>
    //    public int FormatTypeId { get; set; }
    //    /// <summary>
    //    /// Header Format/Body Format/Footer Format
    //    /// </summary>
    //    public string FormatTypeName { get; set; }
    //    public int UserId { get; set; }
    //    public float FontSize { get; set; }
    //    public string FontFamily { get; set; }
    //    public bool FontBold { get; set; }
    //    public bool FontItalic { get; set; }
    //    public string FontColor { get; set; }
    //    /// <summary>
    //    /// Căn theo chiều dọc
    //    /// </summary>
    //    public int VerticalAlignment { get; set; }
    //    /// <summary>
    //    /// Căn theo chiều dọc
    //    /// </summary>
    //    public ExcelVerticalAlignment ExcelVerticalAlignment => (ExcelVerticalAlignment) VerticalAlignment;
    //    /// <summary>
    //    /// Căn theo chiều ngang
    //    /// </summary>
    //    public int HorizontalAlignment { get; set; }
    //    /// <summary>
    //    /// Căn theo chiều ngang
    //    /// </summary>
    //    public ExcelHorizontalAlignment ExcelHorizontalAlignment => (ExcelHorizontalAlignment)HorizontalAlignment;
    //    public string BackgroundColor { get; set; }
    //    public int BorderStyle { get; set; }
    //    public ExcelBorderStyle ExcelBorderStyle => (ExcelBorderStyle)BorderStyle;
    //    public string BorderColor { get; set; }
    //    public CExcelFormat()
    //    {

    //    }
    //}

    //public class CExcelExportModel
    //{
    //    public CUserExcelFile UserExcelFile { get; set; }
    //    public CExcelFormat HeaderFormat { get; set; }
    //    public CExcelFormat BodyFormat { get; set; }
    //    public List<string> ColumnNames { get; set; }
    //    public dynamic ExportData { get; set; }

    //    public CExcelExportModel()
    //    {
    //        HeaderFormat = CustomizeExcelExportFunctions.DefaultHeaderFormat();
    //        BodyFormat = CustomizeExcelExportFunctions.DefaultBodyFormat();
    //        UserExcelFile = new CUserExcelFile();
    //    }
    //}
}
