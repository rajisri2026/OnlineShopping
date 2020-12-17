
using OnlineShopping.ServiceLayer;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    /// <summary>
    /// Only Admin has the privilege to use this controller in order to maintain orders.
    /// This shows the admin all the pending orders that needs to be approved and shipped to the user.
    /// Admin can check the details of the ordered products and users and proceed further.
    /// Admin can also see the orders cancelled by the user and orders that have been delivered.
    /// </summary>

    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        OrderServices orderServices = new OrderServices();
        // GET: Order
        public ActionResult Index()
        {
            return RedirectToAction("ShowPendingOrders");
        }

        //This action method shows the list of orders placed by the users that are 
        //yet to be approved by admin.This also has a details button for each order.
        public ActionResult ShowPendingOrders()
        {
            List<OrderViewModel> orderViewModels = orderServices.ShowPendingOrders();
            return View(orderViewModels);
        }

        //On clicking the detail button in the pending orders page, this will show the 
        //details of the corresponding order, i.e., orderId, ProductId, Quantity and UnitPrice.
        public ActionResult OrderDetails(int? orderId)
        {
            try
            {

            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "There is some problem.");
            }
            else
            {
                OrderViewModel orderViewModel = orderServices.GetOrderDetails(Convert.ToInt32(orderId));
                return View(orderViewModel);
            }
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "ProductsList", "Product"));
            }
        }

        //On admin's approval, this could change the status of the order to either approved, dispatched or deliverd.
        public ActionResult ApproveOrder(int? orderId, bool forApproval, bool forDispatch, bool forCompletion)
        {
            try
            {

            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "There is some problem.");
            }
            else
            {
                if (forApproval)
                {
                    orderServices.ApproveOrder(Convert.ToInt32(orderId));
                }
                else if (forDispatch)
                {
                    orderServices.DispatchOrder(Convert.ToInt32(orderId));
                }
                if (forCompletion)
                {
                    orderServices.CompleteOrder(Convert.ToInt32(orderId));
                }

            }
            return RedirectToAction("ShowPendingOrders");
            }
            catch (Exception e)
            {
                return View("Error", new HandleErrorInfo(e, "ShowPendingOrders", "Order"));
            }
        }

        //This is to display all the orders that are delivered to the end users.
        public ActionResult ShowCompletedOrders()
        {
            List<CompletedOrdersViewModel> completedOrdersViewModels = orderServices.ShowCompletedOrders();
            return View(completedOrdersViewModels);
        }

        //This shows the cancelled orders. Admin can further delete those orders from orders db. 
        public ActionResult CancelledOrders()
        {
            List<OrderViewModel> orderViewModels = orderServices.CancelledOrders();
            return View(orderViewModels);
        }
    }
}