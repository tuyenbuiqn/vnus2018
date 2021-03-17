using System;
using System.Collections.Generic;
using VietUSA.Business.Interfaces;
using VietUSA.Repository;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Enums;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.EFData.Entity;
using VietUSA.Repository.Models;

namespace VietUSA.Business
{
	public class GroupRoleBusiness : BaseBusiness<GroupRoleRepository>
	{
		readonly ICommonBusiness _commonBusiness;

		public GroupRoleBusiness(UserInfoModel userInfoModel) : base(new GroupRoleRepository(userInfoModel))
		{
			CurrentUser = userInfoModel;
			_commonBusiness = new CommonBusiness(userInfoModel);
		}

		public ResultModel<int> UpdateOrInsert(GroupRole dataModel)
		{
			try
			{
				MainRepository.UpdateOrInsert(dataModel);
			    var logObj = new LogModel()
				{
					ModuleId = (int)ObjectTypeEnum.UserGroup,
					Action = dataModel.GroupRoleId > 0 ? "Update GroupRole" : "Insert GroupRole",
					CreatedBy = CurrentUser.UserId,
					CreatedDate = DateTime.Now,
					LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(dataModel),
					LogType = dataModel.GroupRoleId > 0 ? LogType.Update : LogType.Create
				};
				_commonBusiness.InsertLog(logObj);
				return new ResultModel<int>()
				{
					Data = dataModel.GroupRoleId
				};
			}
			catch (Exception exception)
			{
				return new ResultModel<int>()
				{
					Message = exception.Message
				};
			}
		}

		public string Delete(GroupRole objDelete)
		{
			try
			{
				MainRepository.Delete<GroupRole>(objDelete);
				_commonBusiness.InsertLog(new LogModel()
				{
					ModuleId = (int)ObjectTypeEnum.UserGroup,
					Action = "Delete GroupRole",
					CreatedBy = CurrentUser.UserId,
					CreatedDate = DateTime.Now,
					LogType = LogType.Delete,
					LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(objDelete)
				});
				return string.Empty;
			}
			catch (Exception exception)
			{
				return exception.Message;
			}
		}

		public List<GroupRole> GetByAll(GroupRoleModel inputObj)
		{
			return MainRepository.GetbyAll(inputObj);
		}
	}
}
