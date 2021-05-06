using Microsoft.AspNetCore.Mvc;
using MyBlog.DataAccess.Abstract;
using MyBlog.DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebUI.ViewComponents
{
    public class CategoryMenu:ViewComponent
    {
        private ICategoryRepository _categoryRepo;
        public CategoryMenu(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["id"];
            return View(_categoryRepo.GetAll());
        }
    }
}
