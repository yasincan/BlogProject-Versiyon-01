using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Controllers
{
    public class CategoryController : Controller
    {
        blogContext blogContext = new blogContext();
        public ActionResult Index(int id)
        {
            var data = blogContext.Article.Where(a => a.CategoryId == id).ToList();
            return View(data);
        }
        public PartialViewResult _CategoryPartial()
        {
            return PartialView(blogContext.Category.ToList());
        }
    }
}