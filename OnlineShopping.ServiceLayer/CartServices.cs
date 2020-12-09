using AutoMapper;
using OnlineShopping.DomainLayer;
using OnlineShopping.Repository;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.ServiceLayer
{
    public class CartServices
    {
        CartRepository cartRepository;
        List<CartViewModel> cartViewModels;
        public CartServices()
        {
            cartRepository = new CartRepository();
            cartViewModels = new List<CartViewModel>();
        }
        public ProductViewModel MapperFuntion(Product product)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductViewModel>());
            var mapper = config.CreateMapper();
            ProductViewModel productViewModel = mapper.Map<ProductViewModel>(product);
            return productViewModel;
        }
        public Cart CartViewModelToCartMapper(CartViewModel cartViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CartViewModel, Cart>());
            var mapper = config.CreateMapper();
            Cart cart = mapper.Map<Cart>(cartViewModel);
            return cart;
        }
        public List<CartViewModel> CartListToCartViewModelListMapper(List<Cart> cart)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Cart, CartViewModel>());
            var mapper = config.CreateMapper();
            List<CartViewModel> cartViewModels = mapper.Map<List<CartViewModel>>(cart);
            return cartViewModels;
        }
        public ProductViewModel GetProduct(int id)
        {
            Product product = cartRepository.GetProduct(id);
            return MapperFuntion(product);
        }
       
        public bool AlreadyExistsInList(int id)
        {
            return cartViewModels.Any(x => x.ProductId == id);
        }
       
        public List<CartViewModel> AddToList(List<CartViewModel> cartViewModels, ProductViewModel productViewModel)
        {
            bool value = cartViewModels.Any(x => x.ProductId == productViewModel.ProductId);
            if (value)
            {
                foreach (var data in cartViewModels)
                {
                    if (data.ProductId == productViewModel.ProductId)
                    {
                        data.Quantity = ++data.Quantity;
                    }
                }
            }
            else
            {
                cartViewModels.Add(new CartViewModel()
                {
                    ProductId = productViewModel.ProductId,
                    ProductName = productViewModel.ProductName,
                    Price = productViewModel.Price,
                    Quantity = 1,
                    
                });
            }
            
            return cartViewModels;
        }
        public void StoreToCartDb(List<CartViewModel> cartViewModels, string userName)
        {
            
            if (cartViewModels != null)
            {
                foreach (var item in cartViewModels)
                {
                    bool value = cartRepository.AlreadyExists(item.ProductId,userName);
                    item.UserName = userName;
                    if (value)
                    {
                        Cart cart = cartRepository.GetCart(item.CartId);
                       // Cart cart = cartRepository.GetCart(item.ProductId,userName);
                        cart.Quantity =item.Quantity + cart.Quantity;
                        cartRepository.UpdateCartDb(cart);
                    }
                    else
                    {
                        Cart cart = CartViewModelToCartMapper(item);
                        cartRepository.StoreToCartDb(cart);
                    } 
                }
            }
        }
        public List<CartViewModel> ShowCartFromDb(string userName)
        {
            List<CartViewModel> cartViewModels = CartListToCartViewModelListMapper(cartRepository.ShowCartFromDb(userName));
            return cartViewModels;
        }

        public void AddToDb(string username,ProductViewModel productViewModel)
        {
            Cart cart = new Cart();
            int id = productViewModel.ProductId;
            List<Cart> carts = cartRepository.ShowCartFromDb(username);
            bool itemExists = carts.Any(item => item.ProductId == id);
            if(itemExists)
            {
                foreach (var item in carts)
                {
                    if (item.ProductId == id)
                    {
                        item.Quantity = ++item.Quantity;
                        cartRepository.UpdateCartDb(item);
                    }
                }
            }
            else
            {
                cart.Quantity = 1;
                cart.Username = username;
                cart.ProductId = id;
                cartRepository.StoreToCartDb(cart);
            }
        }

        public List<CartViewModel> RemoveFromCart(List<CartViewModel> cartViewModel, int cartId, int productId)
        {
            if(cartId == 0)
            {
                var cartItem = cartViewModel.Single(item=>item.ProductId == productId);
                if(cartItem != null)
                {
                    cartViewModel.Remove(cartItem);
                }
            }
            else
            {
                cartRepository.RemoveFromCart(cartId);
            }
            return cartViewModel;
        }

        public List<CartViewModel> DecreaseQuantity(List<CartViewModel> cartViewModel, int cartId, int productId)
        {
            if (cartId == 0)
            {
                cartViewModel[cartViewModel.FindIndex(x => x.ProductId == productId)].Quantity -= 1;
            }
            else
            {
                Cart cart = cartRepository.GetCart(cartId);
                cart.Quantity -= 1;
                cartRepository.UpdateCartDb(cart);
            }
            return cartViewModel;
        }
        public List<CartViewModel> IncreaseQuantity(List<CartViewModel> cartViewModel, int cartId, int productId)
        {
            if (cartId == 0)
            {
                cartViewModel[cartViewModel.FindIndex(x => x.ProductId == productId)].Quantity += 1;
            }
            else
            {
                Cart cart = cartRepository.GetCart(cartId);
                cart.Quantity += 1;
                cartRepository.UpdateCartDb(cart);
            }
            return cartViewModel;
        }
    }
}
