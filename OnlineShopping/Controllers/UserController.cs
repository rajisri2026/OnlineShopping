using OnlineShopping.DomainLayer;
using OnlineShopping.ServiceLayer;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    /// <summary>
    /// This handles user related functionalities.
    /// Admin and users can view thier profile and update the same; change their passwords;
    /// Admin can change user roles and view user details.
    /// </summary>

    public class UserController : Controller
    {
        UserServices userServices = new UserServices();
        [Authorize(Roles = "Admin")]
        public ActionResult UserDetails()
        {
            IEnumerable<UserViewModel> userViewModels = userServices.DisplayAll();
            return View(userViewModels);
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserViewModel userViewModel = userServices.Detail(Convert.ToInt32(id));
            return View(userViewModel);
        }
        public ActionResult Edit(int userId)
        {
            IEnumerable<Role> Roles = userServices.GetRoles();
            ViewBag.Roles = new SelectList(Roles, "RoleId", "RoleName");

            UserViewModel userViewModel = userServices.Detail(userId);

            if (userViewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView(userViewModel);

        }

        [HttpPost]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    IEnumerable<Role> Roles = userServices.GetRoles();
                    ViewBag.Roles = new SelectList(Roles, "RoleId", "RoleName");

                    userServices.Edit(userViewModel);

                    return RedirectToAction("UserDetails");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "ProductsList", "Product"));
            }
        }
        //[HttpPost]
        //public ActionResult Delete(int userId)
        //{
        //    userServices.Delete(userId);
        //    return RedirectToAction("UserDetails");
        //}
        public ActionResult UpdateProfile()
        {
            UserViewModel userViewModel = userServices.Detail(Session["uname"] as string);
            return View(userViewModel);
        }
        [HttpPost]
        public ActionResult UpdateProfile(UserViewModel userViewModel)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    userViewModel.RoleId = 1;
                }
                else
                {
                    userViewModel.RoleId = 2;
                }
                userServices.Edit(userViewModel);
                Session["uname"] = userViewModel.UserName.ToString();
                return RedirectToAction("Index", "Product");
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "ProductsList", "Product"));
            }
        }

        public ActionResult ChangePassword()
        {
            UserViewModel userViewModel = userServices.Detail(Session["uname"] as string);
            return View(userViewModel);
        }
        [HttpPost]
        public ActionResult ChangePassword(UserViewModel userViewModel)
        {
            try
            {
                if (userViewModel.Password == userViewModel.OldPassword)
                {
                    userViewModel.Password = userViewModel.NewPassword;
                    userServices.Edit(userViewModel);
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect Old Password");
                    return View();
                }
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "ProductsList", "Product"));
            }

        }
    }
}