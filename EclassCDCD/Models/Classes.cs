using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclassCDCD.Models
{
    public partial class Classes
    {
        public Classes()
        {
            Plans = new HashSet<Plans>();
            Students = new HashSet<Students>();
        }

        [Key]
        [Column("ClassID")]
        [StringLength(50)]
        public string ClassId { get; set; }
        [Column("FacultyID")]
        [StringLength(10)]
        public string FacultyId { get; set; }
        public int? Total { get; set; }
        [StringLength(4)]
        public string Year { get; set; }
        [StringLength(11)]
        public string Period { get; set; }
        [Column("EmployeeID")]
        [StringLength(10)]
        public string EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(Employees.Classes))]
        public virtual Employees Employee { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<Plans> Plans { get; set; }
        [InverseProperty("Class")]
        public virtual ICollection<Students> Students { get; set; }
    }
}
