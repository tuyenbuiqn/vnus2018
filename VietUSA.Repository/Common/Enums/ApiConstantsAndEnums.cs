
//using VNPT.CommonLib.Attributes;

namespace VietUSA.Repository.Common.Enums
{
    public class ApiConstantsAndEnums
    {
        public const int Exception = -1;
        public const int DataNotFound = 0;
        public const int DataInputInvalid = 1;
        public const int Success = 200;
    }

    public enum ApiResultCode
    {
        Unknown = -1,
        Success = 0,

        UnrecognizedValue = 1,

        InsufficientPermission = 1000,
        InvalidUser = 1002,
        MissingCredential = 1003,
        DataNotAvailable = 1004,
        InvalidPropertyValue = 1005,
        InvalidProperty = 1006,
        ActionFailed = 1007,
        ValueRequired = 1008
    }

    public class QueryStringName
    {
        public const string Type = "type";
        public const string Id = "id";
        public const string PageIndex = "pageindex";
        public const string PageSize = "pagesize";
        public const string Language = "language";
        public const string VietName = "vietname";
        public const string ForeignName = "foreignname";
        public const string AccommodationRating = "rating";
        public const string District = "district";
    }

    //public enum ApiLoaiDoThu
    //{
    //    [Text("DSLAM")]
    //    ThongSoDsLam = 0,
    //    TrangThaiCongDsLam = 1,
    //    [Text("PORT")]
    //    TrangThaiCongGpon = 2,
    //    ThongSoCongGpon = 3, // cả thông số cổng + thông tin dịch vụ
    //    [Text("BRAS")]
    //    TrangThaiBras = 4,
    //    CauHinhMegaWanTrenBras = 5,
    //    [Text("VISA")]
    //    TraThongTinVisa = 6,
    //    TraMatKhauMegaVnn = 7,
    //    TraThongTinMyTvVasc = 8,
    //    DoThuDienThoai = 9,
    //    ThongTinGphone = 10,
    //    DslamBras = 14,
    //    ResetCongDsLam = 15,
    //    ResetCongGpon = 16,
    //    TraMatKhauMegaFiber = 17,
    //    VisaPort = 62,
    //    [Text("BTS_SH")]
    //    BtsSuyHao = 18,
    //    [Text("CH_PORT_MEN")]
    //    ThongSoCauHinhCongMenGpon = 19 // chỉ thông số cổng
    //}
}