using EgitimOrg.BLL.Identity;
using EgitimOrg.BLL.Repository;
using EgitimOrg.DAL.Context;
using EgitimOrg.Entity.Entity;
using EgitimOrg.Entity.Identity;
using EgitimOrg.Entity.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EgitimOrg.PL.Controllers
{
    public class MessageController : AcBaseController
    {
        Repository<Student> SrRepo = new Repository<Student>(new EduOrgContext());
        Repository<Message> MrRepo = new Repository<Message>(new EduOrgContext());

        public ActionResult Message()
        {
            MessageViewModel model = new MessageViewModel();
            string Gonderici = HttpContext.User.Identity.GetUserId();
            model.Userid = Gonderici;
            var GecmisSohbetGiden = MrRepo.GetAll(m => m.SenderId == Gonderici).Select(go => go.RecieverId).Distinct().ToList();
            var usermanager = IdentityTools.NewUserManager();
            List<ApplicationUser> Kullanicilar = new List<ApplicationUser>();
            foreach (var id in GecmisSohbetGiden)
            {
                var kullanici = usermanager.FindById(id);
                Kullanicilar.Add(kullanici);
            }
            var GecmisSohbetGelen=MrRepo.GetAll(m => m.RecieverId == Gonderici).Select(go => go.SenderId).Distinct().ToList();
            foreach (var id in GecmisSohbetGelen)
            {
                var kullanici = usermanager.FindById(id);
                if (Kullanicilar.Contains(kullanici))
                {
                    continue;
                }
                else
                {
                    Kullanicilar.Add(kullanici);
                }
            }
            var user = usermanager.FindById(User.Identity.GetUserId());
            if (user.Roles.FirstOrDefault().RoleId == "2c7433ca-151b-416f-a74a-e91a5c0ebbce") { model.role = "Admin"; }
            else if (user.Roles.FirstOrDefault().RoleId == "d692f5df-4be7-4a06-b6ba-5301aa7262ac") { model.role = "Academical"; }
            else { model.role = "Student"; }

            model.userlist = Kullanicilar.Reverse<ApplicationUser>().ToList();//güncel konusma sıralama
            return View(model);
        }
        public ActionResult Chat()
        {
            ChatRoomModel crmod = new ChatRoomModel();
            crmod.Icerik = "Mesajlaşmak için sohbet geçmişini kullanın...";
            return PartialView(crmod);
        }
        [HttpPost]
        public ActionResult Chat(string gonderici,string aliciid)
        {
            ChatRoomModel crmod = new ChatRoomModel();
            crmod.msgs=MrRepo.GetAll(m => (m.RecieverId == aliciid && m.SenderId == gonderici) || (m.RecieverId == gonderici && m.SenderId == aliciid)).ToList();
            crmod.Gonderici = gonderici;
            crmod.Alici = aliciid;
            return PartialView(crmod);
        }
        [HttpPost]
        public ActionResult SendMessage(Message msg)
        {
            Message mess = new Message();
            mess.RecieverId = msg.RecieverId;
            mess.SenderId = msg.SenderId;
            mess.SentMessage = msg.SentMessage;
            mess.IsDeleted = false;
            mess.IsRead = false;
            if (MrRepo.Add(mess))
            {
                //Güncelleme
                ChatRoomModel crm = new ChatRoomModel();
                crm.Alici = msg.RecieverId;
                crm.Gonderici = msg.SenderId;
                return PartialView("Chat", crm);
            }
            return PartialView();
        }
        [HttpPost]
        public ActionResult EasyMessage(Message msg)
        {
            bool result = false;
            Message mess = new Message();
            mess.RecieverId = msg.RecieverId;
            mess.SenderId = msg.SenderId;
            mess.SentMessage = msg.SentMessage;
            mess.IsDeleted = false;
            mess.IsRead = false;
            result = MrRepo.Add(mess);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}