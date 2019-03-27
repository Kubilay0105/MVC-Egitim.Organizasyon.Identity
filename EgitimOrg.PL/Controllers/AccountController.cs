using EgitimOrg.BLL.Identity;
using EgitimOrg.DAL.Context;
using EgitimOrg.Entity.Identity;
using EgitimOrg.Entity.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EgitimOrg.PL.Controllers
{
    public class AccountController : Controller
    {
       
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            //ViewBag.returnUrl=returnUrl;
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var usermanager = IdentityTools.NewUserManager();
            var kullanici = usermanager.FindByName(model.Username);
            if (kullanici == null)
            {
                ModelState.AddModelError("", " *Böyle bir kullanıcı kayıtlı değil!");
                return View(model);
            }
            else
            {
                if (!usermanager.CheckPassword(kullanici, model.Password))
                {
                    ModelState.AddModelError("", " *Şifre Hatalı!");
                    return View(model);
                }
                var authManager = HttpContext.GetOwinContext().Authentication;
                var identity = usermanager.CreateIdentity(kullanici, "ApplicationCookie");
                var authProperty = new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe
                };
                authManager.SignIn(authProperty, identity);
                if (kullanici.Roles.FirstOrDefault().RoleId == "2c7433ca-151b-416f-a74a-e91a5c0ebbce")
                    return Redirect(string.IsNullOrEmpty(model.returnUrl) ? "/Admin/Index" : model.returnUrl);
                else if (kullanici.Roles.FirstOrDefault().RoleId == "d692f5df-4be7-4a06-b6ba-5301aa7262ac")
                    return Redirect(string.IsNullOrEmpty(model.returnUrl) ? "/Academical/Index" : model.returnUrl);
                else
                    return Redirect(string.IsNullOrEmpty(model.returnUrl) ? "/Student/Index" : model.returnUrl);


            }
        }
        [Authorize]
        [HttpPost]
        public ActionResult LogOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult ForgotYourPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ForgotYourPassword(ForgotPasswordModel model)
        {
            var usermanager = IdentityTools.NewUserManager();
            var userstore = IdentityTools.NewUserStore();
            var user = await userstore.FindByEmailAsync(model.Email);
            if(user!=null && user.Email==model.Email)
            {
                Random rnd = new Random();
                string str = "qwertyuopilkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM1234567890.,*";
                string NewPassword = "";
                for (int i = 0; i < 7; i++)
                {
                    NewPassword += str[rnd.Next(str.Length)];
                }
                await userstore.SetPasswordHashAsync(user, usermanager.PasswordHasher.HashPassword(NewPassword));
                var result = userstore.Context.SaveChanges();

                MailMessage message = new MailMessage();
                message.From = new MailAddress("gkegitimoto@gmail.com");
                message.To.Add(model.Email);
                message.Body = "Sayın " + user.Name + " " + user.Surname + " şifreniz " + NewPassword + " olarak güncellenmiştir. İyi çalışmalar dileriz.";
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new NetworkCredential("gkegitimoto@gmail.com", "Besiktas1903.");
                client.EnableSsl = true;
                client.Send(message);

                return RedirectToAction("Login");

            }
            else
            {
                return View();
            }
        }
    }
}