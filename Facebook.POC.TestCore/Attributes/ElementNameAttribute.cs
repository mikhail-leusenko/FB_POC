using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.POC.TestCore.Attributes
{
    public class ElementNameAttribute : Attribute
    {
        public string ElementName
        {
            get;
        }

        public ElementNameAttribute(string elementName)
        {
            this.ElementName = elementName;
        }
    }
}
