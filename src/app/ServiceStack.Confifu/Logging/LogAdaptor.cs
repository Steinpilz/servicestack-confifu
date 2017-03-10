using Microsoft.Extensions.Logging;
using ServiceStack.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceStack.Confifu.Logging
{
    public class LogAdaptor : ILog
    {
        readonly ILogger logger;

        public LogAdaptor(ILogger logger)
        {
            this.logger = logger;
        }

        public bool IsDebugEnabled => this.logger.IsEnabled(LogLevel.Debug);

        public void Debug(object message)
        {
            this.logger.LogDebug(message?.ToString());
        }

        public void Debug(object message, Exception exception)
        {
            this.logger.LogDebug(new EventId(), exception, message?.ToString());
        }

        public void DebugFormat(string format, params object[] args)
        {
            this.logger.LogDebug(format, args);
        }

        public void Error(object message)
        {
            this.logger.LogError(message?.ToString());
            
        }

        public void Error(object message, Exception exception)
        {
            this.logger.LogError(new EventId(), exception, message?.ToString());
        }

        public void ErrorFormat(string format, params object[] args)
        {
            this.logger.LogError(format, args);
        }

        public void Fatal(object message)
        {
            this.logger.LogCritical(message?.ToString());
            
        }

        public void Fatal(object message, Exception exception)
        {
            this.logger.LogCritical(new EventId(), exception, message?.ToString());
        }

        public void FatalFormat(string format, params object[] args)
        {
            this.logger.LogCritical(format, args);
        }

        public void Info(object message)
        {
            this.logger.LogInformation(message?.ToString());
            
        }

        public void Info(object message, Exception exception)
        {
            this.logger.LogInformation(new EventId(), exception, message?.ToString());
        }

        public void InfoFormat(string format, params object[] args)
        {
            this.logger.LogInformation(format, args);
        }

        public void Warn(object message)
        {
            this.logger.LogWarning(message?.ToString());
            
        }

        public void Warn(object message, Exception exception)
        {
            this.logger.LogWarning(new EventId(), exception, message?.ToString());
        }

        public void WarnFormat(string format, params object[] args)
        {
            this.logger.LogWarning(format, args);
        }
    }
}
