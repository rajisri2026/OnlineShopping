

using AutoMapper;
using OnlineShopping.DomainLayer;
using OnlineShopping.Repository;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopping.ServiceLayer
{
    public class ShoppingServices
    {
        
        ShoppingRepository shoppingRepository;
       
        public ShoppingServices()
        {
            shoppingRepository = new ShoppingRepository();
            
        }
        public ProductViewModel ProductToProductViewModelMapper(Product product)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductViewModel>());
            var mapper = config.CreateMapper();
            ProductViewModel productViewModel = mapper.Map<ProductViewModel>(product);
            return productViewModel;
        }
        public List<Cart> CartViewModelListTocartListMapper(List<CartViewModel> cartViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CartViewModel, Cart>());
            var mapper = config.CreateMapper();
            List<Cart> cart = mapper.Map<List<Cart>>(cartViewModel);
            return cart;
        }
        public List<CartViewModel> CartListToCartViewModelListMapper(List<Cart> cart)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Cart, CartViewModel>());
            var mapper = config.CreateMapper();
            List<CartViewModel> cartViewModels = mapper.Map<List<CartViewModel>>(cart);
            return cartViewModels;
        }
        //public OrderViewModel OrderToOrderViewModelMapper(Order order)
        //{
        //    var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderViewModel>());
        //    var mapper = config.CreateMapper();
        //    OrderViewModel orderViewModel = mapper.Map<OrderViewModel>(order);
        //    return orderViewModel;
        //}
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
        public List<CartViewModel> GetUserProducts(string username)
        {
            List<Cart> Carts = shoppingRepository.GetUserProducts(username);
            List<CartViewModel> cartViewModels = CartListToCartViewModelListMapper(Carts);
            foreach (var item in cartViewModels)
            {
                item.ProductName = item.Product.ProductName;
                item.Price = item.Product.Price;
            }
            return cartViewModels;
        }
        public List<CartViewModel> GetUserProduct(int productId)
        {
            List<CartViewModel> cartViewModels = new List<CartViewModel>();
            Product product = shoppingRepository.GetProduct(productId);
            CartViewModel cartViewModel = new CartViewModel();
            cartViewModel.ProductId = product.ProductId;
            cartViewModel.ProductName = product.ProductName;
            cartViewModel.Price = product.Price;
            cartViewModel.Quantity = 1;
            cartViewModels.Add(cartViewModel);
            return cartViewModels;
        }
        public List<CartViewModel> DecreaseQuantity(List<CartViewModel> cartViewModel, int productId)
        {
            cartViewModel[cartViewModel.FindIndex(x => x.ProductId == productId)].Quantity -= 1;
            return cartViewModel;
        }
        public List<CartViewModel> IncreaseQuantity(List<CartViewModel> cartViewModel, int productId)
        {
            cartViewModel[cartViewModel.FindIndex(x => x.ProductId == productId)].Quantity += 1;
            return cartViewModel;
        }
        public void PlaceOrder(CheckoutViewModel checkoutViewModel, decimal totalAmount)
        {
            Order order = new Order();
            order.UserId = checkoutViewModel.UserViewModel.UserId;
            order.Status = "Pending";
            order.OrderCreatedOn = DateTime.Now;
            order.TotalAmount = totalAmount;
            order.OrderDetail = new List<OrderDetail>();
            foreach (var item in checkoutViewModel.CartViewModel)
            {
                order.OrderDetail.Add(new OrderDetail()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }) ;
            }
            shoppingRepository.PlaceOrder(order);
        }
        public void RemoveCartproducts(List<CartViewModel> cartViewModel)
        {
            List<Cart> carts = CartViewModelListTocartListMapper(cartViewModel);
            shoppingRepository.RemoveCartproducts(carts);
        }
        public List<OrderViewModel> GetYourOrders(string username)
        {
            List<Order> orders = shoppingRepository.GetYourOrders(username);
            List<OrderViewModel> orderViewModels = OrderListToOrderViewModelListMapper(orders);
            return orderViewModels;
        }
        public List<OrderDetail> GetYourOrderDetails(int orderId)
        {
            List<OrderDetail> orderDetails = shoppingRepository.GetYourOrderDetails(orderId);
            return orderDetails;
        }
        public List<CompletedOrdersViewModel> YourOrdersHistory(string username)
        {
            List<CompletedOrder> completedOrders = shoppingRepository.GetYourOrderHistory(shoppingRepository.GetUserId(username));
            List<CompletedOrdersViewModel> completedOrdersViewModels = CompletedOrderListToCompletedOrdersViewModelListMapper(completedOrders);
            return completedOrdersViewModels;
        }
        public void CancelOrder(int orderId)
        {
            Order order = shoppingRepository.GetOrders(orderId);
            order.Status = "Cancelled";
            shoppingRepository.UpdateYouOrder(order);
        }
    }
}
