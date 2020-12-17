using OnlineShopping.ServiceLayer;
using System.Web.Mvc;
using OnlineShopping.DomainLayer;
using OnlineShopping.ViewModel;
using System.Web.Security;///To use formsAuthentication
using System.Collections.Generic;
using System;//To handle exceptions

namespace OnlineShopping.Controllers
{
    /// <summary>
    /// Account controller : To manage user registration and login.
    /// Allows user to register if they are new; login if they are registered users.
    /// While registering it checks if the email or the userame already exsist in db to ensure unique mail ids and username
    /// Based on whether the authenticated user is either Admin or Customer, functionalities will be displayed.
    /// </summary>

    [AllowAnonymous]
    public class AccountController : Controller
    {
        CartServices cartServices = new CartServices();
        RegistrationServices userServices = new RegistrationServices();

        //Shows the signup form to be filled by the user
        public ActionResult Signup()
        {
            return View();
        }

        //Gets user details; Checks if all the fields fulfilled the validation and stores in database;Then redirects to login page

        [HttpPost]
        public ActionResult Signup(RegistrationViewModel userViewModel, string ReturnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userViewModel.RoleId = 2;
                    User user = userServices.registrationMapperFuntion(userViewModel);
                    userServices.Create(user);
                    Session["Url"] = ReturnUrl;
                    return RedirectToAction("Login");

                }
                return View();

            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Signup", "Account"));
            }
        }

        //This action method is called by the remote attribute from model to check if 
        //the user is already registered by checking if the username already exists in db.
        public JsonResult ExistingUsername(string username)
        {
            bool usernameExists = userServices.ExistingUsername(username);
            return Json(!usernameExists, JsonRequestBehavior.AllowGet);
        }

        //This action method is called by the remote attribute from the model to check if
        //the user is already registered by checking if the email already exists in db. 
        public JsonResult ExistingUserEmail(string userEmail)
        {
            bool userEmailExists = userServices.ExistingUserEmail(userEmail);
            return Json(!userEmailExists, JsonRequestBehavior.AllowGet);
        }

        //Shows up user login form
        public ActionResult Login()
        {
            return View();
        }

        //Gets username and password, validates if registered already and redirects to products page.
        //Also if the user has added any products to cart before login, it is added to their cart automatically.
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
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
                    if (Session["Url"] as string != null)
                    {
                        return Redirect(Session["Url"] as string);
                    }

                    else
                    {
                        return RedirectToAction("ProductsList", "Product");
                    }
                }
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "Signup", "Account"));
            }

        }

        //This will log-out the user by making their sessions null.
        //Also session of cart is made null so that some other user could  not see the previous cart products, 
        //when they are unauthenticated but still add products to the cart.
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["uname"] = null;
            Session["CartItems"] = null;
            return RedirectToAction("Login");
        }

    }
}