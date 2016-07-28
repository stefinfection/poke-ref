using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokeRef_v1;
using System.Collections.Generic;

namespace PokeTests_v1
{
    [TestClass]
    public class PokeUnitTests
    {
        // Test lesser-known valid pokemon
        [TestMethod]
        public void ValidPokeStats1()
        {
            Pokemon mon = new Pokemon("caterpie");
            mon.FillPokeStats();
            Assert.AreEqual(10, mon.id);
            Assert.AreEqual(3, mon.height);
            List<string> expTypes = new List<string>();
            expTypes.Add("bug");
            CollectionAssert.AreEqual(expTypes, mon.types);
        }

        // Test popular valid pokemon
        [TestMethod]
        public void ValidPokeStats2()
        {
            Pokemon mon = new Pokemon("pikachu");
            mon.FillPokeStats();
            Assert.AreEqual(25, mon.id);
            Assert.AreEqual(4, mon.height);
            List<string> expTypes = new List<string>();
            expTypes.Add("electric");
            CollectionAssert.AreEqual(expTypes, mon.types);
        }

        // Test uppercase letters
        [TestMethod]
        public void ValidPokeStats3()
        {
            Pokemon mon = new Pokemon("CHARiZARd");
            mon.FillPokeStats();
            Assert.AreEqual(6, mon.id);
            Assert.AreEqual(17, mon.height);
            List<string> expTypes = new List<string>();
            expTypes.Add("flying");
            expTypes.Add("fire");
            CollectionAssert.AreEqual(expTypes, mon.types);
        }

        // Test invalid parameter
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void InvalidPokeStats1()
        {
            Pokemon mon = new Pokemon("x");
            mon.FillPokeStats();
        }

        // Test an empty string
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void InvalidPokeStats2()
        {
            Pokemon mon = new Pokemon("");
            mon.FillPokeStats();
        }

        // Test a typo
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void InvalidPokeStats3()
        {
            Pokemon mon = new Pokemon("kangler");
            mon.FillPokeStats();
        }

        [TestMethod]
        public void ValidEvoStats1()
        {
            Pokemon mon = new Pokemon("abra");
            mon.FillPokeStats();
            mon.FillEvoStats();
            List<string> expEvo = new List<string>();
            expEvo.Add("abra");
            expEvo.Add("kadabra");
            expEvo.Add("alakazam");
            CollectionAssert.AreEqual(expEvo, mon.evoChain);
        }
    }
}
