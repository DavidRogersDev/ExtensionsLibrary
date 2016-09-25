using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KesselRun.Extensions.Tests
{
    [TestClass]
    public class ExceptionExtensionsTests
    {
        [TestMethod]
        public void MessagesAsDictionaryIncludesMessageOfInnerException()
        {
            Dictionary<Exception, string> aggregate = new Dictionary<Exception, string>(2);

            try
            {
                ThrowException();
            }
            catch (Exception exception)
            {
                exception.MessagesAsDictionary(aggregate);
                Assert.AreEqual(aggregate.First().Value, "This is the top level exception");
            }
        }

        [TestMethod]
        public void MessagesAsDictionaryIncludesMessageOfInnerExceptionAlt()
        {
            Dictionary<Exception, string> aggregate = new Dictionary<Exception, string>(2);

            try
            {
                ThrowException();
            }
            catch (Exception exception)
            {
                try
                {
                    WrapException(exception);
                }
                catch (Exception newException)
                {
                    newException.MessagesAsDictionary(aggregate);
                    Assert.AreEqual(3, aggregate.Count);
                }
            }
        }

        public void ThrowException()
        {
            throw new Exception("This is the top level exception",new Exception("This is the inner excpetion"));
        }

        public void WrapException(Exception exception)
        {
            throw new Exception("This is the now the top level exception", exception);
        }
    }
}
