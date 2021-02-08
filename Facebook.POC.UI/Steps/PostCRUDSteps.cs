using Facebook.POC.TestCore.Steps;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Facebook.POC.UI.Steps
{
    [Binding]
    public sealed class PostCRUDSteps : BaseSteps
    {

        public PostCRUDSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [When(@"the user creates new Post with ""(.*)"" message")]
        public async Task WhenTheUserCreatesNewPostWithmessage(string message)
        {
            this.GetElementOnPage("Create Post", "button", "POC", "page").Click();

            this.GetElementOnPage("Post message", "field", "Create Post", "pop-up").SendKeys(message);

            this.GetElementOnPage("Post", "button", "Create Post", "pop-up").Click();

            await Task.Delay(TimeSpan.FromSeconds(3));
        }

        [When(@"the user (edit|delete)s the (.*)(?:'st|'nd|'rd|'th) Post")]
        public async Task WhenTheUserEditsThePost(string action, int postNumber)
        {
            ClickOnActionOfPost(postNumber);

            var postActions = this.GetElementsOnPage("Post Actions", "list", "Poc", "page").ToList();

            postActions.FirstOrDefault(a => a.Text.Contains(action, StringComparison.CurrentCultureIgnoreCase)).Click();

            await Task.Delay(TimeSpan.FromSeconds(3));
        }

        [When(@"the user (confirm|cancel)s deletion of Post")]
        [When(@"the user (close)s deletion pop-up")]
        public async Task WhenTheUserConfirmsDeletionOfPost(string action)
        {
            switch (action)
            {
                case "confirm":
                    this.GetElementOnPage("Delete", "button", "Delete Post", "pop-up").Click();
                    break;
                case "cancel":
                    this.GetElementOnPage("Cancel", "button", "Delete Post", "pop-up").Click();
                    break;
                case "close":
                    this.GetElementOnPage("Close", "button", "Delete Post", "pop-up").Click();
                    break;
                default:
                    throw new NotImplementedException($"The {action} action is not supported.");
            }

            await Task.Delay(TimeSpan.FromSeconds(3));
        }

        [When(@"the user changes Post message to ""(.*)""")]
        public async Task WhenTheUserChangesPostMessageTo(string postMessage)
        {
            var message = this.GetElementOnPage("Post message", "field", "Edit Post", "pop-up");
            
            message.Click();
            message.Clear();
            await Task.Delay(TimeSpan.FromMilliseconds(500));

            message.SendKeys(postMessage);

            await Task.Delay(TimeSpan.FromMilliseconds(500));

            this.GetElementOnPage("Save", "button", "Edit Post", "pop-up").Click();

            await Task.Delay(TimeSpan.FromSeconds(3));
        }

        [When(@"the user (delete|edit)s the Post with ""(.*)"" message")]
        public async Task WhenTheUserDeletesThePostWithMessage(string action, string message)
        {
            int postIndex = GetPostIndexByMessage(message);

            await WhenTheUserEditsThePost(action, postIndex);
        }

        [Then(@"the Post with ""(.*)"" message is displayed in the (.*)(?:'st|'nd|'rd|'th) position")]
        public void ThenThePostWithMessageIsDisplayedInTheStPosition(string postMessage, int postNumber)
        {
            var pagePosts = this.GetElementsOnPage("Page", "Posts", "Poc", "page").ToList();

            foreach (var post in pagePosts)
            {
                int index = pagePosts.IndexOf(post);
                if (index == postNumber)
                {
                    Assert.AreEqual(postMessage, GetPostmessageByIndex(postNumber));
                }
            }
        }

        [Then(@"the feed does not contain the Post with ""(.*)"" message")]
        public void ThenTheFeedDoesNotContainThePostWithmessage(string message)
        {
            var postmessages = GetPostsmessages();

            CollectionAssert.DoesNotContain(postmessages, message, $"The post with \"{message}\" exists in feed.");
        }

        [Then(@"the Post with ""(.*)"" message is displayed in feed")]
        public void ThenThePostWithIsDisplayedInFeed(string message)
        {
            var postmessages = GetPostsmessages();

            CollectionAssert.Contains(postmessages, message);
        }

        private List<string> GetPostsmessages()
        {
            var pagePosts = this.GetElementsOnPage("Page", "Posts", "POC", "page").ToList();
            List<string> postmessages = new List<string>();

            foreach (var post in pagePosts)
            {
                var postmessage = post.FindElement(By.CssSelector("div[dir='auto'] > div > div > div")).Text;
                postmessages.Add(postmessage);
            }

            return postmessages;
        }
    }
}
