(function() {

  jQuery(function() {
    var input, results;
    input = $('#searchUsers').keyupAsObservable().select(function(e) {
      return $('#searchUsers').val();
    }).throttle(250).distinctUntilChanged();
    results = function(query) {
      return $.ajaxAsObservable({
        url: 'Index',
        data: {
          search: query
        }
      }).select(function(r) {
        return r.data;
      });
    };
    return input.select(results).switchLatest().subscribe(function(r) {
      return $('#searchResults').html(r);
    });
  });

}).call(this);
