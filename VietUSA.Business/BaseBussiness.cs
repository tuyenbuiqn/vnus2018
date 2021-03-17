using System;
using System.Collections.Generic;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;
//using VNPT.HN.DOT.Repository.EFData.Entity;

namespace VietUSA.Business
{
    public class BaseBusiness<TTRepository>
    {
        protected TTRepository MainRepository;
        protected UserInfoModel CurrentUser;

        public BaseBusiness(TTRepository repoParam)
        {
            MainRepository = repoParam;
            //ClassMapping();
        }

        //protected ResultModel<TEntity> ExecuteAction<TEntity>(Func<ResultModel<TEntity>> functionParam) where TEntity : new()
        //{
        //    try
        //    {
        //        var data = functionParam();
        //        return data;
        //    }
        //    catch (Exception exception)
        //    {
        //        ExceptionHandlers.Handle(exception, ExceptionTypes.BoException);
        //        return HandleExecuteActionResult<TEntity>(exception);
        //    }
        //}

        //protected ResultModel<TEntity> ExecuteAction<TEntity, TParameter>(Func<TParameter, ResultModel<TEntity>> functionParam, TParameter parameter) where TEntity : new()
        //{
        //    try
        //    {
        //        var data = functionParam(parameter);
        //        return data;
        //    }
        //    catch (Exception exception)
        //    {
        //        ExceptionHandlers.Handle(exception, ExceptionTypes.BoException);
        //        return HandleExecuteActionResult<TEntity>(exception);
        //    }
        //}

        //protected SearchResultModel<List<TEntity>> GetData<TEntity>(Func<ResultModel<List<TEntity>>> functionParam, string thanhPhanThem)
        //    where TEntity : IHasIdentityAndName, new()
        //{
        //    var dataList = GetData(functionParam);
        //    if (dataList.Data != null && !string.IsNullOrWhiteSpace(thanhPhanThem))
        //    {
        //        dataList.Data.Insert(0, new TEntity { Id = 0, Name = thanhPhanThem, Code = thanhPhanThem });
        //    }
        //    return dataList;
        //}

        //protected SearchResultModel<List<TEntity>> GetData<TEntity>(Func<ResultModel<List<TEntity>>> functionParam)
        //{
        //    try
        //    {
        //        var data = functionParam();
        //        return HandleFunctionResult(new SearchResultModel<List<TEntity>>
        //        {
        //            Data = data.Data,
        //            TotalRecord = 0,
        //            IsError = data.IsError,
        //            Message = data.Message
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        return HandleExceptions<TEntity>(exception);
        //    }
        //}

        //protected SearchResultModel<List<TEntity>> GetData<TEntity, TParameter>(Func<TParameter, ResultModel<List<TEntity>>> functionParam, TParameter parameter)
        //{
        //    try
        //    {
        //        var data = functionParam(parameter);
        //        return HandleFunctionResult(new SearchResultModel<List<TEntity>>
        //        {
        //            Data = data.Data,
        //            TotalRecord = 0,
        //            IsError = data.IsError,
        //            Message = data.Message
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        return HandleExceptions<TEntity>(exception);
        //    }
        //}

        //protected SearchResultModel<List<TEntity>> GetData<TEntity, TParameter>(Func<TParameter, SearchResultModel<List<TEntity>>> functionParam, TParameter parameter)
        //{
        //    try
        //    {
        //        var data = functionParam(parameter);
        //        return HandleFunctionResult(data);
        //    }
        //    catch (Exception exception)
        //    {
        //        return HandleExceptions<TEntity>(exception);
        //    }
        //}

        //protected SearchResultModel<List<TEntity>> GetData<TEntity, TParameter>(Func<TParameter, ResultModel<List<TEntity>>> functionParam, TParameter parameter, string thanhPhanThem)
        //    where TEntity : IHasIdentityAndName, new()
        //{
        //    try
        //    {
        //        var dataList = GetData(functionParam, parameter);
        //        if (dataList.Data != null && !string.IsNullOrWhiteSpace(thanhPhanThem))
        //        {
        //            dataList.Data.Insert(0, new TEntity { Id = 0, Name = thanhPhanThem });
        //        }
        //        return HandleFunctionResult(dataList);
        //    }
        //    catch (Exception exception)
        //    {
        //        return HandleExceptions<TEntity>(exception);
        //    }
        //}

        public static SearchResultModel<List<TEntity>> HandleExceptions<TEntity>(Exception exception)
        {
            ExceptionHandlers.Handle(exception, ExceptionTypes.BoException);
            return new SearchResultModel<List<TEntity>>
            {
                IsError = true,
                Message = exception.Message,
                Data = new List<TEntity>(),
                TotalRecord = 0
            };
        }

        public static SearchResultModel<List<TEntity>> HandleFunctionResult<TEntity>(SearchResultModel<List<TEntity>> data)
        {
            return new SearchResultModel<List<TEntity>>
            {
                IsError = data.IsError,
                Message = data.Message,
                Data = (!data.IsError && data.Data != null) ? data.Data : null,
                TotalRecord = (!data.IsError && data.Data != null) ? (data.TotalRecord > 0 ? data.TotalRecord : data.Data.Count) : 0
            };
        }

        public static ResultModel<TEntity> HandleExecuteActionResult<TEntity>(Exception exception) where TEntity : new()
        {
            return new ResultModel<TEntity>(true, exception.Message, new TEntity());
        }

        public static ResultModel<TEntity> HandleExecuteActionResultPrimitive<TEntity>(Exception exception)
        {
            return new ResultModel<TEntity>(true, exception.Message, default(TEntity));
        }

        //public static ListPagedResultModel<TEntity> ConvertSearchResultModelToListPagedResultModel<TEntity>(SearchResultModel<List<TEntity>> data)
        //{
        //    return new ListPagedResultModel<TEntity>
        //    {
        //        ListItem = data.Data,
        //        PageIndex = data.PageIndex,
        //        PageSize = data.PageSize,
        //        TotalRecord = data.TotalRecord
        //    };
        //}
    }
}
