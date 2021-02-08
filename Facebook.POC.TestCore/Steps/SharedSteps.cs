using Facebook.POC.TestCore.Helpers;
using Facebook.POC.TestCore.Models;
using Facebook.POC.TestCore.Steps;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Steps
{
    [Binding]
    public class SharedSteps : BaseSteps
    {
        public SharedSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [Given(@"the user starts the application")]
        public async Task GivenTheUserStartsTheApplication()
        {
            this.ScenarioContext.Get<IWebDriver>().Navigate().GoToUrl(this.ApplicationUrl);

            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        [Given(@"the ""(.*)"" (?:page|screen) is opened")]
        public void GivenThePageIsOpened(string pageName)
        {
            this.GetElementOnPage("Pages", "button", "Navigation", "menu").Click();
            this.GetPocPageByName(pageName).Click();
        }

        [Given(@"the ""(.*)"" screen is opened from the Linked Pages")]
        public async Task GivenTheScreenIsOpenedFromTheLinkedPages(string pageName)
        {
            this.GetElementOnPage("Pages", "button", "Navigation", "menu").Click();

            await this.JavaScriptClick(this.GetElementOnPage("Linked Pages", "menu item", "Pages", "screen"));

            this.GetPocPageByName(pageName).Click();
        }


        /// <summary>
        /// NUnit Assert and Fluent Assertions are used in this method for demonstration purposes only.
        /// There are no preferences to use only one of these approaches, they are equivalent.
        /// </summary>
        [Then(@"the ""(.*)"" (message|field|button|checkbox|radiobutton|error message) is displayed on ""(.*)"" (page|screen)")]
        public void ThenThemessageIsDisplayedOnPage(string elementName, string elementType, string pageName, string pageType)
        {
            var element = this.GetElementOnPage(elementName, elementType, pageName, pageType);

            Assert.IsTrue(element.Displayed, $"{elementName} {elementType} is not displayed on {pageName} {pageType}");

            if (elementType.Contains("message"))
            {
                var expectedmessage = ConstantmessagesHelper.GetConstantmessage(elementName + " " + elementType);

                element.Text.Should().Contain(expectedmessage);
            }
        }
    }
}
