@Posts
Feature: VisitorPosts
	As an Application user
	I want to be able to create, update and delete posts on Application Page
	So that I can manage my Posts in Page feed

Background:
	Given the user starts the application
		And the "Visitor" user credentials are entered
		And the "POC Page" screen is opened from the Linked Pages

Scenario: 1. When the visitor user creates new Post with some message, then this Post with message is displayed in feed
	When the user creates new Post with "New message" message
	Then the Post with "New message" message is displayed in the 1'st position for visitor user

Scenario: 2. When the visitor user creates Post with some message and updates it, then the Post with update message is displayed in feed
	When the user creates new Post with "A New message" message
		And the visitor user edits the 1'st Post
		And the user changes Post message to "Another message"
	Then the Post with "Another message" message is displayed in the 1'st position for visitor user

Scenario: 3. When the visitor user deletes Post, then this Post is not displayed in feed
	When the user creates new Post with "Another New message" message
		And the visitor user deletes the 1'st Post
		And the user confirms deletion of Post
	Then the feed does not contain the Post with "Another New message" message for visitor user