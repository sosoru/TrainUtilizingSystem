using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public static class EqualTest
    {

        public static void TestEqualNotDefault<T>(T A, T B, Func<T, T, bool> f)
        {
            Assert.AreEqual(f(default(T), default(T)), true);
            Assert.AreEqual(f(A, default(T)), false);
            Assert.AreEqual(f(default(T), B), false);
            Assert.AreEqual(f(A, B), true);
        }

        public static void TestNotEqualNotDefault<T>(T A, T B, Func<T, T, bool> f)
        {
            Assert.AreEqual(f(default(T), default(T)), true);
            Assert.AreEqual(f(A, default(T)), false);
            Assert.AreEqual(f(default(T), B), false);
            Assert.AreEqual(f(A, B), false);
        }

        public static void TestEqualNotDefault<T>(T other, Func<T, bool> f)
        {
            Assert.AreEqual(f(default(T)), false);
            Assert.AreEqual(f(other), true);
        }

        public static void TestNotEqualNotDefault<T>(T other, Func<T, bool> f)
        {
            Assert.AreEqual(f(default(T)), false);
            Assert.AreEqual(f(other), false);
        }
    }
}
