using EgitimOrg.BLL.Repository;
using EgitimOrg.DAL.Context;
using EgitimOrg.Entity.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EgitimOrg.PL.Controllers
{
    public class BaseController : Controller
    {
        EduOrgContext ent = new EduOrgContext();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Repository<Branch> repoBr = new Repository<Branch>(ent);
            ViewBag.Branches = repoBr.GetAll(b => b.IsDeleted == false);


            Repository<Teacher> repoTr = new Repository<Teacher>(ent);
            ViewBag.Teachers= repoTr.GetAll(b => b.IsDeleted == false);
            Repository<Classroom> repoCr = new Repository<Classroom>(ent);
            ViewBag.Classes = repoCr.GetAll(b => b.IsDeleted == false);
            //ViewBag.Branches = new List<Branch>() { new Branch { Id = 1,BranchName="Yazılım" } };

            //Repository<Branch> repoT = new Repository<Branch>(ent);
            //ViewBag.Etiketler = repoT.GetAll(null, t => t.OrderByDescending(x => x.Articles.Count)).Take(5);

            base.OnActionExecuting(filterContext);
        }
    }
}