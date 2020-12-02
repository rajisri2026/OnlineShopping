using AutoMapper;
using OnlineShopping.DomainLayer;
using OnlineShopping.Repository;
using OnlineShopping.ViewModel;
using System.Collections.Generic;

namespace OnlineShopping.ServiceLayer
{
    public class UserServices
    {
        UserRepository userRepository;
        
        public UserServices()
        {
            userRepository = new UserRepository();
           
        }
        public IEnumerable<UserViewModel> DisplayAll()
        {
            
            IEnumerable<User> user = userRepository.DisplayAll();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserViewModel>());
            var mapper = config.CreateMapper();
            IEnumerable<UserViewModel> userViewModel = mapper.Map<IEnumerable<User>,IEnumerable<UserViewModel>>(user);
            
            return userViewModel;
        }
        public IEnumerable<Role> GetRoles()
        {
            IEnumerable<Role> role = userRepository.GetRoles();
            return role;
        }
        public void Create(UserViewModel userViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, User>());
            var mapper = config.CreateMapper();
            User user = mapper.Map<User>(userViewModel);
            userRepository.Create(user);
        }
        public void Edit(UserViewModel userViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, User>());
            var mapper = config.CreateMapper();

            User user = mapper.Map<User>(userViewModel);
            userRepository.Edit(user);
        }
        public UserViewModel Detail(int id)
        {
            User user = userRepository.Detail(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserViewModel>());
            var mapper = config.CreateMapper();
            UserViewModel userViewModel = mapper.Map<UserViewModel>(user);
            return userViewModel;
        }
        public void Delete(int id)
        {
            userRepository.Delete(id);
        }
    }
}
