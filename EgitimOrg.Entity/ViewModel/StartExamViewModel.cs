using EgitimOrg.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.ViewModel
{
    public class StartExamViewModel
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public List<Question> Questions { get; set; }
    }
}
