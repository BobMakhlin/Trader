namespace Trader.DAL.DbModels
{
    public class TradingResourceRate
    {
        public int TradingResourceRateId { get; set; }

        public int TradingResourceResourceId { get; set; }
        public virtual TradingResource TradingResource { get; set; }

        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public double TradingResourcePrice { get; set; }

        public int MoveNumber { get; set; }
    }
}
