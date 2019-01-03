using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SMSApp.Models;

namespace SMSApp.Controllers.api
{
    public class UserDetailsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        // POST api/<controller>
        public int Post([FromBody] SMSAppRegistration userData)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    if (userData.SMSAppRegistrationId == 0)
                    {
                        var newEntry = new SMSAppRegistration()
                        {
                            FullNames = userData.FullNames,
                            NationalIDNo = userData.NationalIDNo,
                            YearofBirth = userData.YearofBirth,
                            PhoneNo = userData.PhoneNo,
                            EmailAddress = userData.EmailAddress,
                            MaritalStatusId = userData.MaritalStatusId,
                            GenderId = userData.GenderId,
                            Occupation = userData.Occupation,
                            CountyId = userData.CountyId,
                            ConstituencyId = userData.ConstituencyId,
                            Location = userData.Location,
                            PWDCategoryId = userData.PWDCategoryId
                        };
                        db.SmsAppRegistration.Add(newEntry);
                        db.SaveChanges();
                    }
                    else
                    {
                        var _exists = db.SmsAppRegistration.SingleOrDefault(n =>
                            n.SMSAppRegistrationId == userData.SMSAppRegistrationId);
                        if (_exists != null)
                        {
                            _exists.FullNames = userData.FullNames;
                            _exists.NationalIDNo = userData.NationalIDNo;
                            _exists.YearofBirth = userData.YearofBirth;
                            _exists.PhoneNo = userData.PhoneNo;
                            _exists.EmailAddress = userData.EmailAddress;
                            _exists.MaritalStatusId = userData.MaritalStatusId;
                            _exists.GenderId = userData.GenderId;
                            _exists.Occupation = userData.Occupation;
                            _exists.CountyId = userData.CountyId;
                            _exists.ConstituencyId = userData.ConstituencyId;
                            _exists.Location = userData.Location;
                            _exists.PWDCategoryId = userData.PWDCategoryId;
                            db.SaveChanges();
                        }
                       

                    }
                }
            }
            return userData.SMSAppRegistrationId;
        }
        

    }


}


