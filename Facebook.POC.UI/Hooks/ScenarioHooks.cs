using Facebook.POC.TestCore.Helpers;
using Facebook.POC.TestCore.Services;
using Facebook.POC.TestCore.WebDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace Facebook.POC.UI.Hooks
{
    [Binding]
    public sealed class ScenarioHooks
    {
        private readonly ScenarioContext ScenarioContext;
        private readonly ApplicationConfigurationHelper ConfigurationHelper;
        private readonly WebDriverManager WebDriverManager;
        private readonly ApiInteractions ApiInteractions;

        public ScenarioHooks(ScenarioContext scenarioContext)
        {
            this.ScenarioContext = scenarioContext;
            this.ConfigurationHelper = new ApplicationConfigurationHelper();
            this.WebDriverManager = new WebDriverManager(scenarioContext, this.ConfigurationHelper);
            this.ApiInteractions = new ApiInteractions(this.ConfigurationHelper);
        }

        [BeforeScenario(Order = 1)]
        public void InitDriverInstance()
        {
            this.WebDriverManager.InitDriver();
        }

        [Scope(Tag = "Comments")]
        [BeforeScenario(Order = 2)]
        public void CreatePostsForScenarios()
        {
            var users = this.ConfigurationHelper.GetUsers().Values;

            foreach (var user in users)
            {
                var pageAccessToken = this.ApiInteractions.GetPageAccessToken(user.FirstName).Access_token;

                if (pageAccessToken != null)
                {
                    this.ApiInteractions.CreateNewPost(pageAccessToken, "Dummy Post message");
                }
            }
        }

        [AfterScenario(Order = 1)]
        public void DisposeDrverInstance()
        {
            this.WebDriverManager.DisposeDriver(this.ScenarioContext);
        }

        [Scope(Tag = "Posts")]
        [Scope(Tag = "Comments")]
        [AfterScenario(Order = 2)]
        public void DeleteCreatedPosts()
        {
            var users = this.ConfigurationHelper.GetUsers().Values;

            foreach(var user in users)
            {
                var pageAccessToken = this.ApiInteractions.GetPageAccessToken(user.FirstName).Access_token;
                var userPosts = this.ApiInteractions.GetPagePosts(pageAccessToken);

                if(userPosts != null)
                {
                    userPosts.ForEach(x => this.ApiInteractions.DeletePost(pageAccessToken, x.Id));
                }
            }
        }
    }
}
