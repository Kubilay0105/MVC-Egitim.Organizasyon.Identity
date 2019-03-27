using EgitimOrg.BLL.Identity;
using EgitimOrg.BLL.Repository;
using EgitimOrg.DAL.Context;
using EgitimOrg.Entity.Entity;
using EgitimOrg.Entity.Identity;
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
    [Authorize(Roles ="Admin")]
    public class AdminController : BaseController
    {
        Repository<Branch> BrRepo = new Repository<Branch>(new EduOrgContext());
        Repository<Classroom> CrRepo = new Repository<Classroom>(new EduOrgContext());
        Repository<Teacher> TrRepo = new Repository<Teacher>(new EduOrgContext());
        Repository<Student> SrRepo = new Repository<Student>(new EduOrgContext());
        Repository<ApplicationRole> role = new Repository<ApplicationRole>(new EduOrgContext());
        Repository<ApplicationUser> userrep = new Repository<ApplicationUser>(new EduOrgContext());

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Teacher()
        {
            return View(TrRepo.GetAll(t=>t.IsDeleted==false));
        }
        public ActionResult SideBar()
        {
            var usermanager = IdentityTools.NewUserManager();
            return PartialView();
        }
        public ActionResult TeacherAdd()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult TeacherAdd(TeacherAddViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var usermanager = IdentityTools.NewUserManager();
            //var kullanici = usermanager.FindByName(model.Username);
            var kullanici = usermanager.FindByEmail(model.Email);
            if (kullanici != null)
            {
                ModelState.AddModelError("", "Bu email sistemde kayıtlı!");
                return View(model);
            }
            ApplicationUser user = new ApplicationUser();
            user.Email = model.Email;
            user.UserName = model.Email;
            model.Password = model.IdentificationNo.Substring(0, 3) + model.Name.Substring(0, 3) + ".";
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.PhoneNumber = model.PhoneNumber;
            if (model.PictureUpload != null)
            {
                string filename = model.PictureUpload.FileName;
                string imagePath = Server.MapPath("/images/Users/" + filename);
                model.PictureUpload.SaveAs(imagePath);
                user.Picture = filename;
            }
            else
            {
                string filename = "default-person.png";
                string imagePath = Server.MapPath("/images/Users/" + filename);
                //model.PictureUpload.SaveAs(imagePath);
                user.Picture = "default-person.png";
            }
            var result = usermanager.Create(user, model.Password);

            if (result.Succeeded)
            {
                usermanager.AddToRole(user.Id, "Academical");
                Teacher teach = new Teacher();
                teach.Name = model.Name;
                teach.Surname = model.Surname;
                teach.Adress = model.Adress;
                teach.UserId = user.Id;
                teach.BranchId = model.BranchId;
               
                teach.IdentificationNo = model.IdentificationNo;
                TrRepo.Add(teach);
                return RedirectToAction("Teacher");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            return View(model);
        }
        public ActionResult Branchs()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetBrancById(int Id)
        {
            Branch model = BrRepo.Get(x => x.Id == Id);
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value,JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public JsonResult Branchs(Branch model)
        {
            BrRepo.Add(model);

            return Json(JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult BranchAddorEdit(Branch model)
        {
            bool result;
            if (model.Id > 0)
            {
               result=BrRepo.Update(model);
                
            }
            else
            {
                 result = BrRepo.Add(model);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult BranchDelete(int BID)
        {          
            return Json(BrRepo.Delete(BID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Classroom()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AddorUpdateClassroom(Classroom model)
        {
            bool result = false;
            if (model.Id == 0)
            {
                result = CrRepo.Add(model);
            }
            else
            {
                Classroom Cr = CrRepo.Get(c => c.Id == model.Id);
                Cr.ClassName = model.ClassName;
                Cr.Description = model.Description;
                Cr.TeacherId = model.TeacherId;
                result = CrRepo.Update(Cr);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ClassDelete(int CID)
        {
            bool result = false;
            Classroom Cr = CrRepo.Get(a => a.Id == CID);
            Cr.IsDeleted = true;

            result = CrRepo.Update(Cr);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetClassById(int CId)
        {
            Classroom model = CrRepo.Get(x => x.Id == CId);
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Student()
        {
            return View(SrRepo.GetAll(x=>x.IsDeleted==false));
        }
        public ActionResult StudentAdd()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult StudentAdd(StudentAddViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var usermanager = IdentityTools.NewUserManager();
            //var kullanici = usermanager.FindByName(model.Username);
            var kullanici = usermanager.FindByEmail(model.Email);
            if (kullanici != null)
            {
                ModelState.AddModelError("", "Bu email sistemde kayıtlı!");
                return View(model);
            }
            Student Tcvarmi = new Student();
            Tcvarmi = SrRepo.Get(s => s.IsDeleted == false && s.IdentificationNo == model.IdentificationNo);
            if (Tcvarmi != null)
            {
                ModelState.AddModelError("", "Bu TC Kimlik No sistemde kayıtlı!");
                return View(model);
            }
            ApplicationUser user = new ApplicationUser();
            user.Email = model.Email;
            user.UserName = model.Email;
            model.Password = model.IdentificationNo.Substring(0, 3) + model.Name.Substring(0, 3) + ".";
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.PhoneNumber = model.PhoneNumber;
            if (model.PictureUpload != null)
            {
                string filename = model.PictureUpload.FileName;
                string imagePath = Server.MapPath("/images/Users/" + filename);
                model.PictureUpload.SaveAs(imagePath);
                user.Picture = filename;
            }
            else
            {                
                user.Picture = "default-person.png";
            }
            var result = usermanager.Create(user, model.Password);
            if (result.Succeeded)
            {
                usermanager.AddToRole(user.Id, "Student");
                Student std = new Student();
                std.Name = model.Name;
                std.Surname = model.Surname;
                std.Adress = model.Adress;
                std.UserId = user.Id;
                std.ClassId = model.ClassId;

                std.IdentificationNo = model.IdentificationNo;
                SrRepo.Add(std);
                return RedirectToAction("Student");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            return View(model);
        }

        [HttpGet]
        public JsonResult GetStudenyByTc(string TC)
        {
            var usermanager = IdentityTools.NewUserManager();

            Student model = SrRepo.Get(x => x.IdentificationNo == TC && x.IsDeleted==false);
            ApplicationUser au = usermanager.FindById(model.UserId);
            StudentUpdateModel sum = new StudentUpdateModel();
            sum.StdId = model.Id;
            sum.Name = model.Name;
            sum.Surname = model.Surname;
            sum.Email = au.Email;
            sum.Adress = model.Adress;
            sum.IdentificationNo = model.IdentificationNo;
            sum.PictureUpload = au.Picture;
            sum.ClassId = model.ClassId;
            string value = string.Empty;
            value = JsonConvert.SerializeObject(sum, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StudentUpdateorDelete()
        {
            return View();
        }
        [HttpPost]
        public JsonResult StudentUpdate(StudentUpdateModel model)
        {
            var usermanager = IdentityTools.NewUserManager();
            bool result = false;
            Student std = SrRepo.Get(x => x.Id == model.StdId);
            std.Name = model.Name;
            std.Surname = model.Surname;
            std.Adress = model.Adress;
            std.ClassId = model.ClassId;
            std.IdentificationNo = model.IdentificationNo;
            ApplicationUser au = usermanager.FindById(std.UserId);
            au.Email = model.Email;
            au.UserName = model.Email;

            if (SrRepo.Update(std))
            {
               var uresult= usermanager.Update(au);
                if (uresult.Succeeded)
                {
                    result = true;
                }
            }

            return Json(result,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult StudentDelete(int SID)
        {

            Student s = SrRepo.Get(a => a.Id == SID);
            s.IsDeleted = true;


            if (SrRepo.Update(s))
            {
                var usermanager = IdentityTools.NewUserManager();
                ApplicationUser au = new ApplicationUser();
                au= usermanager.FindById(s.UserId);
                au.IsDeleted = true;
                var result=usermanager.Update(au);
                if (result.Succeeded)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);

                }
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}