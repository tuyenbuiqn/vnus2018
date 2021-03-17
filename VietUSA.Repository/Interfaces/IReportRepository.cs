using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Repository.Interfaces
{
    public interface IReportRepository
    {
        SearchResultModel<List<UserModel>> AbtractionSubmissionReport(SearchModel<UserModel> parameter);
    }
}
