(function() {
  var BlockController, Controller, SwitchController, appendContents, settings,
    __bind = function(fn, me){ return function(){ return fn.apply(me, arguments); }; },
    __hasProp = {}.hasOwnProperty,
    __extends = function(child, parent) { for (var key in parent) { if (__hasProp.call(parent, key)) child[key] = parent[key]; } function ctor() { this.constructor = child; } ctor.prototype = parent.prototype; child.prototype = new ctor(); child.__super__ = parent.prototype; return child; },
    _this = this;

  settings = {
    serverAddr: "http://192.168.2.9:8012"
  };

  Controller = (function() {

    function Controller(model) {
      this.model = model;
    }

    Controller.prototype.getControllerHtml = function() {
      var obj;
      obj = $.templates(this.model.tmplId).render(this.model.data);
      return $(obj);
    };

    Controller.prototype.SendCommand = function() {
      return $.post(this.model.postAddr, this.model.data);
    };

    return Controller;

  })();

  SwitchController = (function(_super) {

    __extends(SwitchController, _super);

    function SwitchController(jsondata) {
      this.PositionChanged = __bind(this.PositionChanged, this);

      var model;
      model = {
        data: jsondata,
        tmplId: "#SwitchTemplate",
        postAddr: settings.serverAddr + "/switch"
      };
      SwitchController.__super__.constructor.call(this, model);
    }

    SwitchController.prototype.PositionChanged = function() {
      var pos;
      pos = this.model.data.CurrentState.Position;
      if (pos === "Straight") {
        pos = "Curve";
      } else {
        pos = "Straight";
      }
      this.model.data.CurrentState.Position = pos;
      return this.SendCommand();
    };

    SwitchController.prototype.getControllerHtml = function() {
      var html;
      html = SwitchController.__super__.getControllerHtml.apply(this, arguments);
      html.filter(".positionChange").click(this.PositionChanged);
      return html;
    };

    return SwitchController;

  })(Controller);

  BlockController = (function(_super) {

    __extends(BlockController, _super);

    function BlockController(jsondata) {
      var model;
      model = {
        data: jsondata,
        tmplId: "#BlockTemplate",
        postAddr: settings.serverAddr + "/block"
      };
      BlockController.__super__.constructor.call(this, model);
    }

    BlockController.prototype.createController = function(dev) {
      if (dev.ModuleType === 'AvrSwitch') {
        return new SwitchController(dev);
      }
      return void 0;
    };

    BlockController.prototype.getControllerHtml = function() {
      var cnt, dev, html, _i, _len, _ref;
      html = BlockController.__super__.getControllerHtml.apply(this, arguments);
      _ref = this.model.data.Devices;
      for (_i = 0, _len = _ref.length; _i < _len; _i++) {
        dev = _ref[_i];
        cnt = this.createController(dev);
        if (cnt != null) {
          cnt.getControllerHtml().appendTo(html);
        }
      }
      return html;
    };

    return BlockController;

  })(Controller);

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
