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

"""
Evaluates the performance penalty implied by the activation of the ability to
handle multiple callbacks per object
"""

__version__='$Revision: 1.4 $'[11:-2]

import os, sys, time
import utils
from test_NotificationCenter import TestObject
from NotificationFramework import NotificationCenter as NC
utils.fixpath()

N=1000

def loop(nb_iterations):
  o1=TestObject()
  o2=TestObject()
  NC.addObserver(o1, TestObject.callback, 'MSG')
  NC.addObserver(o2, TestObject.callback, 'MSG')
  NC.addObserver(o1, TestObject.callback, 'MSG', '01')
  NC.addObserver(o2, TestObject.callback, 'MSG', '01')
  t=time.time()
  i=0
  while i<nb_iterations:
    NC.postNotification('MSG','01')
    i+=1
  return time.time()-t

def run():
  try:
    del os.environ[NC.env_handlesMultipleCallbackPerObserver]
  except:
    pass
  reload(NC)
  t0=loop(N)
  print 'Nb of iterations: %i'%N
  print 'Without multiple callbacks per observer: %.6f'%t0
  os.environ[NC.env_handlesMultipleCallbackPerObserver]='1'
  reload(NC)
  t1=loop(N)
  print 'With multiple callbacks per observer:    %.6f'%t1
  print 'Ratio: %.4f'%(t1/t0)

if __name__=='__main__':
  #import profile
  #profile.run('run()', 'profile.out')
  run()
