using Facebook.POC.TestCore.Wrappers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Pages.Base
{
    /// <summary>
    /// This class allows to share common instances between child classes and provides the IWebElement selection support.
    /// </summary>
    public class BasePage
    {
        private readonly ScenarioContext ScenarioContext;
        public IWebDriver Driver { get; }

        public WebElementWrapper Wrapper { get;  }

        public BasePage(ScenarioContext scenarioContext)
        {
            this.ScenarioContext = scenarioContext;
            this.Wrapper = new WebElementWrapper(this.ScenarioContext);
            this.Driver = this.ScenarioContext.Get<IWebDriver>();
        }
    }
}
