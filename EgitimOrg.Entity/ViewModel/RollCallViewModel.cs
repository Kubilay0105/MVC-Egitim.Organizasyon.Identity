using EgitimOrg.Entity.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.ViewModel
{
    public class RollCallViewModel
    {
        public List<RollCall> rollCalls { get; set; }
        public List<Student> students { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public int ClassId { get; set; }
    }
}
