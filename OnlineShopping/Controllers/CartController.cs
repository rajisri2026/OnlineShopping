using OnlineShopping.ServiceLayer;
using OnlineShopping.ViewModel;
using System;///to handle exception
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    /// <summary>
    /// This controller handles cart funtionalities;
    /// Stores the products added to cart in session untill the user is logging in and
    ///adds to the cart db only when the user on successful sign-in.
    ///The user can remove the cart products; increase or decrease the product quantity in the cart.
    /// </summary>

    [AllowAnonymous]
    public class CartController : Controller
    {
        CartServices cartServices = new CartServices();

        //if authenticated user adds to cart, this will store the products in cart db
        //if the user is un authenticated, it will simply maintain cart items in session variable.
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
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "ProductsList", "Product"));
            }

        }

        //This methods gets all the cart products either from session or from db
        //based on the authenticity of the user and displays them.
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
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "ProductsList", "Product"));
            }
        }

        //This would remove the selected product from session variable of cart items or,
        //from the db if the user is authenticated.
        [HttpPost]
        public ActionResult RemoveFromCart(int cartId, int productId)
        {
            List<CartViewModel> cartViewModels = Session["CartItems"] as List<CartViewModel>;
            Session["CartItems"] = cartServices.RemoveFromCart(cartViewModels, cartId, productId);
            Session["CartCounter"] = (Session["CartItems"] as List<CartViewModel>).Count;
            return RedirectToAction("ShowCart");

        }

        //This method is called upon clicking the increase quantity button in the cart page 
        //to increase the quantity by one for each click.
        [HttpPost]
        public ActionResult IncreaseQuantity(int cartId, int productId)
        {
            Session["CartItems"] = cartServices.IncreaseQuantity(Session["CartItems"] as List<CartViewModel>, cartId, productId);
            return RedirectToAction("ShowCart");
        }

        //This method is called upon clicking the increase quantity button in the cart page 
        //to increase the quantity by one for each click.
        [HttpPost]
        public ActionResult DecreaseQuantity(int cartId, int productId)
        {
            Session["CartItems"] = cartServices.DecreaseQuantity(Session["CartItems"] as List<CartViewModel>, cartId, productId);
            return RedirectToAction("ShowCart");
        }
    }
}