using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
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
                m_logger.Trace(ex, message);
            }
        }
        public void Debug(string message, Exception ex = null)
        {
            lock (m_locker)
            {
                m_logger.Debug(ex, message);
            }
        }
        public void Info (string message, Exception ex = null)
        {
            lock (m_locker)
            {
                m_logger.Info(ex, message);
            }
        }
        public void Warn (string message, Exception ex = null)
        {
            lock (m_locker)
            {
                m_logger.Warn(ex, message);
            }
        }
        public void Error(string message, Exception ex = null)
        {
            lock (m_locker)
            {
                m_logger.Error(ex, message);
            }
        }
        public void Fatal(string message, Exception ex = null)
        {
            lock (m_locker)
            {
                m_logger.Fatal(ex, message);
            }
        }
        #endregion
    }
}
