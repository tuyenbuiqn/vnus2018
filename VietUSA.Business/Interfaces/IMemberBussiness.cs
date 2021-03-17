using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Business.Interfaces
{
    public  interface IMemberBusiness
    {
        ListPagedResultModel<MemberModel> SearchPaging(SearchModel<MemberModel> parameter);
        SearchResultModel<List<MemberModel>> Search(SearchModel<MemberModel> parameter);
        ResultModel<int> InsertOrUpdate(MemberModel parameter);
        ResultModel<int> RemoveMemberImage(MemberModel parameter);

        ResultModel<MemberModel> GetById(MemberModel parameter);
        ResultModel<MemberModel> Get(MemberModel parameter);
        ResultModel<bool> Remove(MemberModel parameter);
        ResultModel<bool> UpdateMemberOrder(MemberOrderModel parameter);
        ResultModel<List<int>> ChangePublishStatus(List<int> parameters);

    }
}
  
