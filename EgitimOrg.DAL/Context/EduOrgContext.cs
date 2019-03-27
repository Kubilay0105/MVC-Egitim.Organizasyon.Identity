using EgitimOrg.DAL.Migrations;
using EgitimOrg.Entity.Entity;
using EgitimOrg.Entity.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimOrg.DAL.Context
{
    public class EduOrgContext : IdentityDbContext<ApplicationUser>
    {
        public EduOrgContext() : base("EduOrgContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EduOrgContext, Configuration>("EduOrgContext"));
        }
        public virtual DbSet<Announcement> Announcements { get; set; }
        public virtual DbSet<AnnouncementType> AnnouncementTypes { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<Evaluation> Evaluations { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<OnlineExam> OnlineExams { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<RollCall> RollCalls { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<Biography> Biographies { get; set; }

    }
}
