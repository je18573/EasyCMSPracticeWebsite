using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS_Practice.Models;
using Microsoft.Security.Application;
using PagedList;
using Microsoft.AspNet.Identity;
namespace CMS_Practice.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        private CMSDatabaseEntities db = new CMSDatabaseEntities();

        // GET: /Admin/Articles/
        public ActionResult Index(string searchTxt, int page = 1, int pageSize = 5)
        {
            Guid id = new Guid(User.Identity.GetUserId());
            var model = db.Article.AsQueryable().Where(d=>d.CreateUser == id || d.UpdateUser == id);
            if (string.IsNullOrWhiteSpace(searchTxt) == false)
            {
                model = model.Where(d => d.Subject.Contains(searchTxt) ||
                                         d.Summary.Contains(searchTxt));
            }
            var result = model.OrderBy(x => x.CreateDate).ToPagedList(page, pageSize);
            return View(result);
        }
        

        // GET: /Admin/Articles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: /Admin/Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Articles/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Subject,Summary,ContentText,IsPublich,PublishDate,ViewCount,CreateUser,CreateDate,UpdateUser,UpdateDate")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.ContentText = Sanitizer.GetSafeHtmlFragment(article.ContentText);
                article.ViewCount = 0;
                article.CreateDate = DateTime.Now;
                article.CreateUser = new Guid(User.Identity.GetUserId());
                article.UpdateDate = DateTime.Now;
                article.UpdateUser = new Guid(User.Identity.GetUserId());

                article.ID = Guid.NewGuid();
                db.Article.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: /Admin/Articles/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: /Admin/Articles/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Subject,Summary,ContentText,IsPublich,PublishDate,ViewCount,CreateUser,CreateDate,UpdateUser,UpdateDate")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.ContentText = Sanitizer.GetSafeHtmlFragment(article.ContentText);
                article.UpdateDate = DateTime.Now;
                article.UpdateUser = new Guid(User.Identity.GetUserId());
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: /Admin/Articles/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: /Admin/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Article article = db.Article.Find(id);
            db.Article.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
