using OnlineShopping.DomainLayer;
using OnlineShopping.ServiceLayer;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    /// <summary>
    /// This handles customer side functionalities.
    /// User has the option to buy a single product as well as to buy multiple products stored in their cart at once.
    /// User can view their already bought products;currently placed orders;
    /// Make request to cancel their order; on checkout, they are directed to checkout page,
    /// where they fill out their details and they can also increase or decrease product quantity there.
    /// On successfully placing order, request will be sent to admin for further processing.
    /// </summary>
    public class ShoppingController : Controller
    {
        ShoppingServices shoppingServices;
        UserServices userServices;
        CheckoutViewModel checkoutViewModel = new CheckoutViewModel();

        public ShoppingController()
        {
            shoppingServices = new ShoppingServices();
            userServices = new UserServices();
        }

        //Shows checkout page where user views their detals and products details.
        [Authorize(Roles = "Customer")]
        public ActionResult Checkout(int? productId)
        {
            string username = Session["uname"] as string;
            if (productId == null)
            {
                checkoutViewModel.CartViewModel = shoppingServices.GetUserProducts(username);
            }
            else
            {
                checkoutViewModel.CartViewModel = shoppingServices.GetUserProduct(Convert.ToInt32(productId));
            }
            checkoutViewModel.UserViewModel = userServices.Detail(username);
            Session["CheckoutItems"] = checkoutViewModel.CartViewModel.ToList();
            Session["PlaceOrder"] = checkoutViewModel;
            return RedirectToAction("ShowCheckoutPage");
        }

        //This method is called upon clicking the increase quantity button in the checkout page 
        //to increase the quantity by one for each click.
        [HttpPost]
        public ActionResult IncreaseQuantity(int productId)
        {
            Session["CheckoutItems"] = shoppingServices.IncreaseQuantity(Session["CheckoutItems"] as List<CartViewModel>, productId);
            return RedirectToAction("ShowCheckoutPage");
        }

        //This method is called upon clicking the decrease quantity button in the cart page 
        //to decrease the quantity by one for each click.
        [HttpPost]
        public ActionResult DecreaseQuantity(int productId)
        {
            Session["CheckoutItems"] = shoppingServices.DecreaseQuantity(Session["CheckoutItems"] as List<CartViewModel>, productId);
            return RedirectToAction("ShowCheckoutPage");
        }
        public ActionResult ShowCheckoutPage()
        {
            string username = Session["uname"] as string;
            checkoutViewModel.CartViewModel = Session["CheckoutItems"] as List<CartViewModel>;
            checkoutViewModel.UserViewModel = userServices.Detail(username);
            Session["PlaceOrder"] = checkoutViewModel;
            return View(checkoutViewModel);
        }
        public ActionResult PlaceOrder(decimal totalAmount)
        {
            checkoutViewModel = Session["PlaceOrder"] as CheckoutViewModel;
            shoppingServices.PlaceOrder(checkoutViewModel, totalAmount);
            shoppingServices.RemoveCartproducts(checkoutViewModel.CartViewModel);
            return RedirectToAction("ProductsList", "Product");
        }

        //Gets curent user from session variable and display their orders from the orders db.
        public ActionResult YourOrders()
        {
            List<OrderViewModel> orderViewModels = shoppingServices.GetYourOrders(Session["uname"] as string);
            return View(orderViewModels);
        }

        //Gets orderId and shows detail of the same.
        public ActionResult YourOrderDetails(int? orderId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "There is some problem.");
            }
            else
            {
                List<OrderDetail> orderDetails = shoppingServices.GetYourOrderDetails(Convert.ToInt32(orderId));
                return View(orderDetails);
            }
        }

        //Gets current user from session variable and shows their previously bought items from the completedOrders db.
        public ActionResult YourOrdersHistory()
        {
            List<CompletedOrdersViewModel> completedOrdersViewModels = shoppingServices.YourOrdersHistory(Session["uname"] as string);
            return View(completedOrdersViewModels);
        }

        //This would send a cancel request for an order getting the orderId
        public ActionResult CancelOrder(int? orderId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "There is some problem.");
            }
            else
            {
                shoppingServices.CancelOrder(Convert.ToInt32(orderId));
                return RedirectToAction("YourOrders");
            }
        }
    }
}