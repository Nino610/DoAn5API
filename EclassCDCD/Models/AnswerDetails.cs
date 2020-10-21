using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclassCDCD.Models
{
    public partial class AnswerDetails
    {
        [Key]
        [Column("AnswerID")]
        public Guid AnswerId { get; set; }
        [Key]
        [Column("QuestionID")]
        public int QuestionId { get; set; }
        [Required]
        public string Value { get; set; }

        [ForeignKey(nameof(AnswerId))]
        [InverseProperty(nameof(Answers.AnswerDetails))]
        public virtual Answers Answer { get; set; }
        [ForeignKey(nameof(QuestionId))]
        [InverseProperty(nameof(Questions.AnswerDetails))]
        public virtual Questions Question { get; set; }
    }
}
