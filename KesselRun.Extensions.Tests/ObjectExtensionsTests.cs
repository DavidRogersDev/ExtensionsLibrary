using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using KesselRun.Extensions.Tests.Infrastructure;
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
        public void GetJsonTypeDescriptionThrowsExceptionWhereArgumentIsNull()
        {
            //  Arrange
            Array stringArray = null;

            //  Act
            //  Assert                        
            ExceptionAssert.Throws<ArgumentNullException>(() => stringArray.GetJsonTypeDescription());
        }
    }
}

