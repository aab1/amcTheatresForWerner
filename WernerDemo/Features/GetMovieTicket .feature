Feature: GetMovieTicket 
	

@mytag
Scenario: Get a movie ticket
	Given I Navigate to amctheatres hompage
	When I select a random movie time
	And I select a random seat
	And I add adult
	Then The Adult updates to appear as "1 Adult"
	And Continue Button is Enabled