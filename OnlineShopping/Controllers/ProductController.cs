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
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        ProductServices productServices = new ProductServices();
        CategoryServices categoryServices = new CategoryServices();

        public ActionResult Index()
        
        {
            return View();
        }
        public ActionResult ProductsList(string search)
        {
            IEnumerable<ProductViewModel> producViewModel = productServices.DisplayAll();
            if (!string.IsNullOrEmpty(search))
            {
                producViewModel = producViewModel.Where(x => x.ProductName.ToLower().Contains(search.ToLower())).ToList();
            }
            
            return PartialView(producViewModel);
        }
        public ActionResult Create()
       
        {
            var categories = categoryServices.GetCategories();
            return PartialView(categories);
        }
        [HttpPost]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            //try
            //{
                if (ModelState.IsValid)
                {
                    productServices.Create(productViewModel);
                    //TempData["SuccessMessage"] = "Product added successfully";
                    return RedirectToAction("ProductsList");
                }
                else
                {
                    return View();
                }
            //}
            //catch (Exception exception)
            //{
            //    //TempData["ErrorMessage"] = "Failed to add new product" + exception.Message;
            //    return RedirectToAction("Index");
            //}
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductViewModel productViewModel = productServices.Detail(Convert.ToInt32(id));
            return View(productViewModel);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductViewModel productViewModel = productServices.Detail(Convert.ToInt32(id));
           
            if (productViewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView(productViewModel);

        }
        [HttpPost]
        public ActionResult Edit(ProductViewModel productViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productServices.Edit(productViewModel);
                    TempData["SuccessMessage"] = "Product modified successfully";
                    return RedirectToAction("ProductsList");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception exception)
            {
                TempData["ErrorMessage"] = "Failed to edit the product" + exception.Message;
                return RedirectToAction("Index");
            }
        }
        
        [HttpPost]
        public ActionResult Delete(int id)
        {
            productServices.Delete(id);
            return RedirectToAction("ProductsList");
        }
    }
}