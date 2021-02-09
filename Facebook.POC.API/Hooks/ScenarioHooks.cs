using Facebook.POC.TestCore.Helpers;
using Facebook.POC.TestCore.Services;
using TechTalk.SpecFlow;

namespace Facebook.POC.API.Hooks
{
    [Binding]
    public sealed class ScenarioHooks
    {
        private readonly ScenarioContext ScenarioContext;
        private readonly ApplicationConfigurationHelper ConfigurationHelper;
        private readonly ApiInteractions ApiInteractions;

        public ScenarioHooks(ScenarioContext scenarioContext)
        {
            this.ScenarioContext = scenarioContext;
            this.ConfigurationHelper = new ApplicationConfigurationHelper();
            this.ApiInteractions = new ApiInteractions(this.ConfigurationHelper);
        }

        [AfterScenario]
        public void DeleteCreatedPosts()
        {
            var users = this.ConfigurationHelper.GetUsers().Values;

            foreach (var user in users)
            {
                var pageAccessToken = this.ApiInteractions.GetPageAccessToken(user.FirstName).Access_token;
                var userPosts = this.ApiInteractions.GetPagePosts(pageAccessToken);

                if (userPosts != null)
                {
                    userPosts.ForEach(x => this.ApiInteractions.DeletePost(pageAccessToken, x.Id));
                }
            }
        }

    }
}
