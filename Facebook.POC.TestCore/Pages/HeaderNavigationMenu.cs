using Facebook.POC.TestCore.Attributes;
using Facebook.POC.TestCore.Pages.Base;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Pages
{
    public class HeaderNavigationMenu : BasePage
    {
        public HeaderNavigationMenu(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [ElementName(@"Home button")]
        public IWebElement HomeButton =>
            this.Driver.FindElement(By.CssSelector("a[aria-label='Home']"));

        [ElementName(@"Friends button")]
        public IWebElement FriendsButton =>
            this.Driver.FindElement(By.CssSelector("a[aria-label='Friends']"));

        [ElementName(@"Groups button")]
        public IWebElement GroupsButton =>
            this.Driver.FindElement(By.CssSelector("a[aria-label='Groups']"));
    }
}
