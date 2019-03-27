using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.Entity
{
    public class Announcement : EntityBase
    {
        [Required(ErrorMessage = "Lütfen Duyuru başlığı giriniz!")]
        [StringLength(100, ErrorMessage = "Başlık {0} karakterden uzun olmamalıdır!")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Html)]
        public string Summary { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        [DataType(DataType.Html)]
        public string Content { get; set; }
        [StringLength(100)]
        public string Extra { get; set; }
        //public string PicturesId { get; set; }  Pictures tablosu kullanılırsa...
        public int? ClassId { get; set; }
        public int TeacherId { get; set; }
        public int TypeId { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("TypeId")]
        public virtual AnnouncementType AnnouncementType { get; set; }
    }
}
