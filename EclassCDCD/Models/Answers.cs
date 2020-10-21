using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclassCDCD.Models
{
    public partial class Answers
    {
        public Answers()
        {
            AnswerDetails = new HashSet<AnswerDetails>();
        }

        [Key]
        [Column("AnswerID")]
        public Guid AnswerId { get; set; }
        [Required]
        [Column("StudentID")]
        [StringLength(10)]
        public string StudentId { get; set; }
        [Column("PlanID")]
        public int PlanId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Time { get; set; }

        [ForeignKey(nameof(PlanId))]
        [InverseProperty(nameof(Plans.Answers))]
        public virtual Plans Plan { get; set; }
        [InverseProperty("Answer")]
        public virtual ICollection<AnswerDetails> AnswerDetails { get; set; }
    }
}
