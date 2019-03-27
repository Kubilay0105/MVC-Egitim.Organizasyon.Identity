using EgitimOrg.Entity.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.ViewModel
{
    public class AddQuestionViewModel
    {
        [StringLength(250)]
        [DataType(DataType.Html)]
        public string QuestionText { get; set; }
        [StringLength(250)]
        [DataType(DataType.Html)]
        public string Option1 { get; set; }
        [StringLength(250)]
        [DataType(DataType.Html)]
        public string Option2 { get; set; }
        [StringLength(250)]
        [DataType(DataType.Html)]
        public string Option3 { get; set; }
        [StringLength(250)]
        [DataType(DataType.Html)]
        public string Option4 { get; set; }
        [StringLength(250)]
        [DataType(DataType.Html)]
        public string TAnswer { get; set; }
        public int ExamId { get; set; }
        public int ClassId { get; set; }
        public List<Question> Questions { get; set; }

    }
}
