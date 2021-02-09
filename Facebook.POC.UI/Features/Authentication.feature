Feature: Authentication
	As an Application user
	I want to be able to sign in the Application
	So that I can kill my time in it

Background: 
	Given the user starts the application

Scenario: 1. When the user enters valid credentials, then the user profile page is displayed
	When the "Second" user is logged in
	Then the "Welcome" message is displayed on "Home" page

Scenario: 2. When the user enters invalid login, then the appropriate error message is displayed
	When the user enters invalid login
	Then the "Invalid Login" error message is displayed on "LogIn" page

Scenario: 3. When the user enters invalid password, then the appropriate error message is displayed
	When the "Second" user enters invalid password
	Then the "Invalid Password" error message is displayed on "LogIn" page
	