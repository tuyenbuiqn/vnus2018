using System.Collections.Generic;

namespace VietUSA.Repository.Common.Models
{
    public class ListPagedResultModel<T>
    {
        public List<T> ListItem { get; set; }
        public int TotalRecord { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }
    }
}