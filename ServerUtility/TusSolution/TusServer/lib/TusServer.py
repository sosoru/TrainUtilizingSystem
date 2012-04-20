
import unittest

import test.test_layout as layout

def __main__():
    suite = unittest.TestLoader().loadTestsFromModule(layout)
    
    unittest.TextTestRunner(verbosity=2).run(suite)

    print ('end of program')
    while True:
        pass

if __name__ == '__main__':
    __main__()


    