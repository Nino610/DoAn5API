using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclassCDCD.Models
{
    public partial class Types
    {
        public Types()
        {
            Questions = new HashSet<Questions>();
        }

        [Key]
        [Column("TypeID")]
        [StringLength(10)]
        public string TypeId { get; set; }
        [Required]
        [StringLength(250)]
        public string TypeName { get; set; }

        [InverseProperty("Type")]
        public virtual ICollection<Questions> Questions { get; set; }
    }
}
