using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Repository.Interfaces
{
    public  interface IMemberRepository
    {
        SearchResultModel<List<MemberModel>> Search(SearchModel<MemberModel> parameter);
        ResultModel<bool> UpdateMemberOrder(MemberOrderModel parameter);
    }
}
  
