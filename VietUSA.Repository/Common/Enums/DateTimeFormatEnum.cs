using System.ComponentModel;

namespace VietUSA.Repository.Common.Enums
{
      public enum DateTimeFormatEnum
    {
        [Description("dd/MM/yyyy HH:mm:ss")]
        DateTime24 = 0,
        [Description("dd/MM/yyyy")]
        Date = 1,
        [Description("dd/MM/yyyy HH:mm:ss")]
        DateTime24OmitValue = 2,
        [Description("dd/MM/yyyy")]
        DateOmitValue = 3
    }
}
