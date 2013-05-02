# CoffeeScript

appendContents = (cnts, area) ->
    for c in cnts
        html = c.getControllerHtml()
        html.appendTo(area)    

$(document).ready =>
    cnts = 
       new SwitchController(c) for c in testswitchdata
    blockcnts =
        new BlockController(c) for c in testblockdata

    appendContents(cnts, "#area")
    appendContents(blockcnts, "#block")

