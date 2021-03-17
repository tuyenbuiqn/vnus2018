using System.Collections.Generic;
using VietUSA.Repository.EFData.Entity;

namespace VietUSA.Repository.Models
{
   public class LoginModel
    {
       public List<GroupRoleModel> GroupRoleModel { get; set; }
       public User User { get; set; }
       public UserInfoModel UserInfoModel { get; set; }
    }
}
