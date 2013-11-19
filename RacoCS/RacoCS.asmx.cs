using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Oracle.DataAccess.Client;
public class ReceiveOrcl
{
    OracleConnection orcl = new OracleConnection("Data Source=-----------; User Id=-----------; Password=-----------;");

    OracleCommand myCMD = new OracleCommand();
    public void ReceiveSQL(string racoSecur, string racoFrom,string racoMsg)
    {
        string base64String = "";
        byte[] btArrayASCII = Convert.FromBase64String(racoMsg);
        base64String = ASCIIEncoding.ASCII.GetString(btArrayASCII);
        try
        {
            if (orcl.State == ConnectionState.Closed)
                orcl.Open();
        }
        catch (Exception ex)
        {
            //Error happening here
        }
        myCMD.Connection = orcl;
        myCMD.CommandText = "insertintoracoreceived";
        myCMD.CommandType = CommandType.StoredProcedure;
        OracleParameter security = new OracleParameter("p_security", OracleDbType.Varchar2, ParameterDirection.Input);
        OracleParameter phone = new OracleParameter("p_from", OracleDbType.Int64, ParameterDirection.Input);
        OracleParameter msg = new OracleParameter("p_msg", OracleDbType.Varchar2, ParameterDirection.Input);
        security.Value = racoSecur;
        phone.Value = Convert.ToInt64(racoFrom);
        msg.Value = base64String;
        myCMD.Parameters.Add(security);
        myCMD.Parameters.Add(phone);
        myCMD.Parameters.Add(msg);
        try
        {
            myCMD.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            return;
        }
        orcl.Close();
    }
}
namespace RacoCS
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.17929")]
    [System.Web.Services.WebServiceAttribute(Namespace = "https://t-mobile.racowireless.com/SMSRicochet1.0")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ReceiveSoap", Namespace = "https://t-mobile.racowireless.com/SMSRicochet1.0")]
    public partial class FTSGPS : System.Web.Services.WebService
    {
        /// here stuff goes
        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://t-mobile.racowireless.com/SMSRicochet1.0/ReceiveSMS", RequestNamespace = "https://t-mobile.racowireless.com/SMSRicochet1.0", ResponseNamespace = "https://t-mobile.racowireless.com/SMSRicochet1.0", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ServiceResult ReceiveSMS(string securityKey, string from, string message)
        {
            ReceiveOrcl stuff = new ReceiveOrcl();
            stuff.ReceiveSQL(securityKey, from, message);
            return ServiceResult.ACCEPTED;
        }

        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("https://t-mobile.racowireless.com/SMSRicochet1.0/Test", RequestNamespace = "https://t-mobile.racowireless.com/SMSRicochet1.0", ResponseNamespace = "https://t-mobile.racowireless.com/SMSRicochet1.0", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ServiceResult Test(string securityKey)
        {
            return ServiceResult.ACCEPTED;
        }
    }
}
