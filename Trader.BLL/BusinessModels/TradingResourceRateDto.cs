namespace Trader.BLL.BusinessModels
{
    public class TradingResourceRateDto
    {
        public int TradingResourceRateId { get; set; }

        public int TradingResourceId { get; set; }
        // nvarchar(64), not null.
        public string TradingResourceName { get; set; }

        public int GameId { get; set; }

        public double TradingResourcePrice { get; set; }

        public int MoveNumber { get; set; }
    }
}
