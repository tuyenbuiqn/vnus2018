using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VietUSA.Business.Interfaces;
using VietUSA.Repository;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Enums;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.EFData.Entity;
using VietUSA.Repository.Models;
using VietUSA.Repository.Parameters;

namespace VietUSA.Business
{
    public class UserBusiness : BaseBusiness<UserRepository>, IUserBusiness
    {
        readonly ICommonBusiness _commonBusiness;
        private readonly IDocumentBusiness _documentBusiness;

        public UserBusiness(UserInfoModel userInfoModel) : base(new UserRepository(userInfoModel))
        {
            CurrentUser = userInfoModel;
            _commonBusiness = new CommonBusiness(userInfoModel);
            _documentBusiness = new DocumentBusiness(userInfoModel);
        }

        public ResultModel<int> UpdateOrInsert(UserModel parameter)
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<User, UserModel>();
                c.CreateMap<UserModel, User>();
            });

            var user = Mapper.Map<UserModel, User>(parameter);
            if (user.UserId > 0)
            {
                user.LastModifiedById = CurrentUser.UserId;
                user.LastModifiedDate = DateTime.Now;
                user.DataStatusType = DataStatusType.Modified;
            }
            else
            {
                user.CreatedById = CurrentUser.UserId;
                user.CreatedDate = DateTime.Now;
                user.DataStatusType = DataStatusType.Created;
            }

            MainRepository.InsertOrUpdateNew(user);
            _commonBusiness.InsertLog(new LogModel()
            {
                ModuleId = (int)ObjectTypeEnum.User,
                Action = "Update User",
                CreatedBy = CurrentUser.UserId,
                CreatedDate = DateTime.Now,
                LogType = user.UserId > 0 ? LogType.Update : LogType.Create,
                LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(user)
            });


            return new ResultModel<int>
            {
                Data = user.UserId,
                IsError = false,
                Message = CommonConstants.SaveSuccess
            };
        }

       

        public ResultModel<List<int>> ProcessCertificated(List<int> parameters)
        {
            if (parameters == null) return new ResultModel<List<int>>(true, CommonConstants.InvalidInput, null);
            if (parameters.Count <= 0) return new ResultModel<List<int>>(true, CommonConstants.InvalidInput, null);
            var result = new ResultModel<List<int>>()
            {
                Data = new List<int>()
            };
            foreach (var parameter in parameters)
            {
                var user = MainRepository.GetById<User>(parameter);
                if (user != null && user.UserId > 0)
                {
                    user.IsCertificated = true;
                    user.LastModifiedById = CurrentUser.UserId;
                    user.LastModifiedDate = DateTime.Now;

                    MainRepository.InsertOrUpdateNew(user);
                    _commonBusiness.InsertLog(new LogModel()
                    {
                        ModuleId = (int)ObjectTypeEnum.User,
                        Action = "Update User Certificated",
                        CreatedBy = CurrentUser.UserId,
                        CreatedDate = DateTime.Now,
                        LogType = user.UserId > 0 ? LogType.Update : LogType.Create,
                        LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(user)
                    });
                    result.Data.Add(user.UserId);
                }
            }
            result.IsError = false;
            result.Message = CommonConstants.Success;
            return result;
        }

        public ResultModel<List<int>> RemoveRegister(List<int> parameters)
        {
            if (parameters == null) return new ResultModel<List<int>>(true, CommonConstants.InvalidInput, null);
            if (parameters.Count <= 0) return new ResultModel<List<int>>(true, CommonConstants.InvalidInput, null);
            var result = new ResultModel<List<int>>()
            {
                Data = new List<int>()
            };
            foreach (var parameter in parameters)
            {
                var user = MainRepository.GetById<User>(parameter);
                if (user != null && user.UserId > 0 && !user.IsSuperAdministrator && user.UserTypeId == (int)UserTypeEnum.AnonymousUser)
                {
                    user.DataStatusType = DataStatusType.Deleted;
                    user.LastModifiedById = CurrentUser.UserId;
                    user.LastModifiedDate = DateTime.Now;

                    MainRepository.InsertOrUpdateNew(user);
                    _commonBusiness.InsertLog(new LogModel()
                    {
                        ModuleId = (int)ObjectTypeEnum.User,
                        Action = "Delete Abtraction Submission",
                        CreatedBy = CurrentUser.UserId,
                        CreatedDate = DateTime.Now,
                        LogType = user.UserId > 0 ? LogType.Update : LogType.Create,
                        LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(user)
                    });
                    result.Data.Add(user.UserId);
                }
            }
            result.IsError = false;
            result.Message = CommonConstants.Success;
            return result;
        }
        public string Delete(User objDelete)
        {
            try
            {
                MainRepository.Delete<User>(objDelete);
                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.User,
                    Action = "Delete User",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = LogType.Delete,
                    LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(objDelete)
                });
                return string.Empty;
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        //public List<User> GetByAll(User inputObj)
        //{
        //	return MainRepository.GetbyAll(inputObj);
        //}
        public ListPagedResultModel<UserModel> SearchPaging(SearchModel<UserModel> parameter)
        {
            var searchResult = Search(parameter);

            var data = new ListPagedResultModel<UserModel>
            {
                ListItem = searchResult.Data,
                PageIndex = parameter.PageIndex,
                PageSize = parameter.PageSize,
                TotalRecord = searchResult.TotalRecord
            };
            return data;
        }
        public ListPagedResultModel<UserModel> SearchPagingWithDocument(SearchModel<UserModel> parameter)
        {
            var searchResult = Search(parameter);

            var data = new ListPagedResultModel<UserModel>
            {
                ListItem = searchResult.Data,
                PageIndex = parameter.PageIndex,
                PageSize = parameter.PageSize,
                TotalRecord = searchResult.TotalRecord
            };
            foreach (var userModel in data.ListItem)
            {
                var document = _documentBusiness.Search(new DocumentModel()
                {
                    ObjectId = userModel.UserId,
                    ObjectTypeId = (int)ObjectTypeEnum.AbstractSubmission
                });
                if (document != null && !document.IsError && document.Data != null)
                {
                    userModel.AbtractionSubmissions = document.Data.Documents;
                }
            }
            return data;
        }
        public SearchResultModel<List<UserModel>> Search(SearchModel<UserModel> parameter)
        {
            try
            {
                return MainRepository.Search(parameter);
            }
            catch (Exception exception)
            {
                return HandleExceptions<UserModel>(exception);
            }
        }
        public ResultModel<User> Login(LoginParameter parameter)
        {
            try
            {
                parameter.Password = CommonFunctions.GetMd5Hash(parameter.Password);
                return MainRepository.Login(parameter);
            }
            catch (Exception exception)
            {

                return HandleExecuteActionResult<User>(exception);
            }
        }

        public ResultModel<LoginModel> LoginAndProcess(LoginParameter parameter)
        {
            try
            {
                var result = new ResultModel<LoginModel>()
                {
                    Data = new LoginModel()
                    {
                        User = new User(),
                        GroupRoleModel = new List<GroupRoleModel>(),
                        UserInfoModel = new UserInfoModel()
                    },
                    IsError = true,
                    Message = string.Empty
                };

                var loginResult = Login(parameter);
                result.IsError = loginResult.IsError;
                result.Message = loginResult.Message;

                if (!loginResult.IsError)
                {

                    result.Data.User = loginResult.Data;
                    var groupRoleResult = GetRoleByUsername(new User()
                    {
                        Username = parameter.Username
                    });

                    result.Data.GroupRoleModel = groupRoleResult.Data;

                    result.Data.UserInfoModel = new UserInfoModel()
                    {
                        UserId = loginResult.Data.UserId,
                        Username = loginResult.Data.Username,
                        FullName = loginResult.Data.FullName,
                        IsSuperAdministrator = loginResult.Data.IsSuperAdministrator,
                        IsFirstLoginPasswordChanged = CommonFunctions.ConvertObjectToBoolean(loginResult.Data.IsFirstLoginPasswordChanged)
                    };
                    if (!groupRoleResult.IsError && groupRoleResult.Data != null)
                    {
                        result.Data.UserInfoModel.Permissions = groupRoleResult.Data.Select(p => p.RoleId).ToList();
                    }
                }
                return result;

            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<LoginModel>(exception);
            }
        }

        public ResultModel<bool> ChangeFirstLoginPassword(LoginParameter parameter)
        {
            try
            {
                if (CurrentUser.UserId <= 0)
                {
                    return new ResultModel<bool>(true, CommonConstants.SaveError, false);
                }
                if (!parameter.Password.Equals(parameter.RetypePassword))
                {
                    return new ResultModel<bool>(true, CommonConstants.SaveError, false);

                }
                var dataBeforeUpdate = MainRepository.GetById<User>(CurrentUser.UserId);

                dataBeforeUpdate.LastModifiedById = CurrentUser.UserId;
                dataBeforeUpdate.LastModifiedDate = DateTime.Now;
                dataBeforeUpdate.DataStatusType = DataStatusType.Modified;
                dataBeforeUpdate.Password = CommonFunctions.GetMd5Hash(parameter.Password);
                dataBeforeUpdate.IsFirstLoginPasswordChanged = true;

                MainRepository.InsertOrUpdateNew(dataBeforeUpdate);
                _commonBusiness.InsertLog(new LogModel()
                {
                    ModuleId = (int)ObjectTypeEnum.User,
                    Action = "Change first login password",
                    CreatedBy = CurrentUser.UserId,
                    CreatedDate = DateTime.Now,
                    LogType = LogType.Delete,
                    LogContent = Newtonsoft.Json.JsonConvert.SerializeObject(dataBeforeUpdate)
                });
                return new ResultModel<bool>(false, CommonConstants.SaveSuccess, true);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<bool>(exception);
            }
        }
        public ResultModel<List<GroupRoleModel>> GetRoleByUsername(User parameter)
        {
            try
            {
                return MainRepository.GetRoleByUsername(parameter);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<List<GroupRoleModel>>(exception);
            }
        }

        public ResultModel<List<GroupRoleModel>> GetRoleByUserId(User parameter)
        {
            try
            {
                return MainRepository.GetRoleByUserId(parameter);
            }
            catch (Exception exception)
            {
                return HandleExecuteActionResult<List<GroupRoleModel>>(exception);
            }
        }

        public List<User> GetbyUserName(string username)
        {
            try
            {
                return MainRepository.GetbyUserName(username);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }
    }
}
