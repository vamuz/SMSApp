﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using RestSharp;
using SMSApp.Models;
using WebGrease.Css.Ast.Selectors;

namespace SMSApp.Controllers
{
    using System.Diagnostics.CodeAnalysis;

    [Authorize]
    public class SMSAppRegistrationsController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        // GET: SMSAppRegistrations
        //public ActionResult Index()

        //{
        //    //var smsAppRegistration = db.SmsAppRegistration.Include(s => s.Constituency).Include(s => s.County)
        //    //    .Include(s => s.Gender).Include(s => s.MaritalStatus).Include(s => s.PWDCategory);

        //    return View();
        //}

        public ActionResult Index()

        {
            //var smsAppRegistration = db.SmsAppRegistration.Include(s => s.Constituency).Include(s => s.County).Include(s => s.Gender).Include(s => s.MaritalStatus).Include(s => s.PWDCategory);
            //return View(smsAppRegistration);
            ViewBag.ConstituencyId = new SelectList(db.Constituency, "Id", "ConstituencyName");
            ViewBag.CountyId = new SelectList(db.County, "Id", "CountyName");
            ViewBag.GenderId = new SelectList(db.Gender, "Id", "GenderType");
            ViewBag.MaritalStatusId = new SelectList(db.MaritalStatus, "Id", "MaritalStatusType");
            ViewBag.PWDCategoryId = new SelectList(db.PwdCategory, "Id", "PWDCategoryType");
            return View();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create( SMSAppRegistration sMSAppRegistration)
        //{
        //    //[Bind(Include = "SMSAppRegistrationId,")]
        //    this.Validation(sMSAppRegistration);

        //    if (ModelState.IsValid)
        //    {
        //        var newSMSEntry =new SMSAppRegistration()
        //                             {
        //                                 FullNames=sMSAppRegistration.FullNames,
        //                                 NationalIDNo=sMSAppRegistration.NationalIDNo,
        //                                 YearofBirth=sMSAppRegistration.YearofBirth,
        //                                 PhoneNo=sMSAppRegistration.PhoneNo,
        //                                 EmailAddress=sMSAppRegistration.EmailAddress,
        //                                 MaritalStatusId=sMSAppRegistration.MaritalStatusId,
        //                                 GenderId=sMSAppRegistration.GenderId,
        //                                 Occupation=sMSAppRegistration.Occupation,
        //                                 CountyId=sMSAppRegistration.CountyId,
        //                                 ConstituencyId=sMSAppRegistration.ConstituencyId,
        //                                 Location=sMSAppRegistration.Location,
        //                                 PWDCategoryId=sMSAppRegistration.PWDCategoryId
        //                             };
        //        db.SmsAppRegistration.Add(newSMSEntry);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ConstituencyId = new SelectList(db.Constituency, "Id", "ConstituencyName", sMSAppRegistration.ConstituencyId);
        //    ViewBag.CountyId = new SelectList(db.County, "Id", "CountyName", sMSAppRegistration.CountyId);
        //    ViewBag.GenderId = new SelectList(db.Gender, "Id", "GenderType", sMSAppRegistration.GenderId);
        //    ViewBag.MaritalStatusId = new SelectList(db.MaritalStatus, "Id", "MaritalStatusType", sMSAppRegistration.MaritalStatusId);
        //    ViewBag.PWDCategoryId = new SelectList(db.PwdCategory, "Id", "PWDCategoryType", sMSAppRegistration.PWDCategoryId);
        //    return View(sMSAppRegistration);
        //}

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
            var status = false;
            if (nationalid != null)
            {
                status = true;

            }

            return Json(status, JsonRequestBehavior.AllowGet);

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

        public ActionResult County()
        {
            var county = db.County;

            return Json(county, JsonRequestBehavior.AllowGet);

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

            ViewBag.ConstituencyId =
                new SelectList(db.Constituency, "Id", "ConstituencyName", sMSAppRegistration.ConstituencyId);
            ViewBag.CountyId = new SelectList(db.County, "Id", "CountyName", sMSAppRegistration.CountyId);
            ViewBag.GenderId = new SelectList(db.Gender, "Id", "GenderType", sMSAppRegistration.GenderId);
            ViewBag.MaritalStatusId = new SelectList(db.MaritalStatus, "Id", "MaritalStatusType",
                sMSAppRegistration.MaritalStatusId);
            ViewBag.PWDCategoryId =
                new SelectList(db.PwdCategory, "Id", "PWDCategoryType", sMSAppRegistration.PWDCategoryId);
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

            ViewBag.ConstituencyId =
                new SelectList(db.Constituency, "Id", "ConstituencyName", sMSAppRegistration.ConstituencyId);
            ViewBag.CountyId = new SelectList(db.County, "Id", "CountyName", sMSAppRegistration.CountyId);
            ViewBag.GenderId = new SelectList(db.Gender, "Id", "GenderType", sMSAppRegistration.GenderId);
            ViewBag.MaritalStatusId = new SelectList(db.MaritalStatus, "Id", "MaritalStatusType",
                sMSAppRegistration.MaritalStatusId);
            ViewBag.PWDCategoryId =
                new SelectList(db.PwdCategory, "Id", "PWDCategoryType", sMSAppRegistration.PWDCategoryId);
            return View(sMSAppRegistration);
        }

        [HttpPost]
        public void Upload()
        {

            var idno = Request.Form["idno"];

            var nationalId = Int64.Parse(idno);

            var nationalid = db.SmsAppRegistration.SingleOrDefault(n => n.NationalIDNo == nationalId);

            if (nationalid != null)
            {
                foreach (string file in Request.Files)
                {

                    var fileContent = Request.Files[file];

                    if (fileContent != null && fileContent.ContentLength > 0)
                    {

                        {
                            var id = nationalid.SMSAppRegistrationId + ".pdf";


                            //var fileName = Path.GetFileName(fileContent.FileName);
                            if (id != null)
                            {
                                var path = Path.Combine(Server.MapPath("~/ScannedFiles"), id);
                                fileContent.SaveAs(path);
                            }
                        }

                    }
                }
            }

            //return Json(Index());
            Response.StatusCode = (int) HttpStatusCode.BadRequest;

            //    return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);

            //    //Json("File uploaded successfully");
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult GetDetails()
            //public ActionResult GetUserDetail(int SMSAppRegistrationID)
        {
            try
            {
                var user = from o in db.SmsAppRegistration
                    //where o.SMSAppRegistrationId == SMSAppRegistrationID
                    select new SMSAppRegistration()
                    {
                        FullNames = o.FullNames,
                        NationalIDNo = o.NationalIDNo,
                        YearofBirth = o.YearofBirth,
                        Gender = o.Gender,
                        PhoneNo = o.PhoneNo,
                        Occupation = o.Occupation,
                        Location = o.Location,
                        County = o.County,
                        Constituency = o.Constituency,
                        PWDCategory = o.PWDCategory,
                    };
                return Json(user.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }


            return Json("");

        }

        public ActionResult GetUserDetails()
        {
            var smsAppRegistration = db.SmsAppRegistration
                .Select(
                    o => new
                    {
                        id = o.SMSAppRegistrationId,
                        FullNames = o.FullNames,
                        NationalIDNo = o.NationalIDNo,
                        YearofBirth = o.YearofBirth,
                        GenderType = o.Gender.GenderType,
                        MaritalStatusType = o.MaritalStatus.MaritalStatusType,
                        PhoneNo = o.PhoneNo,
                        Occupation = o.Occupation,
                        Location = o.Location,
                        CountyName = o.County.CountyName,
                        EmailAddress = o.EmailAddress,
                        ConstituencyName = o.Constituency.ConstituencyName,
                        PWDCategoryType = o.PWDCategory.PWDCategoryType,


                    }
                );
            //.Include(s => s.Constituency).Include(s => s.County)
            //.Include(s => s.Gender).Include(s => s.MaritalStatus).Include(s => s.PWDCategory);

            return Json(smsAppRegistration, JsonRequestBehavior.AllowGet);

        }

        public ActionResult EditSmsDetails(int id)
        {
            try
            {
                var _getDetails = db.SmsAppRegistration
                    .SingleOrDefault(n => n.SMSAppRegistrationId == id);

                return Json(_getDetails, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }



        }

        public ActionResult DeleteSmsDetails(int id)

        {
            var deleteDetails = db.SmsAppRegistration
                .SingleOrDefault(n => n.SMSAppRegistrationId == id);
            if (deleteDetails != null)
            {
                db.SmsAppRegistration.Remove(deleteDetails);
                db.SaveChanges();
                return Json(deleteDetails, JsonRequestBehavior.AllowGet);
            }

            return Json("");
        }

        public ActionResult SendSMS()
        {
            ViewBag.ConstituencyId =
                new SelectList(db.Constituency.Where(c => c.ConstituencyName != "Select Constituency"),
                    "ConstituencyName", "ConstituencyName");
            ViewBag.CountyId = new SelectList(db.County.Where(c => c.CountyName != "Select County"), "CountyName",
                "CountyName ");
            ViewBag.GenderId = new SelectList(db.Gender.Where(g => g.GenderType != "Select Gender"), "GenderType",
                "GenderType");
            ViewBag.MaritalStatusId =
                new SelectList(db.MaritalStatus.Where(m => m.MaritalStatusType != "Select Marital Status"),
                    "MaritalStatusType", "MaritalStatusType");
            ViewBag.PWDCategoryId = new SelectList(db.PwdCategory.Where(p => p.PWDCategoryType != "Select Disability"),
                "PWDCategoryType", "PWDCategoryType");

            return View();
        }

        public ActionResult FetchData(string[] County, string[] Constituency, string[] Gender, string[] MaritalStatus,
            string[] PWDCategory)
        {
            string CountyList = string.Join(",", County);
            string ConstituencyList = string.Join(",", Constituency);
            string GenderList = string.Join(",", Gender);
            string MaritalStatusList = string.Join(",", MaritalStatus);
            string PWDCategoryList = string.Join(",", PWDCategory);

            var details = db.SmsAppRegistration.Where(d => CountyList.Contains(d.County.CountyName)
                                                           && ConstituencyList.Contains(d.Constituency.ConstituencyName)
                                                           && GenderList.Contains(d.Gender.GenderType)
                                                           && MaritalStatusList.Contains(d.MaritalStatus
                                                               .MaritalStatusType)
                                                           && PWDCategoryList.Contains(d.PWDCategory.PWDCategoryType)
                )
                .Select(n => new
                {
                    fullnames = n.FullNames,
                    nationalidno = n.NationalIDNo,
                    yearofbirth = n.YearofBirth,
                    gendertype = n.Gender.GenderType,
                    maritalstatustype = n.MaritalStatus.MaritalStatusType,
                    phoneno = n.PhoneNo,
                    constituencyname = n.Constituency.ConstituencyName,
                    countyname = n.County.CountyName,
                    pwdcategorytype = n.PWDCategory.PWDCategoryType,
                    occupation = n.Occupation,
                    location = n.Location,
                    emailaddress = n.EmailAddress,
                }).ToList();

            return Json(details, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PushSMS(string[] County, string[] Constituency, string[] Gender, string[] MaritalStatus,
            string[] PWDCategory)
            {
            string CountyList = string.Join(",", County);
            string ConstituencyList = string.Join(",", Constituency);
            string GenderList = string.Join(",", Gender);
            string MaritalStatusList = string.Join(",", MaritalStatus);
            string PWDCategoryList = string.Join(",", PWDCategory);

            var details = db.SmsAppRegistration.Where(d => CountyList.Contains(d.County.CountyName)
                                                           && ConstituencyList.Contains(d.Constituency.ConstituencyName)
                                                           && GenderList.Contains(d.Gender.GenderType)
                                                           && MaritalStatusList.Contains(d.MaritalStatus
                                                               .MaritalStatusType)
                                                           && PWDCategoryList.Contains(d.PWDCategory.PWDCategoryType)
                )
                .Select(n => new
                {
                    fullnames = n.FullNames,
                    nationalidno = n.NationalIDNo,
                    yearofbirth = n.YearofBirth,
                    gendertype = n.Gender.GenderType,
                    maritalstatustype = n.MaritalStatus.MaritalStatusType,
                    phoneno = n.PhoneNo,
                    constituencyname = n.Constituency.ConstituencyName,
                    countyname = n.County.CountyName,
                    pwdcategorytype = n.PWDCategory.PWDCategoryType,
                    occupation = n.Occupation,
                    location = n.Location,
                    emailaddress = n.EmailAddress,
                }).ToList();

            foreach (var item in details)
            {
                var client = new RestClient("http://107.20.199.106/restapi/sms/1/text/single");

                var phone = item.phoneno.ToString();
                if (phone.Length == 10)
                    phone = "254" + phone.Substring(1, 9);
                else
                    phone = "254" + phone;

                var request = new RestRequest(Method.POST);
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "Basic TWFrYXVBZ25lczpXb1JkKjIwMTY==");
                request.AddParameter("application/json", "{\"from\":\"KNCHR\", \"to\":" +
                                                         phone+",\"text\":\"Test SMS.\"}",
                    ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                //if (response.StatusCode == HttpStatusCode.OK)
                //    return "Ok";
                //else
                //    return response.ErrorMessage;
            }

            return Json("");
        }
       

    }
}

