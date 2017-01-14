using Microsoft.Extensions.Logging;
using System;
using RevolutionLogger;

namespace EFRepository
{
    public class MyLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new MyLogger();
        }

        public void Dispose()
        { }

        private class MyLogger : ILogger
        {
            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                LoggingLevel ll = LoggingLevel.Info;
                switch (logLevel)
                {
                    case LogLevel.None:
                    case LogLevel.Trace:
                        ll = LoggingLevel.Info;
                        break;
                    case LogLevel.Debug:
                        ll = LoggingLevel.Debug;
                        break;
                    case LogLevel.Error:
                        ll = LoggingLevel.Error;
                        break;
                    case LogLevel.Critical:
                        ll = LoggingLevel.Fatal;
                        break;
                }
                Logger.Log(ll, formatter(state, exception));
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }
    }
}