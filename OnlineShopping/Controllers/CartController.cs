using OnlineShopping.ServiceLayer;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    /// <summary>
    /// This controller handles products added to the cart;
    /// Stores users products in cart database  
    /// </summary>
    [AllowAnonymous]
    public class CartController : Controller
    {
        CartServices cartServices = new CartServices();
       
        public ActionResult AddToCart(int id)
        {
            try
            {
                ProductViewModel productViewModel = cartServices.GetProduct(id);
                List<CartViewModel> cartViewModels = new List<CartViewModel>();
                if (Session["uname"] == null)
                {
                    if (Session["CartItems"] != null)
                    {
                        cartViewModels = Session["CartItems"] as List<CartViewModel>;
                        cartViewModels = cartServices.AddToList(cartViewModels, productViewModel);
                    }
                    else
                    {

                        cartViewModels = cartServices.AddToList(cartViewModels, productViewModel);
                    }
                    Session["CartCounter"] = cartViewModels.Count;
                    Session["CartItems"] = cartViewModels;
                    return RedirectToAction("ProductsList", "Product");
                }
                else
                {
                    cartServices.AddToDb(Session["uname"] as string, productViewModel);
                    return RedirectToAction("ProductsList", "Product");
                }
            }
            catch(Exception exception)
            {
                return View("Error", new HandleErrorInfo(exception, "Index", "Product"));
            }
            
        }
        public ActionResult ShowCart()
        {
            try
            {
                List<CartViewModel> cartViewModels = new List<CartViewModel>();

                if (Session["uname"] != null)
                {
                    cartViewModels = cartServices.ShowCartFromDb(Session["uname"] as string);
                    return View(cartViewModels);

                }
                if (Session["CartItems"] != null)
                {
                    List<CartViewModel> sessionCartViewModels = Session["CartItems"] as List<CartViewModel>;
                    foreach (var item in sessionCartViewModels)
                    {
                        cartViewModels.Add(item);
                    }

                }
                return View(cartViewModels);
            }
            catch(Exception exception)
            {
                return View("Error", new HandleErrorInfo(exception, "ShowCart", "Cart"));
            }
            
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int cartId, int productId)
        {
            try
            {
                List<CartViewModel> cartViewModels = Session["CartItems"] as List<CartViewModel>;
                Session["CartItems"] = cartServices.RemoveFromCart(cartViewModels, cartId, productId);
                
                return RedirectToAction("ShowCart");
            }
            catch(Exception exception)
            {
                return View("Error", new HandleErrorInfo(exception, "ShowCart", "Cart"));
            }
            
        }

        public ActionResult IncreaseQuantity(int cartId, int productId)
        {
            Session["CartItems"] = cartServices.IncreaseQuantity(Session["CartItems"] as List<CartViewModel>, cartId, productId);
            return RedirectToAction("ShowCart");
        }
        [HttpPost]
        public ActionResult DecreaseQuantity(int cartId, int productId)
        {
            Session["CartItems"] = cartServices.DecreaseQuantity(Session["CartItems"] as List<CartViewModel>, cartId, productId);
            return RedirectToAction("ShowCart");
        }
    }
}