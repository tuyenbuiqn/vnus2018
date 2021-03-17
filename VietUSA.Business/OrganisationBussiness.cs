using System;
using System.Collections.Generic;
using AutoMapper;
using OfficeOpenXml.FormulaParsing.Utilities;
using VietUSA.Business.Interfaces;
using VietUSA.Repository;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Enums;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.EFData.Entity;
using VietUSA.Repository.Models;

namespace VietUSA.Business
{
    public class OrganisationBusiness : BaseBusiness<OrganisationRepository>, IOrganisationBusiness
    {
        readonly ICommonBusiness _commonBusiness;
        private readonly ICategoryBusiness _categoryBusiness;
        public OrganisationBusiness(UserInfoModel userInfoModel) : base(new OrganisationRepository(userInfoModel))
        {
            CurrentUser = userInfoModel;
            _commonBusiness = new CommonBusiness(userInfoModel);
            _categoryBusiness = new CategoryBusiness(userInfoModel);
        }
        public ListPagedResultModel<OrganisationModel> SearchPaging(SearchModel<OrganisationModel> parameter)
        {
            var searchResult = Search(parameter);

            var data = new ListPagedResultModel<OrganisationModel>
            {
                ListItem = searchResult.Data,
                PageIndex = parameter.PageIndex,
                PageSize = parameter.PageSize,
                TotalRecord = searchResult.TotalRecord
            };
            return data;
        }
        public SearchResultModel<List<OrganisationModel>> Search(SearchModel<OrganisationModel> parameter)
        {
            try
            {
                return MainRepository.Search(parameter);
            }
            catch (Exception exception)
            {
                return HandleExceptions<OrganisationModel>(exception);
            }
        }
        public ResultModel<int> InsertOrUpdate(OrganisationModel parameter)
        {
            try
            {
                if (parameter.OrganisationId > 0)
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
                var isInserted = parameter.OrganisationId <= 0;
                Mapper.Initialize(c =>
                {
                    c.CreateMap<Organisation, OrganisationModel>();
                    c.CreateMap<OrganisationModel, Organisation>();
                });
                var itemToProcess = Mapper.Map<OrganisationModel, Organisation>(parameter);
                MainRepository.InsertOrUpdate(itemToProcess, isInserted);
                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Organisation,
                    Action = "Update Organisation",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = itemToProcess.OrganisationId > 0 ? LogType.Update : LogType.Create,
                    LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(itemToProcess)
                });
                return new ResultModel<int>(false, CommonConstants.SaveSuccess, itemToProcess.OrganisationId);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<int>(exception);
            }
        }

        public ResultModel<OrganisationModel> GetById(OrganisationModel parameter)
        {
            try
            {
                var data = MainRepository.GetById<Organisation>(parameter.OrganisationId);
                Mapper.Initialize(c =>
                {
                    c.CreateMap<Organisation, OrganisationModel>();
                    c.CreateMap<OrganisationModel, Organisation>();
                });
                var returnData = Mapper.Map<Organisation, OrganisationModel>(data);
                return new ResultModel<OrganisationModel>(false, CommonConstants.Success, returnData);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<OrganisationModel>(exception);
            }
        }

        public ResultModel<OrganisationModel> Get(OrganisationModel parameter)
        {
            try
            {
                var organisationTypes = _categoryBusiness.Search(new SearchModel<CategoryModel>()
                {
                    PageIndex = PagingConstant.MinPageIndex,
                    PageSize = PagingConstant.LargePageSize,
                    Cretia = new CategoryModel()
                    {
                        IsPublished = true,
                        GroupCategoryId = (int)GroupCategoryEnum.OrganizerType
                    }
                });
                if (parameter == null)
                    return new ResultModel<OrganisationModel>()
                    {
                        IsError = false,
                        Message = CommonConstants.DataNull,
                        Data = new OrganisationModel()
                        {
                            OrganisationTypes = organisationTypes.Data
                        }
                    };


                if (parameter.OrganisationId > 0)
                {
                    var data = GetById(parameter);
                    if (data.Data != null)
                    {
                        data.Data.IsDisableControl = parameter.IsDisableControl;
                        data.Data.OrganisationTypes = organisationTypes.Data;
                    }
                    return data;
                }

                return new ResultModel<OrganisationModel>()
                {
                    IsError = false,
                    Message = CommonConstants.DataNull,
                    Data = new OrganisationModel()
                    {
                        IsDisableControl = parameter.IsDisableControl,
                        OrganisationTypes = organisationTypes.Data
                    }
                };
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<OrganisationModel>(exception);
            }
        }

        public ResultModel<bool> Remove(OrganisationModel parameter)
        {
            try
            {
                if (parameter.OrganisationId <= 0)
                    return new ResultModel<bool>(true, CommonConstants.DeleteError, false);
                var dataBeforeUpdate = MainRepository.GetById<Organisation>(parameter.OrganisationId);

                dataBeforeUpdate.LastModifiedBy = CurrentUser.UserId;
                dataBeforeUpdate.LastModifiedDate = DateTime.Now;
                dataBeforeUpdate.DataStatusType = DataStatusType.Deleted;

                MainRepository.InsertOrUpdateNew(dataBeforeUpdate);

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Organisation,
                    Action = "Delete Organisation",
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
        public ResultModel<bool> UpdateOrganisationOrder(OrganisationOrderModel parameter)
        {
            try
            {
                if (parameter.OrderItems.Count > 0)
                {
                    foreach (var organisationModel in parameter.OrderItems)
                    {
                        parameter.Data += organisationModel.OrganisationId + ",";
                    }
                }

                MainRepository.UpdateOrganisationOrder(parameter);

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Organisation,
                    Action = "Update Organisation Order",
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
        public ResultModel<int> RemoveOrganisationImage(OrganisationModel parameter)
        {
            try
            {
                if (parameter == null)
                    return new ResultModel<int>(true, CommonConstants.SaveError, -1);
                if (parameter.OrganisationId <= 0 || string.IsNullOrEmpty(parameter.ImageUrl))
                    return new ResultModel<int>(true, CommonConstants.SaveError, -1);

                var dataBeforeUpdate = MainRepository.GetById<Organisation>(parameter.OrganisationId);
                if (dataBeforeUpdate.OrganisationId <= 0)
                    return new ResultModel<int>(true, CommonConstants.SaveError, -1);

                dataBeforeUpdate.LastModifiedBy = CurrentUser.UserId;
                dataBeforeUpdate.LastModifiedDate = DateTime.Now;
                dataBeforeUpdate.DataStatusType = DataStatusType.Modified;
                dataBeforeUpdate.ImageUrl = null;

                MainRepository.InsertOrUpdateNew(dataBeforeUpdate);

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Organisation,
                    Action = "Delete Organisation Image",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = LogType.Delete,
                    LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(dataBeforeUpdate)
                });
                return new ResultModel<int>(false, CommonConstants.DeleteSuccess, dataBeforeUpdate.OrganisationId);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<int>(exception);
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
                var organisation = MainRepository.GetById<Organisation>(parameter);
                if (organisation != null && organisation.OrganisationId > 0)
                {
                    organisation.IsPublished = !organisation.IsPublished;
                    organisation.LastModifiedBy = CurrentUser.UserId;
                    organisation.LastModifiedDate = DateTime.Now;

                    MainRepository.InsertOrUpdateNew(organisation);
                    _commonBusiness.InsertLog(new LogModel()
                    {
                        ModuleId = (int)ObjectTypeEnum.Organisation,
                        Action = "Update Organisation Publish status",
                        CreatedBy = CurrentUser.UserId,
                        CreatedDate = DateTime.Now,
                        LogType = organisation.OrganisationId > 0 ? LogType.Update : LogType.Create,
                        LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(organisation)
                    });
                    result.Data.Add(organisation.OrganisationId);
                }
            }
            result.IsError = false;
            result.Message = CommonConstants.Success;
            return result;
        }

        public ResultModel<List<OrganisationViewModel>> GetOrganisation(OrganisationModel parameter)
        {
            var result = new ResultModel<List<OrganisationViewModel>>()
            {
                Data = new List<OrganisationViewModel>()
            };
            var categoryData = _categoryBusiness.Search(new SearchModel<CategoryModel>()
            {
                PageIndex = PagingConstant.MinPageIndex,
                PageSize = PagingConstant.LargePageSize,
                Cretia = new CategoryModel()
                {
                    DisplayInHomePage = parameter.DisplayInHomePage,
                    DisplayInContentPage = parameter.DisplayInContentPage,
                    GroupCategoryId = (int)GroupCategoryEnum.OrganizerType,
                    CategoryId = parameter.CategoryId,
                    IsPublished = true,
                    IsSystemCategory = false
                }
            });

            if (!categoryData.IsError)
            {
                foreach (var categoryModel in categoryData.Data)
                {
                    var item = new OrganisationViewModel()
                    {
                        CategoryId = categoryModel.CategoryId,
                        CategoryName = categoryModel.CategoryName
                    };
                 
                    var organisationData = Search(new SearchModel<OrganisationModel>()
                    {
                        PageIndex = PagingConstant.MinPageIndex,
                        PageSize = PagingConstant.LargePageSize,
                        Cretia = new OrganisationModel()
                        {
                            DisplayInHomePage = parameter.DisplayInHomePage,
                            DisplayInContentPage = parameter.DisplayInContentPage,
                            OrganisationTypeId = categoryModel.CategoryId,
                            IsPublished = true
                        }
                    });
                    if (!organisationData.IsError)
                    {
                        item.Organisations = organisationData.Data;
                    }
                    result.Data.Add(item);
                }
                result.IsError = false;
                return result;
            }
            else
            {
                return new ResultModel<List<OrganisationViewModel>>(true, CommonConstants.ErrorOccurs, new List<OrganisationViewModel>());
            }
        }
    }
}

