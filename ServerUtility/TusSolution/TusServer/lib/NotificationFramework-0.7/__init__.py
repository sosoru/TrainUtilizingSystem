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
"""
The NotificationFramework is a python implementation of the "Observer"
Design Pattern, allowing one-to-many dependency between objects even when
they have no idea of who and where the other objects live.

The complete documentation can found in module `NotificationCenter`.

It is distributed under a 3-clause BSD-style license;
see the LICENSE file for details.

`Download the last release at sourceforge`_

.. _`Download the last release at sourceforge`: https://sourceforge.net/project/showfiles.php?group_id=58935&package_id=54990

Example of use:

.. code-block:: Python

  >>> from NotificationFramework import NotificationCenter as NC
  
  >>> SUBJECT_CHANGED='Subject changed' # string identifying the notification
  
  >>> class Subject:
  ...   value=0
  ...   def __init__(self, name):
  ...     self.name=name
  ...   def change_and_notify(self, value):
  ...     self.value=value
  ...     NC.postNotification(SUBJECT_CHANGED, self, info=value)
  ...   def __str__(self):
  ...     return self.name
  
  >>> class Observer:
  ...   def __init__(self, name):
  ...     self.name=name
  ...   
  ...   def handle_notification(self, notification):
  ...     print self.name, 'received notification:', str(notification)
  ...     print '                    with info:', notification.userInfo()
  ... 
  >>> s1=Subject('s1')
  >>> s2=Subject('s2')
  >>> observ1, observ2 = Observer('observ1'), Observer('observ2')
  >>> generic = Observer('generic')
  
  >>> # observ1 listens to SUBJECT_CHANGED posted by s1
  ... NC.addObserver(observ1, Observer.handle_notification, SUBJECT_CHANGED, s1)
  >>> # observ2 listens to SUBJECT_CHANGED posted by s2
  ... NC.addObserver(observ2, Observer.handle_notification, SUBJECT_CHANGED, s2)
  >>> # generic listens to all SUBJECT_CHANGED notifications
  ... NC.addObserver(generic, Observer.handle_notification, SUBJECT_CHANGED)
  
  
  >>> s1.change_and_notify(value='hop')
  observ1 received notification: <Notification name:'Subject changed' object:'s1'>
                      with info: hop
  generic received notification: <Notification name:'Subject changed' object:'s1'>
                      with info: hop
  
  
  >>> s2.change_and_notify(value=3)
  observ2 received notification: <Notification name:'Subject changed' object:'s2'>
                      with info: 3
  generic received notification: <Notification name:'Subject changed' object:'s2'>
                      with info: 3

.. raw:: html

   Hosted by:<a href="http://sourceforge.net">
   <img src="http://sourceforge.net/sflogo.php?group_id=58935&amp;type=1"
    width="88" height="31" border="0" alt="SourceForge.net Logo">
   </a>

"""
__version__="0.7"
