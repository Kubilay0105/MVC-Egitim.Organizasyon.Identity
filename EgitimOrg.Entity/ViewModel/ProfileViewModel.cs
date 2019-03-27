using EgitimOrg.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.ViewModel
{
    public class ProfileViewModel
    {
        public List<Article> Shares { get; set; }
        public List<Student> Sinifim { get; set; }
        public Biography Bio { get; set; }
        public Student ProfilSahibi { get; set; }
        public string MyUserId { get; set; }
        public int tobeDeleted { get; set; }
    }
}
