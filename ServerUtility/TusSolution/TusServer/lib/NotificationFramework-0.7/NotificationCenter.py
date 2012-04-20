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


"""NotificationCenter is the central object of the framework --full
documentation is here.

  This module acts as a NotificationCenter.

  Objects registers as listeners to some events/notifications.

  Then, anytime a notification is posted to the central observers for this
  notification are automatically notified.

Listeners and callbacks
-----------------------

First at all, the Notification Center needs to have a mean to notify
objects that a notification they are listening to was posted. To achieve
this, objects should supply their reference, as well as a callback method
at registration time (Note: by default only one callback is accepted per
object; see discussion below).

The possible listeners are: class instances (`types.InstanceType`), code
objects or functions (such as functions defined in a module). See method
`addObserver` for full details ; we will focus in the following on class
instances.

For listeners being class instances, callbacks' signature should minimally
be one of these::

  def callback(self, notification)
  def callback(self, *arg)
  
Hence, any of the following signatures are okay::

  def callback(self, notification, param1='param1')
  def callback(self, notification, *arg)
  def callback(self, notification, **kw)
  etc.

Please note that the callback method must be supplied as a unbound
method, e.g.: ``ObjectClass.callback``.

When called by the NotificationCenter upon notification post, callback's
parameter ``notification`` is a `Notification` instance.

Registration: Generic- and Specific- listeners
----------------------------------------------

Object can register as "generic observers" or "specific observers".

Generic observers listens all notifications coming with a given
name. They register themselves with::

  NotificationCenter.addObserver(object, ObjectClass.method, 'NewMessage')

Generic observers will be notified each time a notification with that name
is posted, whatever the accompaying object can be (including None).


Specific observers only listens notifications which have a given name
*and* a given value. They register themselves with::

  NotificationCenter.addObserver(object, ObjectClass.method,
                                 'NewMessage', 'email')
  NotificationCenter.addObserver(object, ObjectClass.method,
                                 'NewMessage', 'news')
  etc.      

Specific observers are only notified when a notification with these name
and object is posted.

Registering more than one callback per object
---------------------------------------------

By default, `addObserver()` checks that only one callback is registered per
object; if you try to register a different one, you'll get a ValueError.

Of course, a single object can listen to multiple notifications. In that
case, you'll probably register a given method, say, handleNotification(),
dedicated to this task::

  def handleNotification(self, notification):
    notification_name=notification.name()
    if notification_name == "NOTIFICATION_ONE":
      # code for notification one
    elif notification_name == "NOTIFICATION_TWO"
      # code for notification two
    else:
      # Unhandled notification

However, sometimes you do not want this; for example, you want to bind
several methods of an object defined by a third-party module, and you do
not want to modify its code. Registering multiple callbacks is possible
when the environment variable
``NOTIFICATION_CENTER_MULTIPLE_CALLBACKS_PER_OBSERVER`` is defined and
its value is not an empty string.

.. IMPORTANT::

   This variable should be set *prior* to any import statement importing the
   `NotificationCenter` module, or it wont have any effect at all.

Example
~~~~~~~
    bash
      ::
        
            export NOTIFICATION_CENTER_MULTIPLE_CALLBACKS_PER_OBSERVER='y'
            
    python
      ::
        
          import os
          os.environ['NOTIFICATION_CENTER_MULTIPLE_CALLBACKS_PER_OBSERVER']='y'
          from NotificationFramework import NotificationCenter

Why is it not the default/only behaviour?
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    Handling multiple callbacks per object implies a performance penalty. You
    can test which are the consequences on your platform with the supplied
    script 'compare_perfs.py' in directory tests/ On my machine,
    postNotification() is about 2.4x slower when the handling of multiple
    callbacks per object is activated.

Notification names and objects
------------------------------

Notifications names should be strings, even if this is not enforced by the
framework.

Notifications objects and info can be (almost: see below) everything.
Restricting their type to existing python types will save you fighting against
some strange behaviour, however, and should be sufficient for most situations
--hopefully!


Zope: some important notes
--------------------------

TBD!!! In the meantime you can look at the code for methods
`observerCodeForZopePersistentObject()` and
`observerCodeForZopeTemporaryObject()`, and to
``tests/test_NotificationCenter.test_7_codeObjectObservers()``.
    


MT status: This library was designed to be MT-safe.

$Id: NotificationCenter.py,v 1.12 2004/06/22 18:51:05 sbigaret Exp $

"""

__version__='$Revision: 1.12 $'[11:-2]

from Notification import Notification
from threading import RLock
import mems_lib, log
import code, os, types, weakref

# ZODB specific checks
_is_ZODB_available=0
try:
  import ZODB
  from Persistence import Persistent
  _is_ZODB_available=1
except:
  pass

env_handlesMultipleCallbackPerObserver='NOTIFICATION_CENTER_MULTIPLE_CALLBACKS_PER_OBSERVER'
__handlesMultipleCallbackPerObserver=os.environ.get(env_handlesMultipleCallbackPerObserver,None)

## The following are private, do NOT access it directly!
__observers__={}
__observers_callbacks__={}
observers_global_lock=RLock()
lock=observers_global_lock.acquire
unlock=observers_global_lock.release

## private API
def _listToNotifyForNotification(notification):
  "Private: do not call directly"
  lock()
  try:
    # first the specific observers
    observers=__observers__.get(notification, [])
    # We need a copy, or the next 'if' block will possibly change 'observers'
    # which is BAD!
    # (cf. test_10_post_to_generic_specific_observers_leaves_obsvs_untouched)

    # Additional note: since the module's methods are reentrant within a
    # thread, it is possible for a listener to unregister itself when
    # receiving the notification ; in that case and if we do not return a copy
    # here, the sequence on which we iterate in postNotification() will be
    # alterated *during* the iteration and this leads to an observer not being
    # notified.

    observers=list(observers)
    # Then check for observers listening to the NotificationName, whatever the
    # object
    if notification.object() is not None:
      generalist_notification=Notification(notification.name())
      generalist_observers=__observers__.get(generalist_notification, [])
      observers+=generalist_observers
    log.trace('_listToNotifyForNotification(%s)'%notification,
              'Returns: %s'%str(observers))
    return observers
  finally:
    unlock()

if __handlesMultipleCallbackPerObserver:
  def _callbacksForObserverAndNotification(observer, notification):
    "Private: do not call directly"
    lock()
    try:
      callback=__observers_callbacks__.get((observer, notification))
      callbacks=callback and [callback] or []
      if notification.object() is not None:
        general_notification=Notification(notification.name())
        general_callback=__observers_callbacks__.get((observer, general_notification),None)
        if general_callback:
          callbacks.append(general_callback)
      return callbacks
    finally:
      unlock()
  
def _observers(): # for testing purposes mainly
  "PRIVATE, DO NOT CALL DIRECTLY"
  obs={}
  for k,v in __observers__.items():
    obs[k]=list(v)
  return obs


##
## PUBLIC API
##
def addObserver(observer, callback, notificationName, object=None,
                sameObserverRegistersOnce=1):
  """Adds the supplied observer to the list.

  Note that if the observer in a class' instance, it will be weakly
  referenced, thus this framework does not play any role in the
  garbage-collection of class' instances ; observers that are
  garbage-collected are automatically removed from the list of observers the
  first time they are supposed to receive a notification.

  `addObserver()` raises ``ValueError`` when an attempt is made to register an
  object that was previously registered with a different callback than the one
  supplied. Please refer to the module's documentation for a complete
  discussion on this topic.

  :Parameters:

    - `observer`
    - `callback`: the observer object to be notified, and the
      method to call on that object when a given notification (see other
      parameters, below) is received. The following combinations of
      observer/callback are possible:

        - observer is a class instance (either a classic or a new-style
          instance): the callback should be a unbound method. For example, if
          class A's instance a is the observer, 'A.callMe' is ok (given that
          callMe is defined in class A). This unbound method should accept a
          Notification object as its second parameter (the first parameter
          being 'self').

        - observer is a code object (see python-core's module 'code'). In that
          case, the code object must define a 'observer_object' variable: this
          object is the one that will be notified. In this situation, the
          callback should a valid method accepting that 'observer_object' as
          its first parameter, and a Notification object as its second
          argument (e.g., if 'observer_object' is an class instance, the
          callback can be an unbound method defined in the corresponding
          class).

          Such an object can be constructed that way (inspired from
          tests.test_NotificationCenter, to which you can have a look as
          well)::

                import code
                code=code.compile_command('import aModule;'+
                                          'observer_object=aModule.anObject')


        - observer is None: the callback must be of type 'types.FunctionType'

    - `notificationName`

    - `object`: identifies the notification the observer listens to. The
      `notificationName` is mandatory and is usually a string. The object is
      optional. If provided, the observer will only be notified when a
      notification is posted with the same name and accompanying object. If
      omitted or ``None``,the observer will be notified each time a
      notification with the correspoinding `notificationName` is posted
      (whatever the accompanying object can be in the posted notification).

    - `sameObserverRegistersOnce`: if omitted or if it evaluates to a true
      value, the same observer will only be added once for the notification
      identified by ``(notificationName, object)`` (if the observer is None,
      replace ``observer`` with ``callback`` in the previous sentence) ; note
      that a notification with an accompanying object being None and another
      one whose accompanying object is not None are always considered
      different, hence in this case this option has obviously no effect. If
      this parameter evaluates to a false value, then the same observer can be
      multiply registered --in this case, such an observer's callback should
      be ready to answer more than once to a single notification post, and
      should be removed as many times as it was registered.

  """
  # Note: object is not used for the moment
  lock()
  log.trace('addObserver', 'Called with: %s, %s, %s, %s'\
            %(observer, callback, notificationName, object))
  if _is_ZODB_available:
    if mems_lib.isinstance(observer, Persistent):
      raise ValueError, 'Persistent object are not allowed as listeners'

  if observer is None:
    if type(callback) != types.FunctionType:
      raise ValueError, 'Only module funcs. are allowed when observer is None'
    observer=callback
  elif type(observer) in (types.CodeType, types.ClassType):
    pass
  else:
      observer=weakref.ref(observer)

  notification=Notification(notificationName, object)
  try:
    if not __handlesMultipleCallbackPerObserver:
      if __observers_callbacks__.get(observer, callback)!=callback:
        raise ValueError, "object is already registered with a different callback (%s)"%__observers_callbacks__.get(observer)
      __observers_callbacks__[observer]=callback
    else:
      __observers_callbacks__[(observer,notification)]=callback

    if __observers__.get(notification):
      if sameObserverRegistersOnce:
        if observer not in __observers__[notification]:
          __observers__[notification].append(observer)
      else:
          __observers__[notification].append(observer)
    else:
      __observers__[notification]=[observer]
  finally:
    unlock()
  

def removeObserver(observer, notificationName=None, object=None):
  """
  Unregisters the observer for the given notification. Regarding the fact that
  an observer can be multiply registered for a single Notification (see:
  `addObserver()`), this method removes **one and only one** of the registered
  occurences except when both `notificationName` and `object` are omitted or
  None: see below.

  Silently returns if 'observer' is not registered.

  :Parameters:
    - `observer`: the observer object to unregister

    - `notificationName`

    - `object`: these parameters identify the
      `Notification` for which the observer should be unregistered. If both
      are omitted or are equal to None, then the observer is completely
      removed from all the lists of observers. This includes all occurrences
      of 'observer' if it has been multiply registered.
    
  """
  lock()
  try:
    if type(observer) in (types.CodeType, types.ClassType,
                            types.FunctionType, weakref.ReferenceType):
      pass
    else:
      observer=weakref.ref(observer)

    if notificationName is None: # remove from all lists
      for notification in __observers__.keys():
        try:
          while 1: # remove observer, possibly multiply registered
            try:
              __observers__[notification].remove(observer)
            except:
              break
          if not __observers__[notification]:
            del __observers__[notification]
        except (KeyError, ValueError):
          continue
    else:
      notification=Notification(notificationName, object)
      try:
        __observers__[notification].remove(observer)
        if not __observers__[notification]:
          del __observers__[notification]
      except (KeyError, ValueError): pass
  finally:
    unlock()



def postNotification(notificationName, object, info=None):
  ""
  try:
    lock()
    log.trace('postNotification', 'Called with: %s, %s, %s'\
              %(notificationName, object, info))
    searchNotification=Notification(notificationName, object)
    observers=_listToNotifyForNotification(searchNotification)
    for observer in observers:
      param=None
      if type(observer) is weakref.ReferenceType:
        if observer():
          param=(observer(), Notification(notificationName, object, info))
        else:
          removeObserver(observer)
      if type(observer) is types.FunctionType:
        param=(Notification(notificationName, object, info),)
      if type(observer) is types.CodeType:
        observer_object=None
        exec observer
        param=(observer_object, Notification(notificationName, object, info))
      if param:
        log.trace('postNotification', 'Notifying: %s'%observer)
        if not __handlesMultipleCallbackPerObserver:
          apply(__observers_callbacks__[observer], param)
        else:
          for c in _callbacksForObserverAndNotification(observer,
                                                        searchNotification):
            apply(c,param)
  finally:
    unlock()

# Finally, some utilities for Zope
def observerCodeForZopePersistentObject(aZopePersistentObject):
  "-"
  codeStr='import Zope;'\
           'observer_object=Zope.app()'
  for path in aZopePersistentObject.getPhysicalPath():
    codeStr+='.'+path
  log.trace('observerCodeForZopePersistentObject',
            'returning code for:\n%s'%codeStr)
  _code=code.compile_command(codeStr)
  return _code

def observerCodeForZopeTemporaryObject(aZopePersistentObject):
  "-"
  codeStr='import Zope;'\
           'observer_object=Zope.app().temp_folder'
  for path in aZopePersistentObject.getPhysicalPath():
    codeStr+='.'+path
  log.trace('observerCodeForZopePersistentObject',
            'returning code for:\n%s'%codeStr)
  _code=code.compile_command(codeStr)
  return _code

