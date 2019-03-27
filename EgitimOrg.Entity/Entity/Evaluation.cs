using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.Entity
{
    public class Evaluation : EntityBase
    {
        
        
        [Required]
        public double Score { get; set; }
        public int Correct { get; set; }
        public int Wrong { get; set; }
        public int Empty { get; set; }
        public int? ExamId { get; set; }
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }


    }
}
