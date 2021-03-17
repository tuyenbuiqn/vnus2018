using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using OfficeOpenXml.Style;
using VietUSA.Repository.Common.Functions;

namespace VietUSA.Repository.Models
{
    public class CustomizeExportExcelModel
    {
        public CExportExcelInputModel ExportExcelInputModel { get; set; }
        public CUserExcelFile UserExcelFile { get; set; }
        public CUserExcelFormat HeaderFormat { get; set; }
        public CUserExcelFormat BodyFormat { get; set; }
        public List<CUserExcelColumn> UserExcelColumns { get; set; }
        public List<CUserExcelColumn> UserExcelUnSelectedColumns { get; set; }
        public List<string> ColumnNames { get; set; }
        public dynamic ExportData { get; set; }

        public CustomizeExportExcelModel()
        {
            ExportExcelInputModel = new CExportExcelInputModel();
            UserExcelFile = new CUserExcelFile();
            HeaderFormat = CustomizeExcelExportFunctions.DefaultHeaderFormat();
            BodyFormat = CustomizeExcelExportFunctions.DefaultBodyFormat();
            UserExcelColumns = new List<CUserExcelColumn>();
            UserExcelUnSelectedColumns = new List<CUserExcelColumn>();
        }
    }

    public class CExcelColumnParameter
    {
        public long CUserExcelColumnId { get; set; }
        public long CExcelColumnId { get; set; }
        public string ColumnName { get; set; }

        public string DisplayName { get; set; }
        public long? CExcelFileId { get; set; }
        public int? UserId { get; set; }
        public int? ObjectTypeId { get; set; }
    }
    public class CExportUpdateExcelColumnParameter
    {
        public long CExcelFileId { get; set; }
        public int ObjectTypeId { get; set; }
        public int UserId { get; set; }

        public List<CExcelColumnParameter> SelectedColumns { get; set; }
        public List<CExcelColumnParameter> UnSelectedColumns { get; set; }
        public DataTable SelectedData { get; set; }
        public DataTable UnSelectedData { get; set; }
    }
    public class CExportExcelInputModel
    {
        public int UserId { get; set; }
        public int ObjectTypeId { get; set; }
        public string ObjectTypeName { get; set; }
        public string StringParameter { get; set; }
        public string TemplateFileName { get; set; }
        public string FileName { get; set; }
    }

    public  class CUserExcelFile
    {
        public long CUserExcelFileId { get; set; }
        public long CExcelFileId { get; set; }
        [StringLength(500)]
        public string TemplateFileName { get; set; }
        [StringLength(500)]
        public string TemplateFilePath { get; set; }
        public int? StartRow { get; set; }
        public int? StartColumn { get; set; }
        [StringLength(500)]
        public string FileName { get; set; }
        public int? SheetIndex { get; set; }
        [StringLength(500)]
        public string SheetName { get; set; }
        public int? UserId { get; set; }
        public int? ObjectTypeId { get; set; }
        [StringLength(500)]
        public string ObjectTypeName { get; set; }
        public int? DataStatusType { get; set; }
        [Column(TypeName = "ntext")]
        public string Note { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }
        public CUserExcelFile()
        {
            StartRow = 1;
            StartColumn = 1;
            FileName = string.Format("_{0}", DateTime.Now.ToString("yyyyMMdd_hhmmss"));
            SheetIndex = 1;
            SheetName = "Sheet";
        }
    }

    public class CExcelColumn
    {
        public long CExcelColumnId { get; set; }
        [StringLength(500)]
        public string ColumnName { get; set; }
        [StringLength(500)]
        public string DisplayName { get; set; }
        public int? ColumnOrder { get; set; }
        public long? CExcelFileId { get; set; }
        [StringLength(500)]
        public string DataType { get; set; }
        [StringLength(500)]
        public string DefaultValue { get; set; }
        public int? ObjectTypeId { get; set; }
        [StringLength(500)]
        public string ObjectTypeName { get; set; }
        public int? DataStatusType { get; set; }
        [Column(TypeName = "ntext")]
        public string Note { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }
    }

    public class CUserExcelColumn
    {
        public long CUserExcelColumnId { get; set; }
        [Required]
        public long CExcelColumnId { get; set; }
        [StringLength(500)]
        public string ColumnName { get; set; }
        [StringLength(500)]
        public string DisplayName { get; set; }
        public int? ColumnOrder { get; set; }
        public long? CExcelFileId { get; set; }
        [StringLength(500)]
        public string DataType { get; set; }
        [StringLength(500)]
        public string DefaultValue { get; set; }
        public int? UserId { get; set; }
        public int? ObjectTypeId { get; set; }
        [StringLength(500)]
        public string ObjectTypeName { get; set; }
        public int? DataStatusType { get; set; }
        [Column(TypeName = "ntext")]
        public string Note { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }
        public int Type { get; set; }
    }

    public class CExcelFormat
    {
        public long CExcelFormatId { get; set; }
        [Required]
        public long CExcelFileId { get; set; }
        /// <summary>
        /// Header Format/Body Format/Footer Format
        /// </summary>
        public int? FormatStyleId { get; set; }
        /// <summary>
        /// Header Format/Body Format/Footer Format
        /// </summary>
        [StringLength(500)]
        public string FormatStyleName { get; set; }
        public float? FontSize { get; set; }
        [StringLength(500)]
        public string FontFamily { get; set; }
        public bool? FontBold { get; set; }
        public bool? FontItalic { get; set; }
        [StringLength(500)]
        public string FontColor { get; set; }
        /// <summary>
        /// Căn theo chiều dọc
        /// </summary>
        public int? VerticalAlignment { get; set; }
        /// <summary>
        /// Căn theo chiều dọc
        /// </summary>
        public ExcelVerticalAlignment ExcelVerticalAlignment
        {
            get {
                if (VerticalAlignment != null)
                    return (ExcelVerticalAlignment) VerticalAlignment;
                return ExcelVerticalAlignment.Top;
            }
        }
        /// <summary>
        /// Căn theo chiều ngang
        /// </summary>
        public int? HorizontalAlignment { get; set; }
        /// <summary>
        /// Căn theo chiều ngang
        /// </summary>
        public ExcelHorizontalAlignment ExcelHorizontalAlignment
        {
            get
            {
                if (HorizontalAlignment != null)
                    return (ExcelHorizontalAlignment) HorizontalAlignment;
                return ExcelHorizontalAlignment.Left;
            }
        }
        [StringLength(500)]
        public string BackgroundColor { get; set; }
        [StringLength(500)]
        public string BorderColor { get; set; }
        public int? BorderStyle { get; set; }
        public ExcelBorderStyle ExcelBorderStyle
        {
            get
            {
                if (BorderStyle != null)
                    return (ExcelBorderStyle) BorderStyle;
                return ExcelBorderStyle.Thin;
            }
        }
        public int? ObjectTypeId { get; set; }
        [StringLength(500)]
        public string ObjectTypeName { get; set; }
        public int? DataStatusType { get; set; }
        [Column(TypeName = "ntext")]
        public string Note { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }
    }

    public class CUserExcelFormat
    {
        public long CUserExcelFormatId { get; set; }
        [Required]
        public long CExcelFormatId { get; set; }
        [Required]
        public long CExcelFileId { get; set; }
        public int? FormatStyleId { get; set; }
        [StringLength(500)]
        public string FormatStyleName { get; set; }
        public float? FontSize { get; set; }
        [StringLength(500)]
        public string FontFamily { get; set; }
        public bool? FontBold { get; set; }
        public bool? FontItalic { get; set; }
        [StringLength(500)]
        public string FontColor { get; set; }
        public int? VerticalAlignment { get; set; }
        public ExcelVerticalAlignment ExcelVerticalAlignment
        {
            get
            {
                if (VerticalAlignment != null)
                    return (ExcelVerticalAlignment)VerticalAlignment;
                return ExcelVerticalAlignment.Top;
            }
        }
        public int? HorizontalAlignment { get; set; }
        public ExcelHorizontalAlignment ExcelHorizontalAlignment
        {
            get
            {
                if (HorizontalAlignment != null)
                    return (ExcelHorizontalAlignment)HorizontalAlignment;
                return ExcelHorizontalAlignment.Left;
            }
        }
        [StringLength(500)]
        public string BackgroundColor { get; set; }
        [StringLength(500)]
        public string BorderColor { get; set; }
        public int? BorderStyle { get; set; }
        public ExcelBorderStyle ExcelBorderStyle
        {
            get
            {
                if (BorderStyle != null)
                    return (ExcelBorderStyle)BorderStyle;
                return ExcelBorderStyle.Thin;
            }
        }
        public int? UserId { get; set; }
        public int? ObjectTypeId { get; set; }
        [StringLength(500)]
        public string ObjectTypeName { get; set; }
        public int? DataStatusType { get; set; }
        [Column(TypeName = "ntext")]
        public string Note { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? LastModifiedBy { get; set; }
    }
}
