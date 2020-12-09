using OnlineShopping.ServiceLayer;
using System.Web.Mvc;
using OnlineShopping.DomainLayer;
using OnlineShopping.ViewModel;
using System.Web.Security;
using System;
using System.Collections.Generic;

namespace OnlineShopping.Controllers
{
    /// <summary>
    /// Account controller : To manage user registration and login.
    /// Allows user to register if they are new; login if they are registered users.
    /// </summary>
    [AllowAnonymous]
    public class AccountController : Controller
    {
        CartServices cartServices = new CartServices();
        RegistrationServices userServices = new RegistrationServices();

        //Shows up signup form to be filled by the user
        public ActionResult Signup()
        {
            return View();
        }

        //Gets user details; validates and stores in database
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

        //Shows up user login form
        public ActionResult Login()
        {
            return View();
        }

        //Gets username and password, validates if registered already and redirects to products page.
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
                    cartServices.StoreToCartDb(Session["CartItems"] as List<CartViewModel>, Session["uname"] as string);
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
            Session["CartItems"] = null;
            return RedirectToAction("Login");
        }
    }
}