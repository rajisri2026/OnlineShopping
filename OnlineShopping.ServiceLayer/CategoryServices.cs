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
    public class CategoryServices
    {
        CategoryRepository categoryRepository;
        public CategoryServices()
        {
            categoryRepository = new CategoryRepository();
        }
        public Category MapperFunction(CategoryViewModel categoryViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CategoryViewModel, Category>());
            var mapper = config.CreateMapper();
            Category category = mapper.Map<Category>(categoryViewModel);
            return category;
        }
        public IEnumerable<CategoryViewModel> ReverseMapperFunction(IEnumerable<Category> category)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryViewModel>());
            var mapper = config.CreateMapper();
            IEnumerable<CategoryViewModel> iCategoryViewModel = mapper.Map< IEnumerable<CategoryViewModel>>(category);
            return iCategoryViewModel;
        }

        public IEnumerable<CategoryViewModel> GetCategories()
        {
            IEnumerable<Category> category = categoryRepository.GetCategories();
            IEnumerable<CategoryViewModel> iCategoryViewModel = ReverseMapperFunction(category);
            return iCategoryViewModel;
            
        }

        public IEnumerable<CategoryViewModel> GetFeaturedCategories()
        {
            IEnumerable<Category> category = categoryRepository.GetFeaturedCategories();
            IEnumerable<CategoryViewModel> iCategoryViewModel = ReverseMapperFunction(category);
            return iCategoryViewModel;

        }

        public void SaveCategoryToDb(CategoryViewModel categoryViewModel)
        {
            Category category = MapperFunction(categoryViewModel);
            categoryRepository.SaveCategoryToDb(category);
        }
        public CategoryViewModel FindById(int id)
        {
            Category category = categoryRepository.FindById(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryViewModel>());
            var mapper = config.CreateMapper();
            CategoryViewModel categoryViewModel = mapper.Map<CategoryViewModel>(category);
            return categoryViewModel;
        }
        public void Edit(CategoryViewModel categoryViewModel)
        {
            Category category = MapperFunction(categoryViewModel);
            categoryRepository.Edit(category);
        }
        public void Delete(int id)
        {
            categoryRepository.Delete(id);
        }
    }
}
