(function() {
  var applyStyle, settings;

  settings = [
    {
      url: "http://192.168.2.9:8012/vehicles"
    }
  ];

  applyStyle = function(doc, rtname) {
    return doc.filter("#" + rtname).each(function() {
      this.style["strokeWidth"] = 4.0;
      return this.style["stroke"] = "#00ff00";
    });
  };

}).call(this);
