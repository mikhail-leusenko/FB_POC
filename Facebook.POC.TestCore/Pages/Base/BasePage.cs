using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Pages.Base
{
    /// <summary>
    /// This class allows to share common instances between child classes and provides the IWebElement wrappers.
    /// Sometimes the Selenium Web Driver tries to find the elements immidiatly after the previous step was ended and it can cause test failures.
    /// In such cases we need wait while element will be displayed on the screen to avoid the tests fails.
    /// The IWebElement wrappers from this class will allow to avoid extra wait time and will provide minimum impact on overall tests execution time.
    /// </summary>
    public class BasePage
    {
        private readonly ScenarioContext ScenarioContext;
        public IWebDriver Driver { get; }

        public BasePage(ScenarioContext scenarioContext)
        {
            this.ScenarioContext = scenarioContext;
            this.Driver = this.ScenarioContext.Get<IWebDriver>();
        }

        /// <summary>
        /// Gets the element by CSS selector with timeouts to exclude possible failures.
        /// </summary>
        /// <param name="elementSelector">CSS selector of element to wait.</param>
        /// <param name="waitTime">Wait timeout.</param>
        /// <returns>Required WebElement.</returns>
        public IWebElement WaitElementByCss(string elementSelector, int waitTime = 10)
        {
            try
            {
                var wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(waitTime));
                wait.Until(w => w.FindElement(By.CssSelector(@elementSelector)));

                return this.Driver.FindElement(By.CssSelector(@elementSelector));
            }
            catch (Exception exception)
            {
                Console.WriteLine($@"Element not found after {waitTime} seconds timeout: {exception}");
                throw;
            }
        }

        /// <summary>
        /// Gets the elements collection by CSS selector with timeouts to exclude possible failures.
        /// </summary>
        /// <param name="elementSelector">CSS selector of elements to wait.</param>
        /// <param name="waitTime">Wait timeout.</param>
        /// <returns>Required collection of WebElements./returns>
        public IReadOnlyCollection<IWebElement> WaitElementsByCss(string elementSelector, int waitTime = 10)
        {
            try
            {
                var wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(waitTime));

                wait.Until(w => w.FindElement(By.CssSelector(@elementSelector)).Displayed);

                return this.Driver.FindElements(By.CssSelector(@elementSelector));
            }
            catch (Exception exception)
            {
                Console.WriteLine($@"Elements not found after {waitTime} seconds timeout: {exception}");
                throw;
            }
        }

        /// <summary>
        /// Gets the element by XPath selector with timeouts to exclude possible failures.
        /// </summary>
        /// <param name="elementSelector">XPath selector of element to wait.</param>
        /// <param name="waitTime">Wait timeout.</param>
        /// <returns>Required WebElement.</returns>
        public IWebElement WaitElementByXPath(string elementSelector, int waitTime = 10)
        {
            try
            {
                var wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(waitTime));
                wait.Until(w => w.FindElement(By.XPath(@elementSelector)));

                return this.Driver.FindElement(By.XPath(@elementSelector));
            }
            catch (Exception exception)
            {
                Console.WriteLine($@"Element not found after {waitTime} seconds timeout: {exception}");
                throw;
            }
        }

        /// <summary>
        /// Gets the elements collection by XPath selector with timeouts to exclude possible failures.
        /// </summary>
        /// <param name="elementSelector">XPath selector of elements to wait.</param>
        /// <param name="waitTime">Wait timeout.</param>
        /// <returns>Required collection of WebElements.</returns>
        public IReadOnlyCollection<IWebElement> WaitElementsByXPath(string elementSelector, int waitTime = 10)
        {
            try
            {
                var wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(waitTime));
                wait.Until(w => w.FindElement(By.XPath(@elementSelector)));

                return this.Driver.FindElements(By.XPath(@elementSelector));
            }
            catch (Exception exception)
            {
                Console.WriteLine($@"Elements not found after {waitTime} seconds timeout: {exception}");
                throw;
            }
        }
    }
}
