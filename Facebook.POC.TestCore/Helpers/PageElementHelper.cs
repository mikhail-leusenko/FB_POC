using Facebook.POC.TestCore.Attributes;
using Facebook.POC.TestCore.Pages.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Helpers
{
    public static class PageElementHelper
    {
        /// <summary>
        /// Gets the appropriate IWebElement from the page.
        /// </summary>
        /// <param name="context">Storage of the instance of page class, which should contain required element.</param>
        /// <param name="value">The name and type of element to be found.</param>
        /// <returns>The appropriate IWebElement.</returns>
        public static IWebElement GetElement(this ScenarioContext context, string value)
        {
            var element = ConvertToObject(context, value);

            if (element is IWebElement webElement)
            {
                return webElement;
            }
            else
            {
                throw new InvalidCastException($"The {value} element does not exist on {context.Get<BasePage>()}. Check the cast.");
            }
        }

        /// <summary>
        /// Gets the appropriate collection of IWebElements from the page.
        /// </summary>
        /// <param name="context">Storage of the instance of page class, which should contain required element.</param>
        /// <param name="value">The name and type of collection of elements to be found.</param>
        /// <returns>The appropriate collection of IWebElements.</returns>
        public static IReadOnlyCollection<IWebElement> GetElements(this ScenarioContext context, string value)
        {
            var element = ConvertToObject(context, value);

            if (element is IReadOnlyCollection<IWebElement> webElement)
            {
                return webElement;
            }
            else
            {
                throw new InvalidCastException($"The {value} elements do not exist on {context} page. Check the cast.");
            }
        }

        private static object ConvertToObject(this ScenarioContext context, string value)
        {
            value = value.Trim();
            var page = context.Get<BasePage>();
            var properties = page.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                var elementNameAttribute = (ElementNameAttribute[])properties[i].GetCustomAttributes(typeof(ElementNameAttribute), false);
                if (elementNameAttribute.Length == 0)
                {
                    continue;
                }

                if (string.Equals(elementNameAttribute[0].ElementName, value, StringComparison.CurrentCultureIgnoreCase))
                {
                    return properties[i].GetValue(page);
                }
            }

            return null;
        }
    }
}
