@Comments
Feature: CommentPost
	As an Application user
	I want to be able to comment posts on Applicaton Page
	So that I can share my oppinion regarding the Posts

Background:
	Given the user starts the application

Scenario: 1. When the user comments Post, then the comment is displayed under this Post
	Given the "Second" user credentials are entered
		And the "POC Page" screen is opened from the Linked Pages
	When the user submits the "Comment message" comment for the 1'st Post
	Then the "Comment message" comment is displayed under the 1'st Post
		And the "Second" user is specified as "Dummy Post message" Posts "Comment message" comment author