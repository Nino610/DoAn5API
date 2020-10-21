using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclassCDCD.Models
{
    public partial class Options
    {
        [Key]
        [Column("OptionID")]
        public int OptionId { get; set; }
        [Column("QuestionID")]
        public int? QuestionId { get; set; }
        public int? OptionOrder { get; set; }
        [StringLength(250)]
        public string Value { get; set; }
        public double Mark { get; set; }

        [ForeignKey(nameof(QuestionId))]
        [InverseProperty(nameof(Questions.Options))]
        public virtual Questions Question { get; set; }
    }
}
