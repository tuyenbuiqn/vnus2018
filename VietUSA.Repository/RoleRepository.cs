using System;
using System.Collections.Generic;
using System.Linq;
using VietUSA.Repository.Base;
using VietUSA.Repository.Models;
using VietUSA.Repository.EFData.Entity;

namespace VietUSA.Repository
{
    public class RoleRepository : EntityFrameworkRepository
    {
		public RoleRepository(UserInfoModel userInfoModel):base(userInfoModel)
        {

		}
		public string UpdateOrInsert(RoleModel objUpdate)
        {
            try
            {
                if (objUpdate.RoleId > 0)
                {
                    InsertOrUpdate<RoleModel>(objUpdate, false);
                }
                else
                {
                    InsertOrUpdate<RoleModel>(objUpdate, true);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public List<Role> GetbyAll(RoleModel inputObj)
        {
                var linqSql = from data in Context.Roles
                              select data;

                if (inputObj.RoleId > 0)
                    linqSql = linqSql.Where(o => o.RoleId == inputObj.RoleId);
             
                return linqSql.ToList();
        }
		public List<Role > GetbyRoleName(string rolename)
		{
			//rolename = EntityFunctions.AsNonUnicode(rolename);
			var linqSql = from data in Context.Roles
						  where data.RoleName.Contains(rolename)
						  select data;
			return linqSql.ToList();
			
		}
	}
}
