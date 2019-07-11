using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BlogProject.Controllers
{
    [LoginControl]
    public class AdminController : Controller
    {
        blogContext blogContext = new blogContext();
        public ActionResult Index()
        {
            ViewBag.Count=blogContext.Article.Count();
            return View();
        }
        public ActionResult Article()
        {
            return View(blogContext.Article.OrderByDescending(a => a.ArticleId).ToList());
        }
        public ActionResult ArticleAdd()
        {
            ViewBag.Date = String.Format("{0:MM.dd.yyyy }", DateTime.Now);
            ViewBag.Categori = blogContext.Category.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult ArticleAdd(Article article, HttpPostedFileBase Picture)
        {
            int pcId=0;
            if (Picture!=null)
            {
                var dt = Guid.NewGuid().ToString() + "_" + Path.GetFileName(Picture.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Images/"), dt);
                Picture.SaveAs(filePath);
                Picture pc = new Picture();
                pc.Path = dt;
                pc.Title = Picture.FileName;
                blogContext.Picture.Add(pc);
                blogContext.SaveChanges();
                if (pc.PictureId!=null)
                {
                    pcId = pc.PictureId;
                }
            }
            if (pcId != -1)
            {
                article.PictureId = pcId;
                article.CreatedDate = DateTime.Now;
                blogContext.Article.Add(article);
                blogContext.SaveChanges();
            }
            
            return RedirectToAction("Article");
        }
        public ActionResult ArticleEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = blogContext.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(blogContext.Category, "CategoryId", "Name", article.CategoryId);

            int PcId = Convert.ToInt32(article.PictureId);
            var picture = blogContext.Picture.Where(a => a.PictureId == PcId).SingleOrDefault();

            return View(article);

        }
        [HttpPost]
        public ActionResult ArticleEdit(Article article , int ? id , HttpPostedFileBase postedFile)
        {
            int PcId = Convert.ToInt32(article.PictureId);
            var pc = blogContext.Picture.Where(a => a.PictureId == PcId).SingleOrDefault();
            if (postedFile != null)
            {
                System.IO.File.Delete(Server.MapPath("~/Images/" + pc.Path));
                var dt = Guid.NewGuid().ToString() + "_" + Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(Server.MapPath("~/Images/"), dt);
                postedFile.SaveAs(filePath);
                pc.Path = dt;
                blogContext.Entry(pc).State = EntityState.Modified;
 
            }
            blogContext.Entry(article).State = EntityState.Modified;
            blogContext.SaveChanges();
            return RedirectToAction("Article", "Admin");
        }
        public ActionResult ArticleDelete(int ? id)
        {
            int PcId = 0;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article = blogContext.Article.Find(id);
            PcId = Convert.ToInt32(article.PictureId);
            var picture = blogContext.Picture.Find(PcId);
            if (ModelState.IsValid)
            {
                blogContext.Picture.Remove(picture);
                blogContext.Article.Remove(article);
                blogContext.SaveChanges();
            }
            if (System.IO.File.Exists(Server.MapPath("~/Images/" + picture.Path)))
            {
                System.IO.File.Delete(Server.MapPath("~/Images/" + picture.Path));      
            }
            return RedirectToAction("Article");
        }


        public ActionResult Category()
        {
            return View(blogContext.Category.OrderByDescending(a => a.CategoryId).ToList());
        }
        public ActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CategoryAdd(Category category)
        {
            if (category != null)
            {
                blogContext.Category.Add(category);
                blogContext.SaveChanges();
            }
            return RedirectToAction("Category");
        }
        public ActionResult CategoryEdit(int ? id)
        {
            var category = blogContext.Category.Find(id);
            return View(category);
        }
        [HttpPost]
        public ActionResult CategoryEdit(Category category)
        {
            if (ModelState.IsValid)
            {
                blogContext.Entry(category).State = EntityState.Modified;
                blogContext.SaveChanges();
            }
            return RedirectToAction("Category");
        }
        public ActionResult CategoryDelete(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categori = blogContext.Category.Find(id);
            int ctId = categori.CategoryId;
            var article = blogContext.Article.Where(a => a.CategoryId == ctId).FirstOrDefault();
            if (article != null)
            { 
                ViewBag.Message = "Kategoriye bağlantılı makaleler var silmek için bağlantılı makaleleri siliniz";
                return View();
            }
            else
            {    
                if (ModelState.IsValid)
                {
                    blogContext.Category.Remove(categori);
                    blogContext.SaveChanges();
                }
                return RedirectToAction("Category");
            }    
        }
    }
}