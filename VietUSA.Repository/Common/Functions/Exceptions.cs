using System;

namespace VietUSA.Repository.Common.Functions
{
    /// <summary>
    /// Ngoại lệ tại lớp BaseDAO
    /// </summary>
    public class BaseDaoException : Exception
    {

        public BaseDaoException(Exception inner)
            : base("Core Error", inner)
        {
        }

    }
    /// <summary>
    /// Ngoại lệ tại lớp DAO
    /// </summary>
    public class DaoException : Exception
    {

        public DaoException(Exception inner)
            : base("Repository Error", inner)
        {
        }
    }
    /// <summary>
    /// Ngoại lệ tại lớp BO
    /// </summary>
    public class BoException : Exception
    {
        public BoException(Exception inner)
            : base("Bussiness Error", inner)
        {
        }
    }
    /// <summary>
    /// Ngoại lệ tại lớp WebApplication
    /// </summary>
    public class WebException : Exception
    {

        public WebException(Exception inner)
            : base("WebApplication Error", inner)
        {
        }
    }
    /// <summary>
    /// Xử lý ngoại lệ
    /// </summary>
    public sealed class ExceptionHandlers
    {
        /// <summary>
        /// Xử lý ngoại lệ tại các tầng ứng dụng
        /// </summary>
        /// <param name="ex">Thông tin ngoại lệ</param>
        /// <param name="type">tầng gây ra ngoại lệ</param>
        public static void Handle(Exception ex, ExceptionTypes type)
        {
#if DEBUG
            throw ex;
#else
            switch (type)
            {
                case ExceptionTypes.BASE_DAO_EXCEPTION:
                    if (ex.GetType() == typeof(BaseDAOException))
                    {
                        throw ex;
                    }
                    else
                    {
                        CommonLib.LogService.Service.Error("Exception in BaseDAO!", ex);
                        throw new BaseDAOException(ex);
                    }

                case ExceptionTypes.DAO_EXCEPTION:
                    if (ex is BaseDAOException || ex is DAOException)
                    {
                        throw ex;
                    }
                    else
                    {
                        CommonLib.LogService.Service.Error("Exception in DataAcess!", ex);
                        throw new DAOException(ex);
                    }
                case ExceptionTypes.BO_EXCEPTION:
                    if (ex is BaseDAOException || ex is DAOException || ex is BOException)
                    {
                        throw ex;
                    }
                    else
                    {
                        CommonLib.LogService.Service.Error("Exception in Business!", ex);
                        throw new BOException(ex);
                    }
                case ExceptionTypes.WEB_EXCEPTION:
                    if (ex is WebException)
                    {
                        CommonLib.LogService.Service.Error("Exception in WebApplication!", ex);
                    }
                    break;
            }
#endif
        }
    }
    /// <summary>
    /// Loại ngoại lệ
    /// </summary>
    public enum ExceptionTypes
    {
        BaseDaoException,
        DaoException,
        BoException,
        WebException
    }

}
