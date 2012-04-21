import sys

import layout.train as trn
import layout.detector as det

class Junction(object):

    def __init__(self):
        self.__sections = []

    def add_section(self, sect):
        self.__sections.append(sect)

    def get_sections(self):
        return set(self.__sections)

class Signal(object):

    def __init__(layout):
        self.__layout = layout
        pass
    
class Section(object):

    def __init__(self, tuple_juncs):
        self.__juncs = tuple_juncs
        self.__detector = self._create_detector()

        for j in tuple_juncs:
            j.add_section(self)
        
    def get_juncs(self):
        return self.__juncs

    @property
    def next_sections(self):
        junc = self.__juncs[1]

        return [sect for sect in junc.get_sections() if not sect == self]

    @property
    def prev_sections(self):
        junc = self_jncs[0]

        return [s for s in junc.get_sections() if not s == self]

    def _change_detection(self, val):
        self.__is_exists = val

    def _create_detector(self):
        self.__is_exists = False

        detected = lambda sender: self._change_detection(True)
        left     = lambda sender: self._change_detection(False)

        d = det.Detector(detected, left)

        return d

    @property
    def is_exists_train(self):
        return self.__is_exists

    @property
    def bound_detector(self):
        return self.__detector

class NotFoundError(Exception):
    def __init__(self):
        pass

class Layout(object):

    def __init__(self, sects, juncs):
        self.__sections = sects
        self.__junctions = juncs

    @property
    def bound_sections(self):
        return self.__sections

    @property
    def bound_junctions(self):
        return self.__junctions

    def get_trains(self):
        dts = [ self.__sections.index(m) for m in self.__sections if m.is_exists_train]
        
        if len(dts) == 0:
            return []

        last = dts[0]
        sect_tmp = []
        av_sects = []
        
        for item in dts:
            if last < item - 1: # not series
                av_sects.append(sect_tmp)
                sect_tmp = []

            sect_tmp.append(item)              
            last = item

        if len(sect_tmp) > 0:
            av_sects.append(sect_tmp)

        trains = []
        for sects in av_sects:
            tr = trn.Train(self)

            [tr.add_section(self.__sections[num]) for num in sects]
            trains.append(tr)
        
        return trains            

    def get_route(self, sects):
        total = [sects[0]] #total route

        for i in range(0, len(sects)-1):
            self._co_get_route(sects[i], sects[i+1], total)

        return Route(total)

    def _co_get_route(self, start, end, history):
        candicates = []
        for j in start.get_juncs():
            candicates.extend(j.get_sections())

        if len(candicates) == 0 :
            raise NotFoundError

        if end in candicates:
            history.append(end)
            return history
        
        for c in candicates:
            if c in history:
                continue
            try:
                history.append(c)
                self._co_get_route(c, end, history)
            except NotFoundError as e:
                history.remove(c)

        return history
            

class Route(object):

    def __init__(self, sects):
        self.__section = sects

    @property
    def sections(self):
        return self.__section