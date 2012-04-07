Feature: Signing up users
	
Scenario: Sign up a valid user
 	Given a sign up page
	When filling the fields with valid data 
	And I click the 'Create' button
	Then the result is the 'Show' page
	And the title is 'Show'
	And contains a 'Sign out' link button

Scenario: Sign up a user with email saved by someone
 	Given a sign up page
	When filling the fields with valid data
	And Email is already in database
	And I click the 'Create' button
	Then the result is the 'Sign up' page
	And the 'alert' message contains 'Email' 
