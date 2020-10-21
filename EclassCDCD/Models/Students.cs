using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclassCDCD.Models
{
    public partial class Students
    {
        public Students()
        {
            Scores = new HashSet<Scores>();
        }

        [Key]
        [Column("StudentID")]
        [StringLength(10)]
        public string StudentId { get; set; }
        [Required]
        [Column("ClassID")]
        [StringLength(50)]
        public string ClassId { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
        public bool? Gender { get; set; }
        [StringLength(10)]
        public string Birthday { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(250)]
        public string Photo { get; set; }

        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(Classes.Students))]
        public virtual Classes Class { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Scores> Scores { get; set; }
    }
}
