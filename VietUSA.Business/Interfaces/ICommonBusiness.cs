using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Business.Interfaces
{
    public interface ICommonBusiness
    {
        ResultModel<bool> InsertLog(LogModel parameter);
        ResultModel<bool> SendEmail(EMailModel parameter);
    }
}
