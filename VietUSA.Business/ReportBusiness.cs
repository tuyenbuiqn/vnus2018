using System.Collections.Generic;
using VietUSA.Business.Interfaces;
using VietUSA.Repository;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Business
{
    public class ReportBusiness : BaseBusiness<ReportRepository>, IReportBusiness
    {
        public ReportBusiness(UserInfoModel userInfoModel) : base(new ReportRepository(userInfoModel))
        {
            CurrentUser = userInfoModel;
        }

        public SearchResultModel<List<UserModel>> AbtractionSubmissionReport(SearchModel<UserModel> parameter)
        {
            return MainRepository.AbtractionSubmissionReport(parameter);
        }
    }
}
