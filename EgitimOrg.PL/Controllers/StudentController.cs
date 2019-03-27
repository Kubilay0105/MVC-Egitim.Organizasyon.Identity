using EgitimOrg.BLL.Identity;
using EgitimOrg.BLL.Repository;
using EgitimOrg.DAL.Context;
using EgitimOrg.Entity.Entity;
using EgitimOrg.Entity.Identity;
using EgitimOrg.Entity.ViewModel;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EgitimOrg.PL.Controllers
{
    public class StudentController : AcBaseController
    {
        Repository<Announcement> AnRepo = new Repository<Announcement>(new EduOrgContext());
        Repository<AnnouncementType> AntRepo = new Repository<AnnouncementType>(new EduOrgContext());
        Repository<Classroom> CrRepo = new Repository<Classroom>(new EduOrgContext());
        Repository<Student> SrRepo = new Repository<Student>(new EduOrgContext());
        Repository<Article> ArRepo = new Repository<Article>(new EduOrgContext());
        Repository<ApplicationUser> AuRepo = new Repository<ApplicationUser>(new EduOrgContext());
        Repository<Exam> ExRepo = new Repository<Exam>(new EduOrgContext());
        Repository<Question> QrRepo = new Repository<Question>(new EduOrgContext());
        Repository<Evaluation> EvRepo = new Repository<Evaluation>(new EduOrgContext());
        Repository<RollCall> RrRepo = new Repository<RollCall>(new EduOrgContext());
        Repository<Biography> BrRepo = new Repository<Biography>(new EduOrgContext());
        Repository<Comment> ComRepo = new Repository<Comment>(new EduOrgContext());

        // GET: Student
        public ActionResult Index()
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            int ClassId= SrRepo.Get(s => s.UserId == uid.Id).ClassId;
            AllAnnouncements AllAnn = new AllAnnouncements();
            AllAnn.ClassAnn = AnRepo.GetAll(a => a.ClassId==ClassId && a.IsDeleted == false && a.TypeId == 2).ToList();
            AllAnn.GeneralAnn = AnRepo.GetAll(a => a.IsDeleted == false && a.TypeId == 1).ToList();

            return View(AllAnn);
        }
        [HttpGet]
        public JsonResult GetAnnoucementById(int AnnId)
        {
            Announcement model = AnRepo.Get(x => x.Id == AnnId);
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DownloadFile(string fileName)
        {
            string filename = fileName;
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "Docs\\" + filename;
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = true,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
        }
        public ActionResult GetExams()
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            int StdId = SrRepo.Get(s => s.UserId == uid.Id).Id;

            GetMyExamsViewModel myExams = new GetMyExamsViewModel();
            myExams.evaluations= EvRepo.GetAll(e => e.IsDeleted == false && e.StudentId == StdId).ToList();
            List<Exam> AllExam = ExRepo.GetAll(e => e.IsDeleted == false).ToList();
            myExams.exams = new List<Exam>();

            foreach (Evaluation item in myExams.evaluations)
            {
                foreach (Exam ex in AllExam)
                {
                    if (item.ExamId == ex.Id)
                        myExams.exams.Add(ex);
                }
            }
            return View(myExams);
        }
        public ActionResult OnlineExams()
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            Student std = SrRepo.Get(s => s.UserId == uid.Id);
            AllExamViewModel aev = new AllExamViewModel();
            aev.exams= ExRepo.GetAll(e => e.ClassId == std.ClassId && e.IsDeleted == false && e.Description == "Online").ToList();
            aev.Evaluations = EvRepo.GetAll(e => e.StudentId == std.Id && e.IsDeleted == false).ToList();
            return View(aev);
        }
        public ActionResult StartExam(int ExamId)
        {
            Exam ex = new Exam();
            ex = ExRepo.Get(e => e.Id == ExamId);
            StartExamViewModel sev = new StartExamViewModel();
            sev.ExamId = ExamId;
            sev.Questions = QrRepo.GetAll(q => q.ExamId == ExamId && q.IsDeleted == false).ToList();
            sev.ExamName = ex.ExamName;
            return View(sev);
        }
        [HttpPost]
        public ActionResult StudentAnswers(int ExamId,List<StudentAnswersModel> model)
        {
            bool result = false;
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            int StdId = SrRepo.Get(s => s.UserId == uid.Id).Id;
            int Correct = 0, Wrong = 0, Empty = 0;
            List<Question> questions = QrRepo.GetAll(q => q.ExamId == ExamId && q.IsDeleted == false).ToList();
            foreach (Question item in questions)
            {
                foreach (StudentAnswersModel sans in model)
                {
                    if (item.Id == sans.QuestionId)
                    {
                        if (item.TAnswer == sans.StdAnswer)
                            Correct++;
                        else if (sans.StdAnswer == null)
                            Empty++;
                        else Wrong++;

                    }
                }
            }
            double Score = Math.Ceiling((100 / (double)model.Count) * Correct);
            Evaluation eva = new Evaluation();
            eva.Score = Score;
            eva.Correct = Correct;
            eva.Wrong = Wrong;
            eva.Empty = Empty;
            eva.ExamId = ExamId;
            eva.StudentId = StdId;
            result= EvRepo.Add(eva);
            
                //int dogru = Correct;
                //int yanlis = Wrong;
                //int bos = Empty;
                      
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult RollCalls()
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            int StdId = SrRepo.Get(s => s.UserId == uid.Id).Id;
            List<RollCall> rollCalls = new List<RollCall>();
            rollCalls = RrRepo.GetAll(r => r.StudentId == StdId).ToList();
            return View(rollCalls);
        }
        public ActionResult MyProfile()
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            int ClassId = SrRepo.Get(s => s.UserId == uid.Id).ClassId;
            ProfileViewModel model = new ProfileViewModel();
            model.Sinifim = SrRepo.GetAll(s => s.ClassId == ClassId && s.IsDeleted == false && s.UserId!=uid.Id).ToList();
            model.Shares = ArRepo.GetAll(a => a.UserId == uid.Id && a.IsDeleted==false).OrderByDescending(a => a.CreatedDate).ToList();
            model.MyUserId = uid.Id;
            model.Bio = BrRepo.Get(b => b.UserId == uid.Id);
            return View(model);
        }
        public ActionResult GetBIO()
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            Biography bio = new Biography();
            bio = BrRepo.Get(x => x.UserId == uid.Id);
            string value = string.Empty;
            value = JsonConvert.SerializeObject(bio, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditBio(Biography model)
        {
            bool result = false;
            if (model.Id > 0)
            {
                result = BrRepo.Update(model);
            }
            else result = BrRepo.Add(model);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Sharing(Article model)
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            int ClassId = SrRepo.Get(s => s.UserId == uid.Id).ClassId;
            bool result = false;
            Article ar = new Article();
            ar.Content = model.Content;
            ar.UserId = uid.Id;
            ar.ClassId = ClassId;
            result = ArRepo.Add(ar);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteShare(int SID)
        {
            bool result = false;            
            Article ar = ArRepo.Get(a => a.Id == SID);
            ar.IsDeleted = true;

            result = ArRepo.Update(ar);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserProfile(string Uid)
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            int ClassId = SrRepo.Get(s => s.UserId == Uid).ClassId;
            ProfileViewModel model = new ProfileViewModel();
            model.Sinifim = SrRepo.GetAll(s => s.ClassId == ClassId && s.IsDeleted == false && s.UserId != uid.Id && s.UserId!=Uid).ToList();
            model.Shares = ArRepo.GetAll(a => a.UserId == Uid).OrderByDescending(a => a.CreatedDate).ToList();
            model.ProfilSahibi = SrRepo.Get(s => s.UserId == Uid);
            model.MyUserId = uid.Id;
            model.Bio = BrRepo.Get(b => b.UserId == Uid);
            return View(model);
        }
        public ActionResult Forum()
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            int ClassId = SrRepo.Get(s => s.UserId == uid.Id).ClassId;
            ForumViewModel fvm = new ForumViewModel();
            fvm.Shares = ArRepo.GetAll(s => s.ClassId == ClassId && s.IsDeleted == false).OrderByDescending(a=>a.CreatedDate).ToList();
            fvm.UserId = uid.Id;
            return View(fvm);
        }
        [HttpPost]
        public ActionResult AddComment(Comment model)
        {
            bool result = false;
            result = ComRepo.Add(model);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}