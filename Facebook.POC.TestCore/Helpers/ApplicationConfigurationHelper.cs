using Facebook.POC.TestCore.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
namespace Facebook.POC.TestCore.Helpers
{
    public class ApplicationConfigurationHelper
    {
        /// <summary>
        /// Gets current configuration from 'appsettings.json' file.
        /// </summary>
        /// <returns>Returns current configuration.</returns>
        public IConfiguration GetCurrentConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile(@"appsettings.json", false, true).Build();
        }

        /// <summary>
        /// Gets selected browser from current configuration.
        /// </summary>
        /// <returns>Browser type to init the appropriate web driver.</returns>
        public string GetCurrentBrowser()
        {
            return GetCurrentConfiguration().GetValue<string>(@"WebBrowser");
        }

        /// <summary>
        /// Gets all available users as dictionary from current configuration.
        /// </summary>
        /// <returns>The dictionary, which contains the User Name as a key and User Data as a value.</returns>
        public Dictionary<string, User> GetUsers()
        {
            var usersSection = GetCurrentConfiguration().GetSection("Users");

            Dictionary<string, User> users = new Dictionary<string, User>();

            var rawUsers = usersSection.GetChildren().ToList();

            rawUsers.ForEach(x => users.Add(x.Key, new User(x.GetSection("FirstName").Value, x.GetSection("LastName").Value, x.GetSection("Email").Value, x.GetSection("Password").Value, x.GetSection("Id").Value)));

            return users;
        }

        /// <summary>
        /// Gets the application URL from current configuration.
        /// </summary>
        /// <returns>The application URL as a string, which should be used for web driver navigation.</returns>
        public string GetApplicationUrl()
        {
            return this.GetCurrentConfiguration().GetValue<string>(@"ApplicationUrl");
        }

        public string GetApiApplicationUrl()
        {
            return GetCurrentConfiguration().GetSection("ApiUrl").Value;
        }

        public string GetApplicationId()
        {
            return GetCurrentConfiguration().GetSection("ApplicationId").Value;
        }

        public string GetClientSecret()
        {
            return GetCurrentConfiguration().GetSection("ClientSecret").Value;
        }

        public string GetPageId()
        {
            return GetCurrentConfiguration().GetSection("PageId").Value;
        }
    }
}
