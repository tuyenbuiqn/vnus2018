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
	public class RoleBusiness : BaseBusiness<RoleRepository>
	{
		readonly ICommonBusiness _commonBusiness;

		public RoleBusiness(UserInfoModel userInfoModel) : base(new RoleRepository(userInfoModel))
		{
			CurrentUser = userInfoModel;
			_commonBusiness = new CommonBusiness(userInfoModel);
		}

		public ResultModel<int> UpdateOrInsert(RoleModel dataModel)
		{
			try
			{
				var logObj = new LogModel()
				{
					ModuleId = (int)ObjectTypeEnum.Role,
					Action = dataModel.RoleId> 0 ? "Update Role" : "InsertRole",
					CreatedBy = CurrentUser.UserId,
					CreatedDate = DateTime.Now,
					LogType = dataModel.RoleId > 0 ? LogType.Update : LogType.Create
				};

				MainRepository.UpdateOrInsert(dataModel);
				logObj.LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(dataModel);
				_commonBusiness.InsertLog(logObj);

				return new ResultModel<int>()
				{
					Data = dataModel.RoleId
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

		public string Delete(RoleModel objDelete)
		{
			try
			{
				MainRepository.Delete<GroupRole>(objDelete);
				_commonBusiness.InsertLog(new LogModel()
				{
					ModuleId = (int)ObjectTypeEnum.Role,
					Action = "Delete Role",
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

		public List<Repository.EFData.Entity.Role> GetByAll(RoleModel inputObj)
		{
			return MainRepository.GetbyAll(inputObj);
		}
		public List<Repository.EFData.Entity.Role> GetRoleByName (string  rolename )
		{
			try
			{
				return MainRepository.GetbyRoleName(rolename);
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
				throw;
			}
		}
	}
}
