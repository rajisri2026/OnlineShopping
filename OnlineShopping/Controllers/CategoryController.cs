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
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        CategoryServices categoryServices = new CategoryServices();

        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult CategoryList(string search)
        {
            IEnumerable<CategoryViewModel> categoryViewModel = categoryServices.GetCategories();
            if (!string.IsNullOrEmpty(search))
            {
                categoryViewModel = categoryViewModel.Where(x => x.CategoryName.ToLower().Contains(search.ToLower())).ToList();
            }

            return PartialView(categoryViewModel);
        }

        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            categoryServices.SaveCategoryToDb(categoryViewModel);
            return RedirectToAction("CategoryList");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CategoryViewModel categoryViewModel = categoryServices.FindById(Convert.ToInt32(id));

            if (categoryViewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView(categoryViewModel);
        }
        [HttpPost]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryServices.Edit(categoryViewModel);
                    return RedirectToAction("CategoryList");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception);
                return RedirectToAction("Index");
            }
        }
     
        [HttpPost]
        public ActionResult Delete(int id)
        {
            categoryServices.Delete(id);
            return RedirectToAction("CategoryList");
        }
    }
}