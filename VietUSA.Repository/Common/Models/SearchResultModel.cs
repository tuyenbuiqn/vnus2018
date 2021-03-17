namespace VietUSA.Repository.Common.Models
{
    public class SearchResultModel<T> : ResultModel<T>
    {
        public int TotalRecord { get; set; } = 0;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5000;

        public int TotalExpiredLicensePaper { get; set; }

        public SearchResultModel<T> DeepCopy()
        {
            SearchResultModel<T> other = (SearchResultModel<T>)this.MemberwiseClone();
            other.TotalRecord = TotalRecord;
            other.PageIndex = PageSize;
            other.Data = Data;
            return other;
        }
    }
}
