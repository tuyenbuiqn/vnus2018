using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VietUSA.Business.Interfaces;
using VietUSA.Repository;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Enums;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.EFData.Entity;
using VietUSA.Repository.Models;

namespace VietUSA.Business
{
	public class GroupBusiness : BaseBusiness<GroupRepository>
	{
		readonly ICommonBusiness _commonBusiness;
		private readonly UserGroupBusiness _userGroupBusiness;

		public GroupBusiness(UserInfoModel userInfoModel) : base(new GroupRepository(userInfoModel))
		{
			CurrentUser = userInfoModel;
			_userGroupBusiness = new UserGroupBusiness(userInfoModel);
			_commonBusiness = new CommonBusiness(userInfoModel);
		}

		public ResultModel<int> UpdateOrInsert(GroupAddOrEdit dataModel)
		{
			try
			{
				var logObj = new LogModel()
				{
					ModuleId = (int)ObjectTypeEnum.Group,
					Action = dataModel.GroupId > 0 ? "Update Group" : "Insert Group",
					CreatedBy = CurrentUser.UserId,
					CreatedDate = DateTime.Now,
					LogType = dataModel.GroupId > 0 ? LogType.Update : LogType.Create
				};
				Mapper.Initialize(c => { c.CreateMap<GroupAddOrEdit, Group>(); });
				var objSave = Mapper.Map<GroupAddOrEdit, Group>(dataModel);

				MainRepository.UpdateOrInsert(objSave);
				logObj.LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(dataModel);
				_commonBusiness.InsertLog(logObj);

				return new ResultModel<int>()
				{
					Data = dataModel.GroupId
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

		public string Delete(Group objDelete)
		{
			try
			{
				var lstUserGroup = _userGroupBusiness.GetByAll(new UserGroup()
				{
					GroupId = objDelete.GroupId
				});

				if (lstUserGroup.Any()) return "Không thể xóa Group";

				MainRepository.Delete<Group>(objDelete);

				_commonBusiness.InsertLog(new LogModel()
				{
					ModuleId = (int)ObjectTypeEnum.Group,
					Action = "Delete Group",
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


		public List<Group> GetByAll(Group inputObj)
		{
			return MainRepository.GetbyAll(inputObj);
		}
		public SearchResultModel<List<GroupModel>> Search(SearchModel<GroupModel> parameter)
		{
			return MainRepository.Search(parameter);
		}
	}
}
