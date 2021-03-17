using System.ComponentModel.DataAnnotations.Schema;
using VietUSA.Repository.EFData.Entity;

namespace VietUSA.Repository.Models
{
    [NotMapped]
    public class GroupRoleModel:GroupRole
    {
        public string Username { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
    }
}
