using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.Entity
{
    public class OnlineExam : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string ExamName { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        //public int StudentId { get; set; }

        //[ForeignKey("StudentId")]
        //public virtual Student Student { get; set; }
        //public virtual List<Question> Questions { get; set; }

    }
}
