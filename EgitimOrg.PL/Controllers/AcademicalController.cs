using EgitimOrg.BLL.Identity;
using EgitimOrg.BLL.Repository;
using EgitimOrg.DAL.Context;
using EgitimOrg.Entity.Entity;
using EgitimOrg.Entity.ViewModel;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EgitimOrg.PL.Controllers
{
    [Authorize(Roles ="Academical")]
    public class AcademicalController : AcBaseController
    {
        Repository<Classroom> CrRepo = new Repository<Classroom>(new EduOrgContext());
        Repository<Student> SrRepo = new Repository<Student>(new EduOrgContext());
        Repository<Teacher> TrRepo = new Repository<Teacher>(new EduOrgContext());
        Repository<Announcement> AnRepo = new Repository<Announcement>(new EduOrgContext());
        Repository<AnnouncementType> AntRepo = new Repository<AnnouncementType>(new EduOrgContext());
        Repository<Exam> ExRepo = new Repository<Exam>(new EduOrgContext());
        Repository<Article> ArRepo = new Repository<Article>(new EduOrgContext());
        Repository<Evaluation> erRepo = new Repository<Evaluation>(new EduOrgContext());
        Repository<Question> QrRepo = new Repository<Question>(new EduOrgContext());
        Repository<RollCall> RrRepo = new Repository<RollCall>(new EduOrgContext());

        // GET: Academical
        public ActionResult Index()
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);

            TeacherIndexModel tim = new TeacherIndexModel();
            tim.Classrooms = CrRepo.GetAll(c => c.IsDeleted == false && c.Teacher.UserId == uid.Id).ToList();
            tim.Announcements = AnRepo.GetAll(a => a.IsDeleted == false && a.Teacher.UserId == uid.Id).ToList();
            return View(tim);
        }     
        
        public ActionResult Classroom(int ClassId)
        {
            return View(SrRepo.GetAll(s=>s.ClassId==ClassId && s.IsDeleted==false));
        }
        public ActionResult AllAnnouncements()
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);

            AllAnnouncements AllAnn = new AllAnnouncements();
            AllAnn.ClassAnn = AnRepo.GetAll(a => a.Teacher.UserId == uid.Id && a.IsDeleted == false && a.TypeId==2).ToList();
            AllAnn.GeneralAnn = AnRepo.GetAll(a=>a.IsDeleted == false && a.TypeId == 1).ToList();

            return View(AllAnn);
        }
        //public JsonResult GetAnnouncementById(int ID)
        //{
        //    Announcement ann = AnRepo.Get(a => a.Id == ID && a.IsDeleted==false);
        //    return Json(ann,JsonRequestBehavior.AllowGet);
        //}
        public ActionResult Announcement()
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            AnnouncementModel anm = new AnnouncementModel();
            anm.AnList= AnRepo.GetAll(a => a.IsDeleted == false && a.Teacher.UserId == uid.Id).ToList();
            anm.AnTList = AntRepo.GetAll(a => a.IsDeleted == false).ToList();
            anm.TeacherId = TrRepo.Get(t => t.IsDeleted == false && t.UserId == uid.Id).Id;
            return View(anm);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AddorUpdateAnnouncement(AnnouncementModel model)
        {            
            bool result = false;
            if (model.Id == 0) {
                Announcement an = new Announcement();
                an.Title = model.Title;
                
                an.Content = model.Content;
                an.Summary = model.Summary;
                an.TeacherId = model.TeacherId;
                an.TypeId = model.TypeId;
                if (model.TypeId == 1)
                {
                    an.ClassId = null;
                }
                else an.ClassId = model.ClassId;
                if (model.Extra != null)
                {
                    string filename = model.Extra.FileName;
                    string imagePath = Server.MapPath("/Docs/" + filename);
                    model.Extra.SaveAs(imagePath);
                    an.Extra = filename;
                }
                else
                {
                    an.Extra = null;
                }
                result = AnRepo.Add(an);
            }
            else
            {
                Announcement Uan = AnRepo.Get(a => a.Id == model.Id);
                Uan.Title = model.Title;
                Uan.Content = model.Content;
                Uan.Summary = model.Summary;
                Uan.TypeId = model.TypeId;
                if (model.TypeId == 1)
                {
                    Uan.ClassId = null;
                }
                else Uan.ClassId = model.ClassId;
                if (model.Extra != null)
                {
                    string filename = model.Extra.FileName;
                    string imagePath = Server.MapPath("/Docs/" + filename);
                    model.Extra.SaveAs(imagePath);
                    Uan.Extra = filename;
                }
                result = AnRepo.Update(Uan);
            }
            
            return RedirectToAction("Announcement");
        }
        [HttpPost]
        public JsonResult AnnouncementDelete(int BID)
        {
            bool result = false;
            Announcement an = AnRepo.Get(a => a.Id == BID);
            an.IsDeleted = true;
            
            result = AnRepo.Update(an);

            return Json(result,JsonRequestBehavior.AllowGet);
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
            return Json(value,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Exams()
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            AllExamViewModel Aev = new AllExamViewModel();
            Aev.exams = ExRepo.GetAll(e => e.Teacher.UserId == uid.Id && e.IsDeleted == false).ToList();
            return View(Aev);
        }
        [HttpGet]
        public JsonResult GetExamById(int ExId)
        {
            Exam model = ExRepo.Get(x => x.Id == ExId);
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddorUpdateExam(AllExamViewModel model)
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            Teacher t=TrRepo.Get(x=>x.UserId==uid.Id);
            bool result = false;
            if (model.Id == 0)
            {
                Exam ex = new Exam();
                ex.ExamName = model.ExamName;
                ex.ClassId = model. ClassId;
                ex.TeacherId = t.Id;
                ex.StartDate = model.StartDate;
                ex.Description = model.Description;
                result = ExRepo.Add(ex);
            }
            else
            {
                Exam uex = ExRepo.Get(a => a.Id == model.Id);
                uex.ExamName = model.ExamName;
                uex.ClassId = model.ClassId;
                uex.TeacherId = t.Id;
                uex.Description = model.Description;
                
                result = ExRepo.Update(uex);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ExamDelete(int EID)
        {
            bool result = false;
            Exam ex = ExRepo.Get(a => a.Id == EID);
            ex.IsDeleted = true;

            result = ExRepo.Update(ex);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RollCall(int ClassId)
        {
            RollCallViewModel rcv = new RollCallViewModel();
            rcv.students = SrRepo.GetAll(s => s.ClassId == ClassId && s.IsDeleted == false).ToList();
            rcv.ClassId = ClassId;
            return View(rcv);
        }
        [HttpPost]
        public JsonResult AddRollCall(List<RollCall> model,DateTime Date)
        {
            bool result = true;
            bool var = false;
            List<RollCall> Controls = new List<RollCall>();
            Controls = RrRepo.GetAll(r => r.StartDate == Date).ToList();
            foreach (RollCall item in model)
            {
                var = false;
                foreach (RollCall ctrl in Controls)
                {
                    if (item.StudentId == ctrl.StudentId)
                    {
                        var = true;
                    }
                }
                if (!var)
                {
                    RrRepo.Add(item);
                }
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Profiles()
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            ProfileViewModel model = new ProfileViewModel();
            model.Shares = ArRepo.GetAll(a => a.UserId == uid.Id).OrderByDescending(a=>a.CreatedDate).ToList();
            return View(model);
        }
        [HttpPost]
        public JsonResult Sharing(Article model)
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            bool result = false;
            Article ar = new Article();
            ar.Content = model.Content;
            ar.UserId = uid.Id;
            result = ArRepo.Add(ar);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddExamScores(int ClassId,int ExamId)
        {
            ViewBag.ExStudents = SrRepo.GetAll(x => x.ClassId == ClassId && x.IsDeleted==false).ToList();
            return View();
        }
        [HttpPost]
        public JsonResult AddExamResults(Evaluation model)
        {
            bool result = false;
            result=erRepo.Add(model);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult OnlineExams()
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid = usermanager.FindByName(User.Identity.Name);
            AllExamViewModel Aev = new AllExamViewModel();
            Aev.exams = ExRepo.GetAll(e => e.Teacher.UserId == uid.Id && e.IsDeleted == false && e.Description=="Online").ToList();
            return View(Aev);
        }
        public ActionResult AddQuestions(int ClassId, int ExamId)
        {
                var usermanager = IdentityTools.NewUserManager();
                var uid = usermanager.FindByName(User.Identity.Name);
                AddQuestionViewModel aqm = new AddQuestionViewModel();
            aqm.Questions = QrRepo.GetAll(q => q.IsDeleted == false && q.ExamId == ExamId).ToList();
            aqm.ExamId = ExamId;
            aqm.ClassId = ClassId;
            return View(aqm);
        }
        [HttpPost]
        public ActionResult AddQuestions(Question model)
        {
            bool result = false;
            result = QrRepo.Add(model);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}