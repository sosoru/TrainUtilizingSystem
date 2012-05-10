import sys

class Command(object):
    
    def __init__(self):
        pass

    def Execute(self, train):
        self.__train = train

    @property
    def Description(self):
        return "base command"

class MoveCommand(Command):

    def __init__(self, route):
        Command.__init__(self)

        __self.route = route

    @property
    def Description(self):
        return "route command"