using AutoMapper;
using OnlineShopping.DomainLayer;
using OnlineShopping.Repository;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;

namespace OnlineShopping.ServiceLayer
{
    public class OrderServices
    {
        OrderRepository orderRepository;
        public OrderServices()
        {
            orderRepository = new OrderRepository();
        }
        public List<OrderViewModel> OrderListToOrderViewModelListMapper(List<Order> orders)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderViewModel>());
            var mapper = config.CreateMapper();
            List<OrderViewModel> orderViewModels = mapper.Map<List<OrderViewModel>>(orders);
            return orderViewModels;
        }
        public List<CompletedOrdersViewModel> CompletedOrderListToCompletedOrdersViewModelListMapper(List<CompletedOrder> completedOrders)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CompletedOrder, CompletedOrdersViewModel>());
            var mapper = config.CreateMapper();
            List<CompletedOrdersViewModel> completedOrdersViewModels = mapper.Map<List<CompletedOrdersViewModel>>(completedOrders);
            return completedOrdersViewModels;
        }
        public OrderViewModel OrderToOrderViewModelMapper(Order order)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderViewModel>());
            var mapper = config.CreateMapper();
            OrderViewModel orderViewModel = mapper.Map<OrderViewModel>(order);
            return orderViewModel;
        }
        public List<OrderViewModel> ShowPendingOrders()
        {
            List<Order> orders = orderRepository.ShowPendingOrders();
            List<OrderViewModel> orderViewModels = OrderListToOrderViewModelListMapper(orders);
            return orderViewModels;
        }
        public OrderViewModel GetOrderDetails(int orderId)
        {
            Order order = orderRepository.GetOrderDetails(orderId);
            OrderViewModel orderViewModel = OrderToOrderViewModelMapper(order);
            return orderViewModel;
        }
        public void ApproveOrder(int orderId)
        {
            Order order = orderRepository.GetOrderDetails(orderId);
            order.Status = "Approved";
            orderRepository.ApproveOrder(order);
        }
        public void DispatchOrder(int orderId)
        {
            Order order = orderRepository.GetOrderDetails(orderId);
            order.Status = "Dispatched";
            orderRepository.ApproveOrder(order);
        }
        public void CompleteOrder(int orderId)
        {
            CompletedOrder completedOrder = new CompletedOrder();
            Order order = orderRepository.GetOrderDetails(orderId);
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            order.Status = "Completed";
            completedOrder.OrderId = order.OrderId;
            completedOrder.UserId = order.UserId;
            completedOrder.OrderCreatedOn = order.OrderCreatedOn;
            completedOrder.OrderDeliveredOn = DateTime.Now;
            completedOrder.TotalAmount = order.TotalAmount;
            orderRepository.CompletedOrders(order,completedOrder);
        }
        public List<CompletedOrdersViewModel> ShowCompletedOrders()
        {
            List<CompletedOrder> completedOrders = orderRepository.ShowCompletedOrders();
            List<CompletedOrdersViewModel> completedOrdersViewModels = CompletedOrderListToCompletedOrdersViewModelListMapper(completedOrders);
            return completedOrdersViewModels;
        }
        public List<OrderViewModel> CancelledOrders()
        {
            List<Order> orders = orderRepository.CancelledOrders();
            List<OrderViewModel> orderViewModels = OrderListToOrderViewModelListMapper(orders);
            return orderViewModels;
        }
    }
}
