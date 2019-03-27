using EgitimOrg.Entity.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            IsDeleted = false;
        }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        [StringLength(100)]
        [Display(Name = "Fotoğraf")]
        public string Picture { get; set; }
        public bool IsDeleted { get; set; }

        //public virtual List<Message> Messages { get; set; }

    }
}
