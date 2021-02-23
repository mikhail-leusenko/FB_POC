##1. Description This POC project created for demonstration purposes.

######1.1. Prior the automated tests execution ensure that the next Visual Studio extensions are installed:

SpecFlow for Visual Studio 2019
NUnit 3 Test Adapter

######1.2. Testing approach:

UI automated tests are created regarding the BDD approach with usage of SpecFlow and NUnit frameworks.
API automated tests are created regarding the BDD approach with usage of SpecFlow and NUnit frameworks.

##2. Possible issues

During the multiple tests run the Facebook can temporary block test user accounts. First and Second users has Page admin permissions, so they can replace each other. Visitor user is a user with wisitor permissions and there is no replacement for it, however this account is used in just several tests, so we don't currently need duplicate user.

##3. Workarounds and features.

Sommetimes the Facebook pages and elements loading takes a lot of time, so that in some methods Task.Delay were used to slowdown the webDriver interaction with page. Also, some methods executes Page refresh to avoid stale elements exceptions. Some low-level methods has multiple try-catch exceptions handlers to allow users with both admin and visitor permissions interact with Facebook UI.