using EgitimOrg.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.ViewModel
{
    public class ChatRoomModel
    {
        public List<Message> msgs { get; set; }
        public string Icerik { get; set; }
        public string Gonderici { get; set; }
        public string Alici { get; set; }
    }
}
