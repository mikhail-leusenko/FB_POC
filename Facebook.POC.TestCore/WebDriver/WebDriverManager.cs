using Facebook.POC.TestCore.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.WebDriver
{
    /// <summary>
    /// Provides the management of the WebDriver instance.
    /// </summary>
    public class WebDriverManager
    {
        private readonly ScenarioContext ScenarioContext;
        private readonly ApplicationConfigurationHelper ConfigurationHelper;
        private readonly string Browser;

        /// <summary>
        /// Instance of the WebDriver.
        /// </summary>
        public IWebDriver Driver { get; private set; }

        /// <summary>
        /// Sets the required fields for the proper interaction with WebDriver instances.
        /// </summary>
        /// <param name="scenarioContext">Is used as storage for the IWebDriver instance.</param>
        /// <param name="applicationConfigurationHelper">Is used to define the type of IWebDriver from the current appsetting.json project file.</param>
        public WebDriverManager(ScenarioContext scenarioContext, ApplicationConfigurationHelper applicationConfigurationHelper)
        {
            this.ScenarioContext = scenarioContext;
            this.ConfigurationHelper = applicationConfigurationHelper;
            this.Browser = ConfigurationHelper.GetCurrentBrowser();
        }

        /// <summary>
        /// Set the web driver depending on the data from the appsettings.
        /// </summary>
        /// <returns>The instance of the WebDriver.</returns>
        public IWebDriver InitDriver()
        {
            switch (this.Browser)
            {
                case "Chrome":
                    {
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddArgument("--allow-insecure-localhost");
                        chromeOptions.AddArgument("disable-popup-blocking");

                        this.Driver = new ChromeDriver(chromeOptions);
                        this.Driver.Manage().Window.Maximize();

                        this.ScenarioContext.Set(this.Driver);
                        this.Driver.Manage().Cookies.DeleteAllCookies();

                        return this.Driver;
                    }
                case "Edge":
                    {
                        this.Driver = new EdgeDriver();
                        this.Driver.Manage().Window.Maximize();
                        this.ScenarioContext.Set(this.Driver);
                        this.Driver.Manage().Cookies.DeleteAllCookies();
                        return this.Driver;
                    }
                default:
                    {
                        throw new NotSupportedException($"{this.Browser} is not supported.");
                    }
            }
        }

        /// <summary>
        /// Disposes the current instance of the WebDriver.
        /// </summary>
        /// <param name="scenarioContext">The IWebDriver is stored in Scenario Context, so it should be called prior to disposing.</param>
        public void DisposeDriver(ScenarioContext scenarioContext)
        {
            var driver = scenarioContext.Get<IWebDriver>();
            driver.Quit();
            driver.Dispose();
        }
    }
}
