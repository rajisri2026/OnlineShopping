
using OnlineShopping.ServiceLayer;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    [Authorize(Roles ="Admin")]
    public class OrderController : Controller
    {
        OrderServices orderServices = new OrderServices();
        // GET: Order
        public ActionResult Index()
        {
            return RedirectToAction("ShowPendingOrders");
        }
        public ActionResult ShowPendingOrders()
        {
            List<OrderViewModel> orderViewModels = orderServices.ShowPendingOrders();
            return View(orderViewModels);
        }
        public ActionResult OrderDetails(int? orderId)
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
        public ActionResult ApproveOrder(int? orderId, bool forApproval, bool forDispatch, bool forCompletion)
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
        public ActionResult ShowCompletedOrders()
        {
            List<CompletedOrdersViewModel> completedOrdersViewModels = orderServices.ShowCompletedOrders();
            return View(completedOrdersViewModels);
        }
        public ActionResult CancelledOrders()
        {
            List<OrderViewModel> orderViewModels = orderServices.CancelledOrders();
            return View(orderViewModels);
        }
    }
}