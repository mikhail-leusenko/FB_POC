using Facebook.POC.TestCore.Helpers;
using Facebook.POC.TestCore.Wrappers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Steps
{
    [Binding]
    public class BaseSteps : TechTalk.SpecFlow.Steps
    {
        private new ScenarioContext ScenarioContext { get; }
        public IWebDriver Driver { get; }
        public ApplicationConfigurationHelper ConfigurationHelper { get; }
        public string ApplicationUrl { get; }

        public WebElementWrapper Wrapper { get; }

        public BaseSteps(ScenarioContext scenarioContext)
        {
            this.ScenarioContext = scenarioContext;
            this.Driver = this.ScenarioContext.Get<IWebDriver>();
            this.ConfigurationHelper = new ApplicationConfigurationHelper();
            this.ApplicationUrl = this.ConfigurationHelper.GetApplicationUrl();
            this.Wrapper = new WebElementWrapper(this.ScenarioContext);
        }

        /// <summary>
        /// Gets single element on page.
        /// </summary>
        /// <param name="elementName">Element Name (see ElementName attribute values in pages classes).</param>
        /// <param name="elementType">Element Type (see ElementName attribute values in pages classes).</param>
        /// <param name="pageName">Page Name (see PageName attribute values in PageStorage.cs).</param>
        /// <param name="pageType">Page Type (see PageName attribute values in PageStorage.cs).</param>
        /// <returns>Returns the IWebElement that matches the specified parameters.</returns>
        public IWebElement GetElementOnPage(string elementName, string elementType, string pageName, string pageType)
        {
            this.SetPage(pageName + " " + pageType);
            return PageElementHelper.GetElement(this.ScenarioContext, (elementName + " " + elementType).Trim());
        }

        /// <summary>
        /// Gets the collection of elements on page.
        /// </summary>
        /// <param name="elementName">Element Name (see ElementName attribute values in pages classes).</param>
        /// <param name="elementType">Element Type (see ElementName attribute values in pages classes).</param>
        /// <param name="pageName">Page Name (see PageName attribute values in PageStorage.cs).</param>
        /// <param name="pageType">Page Type (see PageName attribute values in PageStorage.cs).</param>
        /// <returns>Returns the collection of IWebElement that matches the specified parameters.</returns>
        public IReadOnlyCollection<IWebElement> GetElementsOnPage(string elementName, string elementType, string pageName, string pageType)
        {
            this.SetPage(pageName + " " + pageType);
            return PageElementHelper.GetElements(this.ScenarioContext, elementName + " " + elementType);
        }

        /// <summary>
        /// Perfroms the click on specified element via JavaSript.
        /// </summary>
        /// <param name="element">Element to click.</param>
        /// <returns></returns>
        public async Task JavaScriptClick(IWebElement element)
        {
            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)this.Driver;

            Actions actions = new Actions(this.Driver);
            actions.MoveToElement(element);
            actions.Perform();

            try
            {
                element.Click();
            }
            catch (ElementNotInteractableException)
            {
                javaScriptExecutor.ExecuteScript("arguments[0].click();", element);
            }

            await Task.Delay(TimeSpan.FromMilliseconds(500));
        }

        /// <summary>
        /// Deletes the old value of element and specified the new one.
        /// </summary>
        /// <param name="element">Element which should be updated.</param>
        /// <param name="value">Value which should be specified instead of the old one.</param>
        public async Task UpdateFieldText(IWebElement element, string value)
        {
            await this.JavaScriptClick(element);
            element.Clear();
            element.SendKeys(value);
        }

        /// <summary>
        /// Workaround method to clear the post message.
        /// Need to be used to fully emulate the user iteractions.
        /// Default IWebElement.Clear() does not work because application uses event based field update.
        /// </summary>
        /// <param name="postMessage">IWebElement representation of post message.</param>
        /// <param name="newMessage">Message to replace the original one.</param>
        public void ChangeThePostMessage(IWebElement postMessage, string newMessage)
        {
            int postMessageLength = postMessage.Text.Length;

            for (int i = 0; i < postMessageLength; i ++)
            {
                postMessage.Click();
                postMessage.SendKeys(Keys.Backspace);
            }

            IJavaScriptExecutor javaScriptExecutor = (IJavaScriptExecutor)this.Driver;

            javaScriptExecutor.ExecuteScript("arguments[0].click();", postMessage);

            try
            {
                postMessage.Click();
                postMessage.SendKeys(newMessage);
            }
            catch(ElementNotInteractableException)
            {
                
                javaScriptExecutor.ExecuteScript($"arguments[0].click(); arguments[0].setAttribuite('value','{newMessage}');", postMessage);
            }
        }

        /// <summary>
        /// Gets the specified page on Pages screen.
        /// </summary>
        /// <param name="elementName">Page name to find.</param>
        /// <returns>The IWebElement representation to navigate to the specified page.</returns>
        public IWebElement GetPocPageByName(string elementName)
        {
            return this.Wrapper.WaitElementByCss($"a[aria-label='{elementName}']", 30);
        }

        /// <summary>
        /// Cliks on action button of post by index.
        /// </summary>
        /// <param name="indexOfPost">Index of post in feed.</param>
        public void ClickOnActionOfPost(int indexOfPost)
        {
            this.Wrapper.WaitElementByCss($"div[aria-posinset='{indexOfPost}'] > div > div > div > div > div> div:nth-child(2) > div > div:nth-child(2) > div > div:nth-child(3)", 30).Click();
        }

        /// <summary>
        /// Get post content.
        /// </summary>
        /// <param name="indexOfPost">Index of post in feed.</param>
        /// <returns>The string representation of post content.</returns>
        public string GetPostmessageByIndex(int indexOfPost)
        {
            return this.Wrapper.WaitElementByCss($"div[aria-posinset='{indexOfPost}'] > div > div > div > div > div> div:nth-child(2) > div > div:nth-child(3) > div > div > div > div").Text;
        }

        /// <summary>
        /// Set the post comment.
        /// </summary>
        /// <param name="postMessage">message of post to comment.</param>
        /// <param name="commentText">Text to type into the post comment.</param>
        public void SetPostCommentByMessage(string postMessage, string commentText)
        {
            SetPostCommentByIndex(GetPostIndexByMessage(postMessage), commentText);
        }

        /// <summary>
        /// Set the post comment.
        /// </summary>
        /// <param name="postIndex">Index of post in feed.</param>
        /// <param name="commentText">Text to type into the post comment.</param>
        public void SetPostCommentByIndex(int postIndex, string commentText)
        {
            this.Wrapper.WaitElementByCss($"div[aria-posinset='{postIndex}'] div[aria-label='Leave a comment']").Click();
            IWebElement commentField = this.Wrapper.WaitElementByCss($"div[aria-posinset='{postIndex}'] div[aria-label='Write a comment']");

            commentField.SendKeys(commentText);
            commentField.SendKeys(Keys.Enter);
        }

        /// <summary>
        /// Get the collection of post comments values.
        /// </summary>
        /// <param name="postIndex">Index of post in feed.</param>
        /// <returns>The List of string representations of comments.</returns>
        public List<string> GetPostCommentsTextByPostIndex(int postIndex)
        {
            var postComments = GetPostCommentsByPostIndex(postIndex);
            return postComments.Select(x => x.FindElement(By.CssSelector("span[dir='auto']>div>div[dir='auto']")).Text).ToList();
        }

        /// <summary>
        /// Get the collection of post comments values.
        /// </summary>
        /// <param name="postMessage">message of post to get comments.</param>
        /// <returns>The List of string representations of comments.</returns>
        public List<string> GetPostCommenstTextByPostMessage(string postMessage)
        {
            return this.GetPostCommentsTextByPostIndex(GetPostIndexByMessage(postMessage));
        }

        /// <summary>
        /// Get the collection of post comments IWebElements.
        /// </summary>
        /// <param name="postIndex">Index of post in feed.</param>
        /// <returns>The list of IWebElements of corresponding comments.</returns>
        public List<IWebElement> GetPostCommentsByPostIndex(int postIndex)
        {
            string visitorPath = " > div > div > div > div > div> div:nth-child(2) > div > div:nth-child(4) > div > div > div:nth-child(2) > ";
            string adminPath = " > div > div > div > div > div> div:nth-child(2) > div > div:nth-child(5) > div > div > div:nth-child(2) > ";
            

            List<IWebElement> postComments = new List<IWebElement>();
            try
            {
                postComments = this.Wrapper.WaitElementsByCss($"div[aria-posinset = '{postIndex}']{adminPath}ul > li", 5).ToList();
            }
            catch(WebDriverTimeoutException ex)
            {
                if(ex.InnerException is NoSuchElementException)
                {
                    postComments = TryGetPostComments(postIndex, adminPath, visitorPath);
                }
            }
            catch(NoSuchElementException)
            {
                postComments = TryGetPostComments(postIndex, adminPath, visitorPath);
            }

            return postComments;
        }

        /// <summary>
        /// Get the collection of post comments IWebElements.
        /// </summary>
        /// <param name="postMessage">message of post to get comments.</param>
        /// <returns>The list of IWebElements of corresponding comments.</returns>
        public List<IWebElement> GetPostCommentsByPostMessage(string postMessage)
        {
            return this.GetPostCommentsByPostIndex(GetPostIndexByMessage(postMessage));
        }

        /// <summary>
        /// Get single comment IWebElement by its index.
        /// </summary>
        /// <param name="postComments">The collection of comments IWebElements.</param>
        /// <param name="commentIndex">The index of required comment.</param>
        /// <returns>The single comment IWebElement.</returns>
        public IWebElement GetCommentByIndex(List<IWebElement> postComments, int commentIndex)
        {
            return postComments.ElementAt(commentIndex);
        }

        /// <summary>
        /// Get single comment IWebElement by its text.
        /// </summary>
        /// <param name="postComments">The collection of comments IWebElements.</param>
        /// <param name="commentText">The text of required comment.</param>
        /// <returns>The single comment IWebElement.</returns>
        public IWebElement GetCommentByCommentText(List<IWebElement> postComments, string commentText)
        {
            return postComments.FirstOrDefault(x => x.FindElement(By.CssSelector("span[dir='auto'] > div > div[dir='auto']")).Text == commentText);
        }

        /// <summary>
        /// Click on the actions of specified comment.
        /// </summary>
        /// <param name="comment">Comment to perfrom some actions.</param>
        public void ClickOnActionsOfComment(IWebElement comment)
        {
            comment.FindElement(By.CssSelector("div[aria-label='Edit or delete this']")).Click();
        }

        /// <summary>
        /// Get author of comment.
        /// </summary>
        /// <param name="comment">IWebElement representation of required comment.</param>
        /// <returns>The string representation of auhtor name.</returns>
        public string GetAuthorOfComment(IWebElement comment)
        {
            return comment.FindElement(By.CssSelector("a > span > span")).Text;
        }

        /// <summary>
        /// Get message of singel comment.
        /// </summary>
        /// <param name="comment">IWebElement representation of required comment.</param>
        /// <returns>The string representation of comment message.</returns>
        public string GetmessageOfComment(IWebElement comment)
        {
            return comment.FindElement(By.CssSelector("span[dir='auto'] > div > div[dir='auto']")).Text;
        }

        /// <summary>
        /// Get the index of post by its message.
        /// </summary>
        /// <param name="message">Required Post message.</param>
        /// <returns>The index of post with specified message.</returns>
        public int GetPostIndexByMessage(string message)
        {
            this.Driver.Navigate().Refresh();

            List<IWebElement> pagePosts = new List<IWebElement>();

            try
            {
                pagePosts = this.GetElementsOnPage("Page", "Posts", "POC", "page").ToList();
            }
            catch(TargetInvocationException ex)
            {
                if (ex.InnerException.InnerException is NoSuchElementException)
                {
                    var rawPagePosts = this.GetElementsOnPage("Subscriber Page", "Posts", "POC", "page").ToList();
                    
                    foreach(var rawPost in rawPagePosts)
                    {
                        try
                        {
                            if (rawPost.GetAttribute("aria-posinset").Any())
                            {
                                pagePosts.Add(rawPost);
                            }
                        }
                        catch (ArgumentNullException)
                        {
                            break;
                        }
                    }
                }
            }

            int postIndex = -1;

            if (!pagePosts.Exists(post => post.FindElement(By.CssSelector("div[dir='auto'] > div > div > div")).Text == message))
            {
                throw new NoSuchElementException($"The Post with {message} message does not exist in feed.");
            }

            foreach (var post in pagePosts)
            {
                var postmessage = post.FindElement(By.CssSelector("div[dir='auto'] > div > div > div")).Text;

                if (postmessage == message)
                {
                    postIndex = int.Parse(post.GetAttribute("aria-posinset"));
                }
            }

            return postIndex;
        }

        private void SetPage(string pageName)
        {
            this.ScenarioContext.Set(PageNavigationHelper.GetPage(this.ScenarioContext, pageName));
            this.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(3000);
        }

        private List<IWebElement> TryGetPostComments(int postIndex, string adminPath, string visitorPath)
        {
            try
            {
                this.Wrapper.WaitElementByCss($"div[aria-posinset = '{postIndex}']{adminPath}div:nth-child(5) div[role='button']").Click();
                return this.Wrapper.WaitElementsByCss($"div[aria-posinset = '{postIndex}']{adminPath}ul:nth-child(5) > li").ToList();
            }
            catch (NoSuchElementException)
            {
                this.Wrapper.WaitElementByCss($"div[aria-posinset = '{postIndex}']{visitorPath}div:nth-child(5) div[role='button']").Click();
                return this.Wrapper.WaitElementsByCss($"div[aria-posinset = '{postIndex}']{visitorPath}ul:nth-child(5) > li").ToList();
            }
            catch (WebDriverTimeoutException ex)
            {
                if (ex.InnerException is NoSuchElementException)
                {
                    try
                    {
                        return this.Wrapper.WaitElementsByCss($"div[aria-posinset = '{postIndex}']{visitorPath}ul:nth-child(5) > li").ToList();
                    }
                    catch(WebDriverTimeoutException ex1)
                    {
                        if (ex1.InnerException is NoSuchElementException)
                        {
                            this.Wrapper.WaitElementByCss($"div[aria-posinset = '{postIndex}']{visitorPath}div:nth-child(5) div[role='button']").Click();
                            return this.Wrapper.WaitElementsByCss($"div[aria-posinset = '{postIndex}']{visitorPath}ul:nth-child(5) > li").ToList();
                        }
                    }
                }
            }

            return new List<IWebElement>();
        }
    }   
}
