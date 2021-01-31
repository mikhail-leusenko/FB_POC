Feature: PagePosts
	As an Application user
	I want to be able to create, update and delete posts on Applicaton Page
	So that I can manage the Posts in Page feed

Background: 
	Given the user starts the application
		And the "First" user credentials are entered
		And the "Poc" page is opened

Scenario: 1. hen the user creates Post with some message, then the Post with this message is displayed in top of feed
	When the user creates new Post with "New Mesage" message
	Then the Post with "New Message" message is displayed in the 1'st position

Scenario: 2. When the user creates Post with some message and updates it, then the Post with update message is displayed in feed
	When the user creates new Post with "New Mesage" message
		And the user edits the Post
		And the user changes Post message to "Another Message"
	Then the Post with "Another Message" message is displayed in the 1'st position 

Scenario: 3. When the user deletes Post, then this Post is not displayed in feed
	When the user creates new Post with "New Mesage" message
		And the user deletes the Post
	Then the feed does not contain this Post