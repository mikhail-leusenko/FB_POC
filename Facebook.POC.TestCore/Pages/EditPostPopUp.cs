using Facebook.POC.TestCore.Attributes;
using Facebook.POC.TestCore.Pages.Base;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Pages
{
    public class EditPostPopUp : BasePage
    {
        public EditPostPopUp(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [ElementName(@"Edit Post pop-up")]
        public IWebElement EditPostPopup =>
            this.Wrapper.WaitElementByCss("form[method='POST']");

        [ElementName(@"Pop-up header")]
        public IWebElement PopUpHeader =>
            EditPostPopup.FindElement(By.CssSelector("div > h2 > span > span"));

        [ElementName(@"Close button")]
        public IWebElement CloseButton =>
            EditPostPopup.FindElement(By.CssSelector("div[aria-label='Close']"));

        [ElementName(@"Post message field")]
        public IWebElement PostmessageField =>
            this.Wrapper.WaitElementByCss("div[role='dialog'] div[role='textbox'][contenteditable='true']");

        [ElementName(@"Save button")]
        public IWebElement PostButton =>
            this.Wrapper.WaitElementByCss("div[aria-label='Save']");
    }
}
