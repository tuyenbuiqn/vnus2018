using System.Collections.Generic;

namespace VietUSA.Repository.Models
{
    public class SelectModel
    {
        public dynamic SelectedId { get; set; }
        public dynamic Data { get; set; }
        /// <summary>
        /// Disable or Enable Control
        /// </summary>
        public bool IsDisableControl { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Value field
        /// </summary>
        public string ValueField { get; set; }
        /// <summary>
        /// Text field
        /// </summary>
        public string TextField { get; set; }
        /// <summary>
        /// Class name
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// Other style
        /// </summary>
        public string Style { get; set; }
          /// <summary>
        /// Dat ten function trong su kien Onchange.
        /// Xem .cshtml
        /// VD: getDoiToByTrungTamVienThong();
        /// </summary>
        public string OnchangeFunction { get; set; }


        public long ParentId { get; set; }
        public long AdministrativeDivisionId { get; set; }
        public int AdministrativeDivisionType { get; set; }
        public int GroupCategoryId { get; set; }
        public List<int> GroupCategoryIds { get; set; }
        /// <summary>
        /// Them hay ko them phan default Value cho dropdownlist
        /// </summary>
        public bool HasDefaultValue { get; set; }
        public string DefaultText { get; set; }

        // For Street Search
        public long? ProvinceId { get; set; }
        public long? DistrictId { get; set; }
        public long? WardId { get; set; }
    }
}
