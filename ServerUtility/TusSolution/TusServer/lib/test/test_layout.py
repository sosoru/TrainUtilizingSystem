import unittest

import layout.models as layout
import layout.detector as detector
import layout.train as train

class test_layouts(object):
    
    def __init__(self):
        pass

    def createTestjuncs(self, num):
        junks = []
        for i in range(0, num):
            junks.append(layout.Junction())

        return junks

    def to_sections(self, tuples):
        sects = []

        for t in tuples:
            sects.append(layout.Section(t))

        return sects

    def layout_straight(self, num):
        j = self.createTestjuncs(num)
        sects_array = [(j[n], j[n+1]) for n in range(0, num-1)]
        sects = self.to_sections(sects_array)

        lout = layout.Layout(sects, j)
        return lout

    def layout_loop(self, num):
        j = self.createTestjuncs(num)
        sects_array = [(j[n-1], j[n]) for n in range(0, num)]
        sects = self.to_sections(sects_array)

        lout = layout.Layout(sects, j)
        return lout

    def layout_station_switch(self):
        j= self.createTestjuncs(7)
        sects_array = [(j[0], j[1]), (j[0], j[2]),
                       (j[1], j[3]), (j[2], j[4]),
                       (j[3], j[5]), (j[4], j[5]),
                       (j[5], j[6]), (j[6], j[0])]
        sects = self.to_sections(sects_array)

        lout = layout.Layout(sects, j)
        return lout

    def layout_straight_one_train(self, num):
        lout = self.layout_straight(num)

        lout.bound_sections[0].bound_detector.on_detected()

        return lout

    def layout_loop_one_Train(self, num):
        lout = self.layout_loop(num)
        
        lout.bound_sections[0].bound_detector.on_detected()

        return lout



class TestLayout(unittest.TestCase):

    def setUp(self):
        self.__factory = test_layouts()

    def create_straight_layout(self, num):
        j = self.__factory.createTestjuncs(num+1)
        sects_array = []
        for i in range(0, num):
            sects_array.append((j[i], j[i+1]))

        sects = self.__factory.to_sections(sects_array)

        return layout.Layout(sects, j)

    def test_search_trains_len(self):
        lout = self.create_straight_layout(6)

        lout.bound_sections[2].bound_detector.on_detected()   #conbined sections
        lout.bound_sections[3].bound_detector.on_detected()

        lout.bound_sections[5].bound_detector.on_detected()   #one sections
        
        trs = lout.get_trains()

        self.assertEqual(len(trs), 2)

class TestSignal(unittest.TestCase):

    def setUp(self):
        pass

    def test_signalset(self):
        j1 = layout.Junction()
        j2 = layout.Junction()
        sig = layout.Signal()

        sect = layout.Section((j1, j2), signal)

class TestLayoutDetector(unittest.TestCase):
    
    def setUp(self):
        pass

    def test_detected_left(self):
        dc = detector.Detector(lambda sender: self.assertEqual(sender, dc), lambda sender : self.assertEqual(sender, dc))
        
        dc.on_detected()        
        dc.on_left()

class TestLayoutSection(unittest.TestCase):

    def setUp(self):
        pass

    def test_allocate_detector(self):
        j = (layout.Junction(), layout.Junction())

        sect = layout.Section(j)
        d = sect.bound_detector

        d.on_detected()
        self.assertEqual(sect.is_exists_train, True)

        d.on_left()
        self.assertEqual(sect.is_exists_train, False)

class TestTrain(unittest.TestCase):

    def setUp(self):
        self.__factory = test_layouts()

    def _train_step(self, lout):
        trs = lout.get_trains()

        for t in trs:
            t.run()

    def test_train_run(self):
        lout = self.__factory.layout_straight_one_train(4)

        tr = lout.get_trains()[0]

        tr.run()

        tr = lout.get_trains()[0]

        self.assertEqual(tr.current_sections[0], lout.bound_sections[1])

    def test_train_run_loop(self):
        lout = self.__factory.layout_loop_one_Train(4)

        for i in range(0,4):
            self._train_step(lout)

        tr = lout.get_trains()[0]
        self.assertEqual(tr.current_sections[0], lout.bound_sections[0])

    def test_train_run_outofrail(self):
        lout = self.__factory.layout_straight_one_train(4)
        
        try:
            for i in range(0,5):
                self._train_step(lout)
        except train.OutOfRailError as e:
            return # success
        
        self.fail()

class TestRouteSearch(unittest.TestCase):

    def setUp(self):    
        self.__factory = test_layouts()

    def search_test(self, layout, sects, route_expected):
        actual = layout.get_route(sects)
        expected = route_expected

        self.assertEqual(expected.sections, actual.sections)

    def testStraightcase(self):
        junks = self.__factory.createTestjuncs(3)
        junc_array = [(junks[0], junks[1]), (junks[1], junks[2])]
        sects = self.__factory.to_sections(junc_array)
        lout = layout.Layout(sects, junks)
        
        self.search_test(lout, sects, layout.Route(sects))
        

    def testLoop(self):
        j = self.__factory.createTestjuncs(4)
        junc_array = [(j[0], j[1]), (j[1], j[2]), (j[2], j[3]), (j[3], j[0])]
        sects = self.__factory.to_sections(junc_array)
        
        lout = layout.Layout(sects, j)

        expected = [sects[0], sects[1], sects[2], sects[3]]
        self.search_test(lout, [sects[0], sects[1], sects[3]], layout.Route(expected))
        
    def testRouteFollowing(self):
        lout = self.__factory.layout_station_switch()
        sects = lout.bound_sections
        sects[7].bound_detector.on_detected()

        route = lout.get_route([sects[7], sects[2], sects[6]])
        
        tr = lout.get_trains()[0]
        tr.Route = route

        for i in range(0, 4):
            tr.run()

        tr = lout.get_trains()[0]
        self.assertEqual(tr.current_sections[0], sects[6]) 
