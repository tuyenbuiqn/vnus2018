using System.Collections.Generic;
using VietUSA.Repository.Common.Models;

namespace VietUSA.Repository.Models
{
    public class UserInfoModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Title { get; set; }
        public bool IsSuperAdministrator{ get; set; }
        public bool IsFirstLoginPasswordChanged { get; set; }
        public List<int> Permissions { get; set; }
        public string SessionId { get; set; }
        public ConnectionInfo ConnectionInfo { get; set; }

        public UserInfoModel()
        {
            ConnectionInfo = new ConnectionInfo();
        }
    }
}
