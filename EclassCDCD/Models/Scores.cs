using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclassCDCD.Models
{
    public partial class Scores
    {
        [Key]
        [Column("ScoreID")]
        public int ScoreId { get; set; }
        [Required]
        [Column("StudentID")]
        [StringLength(10)]
        public string StudentId { get; set; }
        [Required]
        [Column("SubjectID")]
        [StringLength(10)]
        public string SubjectId { get; set; }
        [Column(TypeName = "decimal(3, 1)")]
        public decimal FinalScore { get; set; }
        [StringLength(100)]
        public string Details { get; set; }
        [StringLength(20)]
        public string Note { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("PlanID")]
        public int? PlanId { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty(nameof(Students.Scores))]
        public virtual Students Student { get; set; }
        [ForeignKey(nameof(SubjectId))]
        [InverseProperty(nameof(Subjects.Scores))]
        public virtual Subjects Subject { get; set; }
    }
}
