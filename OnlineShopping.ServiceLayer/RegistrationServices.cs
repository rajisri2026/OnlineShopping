using AutoMapper;
using OnlineShopping.DomainLayer;
using OnlineShopping.Repository;
using OnlineShopping.ViewModel;

namespace OnlineShopping.ServiceLayer
{
    public class RegistrationServices
    {
        RegistrationRepository userRepository;
        public RegistrationServices()
        {
            userRepository = new RegistrationRepository();
        }
        public User registrationMapperFuntion(RegistrationViewModel userViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RegistrationViewModel, User>());
            var mapper = config.CreateMapper();
            User user = mapper.Map<RegistrationViewModel, User>(userViewModel);
            return user;
        }

        public User loginMapperFuntion(LoginViewModel userViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<LoginViewModel, User>());
            var mapper = config.CreateMapper();
            User user = mapper.Map<LoginViewModel, User>(userViewModel);

            return user;
        }

        public void AnyUser(User user, out bool email, out bool userName)
        {
            userRepository.AnyUser(user, out email, out userName);
        }
        public void Create(User user)
        {
            userRepository.Create(user);
        }
        public bool IsRegisteredUser(User user)
        {
            return userRepository.IsRegisteredUser(user);
        }
    }
}
