﻿using EgitimOrg.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.Entity.ViewModel
{
    public class TeacherIndexModel
    {
        public List<Classroom> Classrooms{ get; set; }
        public List<Announcement> Announcements { get; set; }

    }
}
