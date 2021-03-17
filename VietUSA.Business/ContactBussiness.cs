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
	public class ContactBusiness : BaseBusiness<ContactRepository>, IContactBusiness
	{
        readonly ICommonBusiness _commonBusiness;
        public ContactBusiness(UserInfoModel userInfoModel) : base(new ContactRepository(userInfoModel))
        {
            CurrentUser = userInfoModel;
            _commonBusiness = new CommonBusiness(userInfoModel);
        }
        public ListPagedResultModel<ContactModel> SearchPaging(SearchModel<ContactModel> parameter)
        {
            var searchResult = Search(parameter);

            var data = new ListPagedResultModel<ContactModel>
            {
                ListItem = searchResult.Data,
                PageIndex = parameter.PageIndex,
                PageSize = parameter.PageSize,
                TotalRecord = searchResult.TotalRecord
            };
            return data;
        }
        public SearchResultModel<List<ContactModel>> Search(SearchModel<ContactModel> parameter)
        {
            try
            {
                return MainRepository.Search(parameter);
            }
            catch (Exception exception)
            {
                return HandleExceptions<ContactModel>(exception);
            }
        }
        public ResultModel<long> InsertOrUpdate(ContactModel parameter)
        {
            try
            {
                if (parameter.ContactId > 0)
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
                var isInserted = parameter.ContactId <= 0;
                Mapper.Initialize(c =>
                {
                    c.CreateMap<Contact, ContactModel>();
                    c.CreateMap<ContactModel, Contact>();
                });
                var itemToProcess = Mapper.Map<ContactModel, Contact>(parameter);
                MainRepository.InsertOrUpdate(itemToProcess, isInserted);

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Contact,
                    Action = "Update Contact",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = itemToProcess.ContactId > 0 ? LogType.Update : LogType.Create,
                    LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(itemToProcess)
                });

                return new ResultModel<long>(false, CommonConstants.SaveSuccess, itemToProcess.ContactId);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<long>(exception);
            }
        }
        
        public ResultModel<ContactModel> GetById(ContactModel parameter)
        {
            try
            {
                var data = MainRepository.GetById<Contact>(parameter.ContactId);
                Mapper.Initialize(c =>
                {
                    c.CreateMap<Contact, ContactModel>();
                    c.CreateMap<ContactModel, Contact>();
                });
                var returnData = Mapper.Map<Contact, ContactModel>(data);
                return new ResultModel<ContactModel>(false, CommonConstants.Success, returnData);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<ContactModel>(exception);
            }
        }
        
        public ResultModel<ContactModel> Get(ContactModel parameter)
        {
            try
            {
                if (parameter == null)
                    return new ResultModel<ContactModel>()
                    {
                        IsError = false,
                        Message = CommonConstants.DataNull,
                        Data = new ContactModel()
                    };

                if (parameter.ContactId > 0)
                {
                    var data = GetById(parameter);
                    if (data.Data != null)
                    {
                        data.Data.IsDisableControl = parameter.IsDisableControl;                    
                    }
                    return data;
                }

                return new ResultModel<ContactModel>()
                {
                    IsError = false,
                    Message = CommonConstants.DataNull,
                    Data = new ContactModel()
                    {
                        IsDisableControl = parameter.IsDisableControl
                    }
                };
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<ContactModel>(exception);
            }
        }
        
        public ResultModel<bool> Remove(ContactModel parameter)
        {
            try
            {
                if (parameter.ContactId <= 0)
                    return new ResultModel<bool>(true, CommonConstants.DeleteError, false);
                var dataBeforeUpdate = MainRepository.GetById<Contact>(parameter.ContactId);

                dataBeforeUpdate.LastModifiedBy = CurrentUser.UserId;
                dataBeforeUpdate.LastModifiedDate = DateTime.Now;
                dataBeforeUpdate.DataStatusType = DataStatusType.Deleted;

                MainRepository.InsertOrUpdateNew(dataBeforeUpdate);

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Contact,
                    Action = "Delete Contact",
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
	}
}
  
