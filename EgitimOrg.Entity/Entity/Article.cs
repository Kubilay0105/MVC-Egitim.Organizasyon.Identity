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
    public class Article : EntityBase
    {        
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        [DataType(DataType.Html)]
        public string Content { get; set; }
        public int? ClassId { get; set; }
        //public string PicturesId { get; set; }  Pictures tablosu kullanılırsa...
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
