using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMSApp.Models;

namespace SMSApp.Controllers
{
    public class ReportingComplianceController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        // GET: ReportingCompliance
        public ActionResult Index()
        {
            ViewBag.TreatyId = new SelectList(db.pTreaty, "Id", "TreatyName");
            ViewBag.SupervisoryBodyId = new SelectList(db.pSupervisoryBody, "Id", "SupervisoryBodyName");
            ViewBag.ArticleId = new SelectList(db.pArticle, "Id", "ArticleName");
            return View();
        }

        // GET: ReportingCompliance/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReportingCompliance/Create
        public ActionResult Create()
        {
            ViewBag.TreatyId = new SelectList(db.pTreaty, "Id", "TreatyName");
            ViewBag.SupervisoryBodyId = new SelectList(db.pSupervisoryBody, "Id", "SupervisoryBodyName");
            ViewBag.ArticleId = new SelectList(db.pArticle, "Id", "ArticleName");
            
            return View();
        }

        // POST: ReportingCompliance/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ReportingCompliance/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReportingCompliance/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ReportingCompliance/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReportingCompliance/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
