using System;
using System.Collections.Generic;

namespace KesselRun.Extensions
{
    public static class ExceptionExtensions
    {
        public static void MessagesAsDictionary(this Exception exception, IDictionary<Exception, string> aggregate)
        {
            if (exception == null) throw new ArgumentNullException("exception");
            if (aggregate == null) throw new ArgumentNullException("aggregate");

            aggregate.Add(exception, exception.Message ?? string.Empty);

            if (exception.InnerException.IsNull())
                return;

            exception.InnerException.MessagesAsDictionary(aggregate);
        }
    }
}
