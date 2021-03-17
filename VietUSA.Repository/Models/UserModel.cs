using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using VietUSA.Repository.EFData.Entity;

namespace VietUSA.Repository.Models
{
    [NotMapped]
    public class UserModel : User
    {
        public long RowNumber { get; set; }
        public bool IsDisableControl { get; set; }
        public List<DocumentModel> AbtractionSubmissions { get; set; }
        // Send Email
        public bool HasAttachment { get; set; }
        public string SentTime { get; set; }
        public UserModel()
        {
            AbtractionSubmissions = new List<DocumentModel>();
        }
    }

    [NotMapped]
    public class UserAddOrEdit : User
    {
        public string Pass { get; set; }
        public string RePassWord { get; set; }
        public List<SelectListItem> ListUserStatusType { get; set; }


    }
    public class UserSalary
    {
        public string PayGrades { get; set; }
        public Single? LevelSalaryIncrease { get; set; }

        public int? TimeSalaryIncrea { get; set; }


    }
}

