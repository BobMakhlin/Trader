using System;

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
