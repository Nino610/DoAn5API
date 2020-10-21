using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclassCDCD.Models
{
    public partial class Faculties
    {
        public Faculties()
        {
            Departments = new HashSet<Departments>();
            Permissions = new HashSet<Permissions>();
        }

        [Key]
        [Column("FacultyID")]
        [StringLength(10)]
        public string FacultyId { get; set; }
        [Required]
        [StringLength(200)]
        public string FacultyName { get; set; }

        [InverseProperty("Faculty")]
        public virtual ICollection<Departments> Departments { get; set; }
        [InverseProperty("Faculty")]
        public virtual ICollection<Permissions> Permissions { get; set; }
    }
}
