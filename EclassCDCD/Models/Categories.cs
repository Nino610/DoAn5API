using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclassCDCD.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Plans = new HashSet<Plans>();
            Questions = new HashSet<Questions>();
        }

        [Key]
        [Column("CateID")]
        [StringLength(10)]
        public string CateId { get; set; }
        [Required]
        [StringLength(250)]
        public string CateName { get; set; }

        [InverseProperty("Cate")]
        public virtual ICollection<Plans> Plans { get; set; }
        [InverseProperty("Cate")]
        public virtual ICollection<Questions> Questions { get; set; }
    }
}
