using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.POC.TestCore.Attributes
{
    public class ConstantNameAttribute : Attribute
    {
        public string ConstantName
        {
            get;
        }

        public ConstantNameAttribute(string constantName)
        {
            this.ConstantName = constantName;
        }
    }
}
