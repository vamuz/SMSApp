using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SMSApp.Models
{
    [Table("pReportingCompliance")]
    public class pReportingCompliance
    {
        public int Id { get; set; }
        public pTreaty Treaty { get; set; }
        [Required]
        public int TreatyId { get; set; }
        public pSupervisoryBody SupervisoryBody { get; set; }
        [Required]
        public int SupervisoryBodyId { get; set; }
        public pArticle Article { get; set; }
        [Required]
        public int ArticleId { get; set; }
        [Required]
        public string InitialReportDueDate { get; set; }
        [Required]
        public int PeriodicReportDueDate { get; set; }
        [StringLength(150)]
        public string GeneralComments { get; set; }

    }
}