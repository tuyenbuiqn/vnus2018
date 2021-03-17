using System.ComponentModel;

namespace VietUSA.Repository.Common.Models
{
    public class ApiResult<T>
    {
        [DefaultValue(false)]
        public int ErrorCode { get; set; }

        public string Message { get; set; }
        public T Data { get; set; }
    }

    //public class UserModel
    //{
    //    public int NguoiDungId { get; set; }
    //    public string TenDangNhap { get; set; }
    //    public string HoTen { get; set; }
    //    public string SessionId { get; set; }
    //    public long TinhThanhId { get; set; }
    //    public string MaTinhThanh { get; set; }
    //    public long? DonViId { get; set; }
    //}

    public class SsoSessionInfo
    {
        public long NguoiDungId { get; set; }
        public string TenDangNhap { get; set; }
        public string SessionKey { get; set; }
    }

    public class TokenKeyInfo
    {
        public string TokenKey { get; set; }

        /// <summary>
        /// Module cần lấy phân quyền
        /// </summary>
        public int ModuleId { get; set; }

        /// <summary>
        /// Có lấy phân quyền theo hệ thống hay không
        /// </summary>
        public bool IsGetPrivilege { get; set; }
    }
}