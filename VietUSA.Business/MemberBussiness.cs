using System;
using System.Collections.Generic;
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
	public class MemberBusiness : BaseBusiness<MemberRepository>, IMemberBusiness
	{
        readonly ICommonBusiness _commonBusiness;
        public MemberBusiness(UserInfoModel userInfoModel) : base(new MemberRepository(userInfoModel))
        {
            CurrentUser = userInfoModel;
            _commonBusiness = new CommonBusiness(userInfoModel);
        }
        public ListPagedResultModel<MemberModel> SearchPaging(SearchModel<MemberModel> parameter)
        {
            var searchResult = Search(parameter);

            var data = new ListPagedResultModel<MemberModel>
            {
                ListItem = searchResult.Data,
                PageIndex = parameter.PageIndex,
                PageSize = parameter.PageSize,
                TotalRecord = searchResult.TotalRecord
            };
            return data;
        }
        public SearchResultModel<List<MemberModel>> Search(SearchModel<MemberModel> parameter)
        {
            try
            {
                return MainRepository.Search(parameter);
            }
            catch (Exception exception)
            {
                return HandleExceptions<MemberModel>(exception);
            }
        }
        public ResultModel<int> InsertOrUpdate(MemberModel parameter)
        {
            try
            {
                if (parameter.MemberId > 0)
                {
                    parameter.LastModifiedBy = CurrentUser.UserId;
                    parameter.LastModifiedDate = DateTime.Now;
                    parameter.DataStatusType = DataStatusType.Modified;
                }
                else
                {
                    parameter.CreatedBy = CurrentUser.UserId;
                    parameter.CreatedDate = DateTime.Now;
                    parameter.DataStatusType = DataStatusType.Created;
                }
                var isInserted = parameter.MemberId <= 0;
                Mapper.Initialize(c =>
                {
                    c.CreateMap<Member, MemberModel>();
                    c.CreateMap<MemberModel, Member>();
                });
                var itemToProcess = Mapper.Map<MemberModel, Member>(parameter);
                MainRepository.InsertOrUpdate(itemToProcess, isInserted);

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Member,
                    Action = "Update Member",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = itemToProcess.MemberId > 0 ? LogType.Update : LogType.Create,
                    LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(itemToProcess)
                });

                return new ResultModel<int>(false, CommonConstants.SaveSuccess, itemToProcess.MemberId);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<int>(exception);
            }
        }
        public ResultModel<int> RemoveMemberImage(MemberModel parameter)
        {
            try
            {
                if (parameter == null)
                    return new ResultModel<int>(true, CommonConstants.SaveError, -1);
                if (parameter.MemberId <= 0 || string.IsNullOrEmpty(parameter.ImageUrl))
                    return new ResultModel<int>(true, CommonConstants.SaveError, -1);

                var dataBeforeUpdate = MainRepository.GetById<Member>(parameter.MemberId);
                if (dataBeforeUpdate.MemberId <= 0)
                    return new ResultModel<int>(true, CommonConstants.SaveError, -1);

                dataBeforeUpdate.LastModifiedBy = CurrentUser.UserId;
                dataBeforeUpdate.LastModifiedDate = DateTime.Now;
                dataBeforeUpdate.DataStatusType = DataStatusType.Modified;
                dataBeforeUpdate.ImageUrl = null;

                MainRepository.InsertOrUpdateNew(dataBeforeUpdate);

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Member,
                    Action = "Delete Member Image",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = LogType.Delete,
                    LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(dataBeforeUpdate)
                });
                return new ResultModel<int>(false, CommonConstants.DeleteSuccess, dataBeforeUpdate.MemberId);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<int>(exception);
            }
        }
        public ResultModel<MemberModel> GetById(MemberModel parameter)
        {
            try
            {
                var data = MainRepository.GetById<Member>(parameter.MemberId);
                Mapper.Initialize(c =>
                {
                    c.CreateMap<Member, MemberModel>();
                    c.CreateMap<MemberModel, Member>();
                });
                var returnData = Mapper.Map<Member, MemberModel>(data);
                return new ResultModel<MemberModel>(false, CommonConstants.Success, returnData);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<MemberModel>(exception);
            }
        }
        
        public ResultModel<MemberModel> Get(MemberModel parameter)
        {
            try
            {
                if (parameter == null)
                    return new ResultModel<MemberModel>()
                    {
                        IsError = false,
                        Message = CommonConstants.DataNull,
                        Data = new MemberModel()
                    };

                if (parameter.MemberId > 0)
                {
                    // Get Position
                    var data = GetById(parameter);
                    if (data.Data != null)
                    {
                        data.Data.IsDisableControl = parameter.IsDisableControl;                    
                    }
                    return data;
                }

                return new ResultModel<MemberModel>()
                {
                    IsError = false,
                    Message = CommonConstants.DataNull,
                    Data = new MemberModel()
                    {
                        IsDisableControl = parameter.IsDisableControl,
                        MemberTypeId = parameter.MemberTypeId
                    }
                };
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<MemberModel>(exception);
            }
        }
        
        public ResultModel<bool> Remove(MemberModel parameter)
        {
            try
            {
                if (parameter.MemberId <= 0)
                    return new ResultModel<bool>(true, CommonConstants.DeleteError, false);
                var dataBeforeUpdate = MainRepository.GetById<Member>(parameter.MemberId);

                dataBeforeUpdate.LastModifiedBy = CurrentUser.UserId;
                dataBeforeUpdate.LastModifiedDate = DateTime.Now;
                dataBeforeUpdate.DataStatusType = DataStatusType.Deleted;

                MainRepository.InsertOrUpdateNew(dataBeforeUpdate);

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Member,
                    Action = "Delete Member",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = LogType.Delete,
                    LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(dataBeforeUpdate)
                });
                return new ResultModel<bool>(false, CommonConstants.DeleteSuccess, true);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<bool>(exception);
            }
        }

	    public ResultModel<bool> UpdateMemberOrder(MemberOrderModel parameter)
	    {
            try
            {
                if (parameter.MemberTypeId <= 0)
                    return new ResultModel<bool>(true, CommonConstants.DeleteError, false);
                if (parameter.OrderItems.Count > 0)
                {
                    foreach (var memberModel in parameter.OrderItems)
                    {
                        parameter.Data += memberModel.MemberId +  ",";
                    }
                }

                MainRepository.UpdateMemberOrder(parameter);

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Member,
                    Action = "Update Member Order",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = LogType.Delete,
                    LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(parameter.Data)
                });
                return new ResultModel<bool>(false, CommonConstants.DeleteSuccess, true);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<bool>(exception);
            }
        }
        public ResultModel<List<int>> ChangePublishStatus(List<int> parameters)
        {
            if (parameters == null) return new ResultModel<List<int>>(true, CommonConstants.InvalidInput, null);
            if (parameters.Count <= 0) return new ResultModel<List<int>>(true, CommonConstants.InvalidInput, null);
            var result = new ResultModel<List<int>>()
            {
                Data = new List<int>()
            };
            foreach (var parameter in parameters)
            {
                var member = MainRepository.GetById<Member>(parameter);
                if (member != null && member.MemberId > 0)
                {
                    member.IsPublished = !member.IsPublished;
                    member.LastModifiedBy = CurrentUser.UserId;
                    member.LastModifiedDate = DateTime.Now;

                    MainRepository.InsertOrUpdateNew(member);
                    _commonBusiness.InsertLog(new LogModel()
                    {
                        ModuleId = (int)ObjectTypeEnum.Member,
                        Action = "Update Member Publish status",
                        CreatedBy = CurrentUser.UserId,
                        CreatedDate = DateTime.Now,
                        LogType = member.MemberId > 0 ? LogType.Update : LogType.Create,
                        LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(member)
                    });
                    result.Data.Add(member.MemberId);
                }
            }
            result.IsError = false;
            result.Message = CommonConstants.Success;
            return result;
        }

    }
}
  
