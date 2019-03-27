using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EgitimOrg.Entity.ViewModel
{
    public class TeacherAddViewModel
    {
        [Required(ErrorMessage = "İsim Gereklidir.")]
        [StringLength(50)]
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyisim Gereklidir.")]
        [StringLength(50)]
        [Display(Name = "Soyadı")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Email Gereklidir.")]
        [EmailAddress()]
        public string Email { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Telefon Numarası Gereklidir.")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "TC Gereklidir")]
        [StringLength(11)]
        [Display(Name = "TC Kimlik No")]
        public string IdentificationNo { get; set; }
        [StringLength(150)]
        [Display(Name = "Adres")]
        public string Adress { get; set; }
        public HttpPostedFileBase PictureUpload { get; set; }

        public int BranchId { get; set; }
    }
}
