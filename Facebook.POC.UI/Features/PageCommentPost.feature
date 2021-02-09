@Comments
Feature: PageCommentPost
	As an Application admin
	I want to be able to comment posts on Application Page
	So that I can share my opinion regarding the Posts as a Page

Background:
	Given the user starts the application

Scenario: 1. When the visitor user comments Post, then the comment is displayed under this Post
	Given the "First" user credentials are entered
		And the "POC Page" screen is opened
	When the user submits the "Comment message" comment for the 1'st Post
	Then the "Comment message" comment is displayed under the 1'st Post
		And the "First" user is specified as "Dummy Post message" Posts "Comment message" comment author