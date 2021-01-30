using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.POC.TestCore.Models
{
    public class User
    {
        public string UserName { get; set; }

        public string UserEmail { get; set; }
        
        public string UserPassword { get; set; }

        public User(string userName, string userEmail, string userPassword)
        {
            this.UserName = userName;
            this.UserEmail = userEmail;
            this.UserPassword = userPassword;
        }
    }
}