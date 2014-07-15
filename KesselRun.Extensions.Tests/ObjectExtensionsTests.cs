using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Newtonsoft.Json.Linq;

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
        [ExpectedException(typeof (NotSupportedException), "The object cannot be a value type.")]
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
        [ExpectedException(typeof (NotSupportedException), "The object cannot be a value type.")]
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
        [ExpectedException(typeof (NotSupportedException), "The object cannot be a value type.")]
        public void VariableWhichIsNullableTypeCallsIsNullThrowsException()
        {
            //  Arrange
            Nullable<int> number = 5;

            //  Act
            var result = number.IsNull();

            //  Assert                        
            Assert.Fail();
        }

        [TestMethod]
        public void ToJsonStringSerializesObjectToJson()
        {
            //  Arrange
            var nowDescription = typeof (Array).GetDescription();

            //  Act
            var result = nowDescription.ToJsonString();

            //  Assert                        
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ToJsonStringSerializesNullToString()
        {
            //  Arrange
            Array nullValue = null;

            //  Act
            // ReSharper disable once ExpressionIsAlwaysNull
            var result = nullValue.ToJsonString();

            //  Assert                        
            Assert.AreEqual("null", result);
        }

        [TestMethod]
        public void GetJsonTypeDescriptionReturnsJsonObjectForTypeDescription()
        {
            //  Arrange
            var stringArray = new string[] {"hello", "all"};

            //  Act
            var jsonTypeDescription = stringArray.GetJsonTypeDescription();
            var jObject = JObject.Parse(jsonTypeDescription);



            //  Assert                        
            Assert.AreEqual("System.String[]", jObject["FullName"]);
            Assert.AreEqual(
                "System.String[], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
                jObject["AssemblyQualifiedName"]);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void GetJsonTypeDescriptionThrowsExceptionWhereArgumentIsNull()
        {
            //  Arrange
            Array stringArray = null;

            //  Act
            // ReSharper disable once ExpressionIsAlwaysNull
            var jsonTypeDescription = stringArray.GetJsonTypeDescription();

            //  Assert                        
            Assert.Fail();
        }
    }
}

