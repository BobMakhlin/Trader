namespace Trader.BLL.BusinessModels
{
    public class TradingResourceDto
    {
        public int ResourceId { get; set; }

        public string ResourceName { get; set; }

        public double MinPrice { get; set; }

        public double MaxPrice { get; set; }
    }
}
