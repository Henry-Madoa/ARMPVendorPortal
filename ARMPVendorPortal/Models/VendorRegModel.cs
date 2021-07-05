using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARMPVendorPortal.Models
{
    public class VendorRegModel
    {
        public string companyName { set; get; }
        public string vatRegNo { set; get; }
        public string taxIdNumber { set; get; }
        public System.DateTime companyDateRegDate { set; get; }
        public string certOfIncorporationNo { set; get; }
        public string regOfficeAddress { set; get; }
        public string state { set; get; }
        public string emailAddress { set; get; }
        public string phoneNumber { set; get; }
        public string memArticlesOfAssocLink { set; get; }
        public string certOfIncorporationLink { set; get; }
        public string cO7PartOfDirectorsLink { set; get; }
        public string vatRegistrationCertLink { set; get; }
        public string taxClearanceCertLink { set; get; }
    }
}