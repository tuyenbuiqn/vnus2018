using System;

namespace VietUSA.Repository.Common.Extensions
{
    public class EnumDescription : Attribute
    {
        public string Text;
        public EnumDescription(string text)
        {
            Text = text;
        }
    }
}
