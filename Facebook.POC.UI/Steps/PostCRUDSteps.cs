using Facebook.POC.TestCore.Steps;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            var messageField = this.GetElementOnPage("Post message", "field", "Create Post", "pop-up");

            this.ChangeThePostMessage(messageField, message);

            this.GetElementOnPage("Post", "button", "Create Post", "pop-up").Click();

            await Task.Delay(TimeSpan.FromSeconds(5));
        }

        [When(@"the (admin|visitor) user (edit|delete)s the (.*)(?:'st|'nd|'rd|'th) Post")]
        public async Task WhenTheUserEditsThePost(string userType, string action, int postNumber)
        {
            ClickOnActionOfPost(postNumber);

            await Task.Delay(TimeSpan.FromSeconds(1));

            var postActions = new List<IWebElement>();

            switch (userType)
            {
                case "admin":
                    postActions = this.GetElementsOnPage("Post Actions", "list", "POC", "page").ToList();
                    break;
                case "visitor":
                    postActions = this.GetElementsOnPage("Post Actions", "list", "Visitor POC", "page").ToList();
                    break;
                default:
                    throw new NotImplementedException($"The {userType} user type does not supported.");
            }

            await Task.Delay(TimeSpan.FromSeconds(1));

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
        public async Task WhenTheUserChangesPostMessageTo(string newPostMessage)
        {
            var postMessage = this.GetElementOnPage("Post message", "field", "Edit Post", "pop-up");

            this.ChangeThePostMessage(postMessage, newPostMessage);

            await Task.Delay(TimeSpan.FromMilliseconds(500));

            this.GetElementOnPage("Save", "button", "Edit Post", "pop-up").Click();

            /// Workaround - handler of unexpectedly appeared 'Chat Directly With Customers' pop-up.
            try
            {
                this.Driver.FindElement(By.CssSelector("div[aria-label='Chat Directly With Customers'] div[aria-label='Close'] > i"));
            }
            catch(NoSuchElementException)
            {
            }

            await Task.Delay(TimeSpan.FromSeconds(3));
        }

        [When(@"the (admin|visitor) user (delete|edit)s the Post with ""(.*)"" message")]
        public async Task WhenTheUserDeletesThePostWithMessage(string userType, string action, string message)
        {
            int postIndex = GetPostIndexByMessage(message);

            await WhenTheUserEditsThePost(userType, action, postIndex);
        }

        [Then(@"the Post with ""(.*)"" message is displayed in the (.*)(?:'st|'nd|'rd|'th) position for (admin|visitor) user")]
        public void ThenThePostWithMessageIsDisplayedInTheStPosition(string postMessage, int postNumber, string userType)
        {
            var pagePosts = new List<IWebElement>();

            switch (userType)
            {
                case "admin":
                    pagePosts = this.GetElementsOnPage("Page", "Posts", "POC", "page").ToList();
                    break;
                case "visitor":
                    pagePosts = this.GetElementsOnPage("Page", "Posts", "Visitor POC", "page").ToList();
                    break;
                default:
                    throw new NotImplementedException($"The {userType} user type does not supported.");
            }

            foreach (var post in pagePosts)
            {
                int index = pagePosts.IndexOf(post) + 1;
                if (index == postNumber)
                {
                    var actualPostMessage = GetPostmessageByIndex(postNumber);
                    Assert.AreEqual(postMessage, actualPostMessage);
                }
            }
        }

        [Then(@"the feed does not contain the Post with ""(.*)"" message for (admin|visitor) user")]
        public void ThenTheFeedDoesNotContainThePostWithMessage(string message, string userType)
        {
            this.Driver.Navigate().Refresh();

            List<string> postMessages = GetPostsMessages(userType);

            CollectionAssert.DoesNotContain(postMessages, message, $"The post with \"{message}\" exists in feed.");
        }

        [Then(@"the Post with ""(.*)"" message is displayed in feed for (admin|visitor) user")]
        public void ThenThePostWithIsDisplayedInFeed(string message, string userType)
        {
            var postmessages = GetPostsMessages(userType);

            CollectionAssert.Contains(postmessages, message);
        }

        private List<string> GetPostsMessages(string userType)
        {
            List<string> postMessages = new List<string>();
            List<IWebElement> pagePosts = new List<IWebElement>();

            string userPage = string.Empty;

            switch(userType)
            {
                case "admin":
                    userPage = "POC";
                    break;
                case "visitor":
                    userPage = "Visitor POC";
                    break;
                default:
                    throw new NotImplementedException($"The {userType} user type does not supported.");
            }

            try
            {
                pagePosts = this.GetElementsOnPage("Page", "Posts", userPage, "page").ToList();
            }
            catch (TargetInvocationException exception)
            {
                if (exception.InnerException.InnerException is NoSuchElementException)
                {
                    return postMessages;
                }
            }
            catch(NoSuchElementException)
            {
                return postMessages;
            }

            if (pagePosts.Any())
            {
                foreach (var post in pagePosts)
                {
                    var postmessage = post.FindElement(By.CssSelector("div[dir='auto'] > div > div > div")).Text;
                    postMessages.Add(postmessage);
                }
            }

            return postMessages;
        }
    }
}
