using Facebook.POC.TestCore.Attributes;
using Facebook.POC.TestCore.Pages.Base;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Pages
{
    class CreatePostPopUp : BasePage
    {
        public CreatePostPopUp(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [ElementName(@"Create Post pop-up")]
        public IWebElement CreatePostPopup =>
            this.Wrapper.WaitElementByCss("form[method='POST']");

        [ElementName(@"Pop-up header")]
        public IWebElement PopUpHeader =>
            CreatePostPopup.FindElement(By.CssSelector("div > h2 > span > span"));

        [ElementName(@"Close button")]
        public IWebElement CloseButton =>
            this.Wrapper.WaitElementByCss("div[aria-label='Close']");

        [ElementName(@"Post message field")]
        public IWebElement PostMessageField =>
            this.Wrapper.WaitElementByCss("div[role='presentation'] div[role='textbox'][contenteditable='true']", 30);

        [ElementName(@"Post button")]
        public IWebElement PostButton =>
            this.Wrapper.WaitElementByCss("div[aria-label='Post']");

    }
}
