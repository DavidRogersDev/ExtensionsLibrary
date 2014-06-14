using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KesselRun.Extensions.Tests
{
    [TestClass]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void VariableNullCallsIsNullReturnsTrue()
        {
            //  Arrange
            GopherStyleUriParser nullObject = null;

            //  Act
            var result = nullObject.IsNull();

            //  Assert                        
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VariableWithValueCallsIsNullReturnsFalse()
        {
            //  Arrange
            var array = new string[10];
            //  Act
            var result = array.IsNull();

            //  Assert                        
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException), "The object cannot be a value type.")]
        public void VariableWhichIsValueTypeCallsIsNullThrowsException()
        {
            //  Arrange
            var number = 5;
            
            //  Act
            var result = number.IsNull();

            //  Assert                        
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException), "The object cannot be a value type.")]
        public void VariableWhichIsGuidCallsIsNullThrowsExceptiosn()
        {
            //  Arrange
            var guid = Guid.NewGuid();
            
            //  Act
            var result = guid.IsNull();

            //  Assert                        
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException), "The object cannot be a value type.")]
        public void VariableWhichIsNullableTypeCallsIsNullThrowsException()
        {
            //  Arrange
            Nullable<int> number = 5;
            
            //  Act
            var result = number.IsNull();

            //  Assert                        
            Assert.Fail();
        }


    }
}
