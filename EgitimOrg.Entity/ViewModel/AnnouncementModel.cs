using EgitimOrg.Entity.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EgitimOrg.Entity.ViewModel
{
    public class AnnouncementModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen Duyuru başlığı giriniz!")]
        [StringLength(100, ErrorMessage = "Başlık {0} karakterden uzun olmamalıdır!")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Html)]
        public string Summary { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        [DataType(DataType.Html)]
        public string Content { get; set; }
        public HttpPostedFileBase Extra { get; set; }
        //public string PicturesId { get; set; }  Pictures tablosu kullanılırsa...
        public int? ClassId { get; set; }
        public int TeacherId { get; set; }
        public int TypeId { get; set; }

        public List<Announcement> AnList { get; set; }
        public List<AnnouncementType> AnTList { get; set; }
    }
}
