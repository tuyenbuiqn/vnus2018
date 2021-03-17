using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VietUSA.Repository.Base;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.EFData.Entity;
using VietUSA.Repository.Interfaces;
using VietUSA.Repository.Models;

namespace VietUSA.Repository
{
	public class ContactRepository : EntityFrameworkRepository, IContactRepository
	{
        public ContactRepository(UserInfoModel userInfoModel) : base(userInfoModel)
        {

        }
        public SearchResultModel<List<ContactModel>> Search(SearchModel<ContactModel> parameter)
        {

            var result = new SearchResultModel<List<ContactModel>>()
            {
                Data = new List<ContactModel>()
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            var totalRecords = new SqlParameter("pTotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<ContactModel>
                ("SP_CONTACT_SEARCH @pPageIndex, @pPageSize, @pOrderColumn, @pTotalRecords out, @pIsError out, @pMessage out"
                , CommonFunctions.GetParameter(parameter.PageIndex, "pPageIndex")
                , CommonFunctions.GetParameter(parameter.PageSize, "pPageSize")
                , CommonFunctions.GetParameter(parameter.ColumnOrder, "pOrderColumn"), totalRecords, isError, message).ToList();
            var listData = new List<ContactModel>();

            foreach (var item in data.ToList())
            {
                var itemData = new ContactModel
                {
                        ContactId = item.ContactId,
                        Subject = item.Subject,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        FullName = item.FullName,
                        PhoneNumber = item.PhoneNumber,
                        Email = item.Email,
                        Website = item.Website,
                        Address = item.Address,
                        Message = item.Message,
                        Note = item.Note,
                        IsProcessed = item.IsProcessed,
                        ProcessedById = item.ProcessedById,
                        ProcessedDate = item.ProcessedDate,
                        CreatedDate = item.CreatedDate,
                        CreatedBy = item.CreatedBy,
                        LastModifiedDate = item.LastModifiedDate,
                        LastModifiedBy = item.LastModifiedBy,
                };
                listData.Add(itemData);
            }

            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.TotalRecord = RepositoryCommonFunctions.ConvertSqlParameterToInt(totalRecords);
            result.PageIndex = parameter.PageIndex;
            result.PageSize = parameter.PageSize;
            result.Data = data;
            //result.Data = listData;
            return result;
        }
        
	}
}
  
