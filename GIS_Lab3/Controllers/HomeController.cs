using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;

namespace GIS_Lab3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (RnBEntities rnb = new RnBEntities())
            {
                XmlDocument doc = new XmlDocument();
                var xml = rnb.Database.SqlQuery<string>("GetRnB").ToList();
                doc.LoadXml(xml[0].ToString());
                doc.Save(@"C:\Users\Glebovsky\Documents\Visual Studio 2017\Projects\GIS_Lab3\GIS_Lab3\test.xml");
            }
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult AddPlace()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult AddPlace(marker Marker)
        {
            if (!ModelState.IsValid)
            {
                return HttpNotFound();
            }
            using (RnBEntities db = new RnBEntities())
            {
                db.markers.Add(Marker);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            using (RnBEntities db = new RnBEntities())
            {
                marker marker = db.markers.Find(id);
                if (marker == null)
                    return HttpNotFound();
                return View(marker);
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(marker marker)
        {
            using (RnBEntities db = new RnBEntities())
            {
                if (!ModelState.IsValid)
                    return HttpNotFound();
                db.markers.Attach(marker);
                db.Entry(marker).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            using (RnBEntities db = new RnBEntities())
            {
                marker marker = db.markers.Find(id);
                if (marker == null)
                    return HttpNotFound();
                return View(marker);
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(marker marker)
        {
            using (RnBEntities db = new RnBEntities())
            {
                db.Entry(marker).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}