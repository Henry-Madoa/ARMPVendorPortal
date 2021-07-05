using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ARMPVendorPortal.Components
{
    public class WebServices
    {
        Globals globals = new Globals();

        public VendorRegWebReference.VendorReg_Service vendorReg_ws = new VendorRegWebReference.VendorReg_Service();
       
        public NetworkCredential Credential()
        {
            NetworkCredential cred = new NetworkCredential();
            cred.UserName = globals.UserName;
            cred.Password = globals.Password;

            return cred;
        }

        public void VendorRegWebService()
        {
            vendorReg_ws.Url = globals.VendorReg_Url;
            vendorReg_ws.UseDefaultCredentials = false;
            vendorReg_ws.Credentials = Credential();
        }
        
    }
}