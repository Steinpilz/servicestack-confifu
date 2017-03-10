using Microsoft.Extensions.Logging;
using ServiceStack.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceStack.Confifu.Logging
{
    public class LoggerFactory : ILogFactory
    {
        readonly Func<ILoggerFactory> loggerFactory;

        public LoggerFactory(Func<ILoggerFactory> loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        public ILog GetLogger(string typeName)
        {
            return new LogAdaptor(loggerFactory().CreateLogger(typeName));
        }

        public ILog GetLogger(Type type)
        {
            return new LogAdaptor(loggerFactory().CreateLogger(type));
        }
    }
}
