using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclassCDCD.Models
{
    public partial class Permissions
    {
        [Key]
        [Column("PID")]
        public int Pid { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [Column("FacultyID")]
        [StringLength(10)]
        public string FacultyId { get; set; }

        [ForeignKey(nameof(FacultyId))]
        [InverseProperty(nameof(Faculties.Permissions))]
        public virtual Faculties Faculty { get; set; }
        [ForeignKey(nameof(Username))]
        [InverseProperty(nameof(Accounts.Permissions))]
        public virtual Accounts UsernameNavigation { get; set; }
    }
}
