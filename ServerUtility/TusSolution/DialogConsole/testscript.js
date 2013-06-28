(function() {
  var appendContents,
    _this = this;

  appendContents = function(cnts, area) {
    var c, html, _i, _len, _results;
    _results = [];
    for (_i = 0, _len = cnts.length; _i < _len; _i++) {
      c = cnts[_i];
      html = c.getControllerHtml();
      _results.push(html.appendTo(area));
    }
    return _results;
  };

  $(document).ready(function() {
    var blockcnts, c, cnts;
    cnts = (function() {
      var _i, _len, _results;
      _results = [];
      for (_i = 0, _len = testswitchdata.length; _i < _len; _i++) {
        c = testswitchdata[_i];
        _results.push(new SwitchController(c));
      }
      return _results;
    })();
    blockcnts = (function() {
      var _i, _len, _results;
      _results = [];
      for (_i = 0, _len = testblockdata.length; _i < _len; _i++) {
        c = testblockdata[_i];
        _results.push(new BlockController(c));
      }
      return _results;
    })();
    appendContents(cnts, "#area");
    return appendContents(blockcnts, "#block");
  });

}).call(this);
