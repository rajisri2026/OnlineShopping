using OnlineShopping.ServiceLayer;
using System.Web.Mvc;
using OnlineShopping.DomainLayer;
using OnlineShopping.ViewModel;
using AutoMapper;
using System.Web.Security;
using System;

namespace OnlineShopping.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        RegistrationServices userServices = new RegistrationServices();
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(RegistrationViewModel userViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userViewModel.RoleId = 2;
                    User user = userServices.registrationMapperFuntion(userViewModel);
                    userServices.AnyUser(user, out bool email, out bool userName);
                    if (email)
                    {
                        ModelState.AddModelError("", "Email id already exists");
                        return View();
                    }
                    else if (userName)
                    {
                        ModelState.AddModelError("", "Username already exists");
                        return View();
                    }
                    else
                    {
                        userServices.Create(user);
                        return RedirectToAction("Login");
                    }
                }
                return View();
            }
            catch(Exception e)
            {
                ModelState.AddModelError("", e);
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel, string ReturnUrl)
        {
            try
            {
                User user = userServices.loginMapperFuntion(loginViewModel);
                bool isValid = userServices.IsRegisteredUser(user);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    Session["uname"] = user.UserName.ToString();
                    if (ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Product");
                    }
                }
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e);
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["uname"] = null;
            return RedirectToAction("Login");
        }
    }
}