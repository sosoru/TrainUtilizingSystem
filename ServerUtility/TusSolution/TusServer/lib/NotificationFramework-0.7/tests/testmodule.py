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
__version__='$Revision: 1.5 $'[11:-2]

from threading import RLock
module_lock=RLock()
lock=module_lock.acquire
unlock=module_lock.release

__state__=0

def callMe(notification):
  lock()
  try:
    global __state__
    __state__=notification.object()
  finally:
    unlock()

def state():
  lock()
  try:     return __state__
  finally: unlock()
 
