Feature: PostsCRUD
	Simple calculator for adding two numbers

@mytag
Scenario: 1. When the post is created via API endpoint, then the response of the GET request to feed contains this post
	When the "First" user sends create POST request with "Message" message
	Then the post with "Message" message exists in response of GET posts request with ID specified in POST request response

Scenario: 2. When the created post is updated via API endpoin, then the response of the GET request to feed contains the updated post with not updated ID
	When the "First" user sends create POST request with "Message" message
		And the "First" user sends update POST request with "New Message" message
	Then the post with "New Message" message exists in response of GET posts request with ID specified in POST request response

Scenario: 3. When the user deletes created post via API endpoint, then the response of the GET request to feed does not contain the post with this ID
	When the "First" user sends create POST request with "Message" message
		And the user deleted the created post via DELETE request
	Then this post does not exist in the response of the GET request to feed

Scenario: 4. When the user tries to create post via API without access token, then the response contains Bad request status code
	When the "First" user sends create POST request with "Message" message without access token
	Then the "BadRequest" status code returns in response

Scenario: 5. When the user tries to create post via API without message, then the response contains Bad request status code
	When the "First" user sends create POST request without message
	Then the "BadRequest" status code returns in response

Scenario: 6. When the user tries to get posts via API without access token, then the response contains Bad request status code
	When the "First" user sends GET request with "Message" message without access token
	Then the "BadRequest" status code returns in response

Scenario: 7. When the user tries to update post via API without access token, then the response contains Bad request status code
	When the "First" user sends create POST request with "Message" message
		And the "First" user sends update POST request with "Message" message without access token
	Then the "BadRequest" status code returns in response

Scenario: 8. When the user tries to update post via API without post Id, then the response contains Bad request status code
	When the "First" user sends create POST request with "Message" message
		And the "First" user sends update POST request with "Message" message without post Id
	Then the "BadRequest" status code returns in response

Scenario: 9. When the user tries to update post via API without message, then the response contains OK status code
	When the "First" user sends create POST request with "Message" message
		And the "First" user sends update POST request without message
	Then the "OK" status code returns in response

Scenario: 10. When the user tries to GET access token with invalid user ID, then the response contains Bad request status code
	When the user treis to GET page access token with invalid user ID
	Then the "BadRequest" status code returns in response