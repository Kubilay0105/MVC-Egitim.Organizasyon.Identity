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
    public class Comment : EntityBase
    {
        [Required(ErrorMessage = "Lütfen yorum içeriğini giriniz!")]
        public string Content { get; set; }
        public int ArticleId { get; set; }
        public string UserId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
