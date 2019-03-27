using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.Entity
{
    public class Slider :EntityBase
    {
        [Required(ErrorMessage = "Lütfen Duyuru başlığı giriniz!")]
        [StringLength(100, ErrorMessage = "Başlık {0} karakterden uzun olmamalıdır!")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Html)]
        public string Summary { get; set; }
        [Required]
        [DataType(DataType.Html)]
        public string Content { get; set; }
        [StringLength(100)]
        public string Picture { get; set; }
        //public string PicturesId { get; set; }  Pictures tablosu kullanılırsa...
    }
}
