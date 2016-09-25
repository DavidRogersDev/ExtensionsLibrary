using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace KesselRun.Extensions.Tests
{
    [TestClass]
    public class CollectionExtensionsTests
    {
        [TestMethod]
        public void AddOrUpdateAddsItemWhereNoneExisted()
        {
            //  Arrange
            var testDictionary = GetTestDictionary();
            //  Act
            testDictionary.AddOrUpdate("Item3", "I got added");

            //  Assert                        
            Assert.AreEqual(testDictionary["Item3"], "I got added");
        }

        [TestMethod]
        public void AddOrUpdateAddsItemWhereItemExisted()
        {
            //  Arrange
            var testDictionary = GetTestDictionary();

            //  Act
            testDictionary.AddOrUpdate("Item2", "I got added");

            //  Assert                        
            Assert.AreEqual(testDictionary["Item2"], "I got added");
        }

        private Dictionary<string, string> GetTestDictionary()
        {
            Dictionary<string, string> testDictionary = new Dictionary<string, string>
            {
                {"Item1", "Value2"},
                {"Item2", "Value2"}
            };
            return testDictionary;
        }
    }
}

