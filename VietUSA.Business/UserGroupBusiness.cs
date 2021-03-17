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
    public class UserGroupBusiness : BaseBusiness<UserGroupRepository>
    {
        readonly ICommonBusiness _commonBusiness;

        public UserGroupBusiness(UserInfoModel userInfoModel) : base(new UserGroupRepository(userInfoModel))
        {
            CurrentUser = userInfoModel;
            _commonBusiness = new CommonBusiness(userInfoModel);
        }

        public ResultModel<int> UpdateOrInsert(UserGroup dataModel)
        {
            try
            {
                var logObj = new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.UserGroup,
                    Action = dataModel.UserGroupId > 0 ? "Update UserGroup" : "Insert UserGroup",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = dataModel.UserGroupId > 0 ? LogType.Update : LogType.Create
                };

                MainRepository.UpdateOrInsert(dataModel);
                logObj.LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(dataModel);
                _commonBusiness.InsertLog(logObj);

                return new ResultModel<int>()
                {
                    Data = dataModel.UserGroupId
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

        public string Delete(UserGroup objDelete)
        {
            try
            {
                MainRepository.Delete<UserGroup>(objDelete);
                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.UserGroup,
                    Action = "Delete UserGroup",
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

        public List<UserGroup> GetByAll(UserGroup inputObj)
        {
            return MainRepository.GetbyAll(inputObj);
        }
    }
}
