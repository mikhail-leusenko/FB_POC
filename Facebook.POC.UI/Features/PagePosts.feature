@Posts
Feature: PagePosts
	As an Application admin
	I want to be able to create, update and delete posts on Application Page as a Page
	So that I can manage the Posts in Page feed

Background:
	Given the user starts the application
		And the "First" user credentials are entered
		And the "POC Page" screen is opened

Scenario: 1. When the admin user creates new Post with some message, then this Post with message is displayed in feed
	When the user creates new Post with "New message" message
	Then the Post with "New message" message is displayed in the 1'st position for admin user

Scenario: 2. When the admin user creates Posts, then the latest Post is displayed in top of feed
	When the user creates new Post with "New message" message
		And the user creates new Post with "Another message" message
	Then the Post with "Another message" message is displayed in the 1'st position for admin user
		And the Post with "New message" message is displayed in the 2'nd position for admin user

Scenario: 3. When the admin user creates Post with some message and updates it, then the Post with update message is displayed in feed
	When the user creates new Post with "New message" message
		And the admin user edits the 1'st Post
		And the user changes Post message to "Another message"
	Then the Post with "Another message" message is displayed in the 1'st position for admin user

Scenario: 4. When the admin user updates Post with some message, then the Post with update message is displayed in feed on the same position
	When the user creates new Post with "New message" message
		And the user creates new Post with "Another message" message
		And the admin user edits the 2'nd Post
		And the user changes Post message to "Updated message"
	Then the Post with "Updated message" message is displayed in the 2'nd position for admin user

Scenario: 5. When the admin user deletes Post, then this Post is not displayed in feed
	When the user creates new Post with "New message" message
		And the admin user deletes the 1'st Post
		And the user confirms deletion of Post
	Then the feed does not contain the Post with "New message" message for admin user

Scenario: 6. When the admin user deletes the first of several Posts, then the previously second Post is displayed on the first position
	When the user creates new Post with "New message" message
		And the user creates new Post with "Another message" message
		And the admin user deletes the 1'st Post
		And the user confirms deletion of Post
	Then the feed does not contain the Post with "Another message" message for admin user
		And the Post with "New message" message is displayed in the 1'st position for admin user