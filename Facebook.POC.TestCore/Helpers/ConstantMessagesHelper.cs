using Facebook.POC.TestCore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.POC.TestCore.Helpers
{
    public class ConstantMessagesHelper
    {
        public static string GetConstantMessage(string value)
        {
            var constantObject = ConvertToObject(value);

            if (constantObject is string constant)
            {
                return constant;
            }
            else
            {
                throw new InvalidCastException($"The {value} constant is not represented in Constants class. Check the cast.");
            }
        }

        private static object ConvertToObject(string value)
        {
            value = value.Trim();

            
            var constants = new Constants.Constants.Messages();
            var properties = constants.GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                var constantNameAttribute = (ConstantNameAttribute[])properties[i].GetCustomAttributes(typeof(ConstantNameAttribute), false);
                if (constantNameAttribute.Length == 0)
                {
                    continue;
                }

                if (string.Equals(constantNameAttribute[0].ConstantName, value, StringComparison.CurrentCultureIgnoreCase))
                {
                    return properties[i].GetValue(constants);
                }
            }

            return null;
        }
    }
}
