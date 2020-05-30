using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.BLL.BusinessModels
{
    public class ResourceDto
    {
        public int ResourceId { get; set; }

        [Required]
        [StringLength(64)]
        public string ResourceName { get; set; }
    }
}
