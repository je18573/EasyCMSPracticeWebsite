using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS_Practice.Models;
using System.Net;

namespace CMS_Practice.Controllers
{
    public class HomeController : Controller
    {
        private CMSDatabaseEntities db = new CMSDatabaseEntities();

        public ActionResult Index()
        {
            var model = (from item in db.Article
                        where item.IsPublich == true
                        orderby item.PublishDate descending
                        select item).ToList();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "這是一個簡易的CMS練習網站，僅提供以下功能：";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "我的連絡資訊如下：";

            return View();
        }
    }
}