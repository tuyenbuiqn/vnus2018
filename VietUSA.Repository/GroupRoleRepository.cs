using System;
using System.Collections.Generic;
using System.Linq;
using VietUSA.Repository.Base;
using VietUSA.Repository.EFData.Entity;
using VietUSA.Repository.Models;

namespace VietUSA.Repository
{
    public class GroupRoleRepository : EntityFrameworkRepository
    {
		public GroupRoleRepository(UserInfoModel userInfoModel):base(userInfoModel)
        {

		}
		public string UpdateOrInsert(GroupRole objUpdate)
        {
            try
            {
                if (objUpdate.GroupRoleId > 0)
                {
                    InsertOrUpdate<GroupRole>(objUpdate, false);
                }
                else
                {
                    InsertOrUpdate<GroupRole>(objUpdate, true);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public List<GroupRole> GetbyAll(GroupRole inputObj)
        {
                var linqSql = from data in Context.GroupRoles
                              select data;

                if (inputObj.GroupRoleId > 0)
                    linqSql = linqSql.Where(o => o.GroupRoleId == inputObj.GroupRoleId);
                if (inputObj.GroupId > 0)
                    linqSql = linqSql.Where(o => o.GroupId == inputObj.GroupId);
                if (inputObj.RoleId > 0)
                    linqSql = linqSql.Where(o => o.RoleId == inputObj.RoleId);

                return linqSql.ToList();
        }
    }
}
