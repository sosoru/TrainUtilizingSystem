#!/usr/bin/env python
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

"""Run all tests."""

__version__='$Revision: 1.6 $'[11:-2]

import os, sys, getopt
import utils
import unittest

def usage(prgName, exitStatus=None):
  _usage="""%s [-v] [-V]
Runs the tests for the Notification package

Options
--------
  -v  Minimally verbose
  -V  Really verbose
  -h  Prints this message
""" % prgName
  if exitStatus is not None:
    print _usage
    sys.exit(exitStatus)
  else:
    return _usage

def test_suite():
  import test_Notification
  suite=unittest.TestSuite()
  suite.addTest(test_Notification.test_suite())
  return suite

def main(args):
  me=args[0]
  try: options, args = getopt.getopt(sys.argv[1:], 'hvVZ')
  except: usage(me, 1)
  verbose=0
  zope_path=''
  for k, v in options:
    if k=='-h': usage(me, 1)
    if k=='-v': verbose=1; continue
    if k=='-V': verbose="Y"; continue
    if k=='-Z': zope_path='/home/big/Zope-2.5.0-linux2-x86/lib/python/'
  if args: usage(me, 1) #raise 'Unexpected arguments', args
  utils.fixpath(zope_path)

  errs=utils.run_suite(test_suite(), verbosity=verbose)
  import test_NotificationCenter
  errs+=test_NotificationCenter.run_tests(verbosity=verbose)

if __name__ == "__main__":
  errs = main(sys.argv)
  sys.exit(errs and 1 or 0)
