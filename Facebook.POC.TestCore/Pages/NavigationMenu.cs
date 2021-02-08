using Facebook.POC.TestCore.Attributes;
using Facebook.POC.TestCore.Pages.Base;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Pages
{
    public class NavigationMenu : BasePage
    {
        public NavigationMenu(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [ElementName(@"User Profile button")]
        public IWebElement UserProfileButton =>
            this.Wrapper.WaitElementByCss("div[data-pagelet='LeftRail'] > div > div > ul > li > div > a");

        [ElementName(@"Pages button")]
        public IWebElement PagesButton =>
            this.Wrapper.WaitElementByCss("div[data-pagelet='LeftRail'] > div > div > div:nth-child(2) > ul > li:nth-child(2) > div a");


    }
}
