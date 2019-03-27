using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.Entity
{
    public class Exam : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string ExamName { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        public int ClassId { get; set; }
        public string Description { get; set; }
        public int TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }

        public virtual List<Question> Questions { get; set; }
    }
}
