using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokeRef_v1;


namespace PokeTests_v1
{
    [TestClass]
    public class PokeUnitTests
    {
        // Setup a pokemon to test
        public void ValidPokeGet1()
        {
            // Construct new pokemon object in isolation
            Pokemon mon = new Pokemon("caterpie");

            // Fill in criteria using GETs
            mon.FillPokeStats();
            mon.FillEvoStats();
            IDPoke1(mon);
            HeightPoke1(mon);
        }

        // Test each property in isolation
        [TestMethod]
        public void IDPoke1(Pokemon mon)
        {
            Assert.AreEqual(10, mon.id);
        }

        [TestMethod]
        public void HeightPoke1(Pokemon mon)
        {
            Assert.AreEqual(3, mon.height);
        }

        [TestMethod]
        public void SpeciesPoke1()
        {

        }

        [TestMethod]
        public void CharacteristicPoke1()
        {

        }

        [TestMethod]
        public void TypesPoke1()
        {

        }

        [TestMethod]
        public void MovesPoke1()
        {

        }

        [TestMethod]
        public void SpritesPoke1()
        {

        }

        // Tests a GET request to the pokemon path w/ invalid criteria
        [TestMethod]
        public void InvalidPokeGet()
        {
            // Ensure error caught and spelling suggestion displayed?
        }


    }
}
