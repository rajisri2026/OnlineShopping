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
        // GET: Shopping
        public ActionResult Index()
        {
            return View();
        }
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
        public ActionResult IncreaseQuantity(int productId)
        {
            Session["CheckoutItems"] = shoppingServices.IncreaseQuantity(Session["CheckoutItems"] as List<CartViewModel>, productId);
            return RedirectToAction("ShowCheckoutPage");
        }
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
        public ActionResult YourOrders()
        {
            List<OrderViewModel> orderViewModels = shoppingServices.GetYourOrders(Session["uname"] as string);
            return View(orderViewModels);
        }
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
        public ActionResult YourOrdersHistory()
        {
            List<CompletedOrdersViewModel> completedOrdersViewModels = shoppingServices.YourOrdersHistory(Session["uname"] as string);
            return View(completedOrdersViewModels);
        }
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