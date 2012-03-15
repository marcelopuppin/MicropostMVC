Feature: Accessing links at the root page
	
Scenario: Invoking the Home link
 	Given a startup root page
	When I click the 'Home' link
	Then the result is the 'Home' page
	And the title is 'Home'
	And contains a 'Sign up' link button

Scenario: Invoking the Help link
 	Given a startup root page
	When I click the 'Help' link
	Then the result is the 'Help' page
	And the title is 'Help'
	
Scenario: Invoking the About link
 	Given a startup root page
	When I click the 'About' link
	Then the result is the 'About' page
	And the title is 'About'

Scenario: Invoking the Contact link
 	Given a startup root page
	When I click the 'Contact' link
	Then the result is the 'Contact' page
	And the title is 'Contact'
	
Scenario: Invoking the Sign up link
 	Given a startup root page
	When I click the 'Sign up now!' link
	Then the result is the 'Sign up' page
	And the title is 'Sign up'
