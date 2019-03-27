using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.Entity
{
    public class AnnouncementType: EntityBase
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Surname { get; set; }

        public virtual List<Announcement> Announcements { get; set; }

    }
}
