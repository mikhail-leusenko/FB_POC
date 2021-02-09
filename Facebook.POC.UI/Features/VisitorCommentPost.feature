@Comments
Feature: VisitorCommentPost
	s an Application user
	I want to be able to comment posts on Application Page
	So that I can share my opinion regarding the Posts


Background:
	Given the user starts the application

Scenario: 1. When the visitor user comments Post, then the comment is displayed under this Post
	Given the "Visitor" user credentials are entered
		And the "POC Page" screen is opened from the Linked Pages
	When the user submits the "Comment message" comment for the 1'st Post
	Then the "Comment message" comment is displayed under the 1'st Post
		And the "Visitor" user is specified as "Dummy Post message" Posts "Comment message" comment author