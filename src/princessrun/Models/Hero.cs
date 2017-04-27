using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace princessrun.Models
{
    [Table("Heroes")]
    public class Hero
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Avatar { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
