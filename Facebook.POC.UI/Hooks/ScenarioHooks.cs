using Facebook.POC.TestCore.Helpers;
using Facebook.POC.TestCore.WebDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Facebook.POC.UI.Hooks
{
    [Binding]
    public sealed class ScenarioHooks
    {
        private readonly ScenarioContext ScenarioContext;
        private readonly ApplicationConfigurationHelper ConfigurationHelper;
        private readonly WebDriverManager WebDriverManager;

        public ScenarioHooks(ScenarioContext scenarioContext)
        {
            this.ScenarioContext = scenarioContext;
            this.ConfigurationHelper = new ApplicationConfigurationHelper();
            this.WebDriverManager = new WebDriverManager(scenarioContext, this.ConfigurationHelper);
        }

        [BeforeScenario(Order = 1)]
        public void InitDriverInstance()
        {
            this.WebDriverManager.InitDriver();
        }

        [AfterScenario]
        public void DisposeDrverInstance()
        {
            this.WebDriverManager.DisposeDriver(this.ScenarioContext);
        }
    }
}
