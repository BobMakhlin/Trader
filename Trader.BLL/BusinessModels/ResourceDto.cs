namespace Trader.BLL.BusinessModels
{
    public class ResourceDto
    {
        public int ResourceId { get; set; }

        // nvarchar(64), not null.
        public string ResourceName { get; set; }
    }
}
