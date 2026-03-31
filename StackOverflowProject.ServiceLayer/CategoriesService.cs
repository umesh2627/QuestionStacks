using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModel;
using StackOverflowProject.ViewModel;
using StackOverflowProject.Repository;
using StackOverflowProject.ServiceLayer;
using AutoMapper;
using AutoMapper.Configuration;

namespace StackOverflowProject.ServiceLayer
{
    public interface ICategoryService
    {
        void AddCategory(CategoryViewModel cvm);
        void UpdateCategory(CategoryViewModel cvm);

        void DeleteCategory(int cid);
        List<CategoryViewModel> GetCategories();
        CategoryViewModel GetCategoriesByCategoryID(int CategoryID);
    }
    public class CategoriesService: ICategoryService
    {
        ICategoriesRepository cr;

        public CategoriesService()
        {
            cr=new CatgoriesRepository();
        }

        public void AddCategory(CategoryViewModel cvm)
        {
             var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });
             IMapper mapper = config.CreateMapper();
             Category c = Mapper.Map<CategoryViewModel, Category>(cvm);
             cr.AddCategory(c);
        
        }

        public void UpdateCategory(CategoryViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CategoryViewModel, Category>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Category c = Mapper.Map<CategoryViewModel, Category>(cvm);
            cr.UpdateCategory(c);

        }
        public void DeleteCategory(int cid)
        {
           cr.DeleteCategory(cid);

        }

        public List<CategoryViewModel> GetCategories()
        {
            List<Category> c = cr.GetCategories();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Category,CategoryViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<CategoryViewModel> cvm = mapper.Map<List<Category>, List<CategoryViewModel>>(c);
            return cvm;
        }

        public CategoryViewModel GetCategoriesByCategoryID(int CategoryID)
        {
            Category c = cr.GetCategoriesByCategoryID(CategoryID).FirstOrDefault();
            CategoryViewModel cvm = null;
            if (c != null)
            {

                var config = new MapperConfiguration(cfg => { cfg.CreateMap< Category,CategoryViewModel > (); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                cvm = mapper.Map<Category,CategoryViewModel>(c);
                
            }
            return cvm;
        }


    }
}
