##1. UI/API project appsettings.json 
The projects configuration should be done via appsettings.json - a settings file with key-value pair structure. In both projects these files are almost similar. Here is a description for these files structure and the meaning of the keys.

######WebBrowser 
parameter to set the web browser for test run. This key is unicue for UI project for obvious reasons.

######Users t
he collection of users to authenticate into the application. Contains several similar blocks which represents the user data appropriate to the specified key.

The common block structure is: 

"user_first_name": { 
	"First Name": "user_first_name", 
	"Last Name": "user_last_name", 
	"Email": "user_email", 
	"Password": "user_password", 
	"Id": "user_id" 
}

Take into account that in current implementation you should specify the user's First name as a name of the appropriate block (see current appsettings.json in UI project). Email and Password values are using for login into the Facebook. Id value is used by API to allow interactions with Page content.

######ApplicationUrl 
the URL to the login page.

######ApiUrl 
the URL for API endpoints triggering. Allows to provide the clean up after tests.

######ApplicationId 
Id of the test application. Can be get from your application's page in Facebook Developers. Used by API to get application access token.

######ClientSecret 
Client Secret of the test application. Can be get from your application settings (Settings -> Base). Used by API to get application access token.

######PageId 
Id of the Page which should be used for test execution. Can be get from the URL of page (e.g. in this URL https://www.facebook.com/POC-Page-332249074681169 the Page ID is '332249074681169'). Used by API to get Page Access Token to interact with page content.