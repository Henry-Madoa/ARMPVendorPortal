using ARMPVendorPortal.Components;
using ARMPVendorPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ARMPVendorPortal.Controllers
{
    public class VendorRegController : ApiController
    {
        WebServices ws = new WebServices();
        // GET api/<controller>
        public IEnumerable<VendorRegWebReference.VendorReg> Get()
        {
            ws.VendorRegWebService();

            List<VendorRegWebReference.VendorReg_Filter> filter = new List<VendorRegWebReference.VendorReg_Filter>();
            VendorRegWebReference.VendorReg[] vendorReglist = ws.vendorReg_ws.ReadMultiple(filter.ToArray(), bookmarkKey: null, setSize: 0);
            return vendorReglist.ToList();
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string id)
        {
            try
            {
                ws.VendorRegWebService();

                VendorRegWebReference.VendorReg vendorReg = new VendorRegWebReference.VendorReg();
                vendorReg = ws.vendorReg_ws.Read(id);

                if (vendorReg != null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            status = 200,
                            data = new { vendorReg }
                        }));
                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound,
                        new
                        {
                            status = 404,
                            message = "The Vendor Registration Request No. Do not exist"
                        }));
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest,
                    new
                    {
                        status = 400,
                        message = ex.Message,
                        details = ex.InnerException
                    }));

            }
        }
        // POST api/<controller>
        public IHttpActionResult Post([FromBody] VendorRegModel vendorRegModel)
        {
            if (vendorRegModel.companyName == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest,
                    new
                    {
                        status = 400,
                        message = "CompanyName Field Cannot be null"
                    }));
            }
            else if (vendorRegModel.vatRegNo == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest,
                    new
                    {
                        status = 400,
                        message = "VAT Registration No. Field Cannot be null"
                    }));
            }
            else if (vendorRegModel.taxIdNumber == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest,
                    new
                    {
                        status = 400,
                        message = "Tax ID Number Field Cannot be null"
                    }));
            }
            else if (vendorRegModel.companyDateRegDate == default(DateTime))
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest,
                    new
                    {
                        status = 400,
                        message = "Company Date Registration Date Field Cannot be null"
                    }));
            }
            else if (vendorRegModel.certOfIncorporationNo == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest,
                    new
                    {
                        status = 400,
                        message = "Certificate Of Incorporation No. Field Cannot be null"
                    }));
            }
            else
            {
                ws.VendorRegWebService();

                List<VendorRegWebReference.VendorReg_Filter> filterArray = new List<VendorRegWebReference.VendorReg_Filter>();
                VendorRegWebReference.VendorReg_Filter vendorReg_Filter = new VendorRegWebReference.VendorReg_Filter();
                vendorReg_Filter.Field = VendorRegWebReference.VendorReg_Fields.Cert_Of_Incorporation_No;
                vendorReg_Filter.Criteria = vendorRegModel.certOfIncorporationNo;
                filterArray.Add(vendorReg_Filter);

                VendorRegWebReference.VendorReg[] vendorReg = ws.vendorReg_ws.ReadMultiple(filterArray.ToArray(), null, 0);
                int count = vendorReg.Count();

                if (count != 0)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new
                        {
                            status = 400,
                            message = "Company With the same Certificate Of Incorporation No " + vendorRegModel.certOfIncorporationNo + "Exists",
                            data = new
                            {
                                requestNo = vendorReg[0].No,
                                companyName = vendorReg[0].Company_Name
                            }
                        }));
                }
                else if (vendorRegModel.regOfficeAddress == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest,
                        new
                        {
                            status = 400,
                            message = "Registration Of Office Address Field Cannot be null"
                        }));
                }
                else if (vendorRegModel.state == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest,
                        new
                        {
                            status = 400,
                            message = "State Field Cannot be null"
                        }));
                }
                else if (vendorRegModel.emailAddress == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest,
                        new
                        {
                            status = 400,
                            message = "Email Address Field Cannot be null"
                        }));
                }
                else if (vendorRegModel.phoneNumber == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest,
                        new
                        {
                            status = 400,
                            message = "Phone Number Field Cannot be null"
                        }));
                }
                else
                {
                    VendorRegWebReference.VendorReg vendor_Reg = new VendorRegWebReference.VendorReg();
                    ws.vendorReg_ws.Create(ref vendor_Reg);
                    vendor_Reg.Company_Name = vendorRegModel.companyName;
                    vendor_Reg.VAT_Reg_No = vendorRegModel.vatRegNo;
                    vendor_Reg.Tax_Id_Number = vendorRegModel.taxIdNumber;
                    vendor_Reg.Company_Date_Reg_Date = vendorRegModel.companyDateRegDate;
                    vendor_Reg.Company_Date_Reg_DateSpecified = true;
                    vendor_Reg.Cert_Of_Incorporation_No = vendorRegModel.certOfIncorporationNo;
                    vendor_Reg.Reg_Office_Address = vendorRegModel.regOfficeAddress;
                    vendor_Reg.State = vendorRegModel.state;
                    vendor_Reg.Email_Address = vendorRegModel.emailAddress;
                    vendor_Reg.Phone_Number = vendorRegModel.phoneNumber;
                    if (vendorRegModel.memArticlesOfAssocLink != null)
                    {
                        vendor_Reg.Mem_Articles_Of_Assoc_Link = vendorRegModel.memArticlesOfAssocLink;
                    }
                    if (vendorRegModel.certOfIncorporationLink != null)
                    {
                        vendor_Reg.Cert_Of_Incorporation_Link = vendorRegModel.certOfIncorporationLink;
                    }
                    if (vendorRegModel.cO7PartOfDirectorsLink != null)
                    {
                        vendor_Reg.CO7_Part_Of_Directors_Link = vendorRegModel.cO7PartOfDirectorsLink;
                    }
                    if (vendorRegModel.vatRegistrationCertLink != null)
                    {
                        vendor_Reg.VAT_Registration_Cert_Link = vendorRegModel.vatRegistrationCertLink;
                    }
                    if (vendorRegModel.taxClearanceCertLink != null)
                    {
                        vendor_Reg.Tax_Clearance_Cert_Link = vendorRegModel.taxClearanceCertLink;
                    }
                    ws.vendorReg_ws.Update(ref vendor_Reg);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            status = 201,
                            message = "Vendor Registration Request have been Successfully Uploaded to Business Central Application",
                            data =
                            new
                            {
                                requestNo = vendor_Reg.No
                            }
                        }));
                }
            }
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(string id, [FromBody] VendorRegModel vendorRegModel)
        {

            ws.VendorRegWebService();
            try
            {
                VendorRegWebReference.VendorReg vendor_Reg = new VendorRegWebReference.VendorReg();
                vendor_Reg = ws.vendorReg_ws.Read(id);

                if (vendor_Reg == null)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound,
                           new
                           {
                               status = 404,
                               message = "Vendor Registration Request No " + id + " do not exist"
                           }));
                }
                else
                {
                    if (vendorRegModel.companyName != null)
                    {
                        vendor_Reg.Company_Name = vendorRegModel.companyName;
                    }

                    if (vendorRegModel.vatRegNo != null)
                    {
                        vendor_Reg.VAT_Reg_No = vendorRegModel.vatRegNo;
                    }
                    if (vendorRegModel.taxIdNumber != null)
                    {
                        vendor_Reg.Tax_Id_Number = vendorRegModel.taxIdNumber;
                    }
                    if (vendorRegModel.companyDateRegDate != default(DateTime))
                    {
                        vendor_Reg.Company_Date_Reg_Date = vendorRegModel.companyDateRegDate;
                        vendor_Reg.Company_Date_Reg_DateSpecified = true;
                    }
                    if (vendorRegModel.certOfIncorporationNo != null)
                    {
                        vendor_Reg.Cert_Of_Incorporation_No = vendorRegModel.certOfIncorporationNo;
                    }
                    if (vendorRegModel.regOfficeAddress!= null)
                    {
                        vendor_Reg.Reg_Office_Address = vendorRegModel.regOfficeAddress;
                    }
                    if (vendorRegModel.state != null)
                    {
                        vendor_Reg.State = vendorRegModel.state;
                    }
                    if (vendorRegModel.emailAddress != null)
                    {
                        vendor_Reg.Email_Address = vendorRegModel.emailAddress;
                    }
                    if (vendorRegModel.phoneNumber != null)
                    {
                        vendor_Reg.Phone_Number = vendorRegModel.phoneNumber;
                    }
                    if (vendorRegModel.memArticlesOfAssocLink != null)
                    {
                        vendor_Reg.Mem_Articles_Of_Assoc_Link = vendorRegModel.memArticlesOfAssocLink;
                    }
                    if (vendorRegModel.certOfIncorporationLink != null)
                    {
                        vendor_Reg.Cert_Of_Incorporation_Link = vendorRegModel.certOfIncorporationLink;
                    }
                    if (vendorRegModel.cO7PartOfDirectorsLink != null)
                    {
                        vendor_Reg.CO7_Part_Of_Directors_Link = vendorRegModel.cO7PartOfDirectorsLink;
                    }
                    if (vendorRegModel.vatRegistrationCertLink != null)
                    {
                        vendor_Reg.VAT_Registration_Cert_Link = vendorRegModel.vatRegistrationCertLink;
                    }
                    if (vendorRegModel.taxClearanceCertLink != null)
                    {
                        vendor_Reg.Tax_Clearance_Cert_Link = vendorRegModel.taxClearanceCertLink;
                    }

                    ws.vendorReg_ws.Update(ref vendor_Reg);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            status = 201,
                            message = "Vendor Registration Request have been Successfully Updated to Business Central Application"
                        }));
                }
            }

            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest,
                    new
                    {
                        status = 400,
                        message = ex.Message
                    }));
            }
        }
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}