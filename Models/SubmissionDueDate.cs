using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SMSApp.Models
{
    [Table("pSubmissionDueDate")]
    public class pSubmissionDueDate
    {
        public int Id { get; set; }
        [Required]
        public DateTime DueDate { get; set; }

        public pReportingCompliance ReportingCompliance { get; set; }

        public int ReportingComplianceId { get; set; }
    }
}