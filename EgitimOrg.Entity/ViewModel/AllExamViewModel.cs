using EgitimOrg.Entity.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.ViewModel
{
    public class AllExamViewModel
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string ExamName { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        public int ClassId { get; set; }
        public string Description { get; set; }
        public int TeacherId { get; set; }
        public bool IsDeleted { get; set; }
        public List<Exam> exams { get; set; }
        public List<Evaluation> Evaluations { get; set; }
    }
}
