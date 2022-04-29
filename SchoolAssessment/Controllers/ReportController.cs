using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.SessionState;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


/*
 * 08/06/2019 AT 
 * Added Varicella doses for 7th grade  
 * 
 * 08/13/2019
 * Made pdf read-only
 *
 */

namespace SchoolAssessment.Controllers
{
    public class ReportController : ApiController
    {


        #region "[Methods]"


        public static void GenerateSchoolSummaryPDF(string filePath, SqlDataReader dr, string temp, string dir)
        {
            DataTable dt = new DataTable();
            string PdfTemplate = temp;
            PdfReader pdfReader = new PdfReader(PdfTemplate);
            int pbe = 0, independentStudy = 0, homeBasedPrivate = 0;

            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }

            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(filePath, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;
            string schtype = string.Empty;

            try
            {
                dr.Read();
                var _with1 = pdfFormFields;
                schtype = dr["SchType"].ToString();
                

                if ((schtype == "PUBLIC"))
                {
                    _with1.SetField("Public", "On");
                }
                if ((schtype == "PRIVATE"))
                {
                    _with1.SetField("Private", "On");
                }
                if ((schtype == "HEAD START"))
                {
                    _with1.SetField("HeadStart", "On");
                }
                

                _with1.SetField("FacilityName", dr["SchName"].ToString());
                _with1.SetField("SchCode", dr["SchCode"].ToString());
                _with1.SetField("schtype", dr["SchType"].ToString());
                _with1.SetField("County", dr["CoName"].ToString());
                
                _with1.SetField("ContactPerson", dr["ContactPerson"].ToString());
                _with1.SetField("ContactEmail", dr["ContactEmail"].ToString());
                _with1.SetField("ContactPhone", dr["ContactPhone"].ToString().Substring(0, 3) + '-' + dr["ContactPhone"].ToString().Substring(3, 3) + '-' + dr["ContactPhone"].ToString().Substring(6, 4));
                //_with1.SetField("ContactPhoneAreaCode", dr["ContactPhone"].ToString().Substring(0, 3));

                _with1.SetField("ReportedPerson", dr["ReportedPerson"].ToString());
                _with1.SetField("ReportedEmail", dr["ReportedEmail"].ToString());
                _with1.SetField("ReportedPhone", dr["ReportedPhone"].ToString().Substring(0,3) + '-' + dr["ReportedPhone"].ToString().Substring(3, 3) + '-' + dr["ReportedPhone"].ToString().Substring(6, 4));
                //_with1.SetField("ReportedPhone", dr["ReportedPhone"].ToString().Substring(4, 8));
                //_with1.SetField("ReportedPhoneExt", dr["ReportedPhoneExt"].ToString().Substring(0, 3));
                _with1.SetField("SubmitDate", Convert.ToDateTime(dr["SubmitDate"]).ToString("yyyy-MM-dd"));
                _with1.SetField("TotNo", dr["TotNo"].ToString());
                _with1.SetField("AllImm", dr["AllImm"].ToString());

                // Only in Childcare
                if (dr["BeleExmp"] == System.DBNull.Value)
                {
                    pbe = 0;
                    _with1.SetField("BeleExmp", "0");

                } else
                {
                    pbe = Convert.ToInt32(dr["BeleExmp"]);
                    _with1.SetField("BeleExmp", (dr["BeleExmp"]).ToString());
                    
                }

                // Only in Kindergarten
                if (dr["IndependentStudy"] == System.DBNull.Value)
                {
                    independentStudy = 0;
                    _with1.SetField("IndependentStudy", "0");

                }
                else
                {
                    independentStudy = Convert.ToInt32(dr["IndependentStudy"]);
                    _with1.SetField("IndependentStudy", (dr["IndependentStudy"]).ToString());

                }

                // Only in Kindergarten
                if (dr["HomeBasedPrivate"] == System.DBNull.Value)
                {
                    homeBasedPrivate = 0;
                    _with1.SetField("HomeBasedPrivate", "0");

                }
                else
                {
                    homeBasedPrivate = Convert.ToInt32(dr["HomeBasedPrivate"]);
                    _with1.SetField("HomeBasedPrivate", (dr["HomeBasedPrivate"]).ToString());

                }

                // Added on 08/25/2021
                if (dr["StudentYesNo"].ToString() == "Yes")
                {
                    _with1.SetField("Status", "Active");

                }
                else if (dr["StudentYesNo"].ToString() == "No")
                {
                    _with1.SetField("Status", (dr["Reason"]).ToString());
                   
                }

                _with1.SetField("MedExmp", dr["MedExmp"].ToString());
                _with1.SetField("IEPService", dr["IEPService"].ToString());
                _with1.SetField("NoImm", dr["NoImm"].ToString());
                _with1.SetField("TempMedExemption", dr["TempMedExemption"].ToString());
                _with1.SetField("EnrolledButNotAttending", dr["EnrolledButNotAttending"].ToString());
                _with1.SetField("Total", dr["TotNo"].ToString());

                _with1.SetField("SchAdmin", dr["SchAdmin"].ToString());
                _with1.SetField("SchEmail", dr["SchEmail"].ToString());
                _with1.SetField("PhysStreet", dr["PhysStreet"].ToString());

                _with1.SetField("Polio", dr["Polio"].ToString()); 
                _with1.SetField("HepB", dr["HepB"].ToString());
                _with1.SetField("DTP_DTAP_DT", dr["DTP_DTAP_DT"].ToString());
                _with1.SetField("VZV", dr["VZV"].ToString());
                _with1.SetField("MMRDose2", dr["MMRDose2"].ToString());
                _with1.SetField("HIB", dr["HIB"].ToString());

                //7th grade only
                if (dr["V_AllImm"] == System.DBNull.Value) { _with1.SetField("V_AllImm", "0"); } else { _with1.SetField("V_AllImm", (dr["V_AllImm"]).ToString()); }
                if (dr["V_MedExmp"] == System.DBNull.Value) { _with1.SetField("V_MedExmp", "0"); } else { _with1.SetField("V_MedExmp", (dr["V_MedExmp"]).ToString()); }
                if (dr["V_MDMO_PermMedExmp"] == System.DBNull.Value) { _with1.SetField("V_MDMO", "0"); } else { _with1.SetField("V_MDMO", (dr["V_MDMO_PermMedExmp"]).ToString()); }
                if (dr["V_IEPService"] == System.DBNull.Value) { _with1.SetField("V_IEPService", "0"); } else { _with1.SetField("V_IEPService", (dr["V_IEPService"]).ToString()); }
                if (dr["V_IndependentStudy"] == System.DBNull.Value) { _with1.SetField("V_IndependentStudy", "0"); } else { _with1.SetField("V_IndependentStudy", (dr["V_IndependentStudy"]).ToString()); }
                if (dr["V_HomeBasedPrivate"] == System.DBNull.Value) { _with1.SetField("V_HomeBasedPrivate", "0"); } else { _with1.SetField("V_HomeBasedPrivate", (dr["V_HomeBasedPrivate"]).ToString()); }
                if (dr["V_ConditionalNotDue"] == System.DBNull.Value) { _with1.SetField("V_ConditionalMissing", "0"); } else { _with1.SetField("V_ConditionalMissing", (dr["V_ConditionalNotDue"]).ToString()); }
                if (dr["V_TempMedExemption"] == System.DBNull.Value) { _with1.SetField("V_TempMedExemption", "0"); } else { _with1.SetField("V_TempMedExemption", (dr["V_TempMedExemption"]).ToString()); }
                if (dr["V_EnrolledButNotAttending"] == System.DBNull.Value) { _with1.SetField("V_EnrolledButNotAttending", "0"); } else { _with1.SetField("V_EnrolledButNotAttending", (dr["V_EnrolledButNotAttending"]).ToString()); }
                if (dr["SchAdmin"] == System.DBNull.Value) { _with1.SetField("AdministerPrincipal", "0"); } else { _with1.SetField("AdministerPrincipal", (dr["SchAdmin"]).ToString()); }

                _with1.SetField("V_Total", dr["TotNo"].ToString());

                //_with1.SetField("TextMissingDosesTotal", dr["TextMissingDosesTotal"].ToString());
                //_with1.SetField("TextMissingDosesTotal", (Convert.ToInt32(dr["MedExmp"]) + Convert.ToInt32((dr["BeleExmp"] ?? "0").ToString()) + Convert.ToInt32(dr["IEPService"]) + Convert.ToInt32(dr["NoImm"]) + Convert.ToInt32(dr["TempMedExemption"]) + Convert.ToInt32(dr["EnrolledButNotAttending"])).ToString());
                _with1.SetField("TextMissingDosesTotal", (Convert.ToInt32(dr["MedExmp"]) + pbe + independentStudy + homeBasedPrivate + Convert.ToInt32(dr["IEPService"]) + Convert.ToInt32(dr["NoImm"]) + Convert.ToInt32(dr["TempMedExemption"]) + Convert.ToInt32(dr["EnrolledButNotAttending"])).ToString());


                //_with1.SetField("Address", dr["PhysStreet"].ToString());
                _with1.SetField("City", dr["PhysCity"].ToString());
                //_with1.SetField("Zip", Left(dr["PhysZip"].ToString(), 5));
                _with1.SetField("Zip", dr["PhysZip"].ToString());


                //pdfStamper.FormFlattening = false;
                // Make pdf Read-Only
                pdfStamper.FormFlattening = true;
                pdfStamper.Close();

            }
            catch (Exception ex)
            {
                pdfReader.Close();
                pdfStamper.Close();
            }

        }
        #endregion




    }
}
