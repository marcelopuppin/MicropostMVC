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
			url: 'Index',			
			data: { search: query }, 
			beforeSend: () -> $("#progress").show()
		).select((r) -> r.data)	
		 
	# bind the input text to the search service	
	input		
		.select(results)		
		.switchLatest()		
		.subscribe((r) -> $("#progress").hide(); $('#searchResults').html(r))