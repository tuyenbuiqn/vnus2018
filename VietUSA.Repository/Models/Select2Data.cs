using System.Collections.Generic;

namespace VietUSA.Repository.Models
{
    public class Select2Data
    {
        public dynamic Id { get; set; }
        public string Text { get; set; }
        public List<Select2Data> Children { get; set; }
    }

    public class SelectedData
    {
        public long Id { get; set; }
        public string Text { get; set; }
    }

    public class DropDownModel
    {
        public string DropDownId { get; set; }
        public List<DropDownData> Source { get; set; }
    }

    public class DropDownData
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
