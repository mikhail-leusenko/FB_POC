using Facebook.POC.TestCore.Attributes;
using Facebook.POC.TestCore.Pages.Base;
using OpenQA.Selenium;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Pages
{
    public class VisitorPocPage : BasePage
    {
        public VisitorPocPage(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [ElementName(@"Create Post button")]
        public IWebElement CreatePostButton =>
            this.Wrapper.WaitElementByCss("div[aria-label='Create Post']", 30);

        [ElementName(@"Actions For This Post dropdown")]
        public IWebElement ActionsForThisPostDropdown =>
            this.Wrapper.WaitElementByCss("div[role='banner'] ~ div div[data-pagelet='root'] div[role='menu'] > div > div > div:nth-child(1) > div", 30);

        [ElementName(@"Post Actions list")]
        public IReadOnlyCollection<IWebElement> PostActionsList =>
            ActionsForThisPostDropdown.FindElements(By.CssSelector("div > div:nth-child(2) > div > div > span"));

        [ElementName(@"Edit Post option")]
        public IWebElement EditPostOption =>
            ActionsForThisPostDropdown.FindElement(By.CssSelector("div[role='menuitem']:nth-child(3)"));

        [ElementName(@"Delete Post option")]
        public IWebElement DeletePostOption =>
            ActionsForThisPostDropdown.FindElement(By.CssSelector("div[role='menuitem']:nth-child(4)"));

        [ElementName(@"Page Posts")]
        public IReadOnlyCollection<IWebElement> PagePosts =>
            this.Wrapper.WaitElementsByCss("div[role='main'] div[role='main'] > div > div > div > div > div");
    }
}
