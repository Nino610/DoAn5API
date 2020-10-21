using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclassCDCD.Models
{
    public partial class Employees
    {
        public Employees()
        {
            Classes = new HashSet<Classes>();
            Plans = new HashSet<Plans>();
        }

        [Key]
        [Column("EmployeeID")]
        [StringLength(10)]
        public string EmployeeId { get; set; }
        [Required]
        [Column("DepartmentID")]
        [StringLength(10)]
        public string DepartmentId { get; set; }
        [Required]
        [StringLength(150)]
        public string FullName { get; set; }
        public bool Gender { get; set; }
        [Required]
        [StringLength(10)]
        public string Birthday { get; set; }
        [Required]
        [StringLength(250)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(250)]
        public string Photo { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty(nameof(Departments.Employees))]
        public virtual Departments Department { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<Classes> Classes { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<Plans> Plans { get; set; }
    }
}
