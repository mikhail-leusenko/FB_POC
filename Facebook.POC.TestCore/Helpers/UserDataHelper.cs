using Facebook.POC.TestCore.Models;
using System.Collections.Generic;

namespace Facebook.POC.TestCore.Helpers
{
    public class UserDataHelper
    {
        private readonly ApplicationConfigurationHelper ConfigurationHelper;

        public UserDataHelper(ApplicationConfigurationHelper applicationConfigurationHelper)
        {
            this.ConfigurationHelper = applicationConfigurationHelper;
        }

        public User GetCurrentUser(string userName)
        {
            return this.ConfigurationHelper.GetUsers().GetValueOrDefault(userName);
        }
    }
}
