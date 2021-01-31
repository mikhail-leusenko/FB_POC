using Facebook.POC.TestCore.Attributes;
using Facebook.POC.TestCore.Pages.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Pages
{
    public class PocPage : BasePage
    {
        public PocPage(ScenarioContext scenarioContext) :base(scenarioContext)
        {
        }

        [ElementName(@"Create Post button")]
        public IWebElement CreatePostButton =>
            this.Driver.FindElement(By.CssSelector("div[aria-label='Create Post']"));

        [ElementName(@"Actions For This Post dropdown")]
        public IWebElement ActionsForThisPostDropdown =>
            this.Driver.FindElement(By.CssSelector("div[role='banner'] ~ div div[data-pagelet='root'] div[role='menu'] > div > div > div:nth-child(1) > div"));

        [ElementName(@"Edit Post option")]
        public IWebElement EditPostOption =>
            ActionsForThisPostDropdown.FindElement(By.CssSelector("div:nth-child(1)"));

        [ElementName(@"Delete Post option")]
        public IWebElement DeletePostOption =>
            ActionsForThisPostDropdown.FindElement(By.CssSelector("div:nth-child(2)"));

        /// <summary>
        /// Page posts are represented as a collection of Web Elements, because the quantity of the posts are dynamic.
        /// </summary>
        [ElementName(@"Page Posts")]
        public IReadOnlyCollection<IWebElement> PagePosts =>
            this.WaitElementsByCss("div[aria-label='Page Admin Content'] > div > div > div:nth-child(4) > div:nth-child(2) > div > div:nth-child(2) > div:nth-child(3) > div > div > div");

        [ElementName(@"Post")]
        public IWebElement PostI(int i) =>
            this.Driver.FindElement(By.CssSelector($"selector {i}"));

    }
}
