##1. Registration 

Register on http://developers.facebook.com/ to be able to create test application and to get access to UI and API features. The Facebook developers community has a huge amout of supported documentation which describes the main features you can use in your work.

##2. Create your test application

You need to create the test application to get the possibility to interact with Facebook features. Select Manage Business Integrations application type during the creation of your test application to get all integration functions. At the next step fill all the required fields to finalize the application creation.

##3. Configure users ######3.1. You will need the test users to use Facebook features. The main purpose of the test users is to interact with all available features of UI and API isolated from the real Facebook to avoid some kind of spamming. Navigate to the Roles -> Test Users to set up the test accounts for further interactions. You need at least 2 users with Page admin privilegies and 2 users with Page visitor privilegies. This is caused by the fact that Facebook can temporary lock the test user because of a huge amount of requests which were done durint the execution of the automated scenarios. Friendly reqomendation is to change email and password for more suitable ones after creation of the user.

######3.2. Set the permissions for your users. The good decision will be to add all required permissions for created user right after its creation to avoid the time wasting in future. For example, if you need to make CRUD operations with Page posts, you need to set the next permissions:

pages_show_list
pages_messaging
pages_read_engagement
pages_manage_posts
public_profile (is set by default for test users) Each API endpoint has the description of required user permissions.

######3.3. Add references between test users. To simplify the further interactions with Page that will be created in step below, it will be suitable to configure friends for test users. Just select the main Admin user and specify other users as a friends for it (Click 'Edit' button on appropriate test user, select 'Manage this test user's friends' option and add other users with a typeahead search).

4. Create the Page
To run the POC tests from this solution you need to create Page. The easiest way is to authenticate as the main Admin user via common Facebook login and manually add the new page. After the creation of the Page you need invite test users from the friends list to subscribe it. Also, you need to set Admin privilegies for the second Admin user via Page settings. The last thing need to be done before you can proceed is to accept the invitation to Page by all test users.

##5. Start testing Now you prepared all the required environment settings to proceed with testing.