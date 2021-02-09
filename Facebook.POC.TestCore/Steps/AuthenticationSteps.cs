using Facebook.POC.TestCore.Helpers;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Steps
{
    [Binding]
    public sealed class AuthenticationSteps : BaseSteps
    {
        private readonly UserDataHelper UserDataHelper;

        public AuthenticationSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
            this.UserDataHelper = new UserDataHelper(this.ConfigurationHelper);
        }

        [Given(@"the ""(.*)"" user credentials are entered")]
        [When(@"the ""(.*)"" user is logged in")]
        public void GivenTheUserCredentialsAreEntered(string user)
        {
            var currentUser = this.UserDataHelper.GetCurrentUser(user);
            this.ScenarioContext.Set(currentUser, "CurrentUser");
            this.EnterLogIn(currentUser.Email);
            this.EnterPassword(currentUser.Password);
            this.GetElementOnPage("LogIn", "button", "LogIn", "page").Click();
        }

        [When(@"the user enters invalid login")]
        public void WhenTheUserEntersInvalidLogin()
        {
            this.EnterLogIn(Constants.Constants.InvalidCredentials.InvalidLogin);
            this.GetElementOnPage("LogIn", "button", "LogIn", "page").Click();
        }

        [When(@"the ""(.*)"" user enters invalid password")]
        public void WhenTheUserEntersInvalidPassword(string user)
        {
            var currentUser = this.UserDataHelper.GetCurrentUser(user);
            this.ScenarioContext.Set(currentUser, "CurrentUser");
            this.EnterLogIn(currentUser.Email);
            this.EnterPassword(Constants.Constants.InvalidCredentials.InvalidPassword);
            this.GetElementOnPage("LogIn", "button", "LogIn", "page").Click();
        }


        private void EnterLogIn(string userEmail)
        {
            var login = this.GetElementOnPage("LogIn", "field", "LogIn", "page");
            login.Click();
            login.SendKeys(userEmail);
        }

        private void EnterPassword(string userPassword)
        {
            var password = this.GetElementOnPage("Password", "field", "LogIn", "page");
            password.Click();
            password.SendKeys(userPassword);
        }
    }
}
