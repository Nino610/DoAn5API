using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclassCDCD.Models
{
    public partial class Plans
    {
        public Plans()
        {
            Answers = new HashSet<Answers>();
        }

        [Key]
        [Column("PlanID")]
        public int PlanId { get; set; }
        [Required]
        [Column("ClassID")]
        [StringLength(50)]
        public string ClassId { get; set; }
        [Required]
        [Column("SubjectID")]
        [StringLength(10)]
        public string SubjectId { get; set; }
        [Required]
        [Column("EmployeeID")]
        [StringLength(10)]
        public string EmployeeId { get; set; }
        [Required]
        [Column("CateID")]
        [StringLength(10)]
        public string CateId { get; set; }
        [Required]
        [StringLength(11)]
        public string Year { get; set; }
        public int Semester { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EndTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActiveDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsCheck { get; set; }
        public bool? HasMark { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MarkDate { get; set; }
        [StringLength(50)]
        public string MarkCode { get; set; }
        [StringLength(50)]
        public string Username { get; set; }

        [ForeignKey(nameof(CateId))]
        [InverseProperty(nameof(Categories.Plans))]
        public virtual Categories Cate { get; set; }
        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(Classes.Plans))]
        public virtual Classes Class { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty(nameof(Employees.Plans))]
        public virtual Employees Employee { get; set; }
        [ForeignKey(nameof(SubjectId))]
        [InverseProperty(nameof(Subjects.Plans))]
        public virtual Subjects Subject { get; set; }
        [InverseProperty("Plan")]
        public virtual ICollection<Answers> Answers { get; set; }
    }
}
