using Facebook.POC.TestCore.Steps;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Facebook.POC.UI.Steps
{
    [Binding]
    public sealed class CommentPostSteps : BaseSteps
    {
        public CommentPostSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [When(@"the user submits the ""(.*)"" comment for the (.*)'st Post")]
        public void WhenTheUserSubmitsTheCommentForTheStPost(string commentmessage, int postIndex)
        {
            this.SetPostCommentByIndex(postIndex, commentmessage);
        }

        [Then(@"the ""(.*)"" comment is displayed under the (.*)'st Post")]
        public void ThenTheCommentIsDisplayedUnderTheStPost(string commentText, int postIndex)
        {
            CollectionAssert.Contains(GetPostCommentsTextByPostIndex(postIndex), commentText, $"The comment with {commentText} text does not exist under Post #{postIndex}");
        }

        [Then(@"the ""(.*)"" user is specified as ""(.*)"" Posts ""(.*)"" comment author")]
        public async Task ThenTheUserIsSpecifiedAsCommentAuthor(string userName, string postmessage, string commentmessage)
        {
            this.Driver.Navigate().Refresh();

            await Task.Delay(TimeSpan.FromSeconds(3));

            var users = this.ConfigurationHelper.GetUsers().Values;

            var user = users.FirstOrDefault(u => u.FirstName == userName);

            var expectedUserName = user.FirstName == "Visitor" ?
                user.FirstName + " " + user.LastName :
                "POC Page";

            var postComments = this.GetPostCommentsByPostMessage(postmessage);

            var comment = this.GetCommentByCommentText(postComments, commentmessage);

            var commentAuthor = this.GetAuthorOfComment(comment);

            Assert.AreEqual(expectedUserName, commentAuthor, $"The expected comment author differs from actual one.{Environment.NewLine}" +
                $"The expected user name is \"{expectedUserName}\".{Environment.NewLine}" +
                $"The actual user name is \"{commentAuthor}\"");
        }
    }
}
