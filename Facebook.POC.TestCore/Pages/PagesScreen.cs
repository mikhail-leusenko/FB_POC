using Facebook.POC.TestCore.Attributes;
using Facebook.POC.TestCore.Pages.Base;
using OpenQA.Selenium;
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

        [ElementName(@"Linked Pages menu item")]
        public IWebElement LinkedPagesMenuItem =>
            this.Driver.FindElement(By.CssSelector("div[aria-label='Page Header'] > div> div:nth-child(2) > div > div:nth-child(4) > div:nth-child(2)"));

        /// <summary>
        /// This element was created to simplify the selection of the page which was created for tests purposes.
        /// Actually, it's better to use GetPocPageByName() helper method from BaseSteps to select the page by it name.
        /// </summary>
        [ElementName(@"POC Page button")]
        public IWebElement PocPageButton =>
            this.Driver.FindElement(By.CssSelector("a[aria-label='POC Page']"));
    }
}
