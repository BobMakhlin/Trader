using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.Logging.Helpers
{
    public class LoggingHelper
    {
        #region Fields
        static readonly LoggingHelper m_instance;

        Logger m_logger;
        object m_locker;
        #endregion

        #region Constructors
        static LoggingHelper()
        {
            m_instance = new LoggingHelper();
        }
        private LoggingHelper()
        {
            m_logger = LogManager.GetCurrentClassLogger();
            m_locker = new object();
        }
        #endregion

        #region Properties
        public static LoggingHelper Instance => m_instance;
        #endregion

        #region Methods
        public void Trace(string message, Exception ex = null)
        {
            lock (m_locker)
            {
                m_logger.Trace(message, ex, null);
            }
        }
        public void Debug(string message, Exception ex = null)
        {
            lock (m_locker)
            {
                m_logger.Debug(message, ex, null);
            }
        }
        public void Info (string message, Exception ex = null)
        {
            lock (m_locker)
            {
                m_logger.Info(message, ex, null);
            }
        }
        public void Warn (string message, Exception ex = null)
        {
            lock (m_locker)
            {
                m_logger.Warn(message, ex, null);
            }
        }
        public void Error(string message, Exception ex = null)
        {
            lock (m_locker)
            {
                m_logger.Error(message, ex, null);
            }
        }
        public void Fatal(string message, Exception ex = null)
        {
            lock (m_locker)
            {
                m_logger.Fatal(message, ex, null);
            }
        }
        #endregion
    }
}
