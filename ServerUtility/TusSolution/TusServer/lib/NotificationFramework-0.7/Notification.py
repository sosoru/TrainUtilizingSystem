# -*- coding: iso-8859-1 -*-
#-----------------------------------------------------------------------------
# Notification Framework: "Observer" Design Pattern for python
#
# Copyright (c) 2001-2004 Sébastien Bigaret <sbigaret@users.sourceforge.net>
# All rights reserved.
#
# This file is part of the Notification Framework.
#
# This code is distributed under a 3-clause BSD-style license;
# see the LICENSE file for details.
#-----------------------------------------------------------------------------

"""A `Notification` object is a message transmitted by the
`NotificationCenter`.

$Id: Notification.py,v 1.8 2004/06/22 18:51:05 sbigaret Exp $
"""

__version__='$Revision: 1.8 $'[11:-2]

class Notification:
  """
  Notification objects are generally encountered when supplied by the
  `NotificationCenter` to its listener's callback method.

  A `Notification` object is designed as an immutable object.

  You should not need to directly instanciate `Notification` objects: rather,
  use `NotificationCenter` methods to add or remove observers and to post
  notifications.
  """
  def __init__(self, name, object=None, userInfo=None):
    """
    This is the Notification initializer -- you should not directly instanciate
    a `Notification` object: rather, use `NotificationCenter` methods to add
    observers or to post notifications
    """
    self.__dict__['_name']=name
    self.__dict__['_object']=object
    self.__dict__['_info']=userInfo
    self.__dict__['_hashCode']=hash(name) ^ hash(object)

  def name(self):
    "Returns the receiver's field 'name'"
    return self._name

  def object(self):
    "Returns the receiver's field 'object'"
    return self._object

  def userInfo(self):
    "Returns the receiver's field 'info'"
    return self._info
  
  def __eq__(self, aNotification):
    """
    Two notifications are equal if and only if their names and objects are
    equal
    """
    if self._name==aNotification.name() and \
       self._object==aNotification.object():
      return 1
    return 0

  def __ne__(self, aNotification):
    "Boolean negation of __eq__"
    return not self.__eq__(aNotification)
  
  def __hash__(self):
    "Returns the hash code for the receiver"
    return self._hashCode

  def __setattr__(self, name, value):
    "Raises ``TypeError`` since this is an immutable object"
    raise TypeError, 'object has read-only attributes'

  def __str__(self):
    "Returns a user-presentable string representing the object"
    return "<Notification name:'%s' object:'%s'>"%(self._name, self._object)
