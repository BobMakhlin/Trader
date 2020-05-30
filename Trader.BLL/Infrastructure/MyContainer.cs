using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Trader.BLL.Infrastructure
{
    public static class MyContainer
    {
        #region Private Definitions
        static IContainer m_container;
        #endregion

        static MyContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new RegistrationModule());
            m_container = builder.Build();
        }

        public static TService Resolve<TService>()
        {
            return m_container.Resolve<TService>();
        }
    }
}
