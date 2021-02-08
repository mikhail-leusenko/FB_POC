@Posts
Feature: PagePosts
	As an Application user
	I want to be able to create, update and delete posts on Applicaton Page
	So that I can manage the Posts in Page feed

Background: 
	Given the user starts the application
		And the "First" user credentials are entered
		And the "POC Page" screen is opened

Scenario: 1. When the user creates new Post with some message, then this Post with message is displayed in feed
	When the user creates new Post with "A_message" message
	Then the Post with "A_message" message is displayed in the 1'st position

Scenario: 2. When the user creates Posts, then the latest Post is displayed in top of feed
	When the user creates new Post with "A_message" message
		And the user creates new Post with "B_message" message
	Then the Post with "B_message" message is displayed in the 1'st position
		And the Post with "A_message" message is displayed in the 2'nd position

Scenario: 3. When the user updates Post with some message, then the Post with update message is displayed in feed
	When the user creates new Post with "A_message" message
		And the user edits the Post with "A_message" message
		And the user changes Post message to "B_message"
	Then the Post with "B_message" message is displayed in feed

Scenario: 4. When the user updates Post with some message, then the Post with update message is displayed in feed on the same position
	When the user creates new Post with "A_message" message
		And the user creates new Post with "B_message" message
		And the user edits the Post with "A_message" message
		And the user changes Post message to "C_message"
	Then the Post with "C_message" message is displayed in the 2'nd position 

Scenario: 5. When the user deletes Post, then this Post is not displayed in feed
	When the user creates new Post with "A_message" message
		And the user creates new Post with "B_message" message
		And the user deletes the Post with "A_message" message
		And the user confirms deletion of Post
	Then the feed does not contain the Post with "A_message" message

Scenario: 6. When the user deletes the first of several Posts, then the previously second Post is displayed on the first position
	When the user creates new Post with "A_message" message
		And the user creates new Post with "B_message" message
		And the user deletes the 1'st Post 
	Then the Post with "B_message" message is displayed in the 1'st position