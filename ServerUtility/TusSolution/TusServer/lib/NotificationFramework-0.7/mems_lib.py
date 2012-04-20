# -*- coding: iso-8859-1 -*-
"""
The code in this module is borrowed from the ZODB Programmer's Guide.

It redefines the python2.1's builtin functions ``issubclass()`` and
``isinstance()`` so that they work properly with extension classes.
Versions 2.2 and up of python do not have the problem so the module's
functions ``issubclass()`` and ``isinstance()`` are the default ones,
unchanged.

**Note**:

   This is not indicated in the ZODB Programming Guide anymore,
   probably because it's of no use for python2.2 and up. Last time I checked I
   was able to find the page I'm referring to at:
   http://cvs.zope.org/StandaloneZODB/Doc/guide/zodb/node18.html?rev=1.2 but
   this URL might be unavailable at the time you're reading this.


So in case it is not available, here is a copy of the comments A.M. Kuchling
wrote around the code (speaking of **python 2.1**):

    Python's built-in functions isinstance() and issubclass don't work on
    ExtensionClass instances, for much the same reason that __cmp__ is never
    called; in some bits of the Python core code, branches are taken only if an
    object is of the InstanceType type, and this can never be true for an
    ExtensionClass instance. Python 2.1 tried to fix this, and changed these
    functions slightly in an effort to make them work for ExtensionClasses;
    unfortunately, the changes didn't work.
    
    The solution is to use customized versions of these functions that handle
    ExtensionClasses specially and fall back to the built-in version
    otherwise. Here are the versions we've written at the MEMS Exchange:

      [code here for issubclass() and isinstance(), see below]
    
    I'd recommend putting these functions in a module that always gets
    imported. The convention on my work project is to put them in
    mems/lib/base.py, which contains various fundamental classes and functions
    for our system, and access them like this:
    
    from mems.lib import base
    ...
    if base.isinstance(object, Class): ...
    
    Don't insert the modified functions into Python's __builtin__ module, or
    import just the isinstance() and issubclass functions. If you consistently
    use base.isinstance(), then forgetting to import the base module will
    result in a NameError exception. In the case of a forgotten import,
    calling the functions directly would use Python's built-in versions,
    leading to subtle bugs that might not be noticed for some time.


The quoted text above, and most of the code included in this module, is
extracted from the ZODB Programmer's Guide release 0.03, February 8, 2002 by
A.M. Kuchling, (c) 2002 and distributed under the GNU Free Documentation
License, Version 1.1 or any later.
"""
import sys
if sys.version_info[:2]>=(2,2):
  issubclass=issubclass
  isinstance=isinstance
else:
  from types import ClassType,TypeType
  import __builtin__
  
  # The built-in 'isinstance()' and 'issubclass()' won't work on
  # ExtensionClasses, so you have to use the versions supplied here.
  # (But those versions work fine on regular instances and classes too,
  # so you should *always* use them.)
  
  def issubclass (class1, class2):
      """A version of 'issubclass' that works with extension classes
      as well as regular Python classes.
      """
  
      # Both class objects are regular Python classes, so use the
      # built-in 'issubclass()'.
      if type(class1) is ClassType and type(class2) is ClassType:
          return __builtin__.issubclass(class1, class2)
  
      # Both so-called class objects have a '__bases__' attribute: ie.,
      # they aren't regular Python classes, but they sure look like them.
      # Assume they are extension classes and reimplement what the builtin
      # 'issubclass()' does behind the scenes.
      elif hasattr(class1, '__bases__') and hasattr(class2, '__bases__'):
          # XXX it appears that "ec.__class__ is type(ec)" for an
          # extension class 'ec': could we/should we use this as an
          # additional check for extension classes?
  
          # Breadth-first traversal of class1's superclass tree.  Order
          # doesn't matter because we're just looking for a "yes/no"
          # answer from the tree; if we were trying to resolve a name,
          # order would be important!
          stack = [class1]
          while stack:
              if stack[0] is class2:
                  return 1
              stack.extend(list(stack[0].__bases__))
              del stack[0]
          else:
              return 0
  
      # Not a regular class, not an extension class: blow up for consistency
      # with builtin 'issubclass()"
      else:
          raise TypeError, "arguments must be class or ExtensionClass objects"
  
  # issubclass ()
  
  def isinstance (object, klass):
      """A version of 'isinstance' that works with extension classes
      as well as regular Python classes."""
      if type(klass) is TypeType:
          return __builtin__.isinstance(object, klass)
      elif hasattr(object, '__class__'):
          return issubclass(object.__class__, klass)
      else:
          return 0
