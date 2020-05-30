using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.DAL.DbModels
{
    public class ResourceRate
    {
        public int ResourceRateId { get; set; }

        public int ResourceId { get; set; }
        public virtual Resource Resource { get; set; }

        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public decimal ResourcePrice { get; set; }

        public int MoveNumber { get; set; }
    }
}
