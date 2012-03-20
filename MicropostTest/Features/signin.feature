Feature: Signing in users
	
Scenario: Sign in a known user
 	Given a sign in page
	When filling the fields with known user 
	And I click the 'SignIn' button
	Then the result is the 'Show' page
	And the title is 'Show'
	And contains a 'Sign out' link button

Scenario: Sign out a known user
 	Given a sign in page
	When filling the fields with known user 
	And I click the 'SignIn' button
	And I click the 'Sign out' link
	Then the result is the 'Home' page
	And the title is 'Home'
	And contains a 'Sign in' link button

Scenario: Sign in a unknown user
 	Given a sign in page
	When filling the fields with unknown user
	And I click the 'SignIn' button
	Then the result is the 'Sign in' page
	And the 'validation-summary-errors' message contains 'Email/Password' 