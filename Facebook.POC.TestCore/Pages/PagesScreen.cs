using Facebook.POC.TestCore.Attributes;
using Facebook.POC.TestCore.Pages.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Pages
{
    public class PagesScreen : BasePage
    {
        public PagesScreen(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [ElementName(@"Create New Page button")]
        public IWebElement CreateNewPageButton =>
            this.Driver.FindElement(By.CssSelector("a[aria-label='Create New Page']"));

        /// <summary>
        /// This element was created to simplify the selection of the page which was created for tests purposes.
        /// Actually, it's better to use GetPocPageByName() helper method from BaseSteps to select the page by it name.
        /// </summary>
        [ElementName(@"POC Page button")]
        public IWebElement PocPageButton =>
            this.Driver.FindElement(By.CssSelector("a[aria-label='POC Page']"));

    }
}
