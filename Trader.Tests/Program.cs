using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.BLL.BusinessModels;
using Trader.BLL.Infrastructure;
using Trader.BLL.Services.Common;
using Trader.BLL.Services.Custom;
using Trader.DAL.DbModels;
using Trader.DAL.Repositories;

namespace Trader.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = MyContainer.Resolve<IGenericService<Resource, ResourceDto, int>>();
            
        }
    }
}
