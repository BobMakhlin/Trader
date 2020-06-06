using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.DAL.DbModels
{
    public class Log
    {
        public int LogId { get; set; }

        [Required]
        [StringLength(128)]
        public string LogMessage { get; set; }

        [Required]
        [StringLength(24)]
        public string LogLevel { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LogDate { get; set; }

        [StringLength(256)]
        public string LogException { get; set; }
    }
}
