﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// sqlHelper class provides with basic sql functions
/// </summary>
public static class SqlHelper
{

    #region "General sql helper methods"

    public static SqlParameter AddInParam(string ParameterName, SqlDbType SqlDbDataType, object Value)
    {
        SqlParameter InParam = new SqlParameter();
        InParam.ParameterName = ParameterName;
        InParam.Direction = ParameterDirection.Input;
        InParam.SqlDbType = SqlDbDataType;
        InParam.Value = Value == null ? DBNull.Value : Value;
        return InParam;
    }

    public static DataTable ReadTable(string sSelectQuery, bool bIsStoredProcedure, params SqlParameter[] sqlParameters)
    {
        DataTable dtReaded = new DataTable();

        SqlConnection conn = new SqlConnection(GlobalVariables.SqlConnectionString);
        SqlDataAdapter daReader = new SqlDataAdapter(sSelectQuery, conn);

        if (bIsStoredProcedure)
            daReader.SelectCommand.CommandType = CommandType.StoredProcedure;
        if (sqlParameters.Length > 0)
            daReader.SelectCommand.Parameters.AddRange(sqlParameters);

        if (conn.State == ConnectionState.Open)
            conn.Close();
        conn.Open();

        daReader.Fill(dtReaded);

        conn.Close();

        return dtReaded;
    }

    public static DataTable ReadTable(string sSelectQuery, string sConnectionString, bool bIsStoredProcedure, params SqlParameter[] sqlParameters)
    {
        DataTable dtReaded = new DataTable();

        SqlConnection conn = new SqlConnection(sConnectionString);
        SqlDataAdapter daReader = new SqlDataAdapter(sSelectQuery, conn);

        if (bIsStoredProcedure)
            daReader.SelectCommand.CommandType = CommandType.StoredProcedure;
        if (sqlParameters.Length > 0)
            daReader.SelectCommand.Parameters.AddRange(sqlParameters);

        if (conn.State == ConnectionState.Open)
            conn.Close();
        conn.Open();

        daReader.Fill(dtReaded);

        conn.Close();

        return dtReaded;
    }

    public static int  UpdateDatabase(string pStrQry, params SqlParameter[] sqlParameters)
    {
        try
        {
            SqlConnection conn = new SqlConnection(Convert.ToString(HttpContext.Current.Session["SystemUserSqlConnectionString"])); //(GlobalVariables.ConnectionString);
            SqlCommand cmdUpdate = new SqlCommand(pStrQry, conn);
            if (sqlParameters.Length > 0)
                cmdUpdate.Parameters.AddRange(sqlParameters);
            OpenSafeConnection(conn);
            int iCnt = cmdUpdate.ExecuteNonQuery();
            CloseSafeConnection(conn);
            return iCnt;
        }
        catch (Exception exUpdate)
        {
            return -1;
        }
    }

    public static void OpenSafeConnection(SqlConnection ConnToOpen)
    {
        if (ConnToOpen.State == ConnectionState.Closed)
            ConnToOpen.Close();
        ConnToOpen.Open();
    }


    public static void CloseSafeConnection(SqlConnection ConnToClose)
    {
        if (ConnToClose.State == ConnectionState.Open)
            ConnToClose.Close();
    }

    #endregion


    public static DataSet ReadDataSet(string pStrQry, string pStrConnectionString, bool bIsStoredProcedure, bool bReturnNullIfNoTables, params SqlParameter[] sqlParameters)
    {
        DataSet dsReaded = new DataSet();

        SqlConnection conn = new SqlConnection(pStrConnectionString);
        SqlDataAdapter daReader = new SqlDataAdapter(pStrQry, conn);

        if (bIsStoredProcedure)
            daReader.SelectCommand.CommandType = CommandType.StoredProcedure;
        if (sqlParameters.Length > 0)
            daReader.SelectCommand.Parameters.AddRange(sqlParameters);
        daReader.SelectCommand.CommandTimeout = 0;

        if (conn.State == ConnectionState.Open)
            conn.Close();
        conn.Open();

        daReader.Fill(dsReaded);

        conn.Close();
        if (bReturnNullIfNoTables && dsReaded.Tables.Count == 0)
            return null;
        else
            return dsReaded;
    }

    private static SqlConnection GetSqlConnection()
    {
        return new SqlConnection(ConfigurationManager.ConnectionStrings["DbAccessString"].ConnectionString);
    }

    public static SqlCommand GetCommand(string pStrQuery)
    {
        return new SqlCommand(pStrQuery, GetSqlConnection());
    }

    public static SqlCommand GetCommand(string pStrQuery, string pStrConnectionString)
    {
        return new SqlCommand(pStrQuery, new SqlConnection(pStrConnectionString));
    }


    #region "Login Page"

    /// <summary>
    /// Authenticates user.
    /// </summary>
    /// <param name="CompanyKey">Company key of user.</param>
    /// <param name="Id">User id.</param>
    /// <param name="Password">Password of user.</param>
    /// <returns>returns object array of company detail [0] and userdetail[1] if valid user else return null.</returns>
    public static object[] AuthenticateForAdmin(string CompanyKey, string Id, string Password)
    {
        object[] AuthenticationResult = new object[1] { null };
        String sQuery;

        sQuery = "Select * from Company_Master ";
        sQuery += "Where Comp_vCharLoginId  ='" + Id + "' and ";
        sQuery += "Comp_vCharKey  ='" + CompanyKey + "' and ";
        sQuery += GlobalFunctions.CreateDecryptTextSyntax("Comp_vCharLoginPass", false) + "='" + Password + "'";

        DataTable dtTable = ReadTable(sQuery, false);
        if (dtTable.Rows.Count > 0)
        {
            try
            {
                DataRow drRow = dtTable.Rows[0];
                sQuery = "Select * from [dbo].[Dove_Dist_t_" + Convert.ToInt64(drRow["Comp_bIntId"]) + "] ";
                sQuery += " where  ";
                sQuery += " DD_bIntId='M1'";
                DataTable dtAdmin = ReadTable(sQuery, false);
                DataRow drAdmin = dtAdmin.Rows[0];
                if (dtAdmin.Rows.Count > 0)
                {
                    SysCompany compObject = new SysCompany(Convert.ToString(drRow["Comp_vCharKey"]), Convert.ToString(drRow["Comp_vCharName"]), Convert.ToInt64(drRow["Comp_bIntId"]), Convert.ToString(drRow["Comp_vCharLoginId"]), Convert.ToString(drRow["Comp_vCharAddress"]));
                    compObject.UserID = Convert.ToString(drAdmin["DD_bIntId"]);
                    compObject.PerOrdComm = Convert.ToString(drAdmin["DD_intPerOrdComm"]);
                    compObject.PercntComm = Convert.ToString(drAdmin["DD_intPercntComm"]);
                    AuthenticationResult[0] = compObject;
                }
                else
                {
                }

            }
            catch (Exception ex)
            {
                return AuthenticationResult;
            }
        }
        return AuthenticationResult;
    }

    public static object[] AuthenticateMainForDove(string CompanyKey, string Id, string Password, int Flag)
    {
        object[] AuthenticationResult = new object[1] { null };
        String sQuery;
        sQuery = "Select * from Company_Master ";
        sQuery += "Where Comp_vCharKey  ='" + CompanyKey + "'";

        DataTable dtTable = ReadTable(sQuery, false);

        if (dtTable.Rows.Count > 0)
        {
            DataRow drRow = dtTable.Rows[0];
            sQuery = "Select * from [dbo].[Dove_Dist_t_" + Convert.ToInt64(drRow["Comp_bIntId"]) + "] ";
            sQuery += " where  ";
            sQuery += " DD_bIntId='" + Id + "'";
            //sQuery += " And UD_bItIsActive = 1";
            sQuery += " And " + GlobalFunctions.CreateDecryptTextSyntaxWithNVarchar("DD_nVarPass", true) + "='" + Password + "' And DD_vCharFlag='" + Flag + "'";

            DataTable dtUser = ReadTable(sQuery, false);
            if (dtUser.Rows.Count > 0)
            {
                try
                {
                    DataRow drUser = dtUser.Rows[0];
                    SysCompany compObject = new SysCompany(Convert.ToString(drRow["Comp_vCharKey"]), Convert.ToString(drRow["Comp_vCharName"]), Convert.ToInt64(drRow["Comp_bIntId"]), Convert.ToString(drUser["DD_nVarFullname"]), Convert.ToString(drRow["Comp_vCharAddress"]));
                    compObject.UserID = Convert.ToString(drUser["DD_bIntId"]);
                    compObject.PerOrdComm = Convert.ToString(drUser["DD_intPerOrdComm"]);
                    compObject.PercntComm = Convert.ToString(drUser["DD_intPercntComm"]);
                    AuthenticationResult[0] = compObject;
                }
                catch (Exception ex)
                {

                    return AuthenticationResult;
                }
            }
        }
        return AuthenticationResult;
    }

    //authentication for sub-admin role, currently creds are hardcoded
    public static object[] AuthenticateForSubAdmin(string CompanyKey, string Id, string Password)
    {
        object[] AuthenticationResult = new object[1] { null };

        if (CompanyKey == "90909090" && Id == "SA_001" && Password == "Welcome@7679")
        {
            String sQuery;
            sQuery = "Select * from Company_Master ";
            sQuery += "Where Comp_vCharKey  ='" + CompanyKey + "'";

            DataTable dtTable = ReadTable(sQuery, false);

            if (dtTable.Rows.Count > 0)
            {
                DataRow drRow = dtTable.Rows[0];
                SysCompany compObject = new SysCompany(Convert.ToString(drRow["Comp_vCharKey"]), Convert.ToString(drRow["Comp_vCharName"]), Convert.ToInt64(drRow["Comp_bIntId"]), Convert.ToString(drRow["Comp_vCharLoginId"]), Convert.ToString(drRow["Comp_vCharAddress"]));
                compObject.UserID = "M1";
                AuthenticationResult[0] = compObject;
            }
        }
        /*
        String sQuery;
        sQuery = "Select * from Company_Master ";
        sQuery += "Where Comp_vCharKey  ='" + CompanyKey + "'";

        DataTable dtTable = ReadTable(sQuery, false);

        if (dtTable.Rows.Count > 0)
        {
            try
            {
                DataRow drRow = dtTable.Rows[0];
                sQuery = "Select * from [dbo].[Dove_EmployeeMaster_" + Convert.ToInt64(drRow["Comp_bIntId"]) + "] ";
                sQuery += "Where EM_vCharEmpCode  ='" + Id + "' and ";
                sQuery += GlobalFunctions.CreateDecryptTextSyntax("EM_vCharPassword", false) + "='" + Password + "'";
                DataTable dtEmp = ReadTable(sQuery, false);
                DataRow drEmp = dtEmp.Rows[0];
                if (dtEmp.Rows.Count > 0)
                {
                    SysCompany compObject = new SysCompany(Convert.ToString(drRow["Comp_vCharKey"]), Convert.ToString(drRow["Comp_vCharName"]), Convert.ToInt64(drRow["Comp_bIntId"]), Convert.ToString(drRow["Comp_vCharLoginId"]), Convert.ToString(drRow["Comp_vCharAddress"]));
                    compObject.UserID = Convert.ToString(drEmp["EM_vCharMobile"]);
                    compObject.EmpCode = Convert.ToString(drEmp["EM_vCharEmpCode"]);
                    compObject.EmpName = Convert.ToString(drEmp["EM_vCharName"]);
                    AuthenticationResult[0] = compObject;
                }
                else
                {
                }

            }
            catch (Exception ex)
            {
                return AuthenticationResult;
            }
        }
        */
        return AuthenticationResult;
    }

}


    #endregion


//    public static object[] AuthenticateUserold(string CompanyKey, string Id, string Password)
//    {

//        object[] AuthenticationResult = new object[2] { null, null };
//        string sQuery = "SP_GetCompanyDtls";
//        DataTable dtTable = ReadTable(sQuery, true,
//                                        AddInParam("@vCharKey", SqlDbType.VarChar, CompanyKey)
//                                      );

//        if (dtTable.Rows.Count > 0)
//        {

//            DataRow drRow = dtTable.Rows[0];
//           // SysCompany compObject = new SysCompany(Convert.ToString(drRow["Comp_vCharKey"]),Convert.ToString(drRow["Comp_vCharName"]), Convert.ToInt64(drRow["Comp_bIntId"]),);

//            sQuery = "Select Usr_Id,Usr_Name,Usr_Pass,Usr_DeviceRegId,Usr_Role ";
//            sQuery += " From vwUserHistory Where Usr_Role = 'A' And Usr_Id = @UsrId And ";
//            sQuery += GlobalFunctions.CreateDecryptTextSyntax("Usr_Pass", false) + " = @UPass";

//            string sClntConnection = String.Format("Server={0};Database={1};User Id={2};Password={3};",
//                                                   GlobalVariables.SqlConnectionInstance,
//                                                   compObject.CompDatabaseName,
//                                                   GlobalVariables.SqlConnectionUserName,
//                                                   GlobalVariables.SqlConnectionUserPass);

//            SqlConnection connUserVerificationSqlConn = new SqlConnection(sClntConnection);
//            SqlCommand cmdVerifyAndUpdateUser = new SqlCommand(sQuery, connUserVerificationSqlConn);
//            cmdVerifyAndUpdateUser.Parameters.Add(SqlHelper.AddInParam("@UsrId", SqlDbType.VarChar, Id));
//            cmdVerifyAndUpdateUser.Parameters.Add(SqlHelper.AddInParam("@UPass", SqlDbType.VarChar, Password));

//            if (connUserVerificationSqlConn.State == ConnectionState.Open)
//                connUserVerificationSqlConn.Close();

//            // Keep this open untill all work are done.
//            //
//            connUserVerificationSqlConn.Open();

//            // This block will destroy adapter asap work is done ... and will improve user exp.
//            //
//            using (SqlDataAdapter daVerifier = new SqlDataAdapter(cmdVerifyAndUpdateUser))
//            {
//                dtTable = new DataTable();
//                daVerifier.Fill(dtTable);
//            }

//            if (dtTable.Rows.Count > 0)
//            {
//                sQuery = "Update Users Set Usr_LogInDevice = 'W' Where Usr_Id = @UsrId";
//                cmdVerifyAndUpdateUser.CommandText = sQuery;

//                cmdVerifyAndUpdateUser.Parameters.RemoveAt("@UPass");

//                try
//                {
//                    if (cmdVerifyAndUpdateUser.ExecuteNonQuery() > 0)
//                    {
//                        drRow = dtTable.Rows[0];
//                        SystemUser userObject = new SystemUser(Id, Convert.ToString(drRow["Usr_Name"]), Convert.ToString(drRow["Usr_DeviceRegId"]), Convert.ToString(drRow["Usr_Role"]), "W");
//                        AuthenticationResult[0] = compObject;
//                        AuthenticationResult[1] = userObject;
//                    }
//                }

//                catch (Exception ex)
//                {
//                    connUserVerificationSqlConn.Close();
//                    return AuthenticationResult;
//                }
//            }

//            connUserVerificationSqlConn.Close();
//        }

//        return AuthenticationResult;
//    }

