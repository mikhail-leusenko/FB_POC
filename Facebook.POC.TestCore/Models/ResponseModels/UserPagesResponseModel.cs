using System.Collections.Generic;

namespace Facebook.POC.TestCore.Models.ResponseModels
{
    public class UserPagesResponseModel
    {
        /// <summary>
        /// Access token to the Page.
        /// </summary>
        public string AccessToken { get; set; }

        public string Category { get; set; }

        public List<Category> CategoryList { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }

        public List<string> Tasks { get; set; }
    }
}
