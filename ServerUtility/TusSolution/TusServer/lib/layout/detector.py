

class Detector(object):

    def __init__(self, func_detected, func_left):
        self.__detected = func_detected
        self.__left = func_left

    def on_detected(self):
        self.__detected(self)

    def on_left(self):
        self.__left(self)
    
