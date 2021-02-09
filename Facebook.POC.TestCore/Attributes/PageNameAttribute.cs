using System;

namespace Facebook.POC.TestCore.Attributes
{
    public class PageNameAttribute : Attribute
    {
        public string PageName
        {
            get;
        }

        public PageNameAttribute(string pageName)
        {
            this.PageName = pageName;
        }
    }
}
