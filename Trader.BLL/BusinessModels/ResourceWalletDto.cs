using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.BLL.BusinessModels
{
    public class ResourceWalletDto
    {
        public int GameId { get; set; }

        public int ResourceId { get; set; }

        // nvarchar(64), not null.
        public string ResourceName { get; set; }
    }
}
