using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.WPF.Helpers
{
    sealed class ApplicationService
    {
        #region Private Definitions
        IEventAggregator m_eventAggregator;
        static readonly ApplicationService m_instance = new ApplicationService();
        #endregion

        private ApplicationService()
        {
        }

        public static ApplicationService Instance => m_instance;

        public IEventAggregator EventAggregator
        {
            get
            {
                if (m_eventAggregator == null)
                    m_eventAggregator = new EventAggregator();
                return m_eventAggregator;
            }
        }
    }
}
