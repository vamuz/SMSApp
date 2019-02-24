using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SMSApp.Models
{
    [Table("pSupervisoryBody")]
    public class pSupervisoryBody
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string SupervisoryBodyName { get; set; }
    }
}