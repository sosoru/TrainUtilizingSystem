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
"""Notification Framework: Observer Design Pattern

The Notification Framework allows one-to-many dependency between objects,
where an object (the subject) can tell to an unknown number of objects (the
observers) that an event occurred (typically, that the object has changed),
without having any idea of who the observers are and where they live
(abstract, minimal coupling).
"""
from distutils.core import setup
import glob, os, sys

# Instruction for PyPi found at:
# http://www.python.org/~jeremy/weblog/030924.html
classifiers = """\
Development Status :: 5 - Production/Stable
Intended Audience :: Developers
License :: OSI Approved :: BSD License
Operating System :: OS Independent
Programming Language :: Python
Natural Language :: English
Natural Language :: French
Topic :: Software Development :: Libraries
Topic :: Software Development :: Libraries :: Application Frameworks
Topic :: Software Development :: Libraries :: Python Modules
"""
if sys.version_info < (2, 3):
    _setup = setup
    def setup(**kwargs):
        if kwargs.has_key("classifiers"):
            del kwargs["classifiers"]
        _setup(**kwargs)

doclines = __doc__.split("\n")
short_description = doclines[0]
long_description = "\n".join(doclines[2:])

setup(name="NotificationFramework",
      version="0.7",
      license ="BSD License",
      description=short_description,
      author="Sebastien Bigaret",
      author_email="sbigaret@users.sourceforge.net",
      maintainer="Sebastien Bigaret",
      maintainer_email="sbigaret@users.sourceforge.net",
      url="http://modeling.sourceforge.net/API/Notification-API/index.html",
      package_dir={'NotificationFramework':'.'},
      packages=['NotificationFramework'],
      #py_modules = ['mod1', 'pkg.mod2'],
      long_description = long_description,
     )
