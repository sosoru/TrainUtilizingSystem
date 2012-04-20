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

"""Helper functions for the test suite."""
# inspired by TAL tests.utils

__version__='$Revision: 1.6 $'[11:-2]

import os, sys

# 
# mydir is where this file is, codedir is 2 level up
def fixpath(zope_path=''):
  mydir = os.path.abspath(os.path.dirname(__file__))
  codedir = os.path.dirname(os.path.dirname(mydir))
  if codedir not in sys.path:
    sys.path=[codedir]+sys.path
  if zope_path and zope_path not in sys.path:
    sys.path=[zope_path]+sys.path

import unittest

def run_suite(suite, outf=sys.stdout, errf=None, verbosity=0):
    """
    Runs the suite.
    Verbosity: 0: not verbose, 1: just print points, "y": verbose
    """
    runner = unittest.TextTestRunner(outf, verbosity=verbosity)
    result = runner.run(suite)

##     print "\n\n"
##     if result.errors:
##         print "Errors (unexpected exceptions):"
##         map(print_error, result.errors)
##         print
##     if result.failures:
##         print "Failures (assertion failures):"
##         map(print_error, result.failures)
##         print
    newerrs = len(result.errors) + len(result.failures)
    if newerrs:
        print "'Errors' indicate exceptions other than AssertionError."
        print "'Failures' indicate AssertionError"
        if errf is None:
            errf = sys.stderr
        errf.write("%d errors, %d failures\n"
                   % (len(result.errors), len(result.failures)))
    return newerrs

#def print_error(info):
#    testcase, (type, e, tb) = info
