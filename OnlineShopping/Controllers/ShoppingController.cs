using OnlineShopping.DomainLayer;
using OnlineShopping.ServiceLayer;
using OnlineShopping.ViewModel;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Checkout()
        {
            
            string username = Session["uname"] as string;
            checkoutViewModel.CartViewModel = shoppingServices.GetUserProducts(username);
            checkoutViewModel.UserViewModel = userServices.Detail(username);
            Session["CheckoutItems"] = checkoutViewModel.CartViewModel.ToList();
            Session["PlaceOrder"] = checkoutViewModel;
            return RedirectToAction("ShowCheckoutPage", checkoutViewModel);
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
            shoppingServices.PlaceOrder(checkoutViewModel,totalAmount);
            return RedirectToAction("ProductsList", "Product");
        }
    }
}