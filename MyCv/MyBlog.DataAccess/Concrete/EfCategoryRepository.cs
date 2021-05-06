using MyBlog.DataAccess.Abstract;
using MyBlog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyBlog.DataAccess.Concrete
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private CvContext _context;
        public EfCategoryRepository(CvContext context)
        {
            _context = context;
        }
        public void AddCategory(Category entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var query = _context.Categories.FirstOrDefault(p => p.Id == categoryId);
            if (query!=null)
            {
                _context.Categories.Remove(query);
                _context.SaveChanges();
            }
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories;
        }

        public Category GetById(int categoryId)
        {
            return _context.Categories.FirstOrDefault(p => p.Id == categoryId);
        }

        public void UpdateCategory(Category entity)
        {
            var category = GetById(entity.Id);
            if (category!=null)
            {
                category.Name = entity.Name;
            }
        }
    }
}
