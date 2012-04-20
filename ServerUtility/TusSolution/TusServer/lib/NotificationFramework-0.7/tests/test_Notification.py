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

"""Test the Notification class"""

__version__='$Revision: 1.8 $'[11:-2]

import unittest, sys
import utils
utils.fixpath()

from NotificationFramework.Notification import Notification


class TestNotification(unittest.TestCase):
  "Tests for the Notification class"
  def test_1_equality(self):
    "[Notification] Tests equality"
    one=Notification('notif_1', 'notif_object_1')
    two=Notification('notif_1', 'notif_object_1')
    self.assertEqual(one, two, "one and two should be equal")
    two=Notification('notif_1', 'notif_object_1', {'additionalInfo': 'Hop'})
    self.assertEqual(one, two,
                     "one and two should be equal (even with different info)")
    
def test_suite():
    suite = unittest.TestSuite()
    suite.addTest(unittest.makeSuite(TestNotification, "test_"))
    return suite


if __name__ == "__main__":
    errs = utils.run_suite(test_suite())
    sys.exit(errs and 1 or 0)
