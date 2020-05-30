using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.BLL.BusinessModels
{
    public class GameDto
    {
        public int GameId { get; set; }

        // nvarchar(64), not null.
        public string GameName { get; set; }

        public DateTime Date { get; set; }

        public int CurrentMoveNumber { get; set; }
    }
}
