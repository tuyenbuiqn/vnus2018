using AutoMapper;
using VietUSA.Repository.Base;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.EFData.Entity;
using VietUSA.Repository.Interfaces;
using VietUSA.Repository.Models;

namespace VietUSA.Repository
{

    public class CommonRepository : EntityFrameworkRepository, ICommonRepository
    {
        public CommonRepository(UserInfoModel userInfoModel) : base(userInfoModel)
        {

        }
        public ResultModel<bool> InsertLog(LogModel parameter)
        {
            Mapper.Initialize(c =>
               {
                   c.CreateMap<LogModel, Log>();
               });
            var log = Mapper.Map<LogModel, Log>(parameter);
            Context.Set<Log>().Add(log);
            Context.SaveChanges();
            return new ResultModel<bool>(false, CommonConstants.SaveError, false);
        }

     
    }
}
