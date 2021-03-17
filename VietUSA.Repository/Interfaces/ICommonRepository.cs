using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Repository.Interfaces
{
    public interface ICommonRepository
    {
        ResultModel<bool> InsertLog(LogModel parameter);
    }
}
