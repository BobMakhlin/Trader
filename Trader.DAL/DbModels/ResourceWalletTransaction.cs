namespace Trader.DAL.DbModels
{
    public class ResourceWalletTransaction
    {
        public int ResourceWalletTransactionId { get; set; }

        public int GameId { get; set; }
        public int ResourceId { get; set; }
        public virtual ResourceWallet ResourceWallet { get; set; }

        public double ResourceCount { get; set; }
    }
}
