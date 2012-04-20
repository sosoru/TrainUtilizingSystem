#! /usr/bin/env python
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

"""Test the Model"""

__version__='$Revision: 1.10 $'[11:-2]

import unittest, os, sys, weakref
import utils
utils.fixpath()

from NotificationFramework import NotificationCenter as NC

# ZODB specific checks
_is_ZODB_available=0
try:
  import ZODB
  from Persistence import Persistent
  _is_ZODB_available=1
except:
  pass

if '--new-style' in sys.argv:
  base_object=object
else:
  class base_object: pass

class TestObject(base_object):
  def __init__(self):
    self.state=self.state_2=0
    self.nbOfNotificationsReceived=0
  def callback(self, *kw):
    self.nbOfNotificationsReceived += 1
    self.state=kw[0].object()
    self.userInfo=kw[0].userInfo()
  def callback_2(self, *kw):
    self.nbOfNotificationsReceived += 1
    self.state_2=kw[0].object()
    self.userInfo_2=kw[0].userInfo()

class TestObjectUnregister(base_object):
  def __init__(self):
    self.state=0
  def callMe(self, *kw):
    self.state=kw[0].object()
    self.userInfo=kw[0].userInfo()
    NC.removeObserver(self)
    
if _is_ZODB_available:
  class ZTestObject(Persistent):
    def __init__(self):
      self.state=0
    def callMe(self, *kw):
      self.state=kw[0].object()
      self.userInfo=kw[0].userInfo()
  
class TestNotificationCenter(unittest.TestCase):
  "Tests for the module NotificationCenter"
  def test_01_initial(self):
    "[NotificationCenter] Initial state"
    self.failIf(NC._observers(), "test initial")

  def test_02_silent(self):
    "[NotificationCenter] Tests that functions keep silent"
    one=TestObject()
    self.failIf(NC.removeObserver(one), "Unregister an unregistered object")
    self.failIf(NC.removeObserver(one, 'dummyNotification'),
                "Unregister an unregistered object from 'dummyNotification'")
    NC.addObserver(one, 'callMe', 'dummyNotification')
    two=TestObject
    self.failIf(NC.removeObserver(two), "Unregister an unregistered object")
    self.failIf(NC.removeObserver(two, 'dummyNotification'),
                "Unregister an unregistered object from 'dummyNotification'")
    NC.removeObserver(one)

  def test_03_add_remove(self):
    "[NotificationCenter] Tests that addition+removal leaves state untouched"
    one=TestObject()
    observers_before=NC._observers()
    NC.addObserver(one, 'callMe', 'notif_test_3')
    NC.addObserver(one, 'callMe', 'notif_test_3') # not multiply registered
    observers_after=NC._observers()
    self.failIfEqual(observers_before, observers_after)

    NC.removeObserver(one, 'notif_test_3')
    observers_after=NC._observers()
    self.failUnlessEqual(observers_before, observers_after)

    # same but global
    observers_before=NC._observers()
    NC.addObserver(one, 'callMe', 'notif_test_3')
    NC.addObserver(one, 'callMe', 'notif_test_3')
    observers_after=NC._observers()
    self.failIfEqual(observers_before, observers_after)

    NC.removeObserver(one)
    observers_after=NC._observers()
    self.failUnlessEqual(observers_before, observers_after)

    # same with a weakref (postNotification removes dead weakref when it finds
    # some)
    observers_before=NC._observers()
    NC.addObserver(one, 'callMe', 'notif_test_3')
    NC.addObserver(one, 'callMe', 'notif_test_3')
    observers_after=NC._observers()
    self.failIfEqual(observers_before, observers_after)

    from NotificationFramework.Notification import Notification
    wone=NC._observers()[Notification('notif_test_3')][0]
    self.assertEqual(type(wone), weakref.ReferenceType)
    NC.removeObserver(wone)
    observers_after=NC._observers()
    self.failUnlessEqual(observers_before, observers_after)

  def test_04_generic_postNotification(self):
    "[NotificationCenter] Tests generic observers"
    one=TestObject()
    two=TestObject()
    NC.addObserver(one, TestObject.callback, 'notif_test_4')
    NC.addObserver(two, TestObject.callback, 'notif_test_4')
    NC.postNotification('notif_test_4', 2);
    self.failIf(one.state!= 2)
    self.failIf(two.state!= 2)

  def test_04b_generic_one_callback_per_notification(self):
    "[NotificationCenter] (generic) One callback per notification"
    if not os.environ.get(NC.env_handlesMultipleCallbackPerObserver):
      return
    one=TestObject()
    NC.addObserver(one, TestObject.callback, 'notif_test_4b_MSG_ONE')
    NC.addObserver(one, TestObject.callback_2, 'notif_test_4b_MSG_TWO')
    NC.postNotification('notif_test_4b_MSG_ONE', 'MSG_ONE');
    self.failIf(one.state != 'MSG_ONE')
    self.failIf(one.state_2 != 0)
    self.failIf(one.nbOfNotificationsReceived != 1)
    NC.postNotification('notif_test_4b_MSG_TWO', 'MSG_TWO');
    self.failIf(one.state != 'MSG_ONE')
    self.failIf(one.state_2 != 'MSG_TWO')
    self.failIf(one.nbOfNotificationsReceived != 2)
    
  def test_05a_specific_postNotification(self):
    "[NotificationCenter] Tests specific observers"
    one=TestObject()
    two=TestObject()
    userInfo={'1': 1, '2': '2'}

    NC.addObserver(one, TestObject.callback, 'notif_test_5', 10)
    NC.addObserver(two, TestObject.callback, 'notif_test_5', 20)
    NC.postNotification('notif_test_5', 2);
    self.failIf(one.state!= 0, "one shouldn't have received 'notif_test_5'")
    self.failIf(two.state!= 0, "two shouldn't have received 'notif_test_5'")
    NC.postNotification('notif_test_5', 10, userInfo);
    self.failIf(one.state!= 10, "one hasn't received 'notif_test_5'")
    self.failIf(one.userInfo!=userInfo, "one hasn't received 'notif_test_5'")
    self.failIf(two.state!= 0, "two shouldn't have received 'notif_test_5'")
    NC.postNotification('notif_test_5', 20);
    self.failIf(one.state!= 10, "one shouldn't have received 'notif_test_5'")
    self.failIf(two.state!= 20, "two hasn't received 'notif_test_5'")

  def test_05b_specific_postNotification(self):
    "[NotificationCenter] Tests specific observers unregistering themselves at notification time"
    # see comments in NotificationCenter._listToNotifyForNotification()
    one=TestObject()
    unregister=TestObjectUnregister()
    two=TestObject()
    userInfo={'1': 1, '2': '2'}

    NC.addObserver(one, TestObject.callback, 'notif_test_5b', 10)
    NC.addObserver(unregister, TestObjectUnregister.callMe, 'notif_test_5b',10)
    NC.addObserver(two, TestObject.callback, 'notif_test_5b', 10)
    NC.postNotification('notif_test_5b', 10)
    self.failIf(one.state!= 10, "one should have received 'notif_test_5b'")
    self.failIf(unregister.state!= 10, "unregister should have received 'notif_test_5b'")
    self.failIf(two.state!= 10, "two should have received 'notif_test_5b'")
    
  def test_05c_specific_one_callback_per_notification(self):
    "[NotificationCenter] (specific) One callback per notification"
    if not os.environ.get(NC.env_handlesMultipleCallbackPerObserver):
      return
    one=TestObject()
    NC.addObserver(one, TestObject.callback, 'notif_test_5c_MSG','01')
    NC.addObserver(one, TestObject.callback_2, 'notif_test_5c_MSG', '02')
    NC.postNotification('notif_test_5c_MSG', '00');
    self.failIf(one.state != 0)
    self.failIf(one.state_2 != 0)
    self.failIf(one.nbOfNotificationsReceived != 0)
    NC.postNotification('notif_test_5c_MSG', '01');
    self.failIf(one.state != '01')
    self.failIf(one.state_2 != 0)
    self.failIf(one.nbOfNotificationsReceived != 1)
    NC.postNotification('notif_test_5c_MSG', '02');
    self.failIf(one.state != '01')
    self.failIf(one.state_2 != '02')
    self.failIf(one.nbOfNotificationsReceived != 2)
    
  def test_06_ZTests(self):
    "[NotificationCenter] Tests the specific checks made when ZODB.Persistent is available"
    if not _is_ZODB_available:
      return
    one=ZTestObject()
    self.failUnlessRaises(ValueError, NC.addObserver,
                          one, TestObject.callback, 'notif_test_6')
    
  def test_07_codeObjectObservers(self):
    "[NotificationCenter] Tests for code objects as observers"
    one=TestObject()
    import testmodule
    testmodule.one=one # make it a module variable
    import code
    userInfo={'1': 1, '2': '2'}

    # saves the current set of observers so that it can be compared
    # after add/remove
    observers_before=NC._observers()

    # Add and tests notifications
    code=code.compile_command('import testmodule;'\
                              'observer_object=testmodule.one')
    NC.addObserver(code, TestObject.callback, 'notif_test_7')
    NC.postNotification('notif_test_7', 7, userInfo)
    self.failIf(one.state!= 7, "one hasn't received 'notif_test_7'")
    self.failIf(one.userInfo!=userInfo, "one hasn't received 'notif_test_7'")
    # remove and tests equality of the observer's set
    # __TBD this is a pity that we need to have the exact compiled code that
    # was registered to be able to unregister it...
    NC.removeObserver(code, 'notif_test_7')
    self.failUnlessEqual(observers_before, NC._observers())

  def test_08_observersAsModuleFunction(self):
    "[NotificationCenter] Tests a module function as an observer"
    import testmodule
    # saves the current set of observers so that it can be compared
    # after add/remove
    observers_before=NC._observers()

    # add and test notification is correctly delivered
    NC.addObserver(None, testmodule.callMe, 'notif_test_8')
    NC.postNotification('notif_test_8', 8)
    self.failUnlessEqual(testmodule.state(),8)
    # remove
    NC.removeObserver(testmodule.callMe, 'notif_test_8')
    self.failUnlessEqual(observers_before, NC._observers(),
                         'couldnt remove FunctionType observer?')

  def test_09_observerMultiplyRegistered(self):
    "[NotificationCenter] observerMultiplyRegistered"
    one=TestObject()
    NC.addObserver(one, TestObject.callback, 'notif_test_9')
    NC.addObserver(one, TestObject.callback, 'notif_test_9',
                   sameObserverRegistersOnce=0)
    NC.postNotification('notif_test_9', 9);
    self.failIf(one.state!= 9)
    self.failIf(one.nbOfNotificationsReceived!=2)

    # this tests the ``remove all'' stuff
    NC.removeObserver(one) # remove all
    NC.postNotification('notif_test_9', 19);
    self.failIf(one.state!= 9)
    self.failIf(one.nbOfNotificationsReceived!=2)

    # Tests that multiply registered observer can be removed and still
    # remains registered
    NC.addObserver(one, TestObject.callback, 'notif_test_9')
    NC.addObserver(one, TestObject.callback, 'notif_test_9',
                   sameObserverRegistersOnce=0)
    NC.removeObserver(one, 'notif_test_9') # remove one
    NC.postNotification('notif_test_9', 29);
    self.failIf(one.state!= 29)
    self.failIf(one.nbOfNotificationsReceived!=3)
    
  def test_10_post_to_generic_specific_observers_leaves_obsvs_untouched(self):
    "[NotificationCenter] post to generic specific observers leaves obsvs untouched"
    # There was a bug in _listToNotifyForNotification triggered when
    # generic and specific observers are listening to a single notification.
    # This bug increases the size of the observers for a notification each
    # time postNotification() was called
    one=TestObject()
    two=TestObject()
    NC.addObserver(one, TestObject.callback, 'notif_test_10')
    NC.addObserver(two, TestObject.callback, 'notif_test_10')
    NC.addObserver(one, TestObject.callback, 'notif_test_10', '10')
    NC.addObserver(two, TestObject.callback, 'notif_test_10', '10')
    observers_before=NC._observers()
    NC.postNotification('notif_test_10', '10');
    NC.postNotification('notif_test_10', '10');
    observers_after=NC._observers()
    self.failUnlessEqual(observers_before, observers_after)

  def test_11_addObserver_and_multiple_callbacks(self):
    "[NotificationCenter] addObserver() and multiple callbacks"
    one=TestObject()
    NC.addObserver(one, TestObject.callback, 'notif_test_11')
    if not os.environ.get(NC.env_handlesMultipleCallbackPerObserver):
      self.assertRaises(ValueError, NC.addObserver,
                        one, TestObject.callback_2, 'notif_test_11b')
    else:
      NC.addObserver(one, TestObject.callback_2, 'notif_test_11b')

  def _test_observer(self):
    "Test with objects as observers"
    NC.addObserver()

def test_suite():
    suite = unittest.TestSuite()
    suite.addTest(unittest.makeSuite(TestNotificationCenter, "test_"))
    return suite

def run_tests(verbosity=0):
    try:
      del os.environ[NC.env_handlesMultipleCallbackPerObserver]
    except:
      pass
    reload(NC)
    errs = utils.run_suite(test_suite(), verbosity=verbosity)
    os.environ[NC.env_handlesMultipleCallbackPerObserver]='1'
    reload(NC)
    errs += utils.run_suite(test_suite(), verbosity=verbosity)
    return errs
  
if __name__ == "__main__":
    errs = run_tests()
    sys.exit(errs and 1 or 0)
