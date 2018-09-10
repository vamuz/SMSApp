using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMSApp.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Constituency
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string ConstituencyName { get; set; }

        public int CountyId { get; set; }
    }
}