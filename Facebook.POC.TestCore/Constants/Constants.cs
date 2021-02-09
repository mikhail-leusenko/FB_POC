using Facebook.POC.TestCore.Attributes;

namespace Facebook.POC.TestCore.Constants
{
    public partial class Constants
    {
        public class messages
        {
            [ConstantName(@"Welcome message")]
            public string Welcomemessage => new string("Welcome to Facebook, ");

            [ConstantName(@"Invalid Login error message")]
            public string InvalidLoginmessage => new string("The email you’ve entered doesn’t match any account. ");

            [ConstantName(@"Invalid Password error message")]
            public string InvalidPasswordmessage => new string("The password you’ve entered is incorrect. ");
        }
        
        public class InvalidCredentials
        {
            public const string InvalidLogin = "invalid@log.in";

            public const string InvalidPassword = "p@ssw0rd";
        }      
    }
}
