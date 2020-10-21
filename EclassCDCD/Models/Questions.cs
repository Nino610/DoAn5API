using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EclassCDCD.Models
{
    public partial class Questions
    {
        public Questions()
        {
            AnswerDetails = new HashSet<AnswerDetails>();
            Options = new HashSet<Options>();
        }

        [Key]
        [Column("QuestionID")]
        public int QuestionId { get; set; }
        [Required]
        [Column("CateID")]
        [StringLength(10)]
        public string CateId { get; set; }
        public int QuestionOrder { get; set; }
        [Required]
        [Column("TypeID")]
        [StringLength(10)]
        public string TypeId { get; set; }
        public int NumberOption { get; set; }
        [Required]
        [StringLength(500)]
        public string Content { get; set; }
        [StringLength(250)]
        public string Desciption { get; set; }
        public double? Mark { get; set; }

        [ForeignKey(nameof(CateId))]
        [InverseProperty(nameof(Categories.Questions))]
        public virtual Categories Cate { get; set; }
        [ForeignKey(nameof(TypeId))]
        [InverseProperty(nameof(Types.Questions))]
        public virtual Types Type { get; set; }
        [InverseProperty("Question")]
        public virtual ICollection<AnswerDetails> AnswerDetails { get; set; }
        [InverseProperty("Question")]
        public virtual ICollection<Options> Options { get; set; }
    }
}
