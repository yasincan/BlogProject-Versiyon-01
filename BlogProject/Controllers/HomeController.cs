using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace BlogProject.Controllers
{
    public class HomeController : Controller
    {
        blogContext blogContext = new blogContext();
        public ActionResult Index(int Page=1)
        {
            var data = blogContext.Article.OrderByDescending(a=> a.ArticleId).ToPagedList(Page, 4);
            return View(data);
        }
        public ActionResult ArticleDetail(int id)
        {
            var article = blogContext.Article.Where(a=>a.ArticleId == id).SingleOrDefault();
            if (article == null)
            {
                return HttpNotFound();

            }
            return View(article);
        }
    }
}