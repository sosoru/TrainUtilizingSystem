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
Logging methods.

  All methods `log()`, `warn()` and `trace()` are disabled by default. To
  enable them set the environment variable ``NOTIFICATION_FRAMEWORK_TRACE``
  (for debugging purpose mainly)

CVS Information

  $Id: log.py,v 1.7 2004/06/22 18:51:05 sbigaret Exp $
"""

__version__='$Revision: 1.7 $'[11:-2]

import os

if os.environ.get('NOTIFICATION_FRAMEWORK_TRACE', None):
  try:
    from zLOG import LOG, INFO, WARNING, TRACE
  except:
    INFO='INFO'
    WARNING='WARNING'
    TRACE='TRACE'
    import sys
    from time import strftime
    def LOG(subsystem, severity, summary, detail, error=None, reraise=None):
      sys.stderr.write('------\n%s LOG[%s]: %s\n%s\n'%\
                       (strftime('%Y-%m-%dT%H:%M:%S'),severity,summary,detail))
      
  def log(summary, detail, error=None, reraise=None):
    LOG(subsystem='NotificationFramework', severity=INFO,
        summary=summary, detail=detail+'\n', error=error, reraise=reraise)
  def warn(summary, detail, error=None, reraise=None):
    LOG(subsystem='NotificationFramework', severity=WARNING,
        summary=summary, detail=detail+'\n', error=error, reraise=reraise)
  def trace(summary, detail, error=None, reraise=None):
    LOG(subsystem='NotificationFramework', severity=INFO,
        summary=summary, detail=detail+'\n', error=error, reraise=reraise)
else:
  def log(summary, detail, error=None, reraise=None):
    pass
  def warn(summary, detail, error=None, reraise=None):
    pass
  def trace(summary, detail, error=None, reraise=None):
    pass
