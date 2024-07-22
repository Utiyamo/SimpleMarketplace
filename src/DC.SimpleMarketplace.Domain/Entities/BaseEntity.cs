using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SimpleMarketplace.Domain.Entities
{
    public class BaseEntity<T> 
    {
        [Key]
        public T ID { get; set; }
        public DateTime CreatedAte { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
