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
    public class Message : EntityBase
    {
        [Required]
        public string RecieverId { get; set; }
        public string SenderId { get; set; }
        public bool IsRead { get; set; }
        [Required]
        public string SentMessage { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("SenderId")]
        public virtual ApplicationUser User { get; set; }
    }
}
