using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.Entity
{
    public class Question : EntityBase
    {
        [Required]
        [StringLength(250)]
        [DataType(DataType.Html)]
        public string QuestionText { get; set; }
        [Required]
        [StringLength(250)]
        [DataType(DataType.Html)]
        public string Option1 { get; set; }
        [Required]
        [StringLength(250)]
        [DataType(DataType.Html)]
        public string Option2 { get; set; }
        [Required]
        [StringLength(250)]
        [DataType(DataType.Html)]
        public string Option3 { get; set; }
        [Required]
        [StringLength(250)]
        [DataType(DataType.Html)]
        public string Option4 { get; set; }
        [StringLength(250)]
        [DataType(DataType.Html)]
        public string TAnswer { get; set; }
        public int ExamId { get; set; }

        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }

    }
}
