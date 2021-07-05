using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ARMPVendorPortal.Components
{
    public class Globals
    {
        private static string CompanyName = "COMPANY";
        private static string BaseUrl = "http://192.168.0.52:7047/BC180/WS/" + CompanyName + "/Page/";
        public String VendorReg_Url = BaseUrl + "VendorReg";
        public String UserName = "THL";
        public String Password = "T@sting1";
    }
}