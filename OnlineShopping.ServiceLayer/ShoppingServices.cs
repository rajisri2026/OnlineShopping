

using AutoMapper;
using OnlineShopping.DomainLayer;
using OnlineShopping.Repository;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;

namespace OnlineShopping.ServiceLayer
{
    public class ShoppingServices
    {
        
        ShoppingRepository shoppingRepository;
        
        public ShoppingServices()
        {
            shoppingRepository = new ShoppingRepository();
        }

        public List<CartViewModel> CartListTocartViewModelListMapper(List<Cart> cart)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Cart, CartViewModel>());
            var mapper = config.CreateMapper();
            List<CartViewModel> cartViewModels = mapper.Map<List<CartViewModel>>(cart);
            return cartViewModels;
        }
        public List<CartViewModel> GetUserProducts(string username)
        {
            List<Cart> Carts = shoppingRepository.GetUserProducts(username);
            List<CartViewModel> cartViewModels = CartListTocartViewModelListMapper(Carts);
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

    }
}
