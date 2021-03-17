using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.EFData.Entity;
using VietUSA.Repository.Models;
using VietUSA.Repository.Parameters;

namespace VietUSA.Repository.Interfaces
{
    public interface IUserRepository
    {
        //string UpdateOrInsert(User objUser);
        //List<User> GetbyAll(User inputObj);
        ResultModel<User> Login(LoginParameter parameter);
        ResultModel<List<GroupRoleModel>> GetRoleByUsername(User parameter);
        ResultModel<List<GroupRoleModel>> GetRoleByUserId(User parameter);
        SearchResultModel<List<UserModel>> Search(SearchModel<UserModel> parameter);
    }
}
