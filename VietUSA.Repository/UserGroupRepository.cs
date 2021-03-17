using System;
using System.Collections.Generic;
using System.Linq;
using VietUSA.Repository.Base;
using VietUSA.Repository.EFData.Entity;
using VietUSA.Repository.Models;

namespace VietUSA.Repository
{
    public class UserGroupRepository : EntityFrameworkRepository
    {
        public UserGroupRepository(UserInfoModel userInfoModel):base(userInfoModel)
        {

        }
    public string UpdateOrInsert(UserGroup objUpdate)
        {
            try
            {
              if (objUpdate.GroupId > 0 && objUpdate.UserId>0)
                {
                    InsertOrUpdate<UserGroup>(objUpdate, true);
                }
               
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public List<UserGroup> GetbyAll(UserGroup inputObj)
        {
                var linqSql = from data in Context.UserGroups
                              select data;

                if (inputObj.UserGroupId > 0)
                    linqSql = linqSql.Where(o => o.UserGroupId == inputObj.UserGroupId);
                if (inputObj.UserId > 0)
                    linqSql = linqSql.Where(o => o.UserId == inputObj.UserId);
                if (inputObj.GroupId > 0)
                    linqSql = linqSql.Where(o => o.GroupId == inputObj.GroupId);

                return linqSql.ToList();
        }
		
	}
}
