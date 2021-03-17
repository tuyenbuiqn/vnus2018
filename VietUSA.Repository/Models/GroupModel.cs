using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VietUSA.Repository.EFData.Entity;

namespace VietUSA.Repository.Models
{
    [NotMapped]
    public class GroupModel:Group
    {
      
    }

    [NotMapped]
    public class GroupAddOrEdit : Group
    {
        
    }
	[NotMapped]
	public class PrivilegeGroupModel : Group
	{
		public int[] ListUserIdNotInGroup { get; set; }
		public List<User> ListUserNotInGroup { get; set; }
		public int[] ListUserIdInGroup { get; set; }
		public List<User> ListUserInGroup { get; set; }

		public int[] ListRoleIdNotInGroup { get; set; }
		public List<Role> ListRoleNotInGroup { get; set; }
		public int[] ListRoleIdInGroup { get; set; }
		public List<Role> ListRoleInGroup { get; set; }
		public string UserNameInGroup { get; set; }
		public string UserNameNotInGroup { get; set; }

		public string RoleNotInGroup { get; set; }
		public string RoleInGroup { get; set; }

	}

}
