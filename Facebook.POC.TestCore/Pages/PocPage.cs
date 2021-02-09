using Facebook.POC.TestCore.Attributes;
using Facebook.POC.TestCore.Pages.Base;
using OpenQA.Selenium;
using System.Collections.Generic;
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
            this.Wrapper.WaitElementByCss("div[aria-label='Create Post']", 30);

        [ElementName(@"Actions For This Post dropdown")]
        public IWebElement ActionsForThisPostDropdown =>
            this.Wrapper.WaitElementByCss("div[role='banner'] ~ div div[data-pagelet='root'] div[role='menu'] > div > div > div:nth-child(1) > div", 30);

        [ElementName(@"Post Actions list")]
        public IReadOnlyCollection<IWebElement> PostActionsList =>
            ActionsForThisPostDropdown.FindElements(By.CssSelector("div > div:nth-child(2) > div > div > span"));

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
            this.Wrapper.WaitElementsByCss("div[aria-label='Page Admin Content'] > div > div > div:nth-child(4) > div:nth-child(2) > div > div:nth-child(2) > div:nth-child(3) > div > div > div > div  > div > div > div");

        [ElementName(@"Subscriber Page Posts")]
        public IReadOnlyCollection<IWebElement> SubscriberPagePosts =>
            this.Wrapper.WaitElementsByCss("div[role='article']");

        [ElementName(@"Create Post pop-up")]
        public IWebElement CreatePostPopUp =>
            this.Wrapper.WaitElementByCss("form[method='POST']");

        [ElementName(@"Manage Page Navigation Menu")]
        public IWebElement ManagePageNavigationMenu =>
            this.Driver.FindElement(By.CssSelector("div[aria-label='Page Header and Tools Navigation']"));

        [ElementName(@"Comment Actions list")]
        public IReadOnlyCollection<IWebElement> CommentActionsList =>
            this.Wrapper.WaitElementsByCss("div[data-pagelet='root'] > div[role='menu'] > div:nth-child(1) > div > div:nth-child(1) > div > div span");
    }
}
