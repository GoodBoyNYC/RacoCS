using Oracle.DataAccess.Client;
using System;
using System.Data;



public class ReceiveOrcl
{
    OracleConnection orcl = new OracleConnection("Data Source=ORCLGPS; User Id=GOAT; Password=WATCHER;");

    OracleCommand myCMD = new OracleCommand();
    public void ReceiveSQL(ref string racoSecur, ref string racoFrom, ref string racoMsg)
    {
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
        msg.Value = racoMsg;
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

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik, @toddanglin
//Facebook: facebook.com/telerik
//=======================================================
