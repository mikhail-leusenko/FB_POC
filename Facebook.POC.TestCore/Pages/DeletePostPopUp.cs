using Facebook.POC.TestCore.Attributes;
using Facebook.POC.TestCore.Pages.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Pages
{
    public class DeletePostPopUp: BasePage
    {
        public DeletePostPopUp(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [ElementName(@"Delete Post pop-up")]
        public IWebElement DeletePostPopup =>
            this.Wrapper.WaitElementByCss("div[aria-label='Delete Post?']");

        [ElementName(@"Delete button")]
        public IWebElement DeleteButton =>
            this.Wrapper.WaitElementByCss("div[aria-label='Delete']");

        [ElementName(@"Cancel button")]
        public IWebElement CancelButton =>
            this.Wrapper.WaitElementByCss("div[aria-label='Cancel']");

        [ElementName(@"Close button")]
        public IWebElement CloseButton =>
            this.Wrapper.WaitElementByCss("div[aria-label='Close']");
    }
}
