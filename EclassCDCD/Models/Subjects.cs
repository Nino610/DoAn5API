using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclassCDCD.Models
{
    public partial class Subjects
    {
        public Subjects()
        {
            Plans = new HashSet<Plans>();
            Scores = new HashSet<Scores>();
        }

        [Key]
        [Column("SubjectID")]
        [StringLength(10)]
        public string SubjectId { get; set; }
        [Required]
        [StringLength(100)]
        public string SubjectName { get; set; }
        public int Credit { get; set; }
        [Required]
        [Column("DepartmentID")]
        [StringLength(10)]
        public string DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty(nameof(Departments.Subjects))]
        public virtual Departments Department { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Plans> Plans { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Scores> Scores { get; set; }
    }
}
