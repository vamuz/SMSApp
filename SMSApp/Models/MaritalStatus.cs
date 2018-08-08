using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMSApp.Models
{
    using System.ComponentModel.DataAnnotations;

    public class MaritalStatus
    {
        public int Id { get; set; }
        
        public string MaritalStatusType { get; set; }
    }
}