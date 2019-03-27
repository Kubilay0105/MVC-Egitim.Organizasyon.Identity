using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.Entity
{
    public class Branch : EntityBase
    {
        [Required]
        public string BranchName { get; set; }
        [StringLength(200)]
        public string Description { get; set; }

        public virtual List<Teacher> Teachers { get; set; }
        public virtual List<Subject> Subjects { get; set; }

    }
}
