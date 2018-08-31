using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SMSApp.Models;
using System.IO;

namespace SMSApp.Controllers
{
    using System.Diagnostics.CodeAnalysis;

    [Authorize]
    public class SMSAppRegistrationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SMSAppRegistrations
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult Index()

        {
            var smsAppRegistration = db.SmsAppRegistration.Include(s => s.Constituency).Include(s => s.County).Include(s => s.Gender).Include(s => s.MaritalStatus).Include(s => s.PWDCategory);
            return View(smsAppRegistration);
        }

        // GET: SMSAppRegistrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSAppRegistration sMSAppRegistration = db.SmsAppRegistration.Find(id);
            if (sMSAppRegistration == null)
            {
                return HttpNotFound();
            }
            return View(sMSAppRegistration);
        }

        // GET: SMSAppRegistrations/Create
        public ActionResult Create()
        {
            ViewBag.ConstituencyId = new SelectList(db.Constituency, "Id", "ConstituencyName");
            ViewBag.CountyId = new SelectList(db.County, "Id", "CountyName");
            ViewBag.GenderId = new SelectList(db.Gender, "Id", "GenderType");
            ViewBag.MaritalStatusId = new SelectList(db.MaritalStatus, "Id", "MaritalStatusType");
            ViewBag.PWDCategoryId = new SelectList(db.PwdCategory, "Id", "PWDCategoryType");
            return View();
        }

        // POST: SMSAppRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1003:SymbolsMustBeSpacedCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1005:SingleLineCommentsMustBeginWithSingleSpace", Justification = "Reviewed. Suppression is OK here.")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( SMSAppRegistration sMSAppRegistration)
        {
            //[Bind(Include = "SMSAppRegistrationId,")]
            this.Validation(sMSAppRegistration);

            if (ModelState.IsValid)
            {
                var newSMSEntry =new SMSAppRegistration()
                                     {
                                         FullNames=sMSAppRegistration.FullNames,
                                         NationalIDNo=sMSAppRegistration.NationalIDNo,
                                         YearofBirth=sMSAppRegistration.YearofBirth,
                                         PhoneNo=sMSAppRegistration.PhoneNo,
                                         EmailAddress=sMSAppRegistration.EmailAddress,
                                         MaritalStatusId=sMSAppRegistration.MaritalStatusId,
                                         GenderId=sMSAppRegistration.GenderId,
                                         Occupation=sMSAppRegistration.Occupation,
                                         CountyId=sMSAppRegistration.CountyId,
                                         ConstituencyId=sMSAppRegistration.ConstituencyId,
                                         Location=sMSAppRegistration.Location,
                                         PWDCategoryId=sMSAppRegistration.PWDCategoryId
                                     };
                db.SmsAppRegistration.Add(newSMSEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConstituencyId = new SelectList(db.Constituency, "Id", "ConstituencyName", sMSAppRegistration.ConstituencyId);
            ViewBag.CountyId = new SelectList(db.County, "Id", "CountyName", sMSAppRegistration.CountyId);
            ViewBag.GenderId = new SelectList(db.Gender, "Id", "GenderType", sMSAppRegistration.GenderId);
            ViewBag.MaritalStatusId = new SelectList(db.MaritalStatus, "Id", "MaritalStatusType", sMSAppRegistration.MaritalStatusId);
            ViewBag.PWDCategoryId = new SelectList(db.PwdCategory, "Id", "PWDCategoryType", sMSAppRegistration.PWDCategoryId);
            return View(sMSAppRegistration);
        }

        private void Validation(SMSAppRegistration sMSAppRegistration)
        {
            if (sMSAppRegistration.CountyId == 1)
            {
                this.ModelState.AddModelError("", "Please Select the County");
            }

            if (sMSAppRegistration.ConstituencyId == 1)
            {
                this.ModelState.AddModelError("", "Please Select the Constituency");
            }

            if (sMSAppRegistration.MaritalStatusId == 1)
            {
                this.ModelState.AddModelError("", "Please Select the Marital Status");
            }

            if (sMSAppRegistration.GenderId == 1)
            {
                this.ModelState.AddModelError("", "Please Select the Gender");
            }

            if (sMSAppRegistration.PWDCategoryId == 1)
            {
                this.ModelState.AddModelError("", "Please Select the PWD Category");
            }
        }

        public ActionResult FillConstituency(int location)
        {
            var constituencies = db.Constituency.Where(c => c.CountyId == location);
            return Json(constituencies, JsonRequestBehavior.AllowGet);
        }

        // GET: SMSAppRegistrations/Edit/5
        //public JsonResult IsNationalIDAvailable(int NationalIDNo) 
        //{
        //    return Json(!this.db.SmsAppRegistration.Any(
        //                    NationalId => NationalId.NationalIDNo == NationalIDNo), JsonRequestBehavior.AllowGet);
        //}
        public ActionResult ValidateNationalID(int idno)
        {
            var nationalid = db.SmsAppRegistration.Where(n => n.NationalIDNo == idno);
            var status = false ;
            if (nationalid != null)
            {
                status = true;
             
            }

            return Json(status , JsonRequestBehavior.AllowGet);

        }
        public ActionResult ValidatePhoneNo(int Phone)
        {
            var phoneNo = db.SmsAppRegistration.Where(n => n.PhoneNo == Phone);
            var status = false;
            if (phoneNo != null)
            {
                status = true;

            }

            return Json(status, JsonRequestBehavior.AllowGet);

        }
     
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSAppRegistration sMSAppRegistration = db.SmsAppRegistration.Find(id);
            if (sMSAppRegistration == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConstituencyId = new SelectList(db.Constituency, "Id", "ConstituencyName", sMSAppRegistration.ConstituencyId);
            ViewBag.CountyId = new SelectList(db.County, "Id", "CountyName", sMSAppRegistration.CountyId);
            ViewBag.GenderId = new SelectList(db.Gender, "Id", "GenderType", sMSAppRegistration.GenderId);
            ViewBag.MaritalStatusId = new SelectList(db.MaritalStatus, "Id", "MaritalStatusType", sMSAppRegistration.MaritalStatusId);
            ViewBag.PWDCategoryId = new SelectList(db.PwdCategory, "Id", "PWDCategoryType", sMSAppRegistration.PWDCategoryId);
            return View(sMSAppRegistration);
        }

        // POST: SMSAppRegistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SMSAppRegistration sMSAppRegistration)
        {
            this.Validation(sMSAppRegistration);
            if (ModelState.IsValid)
            {

                db.Entry(sMSAppRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConstituencyId = new SelectList(db.Constituency, "Id", "ConstituencyName", sMSAppRegistration.ConstituencyId);
            ViewBag.CountyId = new SelectList(db.County, "Id", "CountyName", sMSAppRegistration.CountyId);
            ViewBag.GenderId = new SelectList(db.Gender, "Id", "GenderType", sMSAppRegistration.GenderId);
            ViewBag.MaritalStatusId = new SelectList(db.MaritalStatus, "Id", "MaritalStatusType", sMSAppRegistration.MaritalStatusId);
            ViewBag.PWDCategoryId = new SelectList(db.PwdCategory, "Id", "PWDCategoryType", sMSAppRegistration.PWDCategoryId);
            return View(sMSAppRegistration);
        }

        // GET: SMSAppRegistrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SMSAppRegistration sMSAppRegistration = db.SmsAppRegistration.Find(id);
            if (sMSAppRegistration == null)
            {
                return HttpNotFound();
            }
            return View(sMSAppRegistration);
        }

        // POST: SMSAppRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SMSAppRegistration sMSAppRegistration = db.SmsAppRegistration.Find(id);
            db.SmsAppRegistration.Remove(sMSAppRegistration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    var filename = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    file.SaveAs(path);
                }

                ViewBag.Message = "File Uploaded Successfully";
                return View();
            }
            catch
            {
                ViewBag.Message = "File Not Uploaded Successfully";
                return View();
            }

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
