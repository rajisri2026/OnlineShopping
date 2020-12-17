using OnlineShopping.ServiceLayer;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;//To use HttpStatusCode
using System.Web.Mvc;//To use Controller base class

namespace OnlineShopping.Controllers
{
    /// <summary>
    /// This controller handles creating new products, updating available products and deleting products by admin.
    /// Users could view the products available and have the option to buy a product or add products to cart.
    /// </summary>

    public class ProductController : Controller
    {
        ProductServices productServices = new ProductServices();
        CategoryServices categoryServices = new CategoryServices();

        //This displays list of all available products if the search text is empty.
        //If a particular product is being searched, those alone will be shown.
        [AllowAnonymous]
        public ActionResult ProductsList(string search)
        {
            IEnumerable<ProductViewModel> producViewModel = productServices.DisplayAll();
            if (!string.IsNullOrEmpty(search))
            {
                producViewModel = producViewModel.Where(x => x.ProductName.ToLower().Contains(search.ToLower())).ToList();
                return View(producViewModel);
            }
            return View(producViewModel);
        }

        //Displays partial view under the productsList to create a new product.
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var categories = categoryServices.GetCategories();
            return PartialView(categories);
        }
        [HttpPost]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productServices.Create(productViewModel);
                    return RedirectToAction("ProductsList");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception exception)
            {
                return View("Error", new HandleErrorInfo(exception, "ProductsList", "Products"));
            }
        }

        //Displays the details of particular product based on their Id.
        [Authorize(Roles = "Admin")]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductViewModel productViewModel = productServices.Detail(Convert.ToInt32(id));
            return PartialView(productViewModel);
        }

        //On getting productId this shows the corresponding product details where admin can make changes to them.
        [Authorize(Roles = "Admin")]
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
                    return RedirectToAction("ProductsList");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception exception)
            {
                return View("Error", new HandleErrorInfo(exception, "ProductsList", "Products"));
            }

        }

        //This method removes a product from products database using that product's Id.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            productServices.Delete(id);
            return RedirectToAction("ProductsList");
        }
    }
}