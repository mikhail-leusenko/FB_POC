using Facebook.POC.TestCore.Attributes;
using TechTalk.SpecFlow;

namespace Facebook.POC.TestCore.Pages.Base
{
    public class PageStorage
    {
        private readonly ScenarioContext ScenarioContext;
        public PageStorage(ScenarioContext scenarioContext)
        {
            this.ScenarioContext = scenarioContext;
        }

        [PageName(@"LogIn page")]
        public BasePage LogInPage => new LogInPage(this.ScenarioContext);

        [PageName(@"Home page")]
        public BasePage HomePage => new HomePage(this.ScenarioContext);

        [PageName(@"Header Navigation menu")]
        public BasePage HeaderNavigationMenu => new HeaderNavigationMenu(this.ScenarioContext);

        [PageName(@"Navigation menu")]
        public BasePage NavigationMenu => new NavigationMenu(this.ScenarioContext);

        [PageName(@"Pages screen")]
        public BasePage PagesScreen => new PagesScreen(this.ScenarioContext);

        [PageName(@"POC page")]
        public BasePage PocPage => new PocPage(this.ScenarioContext);

        [PageName(@"Create Post pop-up")]
        public BasePage CreatePostPopUp => new CreatePostPopUp(this.ScenarioContext);

        [PageName(@"Edit Post pop-up")]
        public BasePage EditPostPopUp => new EditPostPopUp(this.ScenarioContext);

        [PageName(@"Delete Post pop-up")]
        public BasePage DeletePostPopUp => new DeletePostPopUp(this.ScenarioContext);
    }
}
