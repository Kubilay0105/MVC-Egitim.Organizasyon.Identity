using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EgitimOrg.Entity.ViewModel
{
    public class StudentUpdateModel
    {
        [Required]
        public int StdId { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Soyadı")]
        public string Surname { get; set; }
        [Required]
        [EmailAddress()]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }
        //[Required]
        //[StringLength(100)]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }
        //[Required]
        //[StringLength(100)]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Şifreler aynı değil!")]
        //public string ConfirmPassword { get; set; }
        [Required]
        [StringLength(11)]
        [Display(Name = "TC Kimlik No")]
        public string IdentificationNo { get; set; }
        [StringLength(150)]
        [Display(Name = "Adres")]
        public string Adress { get; set; }
        public string PictureUpload { get; set; }

        public string UserId { get; set; }
        public int ClassId { get; set; }
    }
}
