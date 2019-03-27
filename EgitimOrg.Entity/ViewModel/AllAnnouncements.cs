using EgitimOrg.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.ViewModel
{
    public class AllAnnouncements
    {
        public List<Announcement> ClassAnn { get; set; }
        public List<Announcement> GeneralAnn { get; set; }
    }
}
