using Facebook.POC.TestCore.Attributes;
using Facebook.POC.TestCore.Pages.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
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
            WaitElementByCss("div[data-pagelet='LeftRail'] > div > div > ul > li > div > a");

        [ElementName(@"Pages button")]
        public IWebElement PagesButton =>
            WaitElementByCss("div[data-pagelet='LeftRail'] > div > div > div:nth-child(2) > ul > li:nth-child(2) > div a");


    }
}
