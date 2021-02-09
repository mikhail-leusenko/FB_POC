using Facebook.POC.TestCore.Helpers;
using Facebook.POC.TestCore.Services;
using NUnit.Framework;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace Facebook.POC.API.Steps
{
    [Binding]
    public class PostsCRUD_API_Steps
    {
        private ScenarioContext ScenarioContext;
        private ApplicationConfigurationHelper ConfigurationHelper;
        private readonly ApiInteractions ApiInteractions;

        public PostsCRUD_API_Steps(ScenarioContext scenarioContext)
        {
            this.ScenarioContext = scenarioContext;
            this.ConfigurationHelper = new ApplicationConfigurationHelper();
            this.ApiInteractions = new ApiInteractions(this.ConfigurationHelper);
        }

        [When(@"the ""(.*)"" user sends (create|update) POST request with ""(.*)"" message")]
        public void WhenThePOSTRequestWithIsSent(string userName, string requestType, string message)
        {
            var accessToken = this.ApiInteractions.GetPageAccessToken(userName).Access_token;
            this.ScenarioContext.Set(accessToken, "PageAccessToken");
            switch(requestType)
            {
                case "create":
                    var response = this.ApiInteractions.CreateNewPost(accessToken, message);
                    
                    this.ScenarioContext.Set(response.Id, "PostId");
                    break;
                case "update":
                    this.ApiInteractions.UpdatePost(accessToken, this.ScenarioContext.Get<string>("PostId"), message);
                    break;
                default:
                    throw new NotImplementedException($"The specified {requestType} request type does not supported.");
            }
        }

        [When(@"the user deleted the created post via DELETE request")]
        public void WhenTheUserDeletedTheCreatedPostViaDELETERequest()
        {
            this.ApiInteractions.DeletePost(this.ScenarioContext.Get<string>("PageAccessToken"), this.ScenarioContext.Get<string>("PostId"));
        }

        [When(@"the ""(.*)"" user sends create POST request with ""(.*)"" message without access token")]
        public void WhenTheUserSendsCreatePOSTRequestWithMessageWithoutAccessToken(string userName, string message)
        {
            var response = this.ApiInteractions.POST_CreatePost(string.Empty, message);
            this.ScenarioContext.Set(response.StatusCode.ToString(), "ResponseStatusCode");
        }

        [When(@"the ""(.*)"" user sends create POST request without message")]
        public void WhenTheUserSendsCreatePOSTRequestWithoutMessage(string userName)
        {
            var response = this.ApiInteractions.POST_CreatePost(this.ApiInteractions.GetPageAccessToken(userName).Access_token, string.Empty);
            this.ScenarioContext.Set(response.StatusCode.ToString(), "ResponseStatusCode");
        }

        [When(@"the ""(.*)"" user sends GET request with ""(.*)"" message without access token")]
        public void WhenTheUserSendsGETRequestWithMessageWithoutAccessToken(string userName, string p1)
        {
            var response = this.ApiInteractions.GET_GetPagePosts(string.Empty);
            this.ScenarioContext.Set(response.StatusCode.ToString(), "ResponseStatusCode");
        }

        [When(@"the ""(.*)"" user sends update POST request with ""(.*)"" message without access token")]
        public void WhenTheUserSendsUpdatePOSTRequestWithMessageWithoutAccessToken(string userName, string message)
        {
            var response = this.ApiInteractions.POST_UpdatePost(string.Empty, this.ScenarioContext.Get<string>("PostId"), message);
            this.ScenarioContext.Set(response.StatusCode.ToString(), "ResponseStatusCode");
        }

        [When(@"the ""(.*)"" user sends update POST request with ""(.*)"" message without post Id")]
        public void WhenTheUserSendsUpdatePOSTRequestWithMessageWithoutPostId(string userName, string message)
        {
            var response = this.ApiInteractions.POST_UpdatePost(this.ScenarioContext.Get<string>("PageAccessToken"), string.Empty, message);
            this.ScenarioContext.Set(response.StatusCode.ToString(), "ResponseStatusCode");
        }

        [When(@"the ""(.*)"" user sends update POST request without message")]
        public void WhenTheUserSendsUpdatePOSTRequestWithoutMessage(string userName)
        {
            var response = this.ApiInteractions.POST_UpdatePost(this.ScenarioContext.Get<string>("PageAccessToken"), this.ScenarioContext.Get<string>("PostId"), string.Empty);
            this.ScenarioContext.Set(response.StatusCode.ToString(), "ResponseStatusCode");
        }

        [When(@"the user treis to GET page access token with invalid user ID")]
        public void WhenTheUserTreisToGETPageAccessTokenWithInvalidUserID()
        {
            var response = this.ApiInteractions.GET_GetPageAccessToken("Invalid");
            this.ScenarioContext.Set(response.StatusCode.ToString(), "ResponseStatusCode");
        }

        [Then(@"the ""(.*)"" status code returns in response")]
        public void ThenTheStatusCodeReturnsInResponse(string statusCode)
        {
            var actualStatusCode = this.ScenarioContext.Get<string>("ResponseStatusCode");

            Assert.AreEqual(statusCode, actualStatusCode);
        }

        [Then(@"this post does not exist in the response of the GET request to feed")]
        public void ThenThisPostDoesNotExistInTheResponseOfTheGETRequestToFeed()
        {
            var posts = this.ApiInteractions.GetPagePosts(this.ScenarioContext.Get<string>("PageAccessToken"));

            var postIds = posts.Select(x => x.Id).ToList();

            CollectionAssert.DoesNotContain(postIds, this.ScenarioContext.Get<string>("PostId"));
        }

        [Then(@"the post with ""(.*)"" message exists in response of GET posts request with ID specified in POST request response")]
        public void ThenThePostWithMessageExistsInResponseOfGETPostRequest(string messageText)
        {
            var posts = this.ApiInteractions.GetPagePosts(this.ScenarioContext.Get<string>("PageAccessToken"));

            var post = posts.FirstOrDefault(x => x.Message == messageText);

            Assert.NotNull(post);

            Assert.AreEqual(this.ScenarioContext.Get<string>("PostId"), post.Id);

            Assert.AreEqual(messageText, post.Message);
        }
    }
}
