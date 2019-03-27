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
    public class Biography : EntityBase
    {
        [StringLength(250)]
        [DataType(DataType.Html)]
        public string About { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Html)]
        public string University { get; set; }
        [DataType(DataType.Html)]
        public string Department { get; set; }
        [DataType(DataType.Html)]
        public string HighSchool { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
