using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.EFData.Entity;
using VietUSA.Repository.Models;
using VietUSA.Repository.Parameters;

namespace VietUSA.Business.Interfaces
{
    public interface IUserBusiness
    {
        ResultModel<User> Login(LoginParameter parameter);
        ResultModel<LoginModel> LoginAndProcess(LoginParameter parameter);
        ResultModel<List<GroupRoleModel>> GetRoleByUsername(User parameter);
        ResultModel<List<GroupRoleModel>> GetRoleByUserId(User parameter);
        ResultModel<int> UpdateOrInsert(UserModel dataModel);
        ResultModel<List<int>> ProcessCertificated(List<int> parameters);
        ResultModel<List<int>> RemoveRegister(List<int> parameters);
            //List<User> GetByAll(User inputObj);
        //string Delete(User objDelete);
        SearchResultModel<List<UserModel>> Search(SearchModel<UserModel> parameter);
       ListPagedResultModel<UserModel> SearchPaging(SearchModel<UserModel> parameter);
        ListPagedResultModel<UserModel> SearchPagingWithDocument(SearchModel<UserModel> parameter);
        //ResultModel<bool> ChangeFirstLoginPassword(LoginParameter parameter);
    }
}
