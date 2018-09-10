using System.Collections.Generic;
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
        public HttpResponseMessage Post([FromBody] SMSAppRegistration userData)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
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
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}