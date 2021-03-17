using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Business.Interfaces
{
    public interface IReportBusiness
    {
        SearchResultModel<List<UserModel>> AbtractionSubmissionReport(SearchModel<UserModel> parameter);
    }
}
