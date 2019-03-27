using EgitimOrg.BLL.Identity;
using EgitimOrg.BLL.Repository;
using EgitimOrg.DAL.Context;
using EgitimOrg.Entity.Entity;
using EgitimOrg.Entity.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EgitimOrg.PL.Controllers
{
    public class AcBaseController : Controller
    {
        EduOrgContext ent = new EduOrgContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var usermanager = IdentityTools.NewUserManager();
            var uid= usermanager.FindByName(User.Identity.Name);
            ViewBag.UserInfo = uid;
            Repository<Classroom> repoCr = new Repository<Classroom>(ent);
            ViewBag.MyClasses = repoCr.GetAll(b => b.IsDeleted == false && b.Teacher.UserId==uid.Id);
            Repository<AnnouncementType> repoAT = new Repository<AnnouncementType>(ent);
            ViewBag.AnType = repoAT.GetAll(b => b.IsDeleted == false);
            //ViewBag.Branches = new List<Branch>() { new Branch { Id = 1,BranchName="Yazılım" } };

            //Repository<Branch> repoT = new Repository<Branch>(ent);
            //ViewBag.Etiketler = repoT.GetAll(null, t => t.OrderByDescending(x => x.Articles.Count)).Take(5);

            base.OnActionExecuting(filterContext);
        }
    }
}