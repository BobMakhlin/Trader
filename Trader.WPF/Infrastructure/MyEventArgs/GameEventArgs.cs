using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.WPF.Infrastructure.MyEventArgs
{
    class GameEventArgs : EventArgs
    {
        public GameEventArgs(int gameId)
        {
            GameId = gameId;
        }

        public int GameId { get; private set; }

        public override string ToString()
        {
            return $"GameId: {GameId}";
        }
    }
}
