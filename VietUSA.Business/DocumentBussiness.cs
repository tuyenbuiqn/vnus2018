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
    public class DocumentBusiness : BaseBusiness<DocumentRepository>, IDocumentBusiness
    {
        readonly ICommonBusiness _commonBusiness;
        public DocumentBusiness(UserInfoModel userInfoModel) : base(new DocumentRepository(userInfoModel))
        {
            CurrentUser = userInfoModel;
            _commonBusiness = new CommonBusiness(userInfoModel);
        }
        public ResultModel<int> UpdateOrInsert(DocumentModel parameter)
        {
            try
            {
                Mapper.Initialize(c =>
                {
                    c.CreateMap<Document, DocumentModel>();
                    c.CreateMap<DocumentModel, Document>();
                });

                var document = Mapper.Map<DocumentModel, Document>(parameter);

                return UpdateOrInsert(document);
            }
            catch (Exception exception)
            {
                return new ResultModel<int>()
                {
                    Message = exception.Message,
                    IsError = true
                };
            }

        }

        public ResultModel<int> UpdateOrInsert(DocumentUpdate parameter)
        {
            try
            {
                Mapper.Initialize(c =>
                {
                    c.CreateMap<Document, DocumentUpdate>();
                    c.CreateMap<DocumentUpdate, Document>();
                });

                var document = Mapper.Map<DocumentUpdate, Document>(parameter);
                if (document.DocumentId > 0)
                {
                    document.LastModifiedBy = CurrentUser.UserId;
                    document.LastModifiedDate = DateTime.Now;
                    document.DataStatusType = DataStatusType.Modified;
                }
                else
                {
                    document.CreatedBy = CurrentUser.UserId;
                    document.CreatedDate = DateTime.Now;
                    document.DataStatusType = DataStatusType.Created;
                }

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Document,
                    Action = "Update Document",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = document.DocumentId > 0 ? LogType.Update : LogType.Create,
                    LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(document)
                });
                return UpdateOrInsert(document);


            }
            catch (Exception exception)
            {
                return new ResultModel<int>()
                {
                    Message = exception.Message,
                    IsError = true
                };
            }

        }

        private ResultModel<int> UpdateOrInsert(Document document)
        {
            if (document.DocumentId > 0)
            {
                document.LastModifiedBy = CurrentUser.UserId;
                document.LastModifiedDate = DateTime.Now;
                document.DataStatusType = DataStatusType.Modified;
            }
            else
            {
                document.CreatedBy = CurrentUser.UserId;
                document.CreatedDate = DateTime.Now;
                document.DataStatusType = DataStatusType.Created;
            }

            MainRepository.InsertOrUpdateNew(document);
            _commonBusiness.InsertLog(new LogModel()
            {
                ModuleId = (int)ObjectTypeEnum.Document,
                Action = "Update Document",
                CreatedBy = CurrentUser.UserId,
                CreatedDate = DateTime.Now,
                LogType = document.DocumentId > 0 ? LogType.Update : LogType.Create,
                LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(document)
            });

            return new ResultModel<int>()
            {
                Data = document.DocumentId,
                IsError = false,
                Message = CommonConstants.SaveSuccess
            };

        }
        public ResultModel<DocumentModel> GetById(DocumentModel parameter)
        {
            try
            {
                var data = MainRepository.GetById<Document>(parameter.DocumentId);
                Mapper.Initialize(c =>
                {
                    c.CreateMap<Document, DocumentModel>();
                    c.CreateMap<DocumentModel, Document>();
                });
                var returnData = Mapper.Map<Document, DocumentModel>(data);
                return new ResultModel<DocumentModel>(false, CommonConstants.Success, returnData);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<DocumentModel>(exception);
            }
        }

        public ResultModel<DocumentModel> Get(DocumentModel parameter)
        {
            try
            {
                if (parameter == null)
                    return new ResultModel<DocumentModel>()
                    {
                        IsError = false,
                        Message = CommonConstants.DataNull,
                        Data = new DocumentModel()
                    };

                if (parameter.DocumentId > 0)
                {
                    var data = GetById(parameter);
                    if (data.IsError || data.Data == null)
                    {
                        return new ResultModel<DocumentModel>()
                        {
                            IsError = false,
                            Message = CommonConstants.DataNull,
                            Data = new DocumentModel()
                            {
                                ObjectId = parameter.ObjectId,
                                ObjectTypeName = parameter.ObjectTypeName,
                                ObjectTypeId = parameter.ObjectTypeId,
                                DocumentTypeId = CommonConstants.SelectAllId,
                                ContainerId = parameter.ContainerId
                            }
                        };
                    }

                    if (data.Data.ObjectId == 0)
                    {
                        data.Data.ObjectId = parameter.ObjectId;
                    }
                    if (string.IsNullOrEmpty(data.Data.ObjectTypeName))
                    {
                        data.Data.ObjectTypeName = parameter.ObjectTypeName;
                    }
                    if (data.Data.ObjectTypeId == 0)
                    {
                        data.Data.ObjectTypeId = parameter.ObjectTypeId;
                    }
                    if (data.Data.DocumentTypeId == 0)
                    {
                        data.Data.DocumentTypeId = parameter.DocumentTypeId;
                    }

                    data.Data.ContainerId = parameter.ContainerId;
                    return data;
                }

                return new ResultModel<DocumentModel>()
                {
                    IsError = false,
                    Message = CommonConstants.DataNull,
                    Data = new DocumentModel()
                    {
                        ObjectId = parameter.ObjectId,
                        ObjectTypeName = parameter.ObjectTypeName,
                        ObjectTypeId = parameter.ObjectTypeId,
                        DocumentTypeId = CommonConstants.SelectAllId,
                        ContainerId = parameter.ContainerId
                    }
                };
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<DocumentModel>(exception);
            }
        }

        public SearchResultModel<List<DocumentModel>> Search(SearchModel<DocumentModel> parameter)
        {
            return MainRepository.Search(parameter);
        }
        public ResultModel<DocumentSearchResult> Search(DocumentModel parameter)
        {
            try
            {
                var searchResult = MainRepository.Search(new SearchModel<DocumentModel>()
                {
                    PageIndex = PagingConstant.MinPageIndex,
                    PageSize = PagingConstant.GiganticPageSize,
                    Cretia = new DocumentModel
                    {
                        ObjectId = parameter.ObjectId,
                        ObjectTypeId = parameter.ObjectTypeId,
                        DocumentTypeId = parameter.DocumentTypeId,
                        OtherType = parameter.OtherType
                    }
                });

                var result = new ResultModel<DocumentSearchResult>()
                {
                    IsError = searchResult.IsError,
                    Message = searchResult.Message,
                    Data = new DocumentSearchResult()
                    {
                        Document = new DocumentModel()
                        {
                            ObjectId = parameter.ObjectId,
                            ObjectTypeName = parameter.ObjectTypeName,
                            ObjectTypeId = parameter.ObjectTypeId,
                            DocumentTypeId = parameter.DocumentTypeId,
                            DocumentName = parameter.DocumentName,
                            OtherType = parameter.OtherType,
                            OtherTypeName = parameter.ObjectTypeName,
                            ContainerId = parameter.ContainerId,
                            IsDisableControl = parameter.IsDisableControl

                        },
                        Documents = new List<DocumentModel>()
                    }
                };
                if (!searchResult.IsError)
                {
                    result.Data.Documents = searchResult.Data;
                }

                return result;
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<DocumentSearchResult>(exception);
            }
        }

        public ResultModel<bool> Remove(DocumentModel parameter)
        {
            try
            {
                if (parameter.DocumentId <= 0)
                    return new ResultModel<bool>(true, CommonConstants.DeleteError, false);
                var dataBeforeUpdate = MainRepository.GetById<Document>(parameter.DocumentId);

                dataBeforeUpdate.LastModifiedBy = CurrentUser.UserId;
                dataBeforeUpdate.LastModifiedDate = DateTime.Now;
                dataBeforeUpdate.DataStatusType = DataStatusType.Deleted;

                MainRepository.InsertOrUpdateNew(dataBeforeUpdate);

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Document,
                    Action = "Delete Document",
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

