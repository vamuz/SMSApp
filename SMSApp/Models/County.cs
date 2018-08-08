using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMSApp.Models
{
    using System.ComponentModel.DataAnnotations;

    public class County
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string CountyName { get; set; }
    }
}