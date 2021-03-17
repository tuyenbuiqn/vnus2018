using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using VietUSA.Repository.Base;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.EFData.Entity;
using VietUSA.Repository.Interfaces;
using VietUSA.Repository.Models;
using VietUSA.Repository.Parameters;

namespace VietUSA.Repository
{
    public class UserRepository : EntityFrameworkRepository, IUserRepository
    {
        public UserRepository(UserInfoModel userInfoModel) : base(userInfoModel)
        {

        }
        //public string UpdateOrInsert(User objUser)
        //{
        //    try
        //    {
        //        if (objUser.UserId > 0)
        //        {
        //            base.InsertOrUpdate(objUser, false);
        //        }
        //        else
        //        {
        //            base.InsertOrUpdate(objUser, true);
        //        }
        //        return string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }

        //}

        //public List<User> GetbyAll(User inputObj)
        //{
        //    var linqSql = from data in Context.Users
        //                  select data;

        //    if (inputObj.UserId > 0)
        //        linqSql = linqSql.Where(o => o.UserId == inputObj.UserId);
        //    if (!string.IsNullOrEmpty(inputObj.Username))
        //        linqSql = linqSql.Where(o => o.Username.Contains(inputObj.Username));

        //    return linqSql.ToList();
        //}

        public ResultModel<User> Login(LoginParameter parameter)
        {
            var result = new ResultModel<User>()
            {
                IsError = false,
                Message = string.Empty,
                Data = new User()
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output }; 
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar,500) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<UserModel>("dbo.SP_LOGIN @pUsername, @pPassword, @pIsError out, @pMessage out", 
                CommonFunctions.GetParameter(parameter.Username, "pUsername"), CommonFunctions.GetParameter(parameter.Password, "pPassword"), isError, message).FirstOrDefault();

            result.IsError = (bool)isError.Value;
            result.Message = message.Value.ToString();
            result.Data = data;

            return result;
        }
        
        public ResultModel<List<GroupRoleModel>> GetRoleByUsername(User parameter)
        {
            var result = new ResultModel<List<GroupRoleModel>>()
            {
                IsError = false,
                Message = string.Empty,
                Data = new List<GroupRoleModel>()
            };
            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar,500) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<SP_GET_ROLE_BY_USER_Result>("dbo.SP_GET_ROLE_BY_USER @pUsername, @pIsError out, @pMessage out", CommonFunctions.GetParameter(parameter.Username, "pUsername"), isError, message).ToList();
            var userData = new List<GroupRoleModel>();
            if (data != null)
            {
                Mapper.Initialize(c =>
                {
                    c.CreateMap<SP_GET_ROLE_BY_USER_Result, GroupRoleModel>();
                });
                foreach (var item in data.ToList())
                {
                    var groupRole = new GroupRoleModel();

                    groupRole = Mapper.Map<SP_GET_ROLE_BY_USER_Result, GroupRoleModel>(item);
                    userData.Add(groupRole);
                }
            }
            result.IsError = (bool)isError.Value;
            result.Message = message.Value.ToString();
            result.Data = userData;
            return result;
        }

        public ResultModel<List<GroupRoleModel>> GetRoleByUserId(User parameter)
        {
            var result = new ResultModel<List<GroupRoleModel>>()
            {
                IsError = false,
                Message = string.Empty,
                Data = new List<GroupRoleModel>()
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar,500) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<SP_GET_ROLE_BY_USERID_Result>("dbo.SP_GET_ROLE_BY_USERID @pUserId, @pIsError out, @pMessage out", CommonFunctions.GetParameter(parameter.UserId, "pUserId"), isError, message).ToList();
            var userData = new List<GroupRoleModel>();
            if (data != null)
            {
                Mapper.Initialize(c =>
                {
                    c.CreateMap<SP_GET_ROLE_BY_USERID_Result, GroupRoleModel>();
                });

                foreach (var item in data)
                {
                    var groupRole = new GroupRoleModel();

                    groupRole = Mapper.Map<SP_GET_ROLE_BY_USERID_Result, GroupRoleModel>(item);
                    result.Data.Add(groupRole);
                }
            }
            result.IsError = (bool)isError.Value;
            result.Message = message.Value.ToString();
            result.Data = userData;
            return result;
        }

        public SearchResultModel<List<UserModel>> Search(SearchModel<UserModel> parameter)
        {
            var result = new SearchResultModel<List<UserModel>>()
            {
                Data = new List<UserModel>()
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar,500) { Direction = ParameterDirection.Output };
            var totalRecords = new SqlParameter("pTotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<UserModel>("dbo.sp_User_Search @pPageIndex, @pPageSize, @pOrderColumn, @pTotalRecords out, @pIsError out, @pMessage out, @pUserName, @pFullName,@pUserType,@pEmail,@pOrganization,@pGender,@pIsCertificated,@pIsEmail,@pAddress"
                , CommonFunctions.GetParameter(parameter.PageIndex, "pPageIndex")
				, CommonFunctions.GetParameter(parameter.PageSize, "pPageSize")
				, CommonFunctions.GetParameter(parameter.ColumnOrder, "pOrderColumn")
				, totalRecords, isError, message
                , CommonFunctions.GetParameter(parameter.Cretia.Username, "pUserName")
				, CommonFunctions.GetParameter(parameter.Cretia.FullName, "pFullName")
                , CommonFunctions.GetParameter(parameter.Cretia.UserTypeId, "pUserType")
                , CommonFunctions.GetParameter(parameter.Cretia.Email, "pEmail")
                , CommonFunctions.GetParameter(parameter.Cretia.Organization, "pOrganization")
                , CommonFunctions.GetParameter(parameter.Cretia.Gender, "pGender")
                , CommonFunctions.GetParameter(parameter.Cretia.IsCertificated, "pIsCertificated")
                , CommonFunctions.GetParameter(parameter.Cretia.IsEmailed, "pIsEmail")
                , CommonFunctions.GetParameter(parameter.Cretia.Address, "pAddress")
                ).ToList();

            result.Data = data;
            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.TotalRecord = RepositoryCommonFunctions.ConvertSqlParameterToInt(totalRecords);
            result.PageIndex = parameter.PageIndex;
            result.PageSize = parameter.PageSize;

            return result;
        }
		public List<User> GetbyUserName(string username )
		{
			var linqSql = from data in Context.Users
						  where data.Username.Contains(username)
						  select data;
			return linqSql.ToList();
		}
		

	}
}
