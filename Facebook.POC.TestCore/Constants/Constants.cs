using Facebook.POC.TestCore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facebook.POC.TestCore.Constants
{
    public partial class Constants
    {
        public class Messages
        {
            [ConstantName(@"Welcome message")]
            public string WelcomeMessage => new String("Welcome to Facebook, ");

            [ConstantName(@"Invalid Login error message")]
            public string InvalidLoginMessage => new String("The email you’ve entered doesn’t match any account. ");

            [ConstantName(@"Invalid Password error message")]
            public string InvalidPasswordMessage => new String("The password you’ve entered is incorrect. ");
        }
        
        public class InvalidCredentials
        {
            public const string InvalidLogin = "invalid@log.in";

            public const string InvalidPassword = "p@ssw0rd";
        }
        
    }
}
