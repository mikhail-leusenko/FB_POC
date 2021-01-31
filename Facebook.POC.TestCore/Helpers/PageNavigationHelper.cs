using Facebook.POC.TestCore.Attributes;
using Facebook.POC.TestCore.Pages.Base;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Helpers
{
    public static class PageNavigationHelper
    {
        /// <summary>
        /// Gets the instance of page from the PageStorage class according to the specified value.
        /// </summary>
        /// <param name="scenarioContext">Storage of helper instances.</param>
        /// <param name="value">The name and type of page to get.</param>
        /// <returns>The insance of appropriate page class.</returns>
        public static BasePage GetPage(this ScenarioContext scenarioContext, string value)
        {
            var page = ConvertToObject(scenarioContext, value);
            try
            {
                return (BasePage)page;
            }
            catch (InvalidCastException ex)
            {
                throw ex;
            }
        }

        private static object ConvertToObject(this ScenarioContext scenarioContext, string value)
        {
            var storage = new PageStorage(scenarioContext);
            value = value.Trim();
            var properties = storage.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                var pageNameAttribute = (PageNameAttribute[])properties[i].GetCustomAttributes(typeof(PageNameAttribute), false);
                if (pageNameAttribute.Length == 0)
                {
                    continue;
                }

                if (string.Equals(pageNameAttribute[0].PageName.Trim(), value.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    return properties[i].GetValue(storage);
                }
            }

            return null;
        }
    }
}
