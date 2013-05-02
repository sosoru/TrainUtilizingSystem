# CoffeeScript
settings = 
    serverAddr : "http://192.168.2.9:8012"

class Controller
    constructor: (@model) ->

    getControllerHtml: ->
        obj = $.templates(this.model.tmplId).render(this.model.data)
        return $(obj)
    
    SendCommand: ->
        $.post(this.model.postAddr, this.model.data)

class SwitchController extends Controller
    constructor: (jsondata) ->
       model = 
            data : jsondata
            tmplId : "#SwitchTemplate"
            postAddr : settings.serverAddr + "/switch"
       super model 

    PositionChanged: =>
        pos = this.model.data.CurrentState.Position
        if pos == "Straight"
            pos = "Curve"
        else # if curve or any
            pos = "Straight"
        this.model.data.CurrentState.Position = pos
        this.SendCommand()

    getControllerHtml: ->
        html = super
        html.filter(".positionChange").click(@PositionChanged)
        return html

class BlockController extends Controller
    constructor: (jsondata) ->
        model = 
            data : jsondata
            tmplId : "#BlockTemplate"
            postAddr : settings.serverAddr + "/block"
        super model

    createController:(dev)->
        return new SwitchController(dev) if dev.ModuleType == 'AvrSwitch'
        return undefined

    getControllerHtml: ->
        html = super
        for dev in this.model.data.Devices
            cnt = this.createController(dev)
            cnt.getControllerHtml().appendTo(html) if cnt?
        return html

