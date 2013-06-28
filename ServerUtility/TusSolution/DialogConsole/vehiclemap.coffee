# CoffeeScript

settings = [
    url :"http://192.168.2.9:8012/vehicles",
    ]

applyStyle = (doc, rtname) ->
    doc.filter("#" + rtname).each ->
       this.style["strokeWidth"] = 4.0
       this.style["stroke"] = "#00ff00"
