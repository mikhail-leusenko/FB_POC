# 1_General info

## 1. Description This POC project created for demonstration purposes.

###### 1.1. Prior the automated tests execution ensure that the next Visual Studio extensions are installed:

SpecFlow for Visual Studio 2019
NUnit 3 Test Adapter

###### 1.2. Testing approach:

UI automated tests are created regarding the BDD approach with usage of SpecFlow and NUnit frameworks.
API automated tests are created regarding the BDD approach with usage of SpecFlow and NUnit frameworks.

## 2. Possible issues

During the multiple tests run the Facebook can temporary block test user accounts. First and Second users has Page admin permissions, so they can replace each other. Visitor user is a user with wisitor permissions and there is no replacement for it, however this account is used in just several tests, so we don't currently need duplicate user.

## 3. Workarounds and features.

Sommetimes the Facebook pages and elements loading takes a lot of time, so that in some methods Task.Delay were used to slowdown the webDriver interaction with page. Also, some methods executes Page refresh to avoid stale elements exceptions. Some low-level methods has multiple try-catch exceptions handlers to allow users with both admin and visitor permissions interact with Facebook UI.

# 2_Get started

## 1. Registration 

Register on http://developers.facebook.com/ to be able to create test application and to get access to UI and API features. The Facebook developers community has a huge amout of supported documentation which describes the main features you can use in your work.

## 2. Create your test application

You need to create the test application to get the possibility to interact with Facebook features. Select Manage Business Integrations application type during the creation of your test application to get all integration functions. At the next step fill all the required fields to finalize the application creation.

## 3. Configure users 

###### 3.1. You will need the test users to use Facebook features. 

The main purpose of the test users is to interact with all available features of UI and API isolated from the real Facebook to avoid some kind of spamming. Navigate to the Roles -> Test Users to set up the test accounts for further interactions. You need at least 2 users with Page admin privilegies and 2 users with Page visitor privilegies. This is caused by the fact that Facebook can temporary lock the test user because of a huge amount of requests which were done durint the execution of the automated scenarios. Friendly reqomendation is to change email and password for more suitable ones after creation of the user.

###### 3.2. Set the permissions for your users. 

The good decision will be to add all required permissions for created user right after its creation to avoid the time wasting in future. For example, if you need to make CRUD operations with Page posts, you need to set the next permissions:

pages_show_list
pages_messaging
pages_read_engagement
pages_manage_posts
public_profile (is set by default for test users). 

Each API endpoint has the description of required user permissions.

###### 3.3. Add references between test users. 

To simplify the further interactions with Page that will be created in step below, it will be suitable to configure friends for test users. Just select the main Admin user and specify other users as a friends for it (Click 'Edit' button on appropriate test user, select 'Manage this test user's friends' option and add other users with a typeahead search).

## 4. Create the Page

To run the POC tests from this solution you need to create Page. 
The easiest way is to authenticate as the main Admin user via common Facebook login and manually add the new page. After the creation of the Page you need invite test users from the friends list to subscribe it. Also, you need to set Admin privilegies for the second Admin user via Page settings. The last thing need to be done before you can proceed is to accept the invitation to Page by all test users.

## 5. Start testing 

Now you prepared all the required environment settings to proceed with testing.


# 3_Configure test projects

## 1. UI/API project appsettings.json

The projects configuration should be done via appsettings.json - a settings file with key-value pair structure. In both projects these files are almost similar. Here is a description for these files structure and the meaning of the keys.

###### WebBrowser
parameter to set the web browser for test run. This key is unicue for UI project for obvious reasons.

###### Users
the collection of users to authenticate into the application. Contains several similar blocks which represents the user data appropriate to the specified key.

The common block structure is: 

"user_first_name": { 
	"First Name": "user_first_name", 
	"Last Name": "user_last_name", 
	"Email": "user_email", 
	"Password": "user_password", 
	"Id": "user_id" 
}

Take into account that in current implementation you should specify the user's First name as a name of the appropriate block (see current appsettings.json in UI project). Email and Password values are using for login into the Facebook. Id value is used by API to allow interactions with Page content.

###### ApplicationUrl 
the URL to the login page.

###### ApiUrl 
the URL for API endpoints triggering. Allows to provide the clean up after tests.

###### ApplicationId 
Id of the test application. Can be get from your application's page in Facebook Developers. Used by API to get application access token.

###### ClientSecret 
Client Secret of the test application. Can be get from your application settings (Settings -> Base). Used by API to get application access token.

###### PageId 
Id of the Page which should be used for test execution. Can be get from the URL of page (e.g. in this URL https://www.facebook.com/POC-Page-332249074681169 the Page ID is '332249074681169'). Used by API to get Page Access Token to interact with page content.