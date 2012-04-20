import socket
import time
import threading

class tus_server(object):

    def __init__(self, host, port):
        self.__host = host
        self.__port = port

    def open(self):
        s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        s.bind((host, port))
    
class ListeningThread(threading.Thread):

    def __init__(self, socket):
        threading.Thread.__init__(self)
        self.__socket = socket

