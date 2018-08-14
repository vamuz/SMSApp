using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMSApp.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class SMSAppRegistration
    {

        public int SMSAppRegistrationId { get; set; }

        [Required]
        [StringLength(150,MinimumLength = 4)]
        public string FullNames { get; set; }

        [Required]
       [MinLength(5)]
        //[Remote("IsNationalIDAvailable","SMSAppRegistrations",ErrorMessage = "National ID No already exists")]
        public int NationalIDNo { get; set; }

        [Required]
        [Range(1900, 2000, ErrorMessage = "Invalid Year of Birth")]
        public int YearofBirth { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Range(700000000, 7999999999, ErrorMessage = "Invalid Mobile No.Mobile No. should be in this format 7xxxxxxxx")]
        public int PhoneNo { get; set; }

        [StringLength(150)]
        public string EmailAddress { get; set; }
        
        public MaritalStatus MaritalStatus { get; set; }
        [Required]
        public int MaritalStatusId { get; set; }

        public Gender Gender { get; set; }
        [Required]
        public int GenderId { get; set; }
        [StringLength(150)]
        public string Occupation { get; set; }

        public County County { get; set; }
        [Required]
        public int CountyId { get; set; }

        public Constituency Constituency { get; set; }
        [Required]
        public int ConstituencyId { get; set; }
        [StringLength(150)]
        public string Location { get; set; }
        [Required]
        public int PWDCategoryId { get; set; }

        public PWDCategory PWDCategory { get; set; }


    }
}