using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Repository.Base
{
    public class EntityFrameworkReadOnlyRepository<TContext> : IDisposable
    where TContext : DbContext, new()
    {
        private TContext _context;
        private readonly object _syncRoot = new object();
        public readonly UserInfoModel CurrentUser;

        public EntityFrameworkReadOnlyRepository()
        {
        }
        public EntityFrameworkReadOnlyRepository(UserInfoModel currentUser)
        {
            CurrentUser = currentUser;
        }
        public TContext Context
        {
            get
            {
                if (_context == null)
                {
                    lock (_syncRoot)
                    {
                        if (_context == null)
                            _context = new TContext();
                    }
                }
                return _context;
            }
        }

        public SearchResultModel<List<TModel>> GetListDataPackage<TModel, TParameter>(string query, Func<SearchModel<TParameter>, List<object>> addCustomParams, SearchModel<TParameter> parameter) where TParameter : new()
        {
            var listParams = addCustomParams?.Invoke(parameter);
            SqlParameter pTotalRecord, pIsError, pMessage;

            AddCommonParameterForGettingListOfData(parameter, ref listParams, out pTotalRecord, out pIsError, out pMessage);
            
            var result = new SearchResultModel<List<TModel>> { TotalRecord = 0, PageSize = parameter.PageSize};
            try
            {
                var abc = Context.Database.SqlQuery<TModel>(query, listParams.ToArray());
                result.Data = abc.ToList();
                result.TotalRecord = Convert.ToInt32(pTotalRecord.Value);
                var isError = int.Parse(pIsError.Value.ToString());
                result.IsError = isError == 1;
                result.Message = pMessage.Value.ToString();
                return result;
            }
            catch (Exception exc)
            {
                throw exc;

            }
        }

        protected void AddCommonParameterForGettingListOfData<TParameter>(SearchModel<TParameter> parameter, ref List<object> parameters,
            out SqlParameter pTotalRecord, out SqlParameter pIsError, out SqlParameter pMessage) where TParameter : new()
        {
            
            parameters.Add(new SqlParameter("pPageIndex", parameter.PageIndex));
            parameters.Add(new SqlParameter("pPageSize", parameter.PageSize));
            parameters.Add(new SqlParameter("pOrderColumn", parameter.ColumnOrder?? string.Empty));
            pTotalRecord = new SqlParameter("pTotalRecords", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };

            pIsError = new SqlParameter("pIsError", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            pMessage = new SqlParameter("pMessage", SqlDbType.NVarChar, 250)
            {
                Direction = ParameterDirection.Output
            };

            
            parameters.Add(pTotalRecord);
            parameters.Add(pIsError);
            parameters.Add(pMessage);
        }
        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null) where TEntity : class
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = Context.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }
            return query;
        }
        public virtual IEnumerable<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null) where TEntity : class
        {
            return GetQueryable(null, orderBy, includeProperties, skip, take).ToList();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null) where TEntity : class
        {
            return await GetQueryable(null, orderBy, includeProperties, skip, take).ToListAsync();
        }
        public virtual IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null) where TEntity : class
        {
            return GetQueryable(filter, orderBy, includeProperties, skip, take).ToList();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null) where TEntity : class
        {
            return await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }
        public virtual TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "") where TEntity : class
        {
            return GetQueryable(filter, null, includeProperties).SingleOrDefault();
        }
        public virtual async Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null) where TEntity : class
        {
            return await GetQueryable(filter, null, includeProperties).SingleOrDefaultAsync();
        }
        public virtual TEntity GetFirst<TEntity>(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "") where TEntity : class
        {
            return GetQueryable(filter, orderBy, includeProperties).FirstOrDefault();
        }
        public virtual async Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null) where TEntity : class
        {
            return await GetQueryable(filter, orderBy, includeProperties).FirstOrDefaultAsync();
        }
        public virtual TEntity GetById<TEntity>(object id) where TEntity : class
        {
            return Context.Set<TEntity>().Find(id);
        }
        public virtual Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : class
        {
            return Context.Set<TEntity>().FindAsync(id);
        }
        public virtual int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            return GetQueryable(filter).Count();
        }
        public virtual Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            return GetQueryable(filter).CountAsync();
        }
        public virtual bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            return GetQueryable(filter).Any();
        }
        public virtual Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            return GetQueryable(filter).AnyAsync();
        }

        //protected static ResultModelMoreInfor<List<TModel>> HandleException<TModel>(ResultModelMoreInfor<List<TModel>> returnedData, Exception objErr)
        //{
        //    returnedData.IsError = true;
        //    returnedData.Message = LogMessage.ErrorLoadingData;
        //    WebFleepConsumer webFleepLogger = new WebFleepConsumer();
        //    webFleepLogger.HookMessage("*[" + DateTime.Now + "][" + LogError.RepositoryException + "]* \n::: \n" + objErr + "\n :::", Constants.RestApiConstant.FleepWebApplicationHookId, Constants.RestApiConstant.WebUserHook);
        //    //LogManager.GetLogger(LoggerName.REPOSITORY).Error(LogError.RepositoryException, objErr);
        //    return returnedData;
        //}

        //protected static ResultModel<TModel> HandleException<TModel>(ResultModel<TModel> returnedData, Exception objErr)
        //{
        //    returnedData.IsError = true;
        //    returnedData.Message = LogMessage.ErrorOccured;
        //    WebFleepConsumer webFleepLogger = new WebFleepConsumer();
        //    webFleepLogger.HookMessage("*[" + DateTime.Now + "][" + LogError.RepositoryException + "]* \n::: \n" + objErr + "\n :::", Constants.RestApiConstant.FleepWebApplicationHookId, Constants.RestApiConstant.WebUserHook);
        //    //LogManager.GetLogger(LoggerName.REPOSITORY).Error(LogError.RepositoryException, objErr);
        //    return returnedData;
        //}

        //protected static ResultModel<TModel> HandleCustomMessage<TModel>(string message)
        //{
        //    ResultModel<TModel> returnedData = new ResultModel<TModel>
        //    {
        //        IsError = true,
        //        Message = message
        //    };
        //    return returnedData;
        //}

        void ReleaseManagedResources()
        {
            Context?.Database.Connection.Dispose();
            Context?.Dispose();
            //WebFleepLogger.HookMessage("*[" + DateTime.Now + "][TEST CONTEXT DISPOSING]* \n::: \n" + Context + "\n :::", Constants.RestApiConstant.FleepWebApplicationHookId, Constants.RestApiConstant.WebUserHook);
        }


        public void Dispose()
        {
            // If this function is being called the user wants to release the
            // resources. lets call the Dispose which will do this for us.
            Dispose(true);

            // Now since we have done the cleanup already there is nothing left
            // for the Finalizer to do. So lets tell the GC not to call it later.
            GC.SuppressFinalize(this);
            //Context.Dispose();

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //someone want the deterministic release of all resources
                //Let us release all the managed resources
                ReleaseManagedResources();
            }
            else
            {
                // Do nothing, no one asked a dispose, the object went out of
                // scope and finalized is called so lets next round of GC 
                // release these resources
            }

        }

        ~EntityFrameworkReadOnlyRepository()
        {
            Dispose(false);
        }
    }
}
