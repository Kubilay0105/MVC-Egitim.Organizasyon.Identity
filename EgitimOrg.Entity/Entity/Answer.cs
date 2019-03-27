using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.Entity
{
    public class Answer : EntityBase
    {
        [Required]
        [StringLength(250)]
        [DataType(DataType.Html)]
        public string AnswerText { get; set; }

        public int QuestionId { get; set; }

        //[ForeignKey("QuestionId")]
        //public virtual Question Question { get; set; }
    }
}
