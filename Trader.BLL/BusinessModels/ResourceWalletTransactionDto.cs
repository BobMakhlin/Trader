namespace Trader.BLL.BusinessModels
{
    public class ResourceWalletTransactionDto
    {
        public int ResourceWalletTransactionId { get; set; }

        public int GameId { get; set; }

        public int ResourceId { get; set; }
        public string ResourceName { get; set; }

        public double ResourceCount { get; set; }
    }
}
