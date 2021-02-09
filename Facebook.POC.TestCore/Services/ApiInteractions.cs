using Facebook.POC.TestCore.Helpers;
using Facebook.POC.TestCore.Models.ResponseModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facebook.POC.TestCore.Services
{
    public class ApiInteractions
    {
        private string ApiApplicationUrl { get; set; }
        private string ApplicationId { get; set; }
        private string ClientSecret { get; set; }
        private string PageId { get; set; }

        private ApplicationConfigurationHelper ConfigurationHelper;

        private RestClient RestClient;

        public ApiInteractions(ApplicationConfigurationHelper configurationHelper)
        {
            this.ConfigurationHelper = new ApplicationConfigurationHelper();
            this.ApiApplicationUrl = this.ConfigurationHelper.GetApiApplicationUrl();
            this.ApplicationId = this.ConfigurationHelper.GetApplicationId();
            this.ClientSecret = this.ConfigurationHelper.GetClientSecret();
            this.PageId = this.ConfigurationHelper.GetPageId();
            this.RestClient = new RestClient(this.ApiApplicationUrl);
        }

        public List<PagePostsResponseModel> GetPagePosts(string pageAccessToken)
        {
            return JsonConvert.DeserializeObject<ResponseRoot<PagePostsResponseModel>>(GET_GetPagePosts(pageAccessToken).Content).Data;
        }

        public CreatePostResponseModel CreateNewPost(string pageAccessToken, string message)
        {
            return JsonConvert.DeserializeObject<CreatePostResponseModel>(POST_CreatePost(pageAccessToken, message).Content);
        }

        public ModifyPostResponseModel UpdatePost(string pageAccessToken, string postId, string message)
        {
            return JsonConvert.DeserializeObject<ModifyPostResponseModel>(POST_UpdatePost( pageAccessToken, postId, message).Content);
        }

        public ModifyPostResponseModel DeletePost(string pageAccessToken, string postId)
        {
            return JsonConvert.DeserializeObject<ModifyPostResponseModel>(DELETE_Post(pageAccessToken, postId).Content);
        }

        public PageAccessTokenResponseModel GetPageAccessToken(string userName)
        {
            string responseContent;
            try
            {
                responseContent = GET_GetPageAccessToken(userName).Content;
            }
            catch(NullReferenceException ex)
            {
                throw ex;
            }
            return JsonConvert.DeserializeObject<PageAccessTokenResponseModel>(responseContent);
        }

        public IRestResponse GET_GetPageAccessToken(string userName)
        {
            var testUsers = GetAppTestUsers();
            var users = this.ConfigurationHelper.GetUsers();

            var user = users.SingleOrDefault(x => x.Value.FirstName == userName).Value;

            string userAccessToken = string.Empty;

            try
            {
                foreach (var testUser in testUsers)
                {
                    if (testUser.Id == user.UserId)
                    {
                        userAccessToken = testUser.Access_token;
                    }
                }
            }
            catch(NullReferenceException ex)
            {
                throw ex;
            }

            var requestBody = $"{this.PageId}?fields=access_token&access_token={userAccessToken}";
            IRestRequest request = new RestRequest(requestBody, Method.GET);

            return this.RestClient.Execute(request);
        }

        public IRestResponse GET_GetPagePosts(string pageAccessToken)
        {
            var requestBody = $"{this.PageId}/feed/?access_token={pageAccessToken}";

            IRestRequest request = new RestRequest(requestBody, Method.GET);

            return this.RestClient.Execute(request);
        }

        public IRestResponse POST_CreatePost(string pageAccessToken, string message)
        {
            var requestBody = $"{this.PageId}/feed/?message={message}&access_token={pageAccessToken}";

            IRestRequest request = new RestRequest(requestBody, Method.POST);

           return this.RestClient.Execute(request);
        }

        public IRestResponse POST_UpdatePost(string pageAccessToken, string postId, string message)
        {
            var requestBody = $"{postId}/?message={message}&access_token={pageAccessToken}";

            IRestRequest request = new RestRequest(requestBody, Method.POST);

            return this.RestClient.Execute(request);
        }
        public IRestResponse DELETE_Post(string pageAccessToken, string postId)
        {
            var requestBody = $"{postId}?access_token={pageAccessToken}";

            IRestRequest request = new RestRequest(requestBody, Method.DELETE);

            return this.RestClient.Execute(request);
        }

        private AppAccessTokenResponseModel GetAppAccessToken()
        {
            var r = $"oauth/access_token?client_id={this.ApplicationId}&client_secret={this.ClientSecret}&grant_type=client_credentials";
            var requestBody = new StringBuilder();
            requestBody.Append("oauth/access_token?client_id=");
            requestBody.Append(this.ApplicationId);
            requestBody.Append("&client_secret=");
            requestBody.Append(this.ClientSecret);
            requestBody.Append("&grant_type=client_credentials");
            IRestRequest request = new RestRequest(r, Method.GET);

            var response = this.RestClient.Execute(request);

            var token = JsonConvert.DeserializeObject<AppAccessTokenResponseModel>(response.Content);

            return token;
        }

        private List<TestUsersResponseModel> GetAppTestUsers()
        {
            var authorization = this.GetAppAccessToken();
            var requestBody = $"{this.ApplicationId}/accounts/test-users";
            IRestRequest request = new RestRequest(requestBody, Method.GET);
            request.AddHeader("Authorization", "Bearer " + authorization.Access_token);

            var responce = this.RestClient.Execute(request);

            return JsonConvert.DeserializeObject<ResponseRoot<TestUsersResponseModel>>(responce.Content).Data;
        }
    }
}
