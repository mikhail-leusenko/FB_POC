using Facebook.POC.TestCore.Attributes;
using Facebook.POC.TestCore.Pages.Base;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Pages
{
    public class LogInPage : BasePage
    {
        public LogInPage(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [ElementName(@"LogIn field")]
        public IWebElement LogInField =>
            this.Wrapper.WaitElementByCss("input[id='email']");

        [ElementName(@"Password field")]
        public IWebElement PasswordField =>
            this.Driver.FindElement(By.CssSelector("input[id='pass']"));

        [ElementName(@"LogIn button")]
        public IWebElement LogInButton =>
            this.Driver.FindElement(By.CssSelector("button[name='login']"));

        [ElementName(@"Invalid Login error message")]
        public IWebElement InvalidLoginErrormessage =>
            this.Driver.FindElement(By.CssSelector("#email_container > div ~ div"));

        [ElementName(@"Invalid Password error message")]
        public IWebElement InvalidPasswordErrormessage =>
            this.Driver.FindElement(By.CssSelector("input[type='password'] ~ div ~ div"));
    }
}
