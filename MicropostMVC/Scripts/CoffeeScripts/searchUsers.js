(function() {

  jQuery(function() {
    var input, results;
    input = $('#searchUsers').keyupAsObservable().select(function(e) {
      return $('#searchUsers').val();
    }).throttle(500).distinctUntilChanged();
    results = function(query) {
      return $.ajaxAsObservable({
        url: 'Index',
        data: {
          search: query
        },
        beforeSend: function() {
          return $("#progress").show();
        }
      }).select(function(r) {
        return r.data;
      });
    };
    return input.select(results).switchLatest().subscribe(function(r) {
      $("#progress").hide();
      return $('#searchResults').html(r);
    });
  });

}).call(this);
