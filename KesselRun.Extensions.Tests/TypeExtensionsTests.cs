using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KesselRun.Extensions.Tests
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void GetDescriptionReturnsCorrectAssemblyQualifiedNameForArrayType()
        {
            //  Arrange
            var type = typeof(Array);

            //  Act
            var typeDescription = type.GetDescription();

            //  Assert         
            Assert.AreEqual(typeDescription.AssemblyQualifiedName, "System.Array, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
        }

        [TestMethod]
        public void GetDescriptionReturnsCorrectFullNameForArrayType()
        {
            //  Arrange
            var type = typeof(Array);

            //  Act
            var typeDescription = type.GetDescription();

            //  Assert         
            Assert.AreEqual(typeDescription.FullName, "System.Array");
        }
    }
}
