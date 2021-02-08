using Facebook.POC.TestCore.Attributes;
using Facebook.POC.TestCore.Pages.Base;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [ElementName(@"Welcome message")]
        public IWebElement Welcomemessage =>
            this.Wrapper.WaitElementByCss("#mount_0_0 div[role='main'] > div > div > div > span");
    }
}
