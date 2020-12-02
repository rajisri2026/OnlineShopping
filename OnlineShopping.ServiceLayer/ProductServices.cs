using System.Collections.Generic;
using AutoMapper;
using OnlineShopping.DomainLayer;
using OnlineShopping.Repository;
using OnlineShopping.ViewModel;

namespace OnlineShopping.ServiceLayer
{
    public class ProductServices
    {
        ProductRepository productRepository;
        CategoryRepository categoryRepository;
        public ProductServices()
        {
            productRepository = new ProductRepository();
            categoryRepository = new CategoryRepository();
        }
        public IEnumerable<ProductViewModel> DisplayAll()
        {
            IEnumerable<Product> product = productRepository.DisplayAll();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductViewModel>());
            var mapper = config.CreateMapper();
            IEnumerable<ProductViewModel> iEnumerableProductViewModel = mapper.Map<IEnumerable<ProductViewModel>>(product);
            return iEnumerableProductViewModel;
        }
        public void Create(ProductViewModel productViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, Product>());
            var mapper = config.CreateMapper();
            Product product = mapper.Map<Product>(productViewModel);
            productRepository.Create(product);
        }
        public void Edit(ProductViewModel productViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductViewModel, Product>());
            var mapper = config.CreateMapper();
            Product product = mapper.Map<Product>(productViewModel);
            productRepository.Edit(product);
        }
        public ProductViewModel Detail(int id)
        {
            Product product = productRepository.Detail(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductViewModel>());
            var mapper = config.CreateMapper();
            ProductViewModel productViewModel = mapper.Map<ProductViewModel>(product);
            return productViewModel;
        }
        public void Delete(int id)
        {
            productRepository.Delete(id);
        }
    }
}
