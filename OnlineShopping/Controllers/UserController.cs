using OnlineShopping.DomainLayer;
using OnlineShopping.ServiceLayer;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    public class UserController : Controller
    {
        UserServices userServices = new UserServices();
     //   User user = new User();
        public ActionResult UserDetails()
        {
            IEnumerable<UserViewModel> userViewModels = userServices.DisplayAll();
            return View(userViewModels);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IEnumerable<Role> Roles = userServices.GetRoles();
            ViewBag.Roles = new SelectList(Roles, "RoleId", "RoleName");

            UserViewModel userViewModel = userServices.Detail(Convert.ToInt32(id));
       
            if (userViewModel == null)
            {
                return HttpNotFound();
            }
            return View(userViewModel);

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
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception);
                return View();
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserViewModel userViewModel = userServices.Detail(Convert.ToInt32(id));
            if (userViewModel == null)
            {
                return HttpNotFound();
            }
            return View(userViewModel);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            userServices.Delete(id);
            return RedirectToAction("UserDetails");
        }
    }
}