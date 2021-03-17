namespace VietUSA.Repository.Common.Constants
{
    public static class CommonConstants
    {

        public const string Success = "SUCCESS";
        public const string Error = "ERROR";
        public const string DataNull = "Không có dữ liệu";
        public const string InvalidInput = "Đầu vào sai: lỗi sai parameter hoặc giá trị parameter không chính xác";
        public const string FileNotFound = "Không có tìm thấy file";
        public const string AddNew = "Thêm mới";
        public const string Update = "Cập nhật";
        public const string Warning = "Cảnh báo";
        public const string SaveSuccess = "Cập nhật thành công";
        public const string ImportSuccess = "Import dữ liệu vào hệ thống thành công.";
        public const string SaveError = "Cập nhật không thành công, vui lòng thử lại.";
        public const string ChooseMarketError = "Vui lòng chọn thị trường";
        public const string ChooseDetailTypeError = "Vui lòng chọn Loại";
        public const string DataNotFound = "Không có dữ liệu.";
        public const string DeleteSuccess = "Xóa dữ liệu thành công";
        public const string DeleteError = "Xóa dữ liệu không thành công";
        public const string CategoryCannotRemove = "Dữ liệu không thể xóa, có thể gây ảnh hưởng đến các bản ghi khác.";
        public const string ConfirmDelete = "Bạn chắc chắn muốn xóa bản ghi này";
        public const string ConfirmDeleteFile = "Bạn chắc chắn muốn xóa tệp tin này";
        public const string DeleteFileError = "Xóa tệp tin không thành công, vui lòng thử lại.";
        public const int SelectAllId = -1;
        public const string SelectAll = ".: Chọn tất cả :.";
        public const string GetOne = ".: Vui lòng chọn :.";
        public const string IntegerValidation = "Vui lòng nhập số";
        public const string GoogleMapCannotGetData = "Địa chỉ này không chính xác, vui lòng thử lại.";
        public const string DefaultSchema = "dbo";
        public const string ErrorOccurs = "Có lỗi xảy ra, vui lòng liên hệ quản trị viên";

        public const string ExcelGetAll = "GetAll";

        /// <summary>
        /// Phuc vu cho tim kiem, -1 => get all
        /// </summary>
        public const int SearchAll = -1;

        /// <summary>
        /// Phuc vu cho tim kiem, -1 => get all
        /// </summary>
        public const string SearchAllByString = "-1";

        public const int AdministrativeDivisionHaNoi = 37;
        public const int AdministrativeDivisionBaDinh = 37;
        public const int AdministrativeDivisionVietnam = 33;
		public const int AdministrativeDivisionNation = 0;

        public const string QrCodeInputEmpty = "Không có dữ liệu đầu vào";
        public const string QrCodeInputInvalid = "File QRCode không đúng với định dạng của hệ thống";
        public const string QrCodeInputDataNotFound = "Không tìm thấy dữ liệu trong hệ thống";
        public const string QrCodeEncrytionKey = "2341sdfdsfsdfsdfdsfytfysydfudsfusdytfsdf7f657sd6fdsfsdfsdufysudf7sd6fs7d657fsdfdsfsdfsdf6sd7f67ds57fds5fdsfdsfdsfdsf76ds7f67dsfsdfdsfdsfsd87f8d7sf8dsfsdfsdfsd8f78ds6f8sd6fsdfsdfsdf7sd6f6ds76fsdfsdfsd8f76s8df68dsfdsfsdfsđ8f77sd86f8s8d6f8dsfsdfsdf87sd678f68sdfsdfsdfsd7f67sdf67sd6fsdfsdfs7d7ds5f";
    }

    public static class DataStatusType
    {
        public const int Created = 1;
        public const int Modified = 2;
        public const int Deleted = 3;
    }

    public sealed class SessionKeys
    {
        /// <summary>
        /// Thông tin người dùng hiện tại
        /// </summary>
        public const string UserInfo = "CURRENT_USER_INFO";

        public const string SocketClientName = "CURRENT_SOCKET_CLIENT_NAME";

        /// <summary>
        /// Thông tin ngôn ngữ hiện tại
        /// </summary>
        public const string CultureInfo = "CURRENT_CULTURE_INFO";
    }

    //public sealed class Roles
    //{
    //    /// <summary>
    //    /// Quản trị hệ thống - Quản lý toàn bộ các chức năng
    //    /// </summary>
    //    public const int SuperAdmin = -1;
    //    public const int Admin = 1;
    //    public const int AdminTapdoan = 9;
    //    public const int Member = 10;
    //    public const int ApiUser = -2;
    //}

    public sealed class LoginConstant
    {
        public const string Success = "SUCCESS";
        public const string NotExisted = "NOT_EXISTED";
        public const string NotAuthorized = "NOT_AUTHORIZED";
    }

    /// <summary>
    /// GroupCategory, see Category.GroupCategoryId
    /// </summary>
    public static class GroupCategory
    {
        /// <summary>
        /// Created/Modified/Deleted
        /// </summary>
        public const int DataStatusType = 1;

        /// <summary>
        /// User Type
        /// </summary>
        public const int UserType = 2;

        /// <summary>
        /// Member Type
        /// </summary>
        public const int MemberType = 3;

    }

    public static class LicensePaperType
    {
        public const int GiayPhepKinhDoanhLhQuocTe = 69;
        public const int GiayPhepKinhDoanhLhNoiDia = 71;
        public const int GiayPhepKinhDoanhVanChuyenDuongBo = 72;
        public const int GiayPhepKinhDoanhVanChuyenDuongThuy = 73;
    }
    public static class AdministrativeDivisionType
    {
        /// <summary>
        /// Quốc gia
        /// </summary>
        public const int Nation = 17;

        /// <summary>
        /// Tỉnh, thành phố
        /// </summary>
        public const int Province = 12;

        /// <summary>
        /// Quận huyện
        /// </summary>
        public const int District = 13;

        /// <summary>
        /// Phường xã
        /// </summary>
        public const int Ward = 14;

        public const int Street = 40;
    }

    public static class TravelCompanyType
    {
        /// <summary>
        /// Công ty TNHH
        /// </summary>
        public const int CTTNHH = 35;
        /// <summary>
        /// Công ty Cổ phần
        /// </summary>
        public const int CTCP = 36;
        /// <summary>
        /// Doanh nghiệp tư nhân
        /// </summary>
        public const int DNTN = 37;
        /// <summary>
        /// Doanh nghiệp nhà nước
        /// </summary>
        public const int DNNN = 38;
        /// <summary>
        /// Công ty Liên doanh
        /// </summary>
        public const int CTLD = 39;
    }

    public static class TravelCompanyReferenceType
    {
        /// <summary>
        /// Chi nhánh(Công ty lữ hành)
        /// </summary>
        public const int TravelCompanyAgency = 30;
        /// <summary>
        /// Địa điểm kinh doanh(Công ty lữ hành)
        /// </summary>
        public const int TravelCompanyBusinessLocation = 31;
        /// <summary>
        /// Văn phòng đại diện(Công ty lữ hành)
        /// </summary>
        public const int TravelCompanyRepresentOffice = 32;
    }

    public static class TabTraComRef
    {
        public const string TabGeneral = "Thông tin chung";
        public const string TabTravelReference = "VPĐD, Chi nhánh";
        public const string TabLicensePaper = "Giấy phép, thông báo";
        public const string TabTravelBusiness = "Thị trường, lĩnh vực kinh doanh";
        public const string TabHistory = "Lịch sử, thay đổi";
    }

    public static class PagingConstant
    {
        public const int MinPageSize = 1;
        public const int DefaultPageSize = 20;
        public const int TinyPageSize = 50;
        public const int SmallPageSize = 100;
        public const int PageSize5000 = 5000;
        public const int LargePageSize = 20000;
        public const int HugePageSize = 50000;
        public const int GiganticPageSize = 100000;
        public const int MaxPageSize = int.MaxValue;
        public const int MinPageIndex = 1;
    }

    public static class LogType
    {
        public static string Create = "Thêm mới";
        public static string Read = "Xem";
        public static string Update = "Cập nhật";
        public static string Delete = "Xóa";
    }
    public static class LogEnum
    {
        /*
            C = Create
            R = Read
            U = Update
            D = Delete
            
            RO  = RepresentativeOffice
            R   = Restaurant 
            AF  = AccommodationFacility
            MF  = MainFood
             
        */
        /// <summary>
        /// Tạo mới Văn phòng đại diện
        /// </summary>
        public const string C_RO = "Tạo mới Văn phòng đại diện";
        /// <summary>
        /// Xem Văn phòng đại diện
        /// </summary>
        public const string R_RO = "Xem Văn phòng đại diện";
        /// <summary>
        /// Cập nhật Văn phòng đại diện
        /// </summary>
        public const string U_RO = "Cập nhật Văn phòng đại diện";
        /// <summary>
        /// Xóa Văn phòng đại diện
        /// </summary>
        public const string D_RO = "Xóa Văn phòng đại diện";

        /// <summary>
        /// Tạo mới Nhà hàng
        /// </summary>
        public const int C_R = 5;
        /// <summary>
        /// Xem Nhà hàng
        /// </summary>
        public const int R_R = 6;
        /// <summary>
        /// Cập nhật Nhà hàng
        /// </summary>
        public const int U_R = 7;
        /// <summary>
        /// Xóa Nhà hàng
        /// </summary>
        public const int D_R = 8;

        /// <summary>
        /// Tạo mới Cơ sở lưu trú
        /// </summary>
        public const int C_AF = 9;
        /// <summary>
        /// Xem Cơ sở lưu trú
        /// </summary>
        public const int R_AF = 10;
        /// <summary>
        /// Cập nhật Cơ sở lưu trú
        /// </summary>
        public const int U_AF = 11;
        /// <summary>
        /// Xóa Cơ sở lưu trú
        /// </summary>
        public const int D_AF = 12;

        /// <summary>
        /// Tạo mới Món ăn chính
        /// </summary>
        public const int C_MF = 13;
        /// <summary>
        /// Xem Món ăn chính
        /// </summary>
        public const int R_MF = 14;
        /// <summary>
        /// Cập nhật Món ăn chính
        /// </summary>
        public const int U_MF = 15;
        /// <summary>
        /// Xóa Món ăn chính
        /// </summary>
        public const int D_MF = 16;

        #region Category
        /// <summary>
        /// Tạo mới Danh mục
        /// </summary>
        public const string C_CATE = "Tạo mới Danh mục";
        /// <summary>
        /// Xem Danh mục
        /// </summary>
        public const string R_CATE = "Xem Danh mục";
        /// <summary>
        /// Cập nhật Danh mục
        /// </summary>
        public const string U_CATE = "Cập nhật Danh mục";
        /// <summary>
        /// Xóa Danh mục
        /// </summary>
        public const string D_CATE = "Xóa Danh mục";
        #endregion Category


        #region Department
        /// <summary>
        /// Tạo mới Phòng ban
        /// </summary>
        public const string C_DEP = "Tạo mới Phòng ban";
        /// <summary>
        /// Xem Phòng ban
        /// </summary>
        public const string R_DEP = "Xem Phòng ban";
        /// <summary>
        /// Cập nhật Phòng ban
        /// </summary>
        public const string U_DEP = "Cập nhật Phòng ban";
        /// <summary>
        /// Xóa Phòng ban
        /// </summary>
        public const string D_DEP = "Xóa Phòng ban";
        #endregion Department

        #region ForeignLanguage
        /// <summary>
        /// Tạo mới Ngôn ngữ
        /// </summary>
        public const string C_LAN = "Tạo mới Ngôn ngữ";
        /// <summary>
        /// Xem Danh mục
        /// </summary>
        public const string R_LAN = "Xem Ngôn ngữ";
        /// <summary>
        /// Cập nhật Danh mục
        /// </summary>
        public const string U_LAN = "Cập nhật Ngôn ngữ";
        /// <summary>
        /// Xóa Danh mục
        /// </summary>
        public const string D_LAN = "Xóa Ngôn ngữ";
        #endregion ForeignLanguage

        #region AdministrativeDivision
        /// <summary>
        /// Tạo mới Danh mục
        /// </summary>
        public const string C_ADMIN_DIVISION = "Tạo mới Danh mục Đơn vị hành chính";
        /// <summary>
        /// Xem Danh mục
        /// </summary>
        public const string R_ADMIN_DIVISION = "Xem Danh mục Đơn vị hành chính";
        /// <summary>
        /// Cập nhật Danh mục
        /// </summary>
        public const string U_ADMIN_DIVISION = "Cập nhật Danh mục Đơn vị hành chính";
        /// <summary>
        /// Xóa Danh mục
        /// </summary>
        public const string D_ADMIN_DIVISION = "Xóa Danh mục Đơn vị hành chính";
        #endregion AdministrativeDivision

        #region COMPLEX
        /// <summary>
        /// Tạo mới Danh mục
        /// </summary>
        public const string C_COMPLEX = "Tạo mới Cơ sở mua sắm";
        /// <summary>
        /// Xem Danh mục
        /// </summary>
        public const string R_COMPLEX = "Xem Cơ sở mua sắm";
        /// <summary>
        /// Cập nhật Danh mục
        /// </summary>
        public const string U_COMPLEX = "Cập nhật Cơ sở mua sắm";
        /// <summary>
        /// Xóa Danh mục
        /// </summary>
        public const string D_COMPLEX = "Xóa Cơ sở mua sắm";
        #endregion COMPLEX


        #region TRAVEL COMPANY
        /// <summary>
        /// Tạo mới Công ty lữ hành
        /// </summary>
        public const string C_TRAVEL_COMPANY = "Tạo mới Công ty lữ hành";
        /// <summary>
        /// Xem Công ty lữ hành
        /// </summary>
        public const string R_TRAVEL_COMPANY = "Xem Công ty lữ hành";
        /// <summary>
        /// Cập nhật Công ty lữ hành
        /// </summary>
        public const string U_TRAVEL_COMPANY = "Cập nhật Công ty lữ hành";
        /// <summary>
        /// Xóa Công ty lữ hành
        /// </summary>
        public const string D_TRAVEL_COMPANY = "Xóa Công ty lữ hành";
        #endregion TRAVEL COMPANY

        #region LICENSE PAPER
        /// <summary>
        /// Tạo mới Giấy phép
        /// </summary>
        public const string C_LICENSE_PAPER = "Tạo mới Giấy phép";
        /// <summary>
        /// Xem Giấy phép
        /// </summary>
        public const string R_LICENSE_PAPER = "Xem Giấy phép";
        /// <summary>
        /// Cập nhật Giấy phép
        /// </summary>
        public const string U_LICENSE_PAPER = "Cập nhật Giấy phép";
        /// <summary>
        /// Xóa Giấy phép
        /// </summary>
        public const string D_LICENSE_PAPER = "Xóa Giấy phép";
        #endregion LICENSE PAPER

        #region TRANSPORT_COMPANY
        /// <summary>
        /// Tạo mới Danh mục
        /// </summary>
        public const string C_TRANSPORT_COMPANY = "Tạo mới Doanh nghiệp vận chuyển";
        /// <summary>
        /// Xem Danh mục
        /// </summary>
        public const string R_TRANSPORT_COMPANY = "Xem Doanh nghiệp vận chuyển";
        /// <summary>
        /// Cập nhật Danh mục
        /// </summary>
        public const string U_TRANSPORT_COMPANY = "Cập nhật Doanh nghiệp vận chuyển";
        /// <summary>
        /// Xóa Danh mục
        /// </summary>
        public const string D_TRANSPORT_COMPANY = "Xóa Doanh nghiệp vận chuyển";
        #endregion TRANSPORT_COMPANY
    }

    /// <summary>
    /// ObjectName is TableName of geography
    /// </summary>
    public static class ObjectName
    {
        public const string Vehicle = "Vehicle";
        public const string AccommodationFacility = "AccommodationFacility";
        public const string Complex = "Complex";
        public const string TravelAgentCompany = "TravelAgentCompany";
        public const string TravelCompany = "TravelCompany";
        public const string TransportCompany = "TransportCompany";
        public const string Restaurant = "Restaurant";
        public const string RepresentativeOffice = "RepresentativeOffice";
        // TravelCompanyReferenceType
        /// <summary>
        /// Chi nhánh(Công ty lữ hành)
        /// </summary>
        public const string TravelCompanyAgency = "TravelCompanyAgency";
        /// <summary>
        /// Địa điểm kinh doanh(Công ty lữ hành)
        /// </summary>
        public const string TravelCompanyBusinessLocation = "TravelCompanyBusinessLocation";
        /// <summary>
        /// Văn phòng đại diện(Công ty lữ hành)
        /// </summary>
        public const string TravelCompanyRepresentOffice = "TravelCompanyRepresentOffice";


        public const string TouristGuide = "TouristGuide";

        // Giai doan 2
        public const string QhProject = "QhProject";
    }

    public static class ConfigType
    {
        public const int ImportTempate = 1;
    }

    /// <summary>
    /// Dùng cho log
    /// </summary>
    //public static class ModuleId
    //{
    //    public const int AccommodationFacility = 1;
    //    public const int Complex = 2;
    //    public const int TransportCompany = 3;
    //    public const int Restaurant = 4;
    //    public const int RepresentativeOffice = 5;
    //    public const int HeadCompany = 6;
    //    public const int LicensePaper = 7;
    //    public const int Inspection = 7;
    //    public const int User = 8;
    //    public const int TouristGuide = 9;
    //    public const int Degree = 10;
    //    public const int Vehicle = 11;
    //    public const int TravelCompany = 12;
    //    public const int TravelCompanyReference = 13;
    //    public const int TravelBusiness = 14;
    //    public const int ObjectLanguage = 15;
    //    public const int Group = 16;
    //    public const int UserGroup = 17;
    //}

    //public static class ObjectType
    //{
    //    public const int AccommodationFacility = 1;
    //    public const int Complex = 2;
    //    public const int TransportCompany = 3;
    //    public const int Restaurant = 4;
    //    public const int RepresentativeOfficeLuHanh = 5;
    //    public const int RepresentativeOfficeLuuTru = 7;
    //    public const int Vehicle = 6;
    //}

    public class TravelAgentCompanyType
    {
        public const int All = -1;
        public const int International = 1;
        public const int Domestic = 0;
    }

    public class LicenseType
    {
        public const int CapMoi = 41;
        public const int CapLai = 42;
        public const int SuaDoi = 43;
        public const int GiaHan = 44;
        public const int BoSung = 45;
    }

    //hạng sao Khách sạn hoặc căn hộ cao cấp
    public class RatingHotel
    {
        public const int MotSao = 25;
        public const int HaiSao = 26;
        public const int BaSao = 27;
        public const int BonSao = 28;
        public const int NamSao = 29;
        public const int ChuaXepHang = 67;

    }

    //Rating các cslt khác
    public class RatingOther
    {
        public const int DatChuan = 66;
        public const int ChuaXepHang = 80;
        public const int CaoCap = 68;
    }

    public class AccomType
    {
        public const int KhachSan = 4;
        public const int NhaNghiDuLich = 5;
        public const int CanHoDuLich = 6;
        public const int NhaOCoPhongChoKdlThue = 7;
        public const int NhaKhach = 8;
        public const int TauThuyLuuTru = 61;
        public const int BaiCamTrai = 64;
        public const int CsltKhac = 65;
    }

    public class Module
    {
        public const int LuuTru = 1;
        public const int LuHanh = 2;
    }

    public class UploadTemp
    {
        public const int CsLuuTru = 1;
        public const int NhaHangDatChuan = 2;
        public const int VpDaiDienLuuTru = 3;
        public const int CoSoMuaSamDatChuan = 4;
        public const int DoanhNghiepVanChuyen = 5;
        public const int CongTyLuHanh = 6;
        public const int HuongDanVienDuLich = 7;
        public const int VpDaiDienLuHanh = 8;
        public const int ThongKeKhachDuLich = 9;
    }

    public class DropDownType
    {
        public const string Ward = "DropDownWard";
        public const string District = "DropDownDistrict";
    }

    public class EmailConstant
    {
        public const string FromEmail = "noreply.vnus2019@gmail.com";
        public const string FromEmailPassword = "Qfv2t6%1";
        public const string Host = "smtp.gmail.com";
        public const int Port = 587;
        public const int Timeout = 600000;
        public const bool UseDefaultCredentials = false;

        public const string ToEmail1 = "vnus2019@viasm.edu.vn";
        public const string ToEmail2 = "tuyenbuiqn@gmail.com";
    }
}
