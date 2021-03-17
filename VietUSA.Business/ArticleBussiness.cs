using System;
using System.Collections.Generic;
using AutoMapper;
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
    public class ArticleBusiness : BaseBusiness<ArticleRepository>, IArticleBusiness
    {
        readonly ICommonBusiness _commonBusiness;
        public ArticleBusiness(UserInfoModel userInfoModel) : base(new ArticleRepository(userInfoModel))
        {
            CurrentUser = userInfoModel;
            _commonBusiness = new CommonBusiness(userInfoModel);
        }
        public ListPagedResultModel<ArticleModel> SearchPaging(SearchModel<ArticleModel> parameter)
        {
            var searchResult = Search(parameter);

            var data = new ListPagedResultModel<ArticleModel>
            {
                ListItem = searchResult.Data,
                PageIndex = parameter.PageIndex,
                PageSize = parameter.PageSize,
                TotalRecord = searchResult.TotalRecord
            };
            return data;
        }
        public SearchResultModel<List<ArticleModel>> Search(SearchModel<ArticleModel> parameter)
        {
            try
            {
                return MainRepository.Search(parameter);
            }
            catch (Exception exception)
            {
                return HandleExceptions<ArticleModel>(exception);
            }
        }
        public ResultModel<int> InsertOrUpdate(ArticleModel parameter)
        {
            try
            {
                if (parameter.ArticleId > 0)
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
                var isInserted = parameter.ArticleId <= 0;
                Mapper.Initialize(c =>
                {
                    c.CreateMap<Article, ArticleModel>();
                    c.CreateMap<ArticleModel, Article>();
                });
                var itemToProcess = Mapper.Map<ArticleModel, Article>(parameter);
                MainRepository.InsertOrUpdate(itemToProcess, isInserted);

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Article,
                    Action = "Update Article",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = itemToProcess.ArticleId > 0 ? LogType.Update : LogType.Create,
                    LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(itemToProcess)
                });

                return new ResultModel<int>(false, CommonConstants.SaveSuccess, itemToProcess.ArticleId);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<int>(exception);
            }
        }

        public ResultModel<ArticleModel> GetById(ArticleModel parameter)
        {
            try
            {
                var data = MainRepository.GetById<Article>(parameter.ArticleId);
                Mapper.Initialize(c =>
                {
                    c.CreateMap<Article, ArticleModel>();
                    c.CreateMap<ArticleModel, Article>();
                });
                var returnData = Mapper.Map<Article, ArticleModel>(data);
                return new ResultModel<ArticleModel>(false, CommonConstants.Success, returnData);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<ArticleModel>(exception);
            }
        }

        public ResultModel<ArticleModel> Get(ArticleModel parameter)
        {
            try
            {
                if (parameter == null)
                    return new ResultModel<ArticleModel>()
                    {
                        IsError = false,
                        Message = CommonConstants.DataNull,
                        Data = new ArticleModel()
                    };

                if (parameter.ArticleId > 0)
                {
                    var data = GetById(parameter);
                    if (data.Data != null)
                    {
                        data.Data.IsDisableControl = parameter.IsDisableControl;
                    }
                    return data;
                }

                return new ResultModel<ArticleModel>()
                {
                    IsError = false,
                    Message = CommonConstants.DataNull,
                    Data = new ArticleModel()
                    {
                        IsDisableControl = parameter.IsDisableControl
                    }
                };
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<ArticleModel>(exception);
            }
        }

        public ResultModel<bool> Remove(ArticleModel parameter)
        {
            try
            {
                if (parameter.ArticleId <= 0)
                    return new ResultModel<bool>(true, CommonConstants.DeleteError, false);
                var dataBeforeUpdate = MainRepository.GetById<Article>(parameter.ArticleId);

                dataBeforeUpdate.LastModifiedBy = CurrentUser.UserId;
                dataBeforeUpdate.LastModifiedDate = DateTime.Now;
                dataBeforeUpdate.DataStatusType = DataStatusType.Deleted;

                MainRepository.InsertOrUpdateNew(dataBeforeUpdate);

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Article,
                    Action = "Delete Article",
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

        public ResultModel<int> RemoveArticleImage(ArticleModel parameter)
        {
            try
            {
                if (parameter == null)
                    return new ResultModel<int>(true, CommonConstants.SaveError, -1);
                if (parameter.ArticleId <= 0 || string.IsNullOrEmpty(parameter.ImageUrl))
                    return new ResultModel<int>(true, CommonConstants.SaveError, -1);

                var dataBeforeUpdate = MainRepository.GetById<Article>(parameter.ArticleId);
                if(dataBeforeUpdate.ArticleId <= 0)
                    return new ResultModel<int>(true, CommonConstants.SaveError, -1);

                dataBeforeUpdate.LastModifiedBy = CurrentUser.UserId;
                dataBeforeUpdate.LastModifiedDate = DateTime.Now;
                dataBeforeUpdate.DataStatusType = DataStatusType.Modified;
                dataBeforeUpdate.ImageUrl = null;

                MainRepository.InsertOrUpdateNew(dataBeforeUpdate);

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Article,
                    Action = "Delete Article Image",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = LogType.Delete,
                    LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(dataBeforeUpdate)
                });
                return new ResultModel<int>(false, CommonConstants.DeleteSuccess, dataBeforeUpdate.ArticleId);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<int>(exception);
            }
        }
    }
}

