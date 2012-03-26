jQuery ->	

	# observe text as it is entered into the search box	
	input = $('#searchUsers')		
		.keyupAsObservable()		
		.select((e) -> $('#searchUsers').val())		
		.throttle(250)		
		.distinctUntilChanged()	
		
	# define an ajax request to get the search results	
	results = (query) -> 		
		$.ajaxAsObservable(			
			url: '/Users/Index',			
			data: { q: query }		
			)		
		 .select((r) -> r.data)	
		 
	# bind the input text to the search service	
	input		
		.select(results)		
		.switchLatest()		
		.subscribe((r) -> $('#searchResults').html(r))

