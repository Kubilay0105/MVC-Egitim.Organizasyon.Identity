using EgitimOrg.Entity.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.Entity
{
    public class Teacher : EntityBase
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required]
        [StringLength(11)]
        public string IdentificationNo { get; set; }
        [StringLength(150)]
        public string Adress { get; set; }

        public string UserId { get; set; }
        public int BranchId { get; set; }

        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual List<Classroom> Classrooms { get; set; }
        public virtual List<Announcement> Announcements { get; set; }
        public virtual List<Exam> Exams { get; set; }



    }
}
