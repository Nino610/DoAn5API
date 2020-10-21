using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclassCDCD.Models
{
    public partial class UserRequests
    {
        [Key]
        [Column("RequestID")]
        public int RequestId { get; set; }
        public Guid Code { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RequestTime { get; set; }
        public bool IsHandled { get; set; }
        [Required]
        public bool? IsFromStudent { get; set; }
    }
}
