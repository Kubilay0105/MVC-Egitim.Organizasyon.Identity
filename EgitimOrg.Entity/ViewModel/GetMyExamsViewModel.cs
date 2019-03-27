using EgitimOrg.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.ViewModel
{
    public class GetMyExamsViewModel
    {
        public List<Exam> exams { get; set; }
        public List<Evaluation> evaluations { get; set; }
    }
}
