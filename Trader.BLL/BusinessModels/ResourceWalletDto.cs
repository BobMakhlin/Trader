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
