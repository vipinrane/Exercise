using System;
using NLog;

namespace NlogTest
{

    public static class Logger
    {
        private static readonly NLog.Logger _logger; //NLog logger
        private const string _DEFAULTLOGGER = "CustomLogger";
 
        static Logger()
        {
            //_logger = LogManager.GetLogger(_DEFAULTLOGGER) ?? LogManager.GetCurrentClassLogger();
            _logger = LogManager.GetLogger(_DEFAULTLOGGER);
        }
 
        #region Public Methods
        /// <summary>
        /// This method writes the Debug information to trace file
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Debug(String message)
        {
            if (!_logger.IsDebugEnabled) return;
            _logger.Debug(message);
        }
 
        /// <summary>
        /// This method writes the Information to trace file
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Info(String message)
        {
            if (!_logger.IsInfoEnabled) return;
            _logger.Info(message);
        }
 
        /// <summary>
        /// This method writes the Warning information to trace file
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Warn(String message)
        {
            if (!_logger.IsWarnEnabled) return;
            _logger.Warn(message);
        }
 
        /// <summary>
        /// This method writes the Error Information to trace file
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="exception">The exception.</param>
        public static void Error(String error, Exception exception)
        {
            if (!_logger.IsErrorEnabled) return;
            _logger.ErrorException(error, exception);
        }
 
        /// <summary>
        /// This method writes the Fatal exception information to trace target
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Fatal(String message)
        {
            if (!_logger.IsFatalEnabled) return;
            _logger.Warn(message);
        }
 
        /// <summary>
        /// This method writes the trace information to trace target
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Trace(String message)
        {
            if (!_logger.IsTraceEnabled) return;
            _logger.Trace(message);
        }
 
        #endregion
 
    }
}