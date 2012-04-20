import sys

import layout.models

class OutOfRailError(Exception):
    def __init__(self):
        pass    

class WanderingError(Exception):
    def __init__(self):
        pass

class Train(object):
    """represents a train running its layout"""

    def __init__(self, layout, route=None):
        self.__layout = layout
        self.__sections = []
        self.__route = route

    def add_section(self, sect):
        self.__sections.append(sect)

    def del_section(self, sect):
        self.__sections.remove(sect)

    @property
    def current_sections(self):
        return self.__sections

    def get_route(self):
        return self.__route
    def set_route(self, route):
        self.__route = route
    Route = property(get_route, set_route)

    def run(self):
        sect_first = self.__sections[0]
        sect_last = self.__sections[-1]
        sect_newer = sect_last.next_sections

        if self.Route is None:
            raise WanderingError

        if len(sect_newer) == 0:
            raise OutOfRailError
        
        sect_first.bound_detector.on_left()
        
        

       



    


