using Facebook.POC.TestCore.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Steps
{
    [Binding]
    public class BaseSteps : TechTalk.SpecFlow.Steps
    {
        private new ScenarioContext ScenarioContext { get; }
        public IWebDriver Driver { get; }
        public ApplicationConfigurationHelper ConfigurationHelper { get; }
        public string ApplicationUrl { get; }

        public BaseSteps(ScenarioContext scenarioContext)
        {
            this.ScenarioContext = scenarioContext;
            this.Driver = this.ScenarioContext.Get<IWebDriver>();
            this.ConfigurationHelper = new ApplicationConfigurationHelper();
            this.ApplicationUrl = this.ConfigurationHelper.GetApplicationUrl();
        }

        /// <summary>
        /// Gets single element on page.
        /// </summary>
        /// <param name="elementName">Element Name (see ElementName attribute values in pages classes).</param>
        /// <param name="elementType">Element Type (see ElementName attribute values in pages classes).</param>
        /// <param name="pageName">Page Name (see PageName attribute values in PageStorage.cs).</param>
        /// <param name="pageType">Page Type (see PageName attribute values in PageStorage.cs).</param>
        /// <returns>Returns the IWebElement that matches the specified parameters.</returns>
        public IWebElement GetElementOnPage(string elementName, string elementType, string pageName, string pageType)
        {
            this.SetPage(pageName + " " + pageType);
            return PageElementHelper.GetElement(this.ScenarioContext, (elementName + " " + elementType).Trim());
        }

        /// <summary>
        /// Gets the collection of elements on page.
        /// </summary>
        /// <param name="elementName">Element Name (see ElementName attribute values in pages classes).</param>
        /// <param name="elementType">Element Type (see ElementName attribute values in pages classes).</param>
        /// <param name="pageName">Page Name (see PageName attribute values in PageStorage.cs).</param>
        /// <param name="pageType">Page Type (see PageName attribute values in PageStorage.cs).</param>
        /// <returns>Returns the collection of IWebElement that matches the specified parameters.</returns>
        public IReadOnlyCollection<IWebElement> GetElementsOnPage(string elementName, string elementType, string pageName, string pageType)
        {
            this.SetPage(pageName + " " + pageType);
            return PageElementHelper.GetElements(this.ScenarioContext, elementName + " " + elementType);
        }

        /// <summary>
        /// Perfroms the click on specified element via JavaSript.
        /// </summary>
        /// <param name="element">Element to click.</param>
        /// <returns></returns>
        public async Task JavaScriptClick(IWebElement element)
        {
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)this.Driver;

            Actions actions = new Actions(this.Driver);
            actions.MoveToElement(element);
            actions.Perform();

            try
            {
                element.Click();
            }
            catch (ElementNotInteractableException)
            {
                javaScriptExecutor.ExecuteScript("arguments[0].click();", element);
            }

            await Task.Delay(TimeSpan.FromMilliseconds(500));
        }

        /// <summary>
        /// Deletes the old value of element and specified the new one.
        /// </summary>
        /// <param name="element">Element which should be updated.</param>
        /// <param name="value">Value which should be specified instead of the old one.</param>
        public async Task UpdateFieldText(IWebElement element, string value)
        {
            await this.JavaScriptClick(element);
            element.Clear();
            element.SendKeys(value);
        }

        public IWebElement GetPocPageByName(string elementName)
        {
            return this.Driver.FindElement(By.CssSelector($"a[aria-label='{elementName}']"));
        }

        public void ClickOnActionOfPost(IWebElement post, int indexOfPost)
        {
            post.FindElement(By.CssSelector($"div[aria-posinset='{indexOfPost}'] > div > div > div > div > div> div:nth-child(2) > div > div:nth-child(2) > div > div:nth-child(3)")).Click();
        }

        public string GetPostContent(IWebElement post, int indexOfPost)
        {
            return post.FindElement(By.CssSelector($"div[aria-posinset='{indexOfPost}'] > div > div > div > div > div> div:nth-child(2) > div > div:nth-child(3) > div > div > div > div")).Text;
        }

        private void SetPage(string pageName)
        {
            this.ScenarioContext.Set(PageNavigationHelper.GetPage(this.ScenarioContext, pageName));
            this.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(3000);
        }
    }
}
