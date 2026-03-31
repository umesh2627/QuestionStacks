using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModel;

namespace StackOverflowProject.Repository
{

    public interface ICategoriesRepository
    {
        void AddCategory(Category c);
        void UpdateCategory(Category c);
        void DeleteCategory(int cid);
        List<Category> GetCategories();
        List<Category> GetCategoriesByCategoryID(int CategoryID);

    }
    public class CatgoriesRepository:ICategoriesRepository
    {
        StackOverflowDatabaseDbcontext db;

        public CatgoriesRepository()
        {
            db = new StackOverflowDatabaseDbcontext();
        }

        public void AddCategory(Category c)
        {
            db.categories.Add(c);
            db.SaveChanges();
        }

        public void UpdateCategory(Category c)
        {
            Category ct=db.categories.Where(temp=>temp.CategoryID==c.CategoryID).FirstOrDefault();
            if(ct!=null)
            {
                   ct.CategoryName=c.CategoryName;
                   db.SaveChanges();
            }
        }
        public void DeleteCategory(int cid)
        {
            Category ct = db.categories.Where(temp => temp.CategoryID == cid).FirstOrDefault();
            if (ct != null)
            {
               db.categories.Remove(ct);
                db.SaveChanges();
            }
        }

        public List<Category> GetCategories()
        {
            List<Category> ct = db.categories.ToList();
            return ct;
        }

        public List<Category> GetCategoriesByCategoryID(int CategoryID)
        {
            List<Category> ct = db.categories.Where(temp=>temp.CategoryID==CategoryID).ToList();
            return ct;
        }


    }

   
}
