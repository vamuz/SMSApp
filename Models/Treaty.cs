using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SMSApp.Models
{
    [Table("pTreaty")]
    public class pTreaty
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string TreatyName { get; set; }
    }
}