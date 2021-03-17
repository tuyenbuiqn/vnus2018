using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CategoryBusiness : BaseBusiness<CategoryRepository>, ICategoryBusiness
    {
        private readonly ICommonBusiness _commonBusiness;
        public CategoryBusiness(UserInfoModel userInfoModel) : base(new CategoryRepository(userInfoModel))
        {
            CurrentUser = userInfoModel;
            _commonBusiness = new CommonBusiness(userInfoModel);
        }
        #region Category
        public ListPagedResultModel<CategoryModel> SearchPaging(SearchModel<CategoryModel> parameter)
        {
            var searchResult = Search(parameter);

            var data = new ListPagedResultModel<CategoryModel>
            {
                ListItem = searchResult.Data,
                PageIndex = parameter.PageIndex,
                PageSize = parameter.PageSize,
                TotalRecord = searchResult.TotalRecord
            };
            return data;
        }
        public ListPagedResultModel<CategoryModel> SearchPagingFilter(SearchModel<CategoryModel> parameter)
        {
            var searchResult = SearchFilter(parameter);

            var data = new ListPagedResultModel<CategoryModel>
            {
                ListItem = searchResult.Data,
                PageIndex = parameter.PageIndex,
                PageSize = parameter.PageSize,
                TotalRecord = searchResult.TotalRecord
            };
            return data;
        }
        public SearchResultModel<List<CategoryModel>> SearchFilter(SearchModel<CategoryModel> parameter)
        {
            try
            {
                return MainRepository.SearchFilter(parameter);
            }
            catch (Exception exception)
            {
                return HandleExceptions<CategoryModel>(exception);
            }
        }
        public SearchResultModel<List<CategoryModel>> Search(SearchModel<CategoryModel> parameter)
        {
            try
            {
                return MainRepository.Search(parameter);
            }
            catch (Exception exception)
            {
                return HandleExceptions<CategoryModel>(exception);
            }
        }
        public ResultModel<bool> UpdateOrInsert(CategoryModel parameter)
        {
            try
            {
                if (parameter.CategoryId > 0)
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
                var isInserted = parameter.CategoryId <= 0;
                Mapper.Initialize(c =>
               {
                   c.CreateMap<Category, CategoryModel>();
                   c.CreateMap<CategoryModel, Category>();
               });
                var category = Mapper.Map<CategoryModel, Category>(parameter);
                MainRepository.InsertOrUpdate(category, isInserted);

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Category,
                    Action = "Update Category",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = category.CategoryId > 0 ? LogType.Update : LogType.Create,
                    LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(category)
                });
                return new ResultModel<bool>(false, CommonConstants.SaveSuccess, true);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<bool>(exception);
            }
        }
        public ResultModel<bool> RemoveCategory(CategoryModel parameter)
        {
            try
            {
                if (parameter == null)
                    return new ResultModel<bool>(true, CommonConstants.SaveError, false);

                var dataBeforeUpdate = MainRepository.GetById<Member>(parameter.CategoryId);
                if (dataBeforeUpdate.MemberId <= 0)
                    return new ResultModel<bool>(true, CommonConstants.SaveError, false);

                dataBeforeUpdate.LastModifiedBy = CurrentUser.UserId;
                dataBeforeUpdate.LastModifiedDate = DateTime.Now;
                dataBeforeUpdate.DataStatusType = DataStatusType.Deleted;

                MainRepository.InsertOrUpdateNew(dataBeforeUpdate);

                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.Category,
                    Action = "Delete Category",
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
      
        public ResultModel<CategoryModel> GetById(CategoryModel parameter)
        {
            try
            {
                var data = MainRepository.GetById<Category>(parameter.CategoryId);
                  Mapper.Initialize(c =>
               {
                   c.CreateMap<Category, CategoryModel>();
                   c.CreateMap<CategoryModel, Category>();
               });
                var returnData = Mapper.Map<Category, CategoryModel>(data);
                return new ResultModel<CategoryModel>(false, CommonConstants.Success, returnData);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<CategoryModel>(exception);
            }
        }
        public ResultModel<CategoryModel> GetCategory(CategoryModel parameter)
        {
            try
            {
                if (parameter == null)
                    return new ResultModel<CategoryModel>()
                    {
                        IsError = false,
                        Message = CommonConstants.DataNull,
                        Data = new CategoryModel()
                    };

                if (parameter.CategoryId > 0)
                {
                    return GetById(parameter);
                }

                return new ResultModel<CategoryModel>()
                {
                    IsError = false,
                    Message = CommonConstants.DataNull,
                    Data = new CategoryModel()
                    {
                        GroupCategoryId = parameter.GroupCategoryId
                    }
                };
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<CategoryModel>(exception);
            }
        }
        public ResultModel<SelectModel> CategorySearchToSelect2(SelectModel parameter)
        {
            try
            {
                if (parameter == null)
                {
                    return new ResultModel<SelectModel>(true, "InputModelIsNull", null);
                }
                var result = parameter;
                result.Data = new List<CategoryModel>();
                var data = Search(new SearchModel<CategoryModel>()
                {
                    PageIndex = PagingConstant.MinPageIndex,
                    PageSize = PagingConstant.PageSize5000,
                    Cretia = new CategoryModel()
                    {
                        GroupCategoryId = parameter.GroupCategoryId,
                        IsSystemCategory = false
                    }
                });
                if (parameter.HasDefaultValue)
                    if (!string.IsNullOrEmpty(parameter.DefaultText))
                    {
                        result.Data.Add(new CategoryModel
                        {
                            CategoryId = CommonConstants.SelectAllId,
                            CategoryName = parameter.DefaultText
                        });
                    }
                    else
                        result.Data.Add(new CategoryModel
                        {
                            CategoryId = CommonConstants.SelectAllId,
                            CategoryName = CommonConstants.SelectAll
                        });

                if (!data.IsError)
                {
                    result.Data.AddRange(data.Data);
                }
                return new ResultModel<SelectModel>(false, CommonConstants.Success, result);
            }
            catch (Exception exception)
            {
                return new ResultModel<SelectModel>(true, exception.Message, null);
            }
        }
        public ResultModel<SelectModel> GroupCategoryToSelect2(SelectModel parameter)
        {
            try
            {
                if (parameter == null)
                {
                    return new ResultModel<SelectModel>(true, "InputModelIsNull", null);
                }
                var result = parameter;
                result.Data = new List<Select2Data>();

                if (parameter.HasDefaultValue)
                    if (!string.IsNullOrEmpty(parameter.DefaultText))
                    {
                        result.Data.Add(new Select2Data
                        {
                            Id = CommonConstants.SelectAllId,
                            Text = parameter.DefaultText
                        });
                    }
                    else
                        result.Data.Add(new Select2Data
                        {
                            Id = CommonConstants.SelectAllId,
                            Text = CommonConstants.SelectAll
                        });


                result.Data.AddRange(CommonFunctions.GroupCategoryEnumToList());
                return new ResultModel<SelectModel>(false, CommonConstants.Success, result);
            }
            catch (Exception exception)
            {
                return new ResultModel<SelectModel>(true, exception.Message, null);
            }
        }
        public ResultModel<SelectModel> CategorySearchToSelect2ByParentOrGroupCategories(SelectModel parameter)
        {
            try
            {
                if (parameter == null)
                {
                    return new ResultModel<SelectModel>(true, "InputModelIsNull", null);
                }
                var result = parameter;
                result.Data = new List<CategoryModel>();
                var fullData = new SearchResultModel<List<CategoryModel>>()
                {
                    Data = new List<CategoryModel>()
                };

                if (parameter.ParentId > 0)
                {
                    fullData = Search(new SearchModel<CategoryModel>()
                    {
                        PageIndex = PagingConstant.MinPageIndex,
                        PageSize = PagingConstant.PageSize5000,
                        Cretia = new CategoryModel()
                        {
                            GroupCategoryId = (int)parameter.ParentId,
                            IsSystemCategory = false
                        }
                    });
                }
                else
                {
                    foreach (var item in parameter.GroupCategoryIds)
                    {
                        var data = Search(new SearchModel<CategoryModel>()
                        {
                            PageIndex = PagingConstant.MinPageIndex,
                            PageSize = PagingConstant.PageSize5000,
                            Cretia = new CategoryModel()
                            {
                                GroupCategoryId = item,
                                IsSystemCategory = false
                            }
                        });
                        if (!data.IsError)
                            fullData.Data.AddRange(data.Data);
                    }
                }

                if (parameter.HasDefaultValue)
                    if (!string.IsNullOrEmpty(parameter.DefaultText))
                    {
                        result.Data.Add(new CategoryModel
                        {
                            CategoryId = CommonConstants.SelectAllId,
                            CategoryName = parameter.DefaultText
                        });
                    }
                    else
                        result.Data.Add(new CategoryModel
                        {
                            CategoryId = CommonConstants.SelectAllId,
                            CategoryName = CommonConstants.SelectAll
                        });

                if (!fullData.IsError)
                {
                    result.Data.AddRange(fullData.Data);
                }
                return new ResultModel<SelectModel>(false, CommonConstants.Success, result);
            }
            catch (Exception exception)
            {
                return new ResultModel<SelectModel>(true, exception.Message, null);
            }
        }
        public ResultModel<SelectModel> TraBusCategorySearchToSelect2(SelectModel parameter)
        {
            try
            {
                if (parameter == null)
                {
                    return new ResultModel<SelectModel>(true, "InputModelIsNull", null);
                }
                var result = parameter;
                result.Data = new List<CategoryModel>();
                var data = Search(new SearchModel<CategoryModel>()
                {
                    PageIndex = PagingConstant.MinPageIndex,
                    PageSize = PagingConstant.PageSize5000,
                    Cretia = new CategoryModel()
                    {
                        GroupCategoryId = parameter.GroupCategoryId,
                        IsSystemCategory = false
                    }
                });
                if (parameter.HasDefaultValue)
                    result.Data.Add(new CategoryModel
                    {
                        CategoryId = CommonConstants.SelectAllId,
                        CategoryName = parameter.DefaultText
                    });

                if (!data.IsError)
                {
                    result.Data.AddRange(data.Data.OrderBy(x => x.CategoryId));
                }
                return new ResultModel<SelectModel>(false, CommonConstants.Success, result);
            }
            catch (Exception exception)
            {
                return new ResultModel<SelectModel>(true, exception.Message, null);
            }
        }
        public ResultModel<List<Category>> GetCategoryByGroup(int groupId)
        {
            try
            {
                return MainRepository.GetCategoryByGroup(groupId);
            }
            catch (Exception exception)
            {
                return new ResultModel<List<Category>>()
                {
                    IsError = false,
                    Message = exception.Message,
                    Data = null
                };
            }
        }

        #endregion End Category
      
    }
}
