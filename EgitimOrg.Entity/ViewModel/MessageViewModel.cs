using EgitimOrg.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.ViewModel
{
    public class MessageViewModel
    {
        public List<ApplicationUser> userlist { get; set; }
        public string Userid { get; set; }
        public string role { get; set; }
    }
}
