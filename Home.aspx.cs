using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace VTalk_WebApp
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SystemCompany"] != null)
            {
                if (!IsPostBack)
                {
                    txtCompanyName.Text = Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyName);
                    //txtAddress.Text = Convert.ToString(((SysCompany)Session["SystemCompany"]).CompanyAddress);
                    //if (Convert.ToString(Session["UserType"]) != "A")
                    {
                        //getCommission();
                        //fillChart();
                    }
                    FillTalukaCombo();
                    cmbTaluka.SelectedIndex = 1;
                    btnLogin_ServerClick();
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }

        }

        protected void getCommission()
        {
            DataTable dtCatDataCnt = SqlHelper.ReadTable("Dove_GetCommissionForGraph ", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                                  SqlHelper.AddInParam("@bIntTalukaID", SqlDbType.BigInt, Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId)),
                                            SqlHelper.AddInParam("@bnVarDistID", SqlDbType.NVarChar, Session["SystemUserID"]),
                                            SqlHelper.AddInParam("@decPerOrdComm", SqlDbType.Decimal, Convert.ToDecimal(Session["PerOrdComm"])),
                                            SqlHelper.AddInParam("@decPercntComm", SqlDbType.Decimal, Convert.ToDecimal(Session["PercntComm"])));
        }

        protected void fillChart()
        {
            /*
            string con = Convert.ToString(Session["SystemUserSqlConnectionString"]); //GlobalVariables.ConnectionString;
            //changed by vibha and query given by bejoy 2021-02-17
            string sql = " select top 7 DCD_decComm, convert(varchar,DCD_dtDate,23)DCD_dtDate " +
                         " from Dove_EveryDayComm_Distri_17 where DCD_nCharDistId=@UserId and DCD_decComm>0 " +
                         //commented to fetch last 7 comm entries, not dependent on dates
                         //" and convert(varchar,DCD_dtDate,23)>=dateadd(day, -10,CONVERT(date,getdate())) "+
                         " order by DCD_dtDate desc";
            //" and convert(varchar,DCD_dtDate,23)>=dateadd(day, 1-datepart(dw, getdate()), CONVERT(date,getdate())) " +
            //" and convert(varchar,DCD_dtDate,23)<=dateadd(day, 8-datepart(dw, getdate()), CONVERT(date,getdate()))";
            DataTable dtCatDataCnt = SqlHelper.ReadTable(sql, Convert.ToString(Session["SystemUserSqlConnectionString"]), false,
                                            SqlHelper.AddInParam("@UserId", SqlDbType.NVarChar, Session["SystemUserID"]));



            //Chart1.DataSource = dt;
            //Chart1.ChartAreas[0].AxisY.LabelStyle.Format = "C1";

            //storing total rows count to loop on each Record  
            string[] XPointMember = new string[dtCatDataCnt.Rows.Count];
            decimal[] YPointMember = new decimal[dtCatDataCnt.Rows.Count];

            for (int count = 0; count < dtCatDataCnt.Rows.Count; count++)
            {
                //storing Values for X axis  
                XPointMember[count] = dtCatDataCnt.Rows[count]["DCD_dtDate"].ToString();
                //storing values for Y Axis  
                YPointMember[count] = Convert.ToDecimal(dtCatDataCnt.Rows[count]["DCD_decComm"]);

                //Setting width of line  
            }
            Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);
            Chart1.Series[0].BorderWidth = 1;
            //setting Chart type   
            Chart1.Series[0].ChartType = SeriesChartType.Column;
            Chart1.Series[0].ToolTip = " #VALX | #VALY";
            Chart1.Series[0]["PixelPointWidth"] = "50";

            */
        }

        protected void FillTalukaCombo()
        {
            #region FillTaluka Combo
            string sql = "Select TM_vCharName_En,TM_bIntId from Taluka_Master where TM_bIntCompId =@compID";
            DataTable dtTable = SqlHelper.ReadTable(sql, false,
                SqlHelper.AddInParam("@compID",SqlDbType.BigInt,Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId)));

            if (dtTable.Rows.Count > 0)
            {
                cmbTaluka.AppendDataBoundItems = true;
                cmbTaluka.Items.Insert(0, new ListItem("<< Select Support Center >>", "0"));
                cmbTaluka.DataSource = dtTable;
                cmbTaluka.DataTextField = "TM_vCharName_En";
                cmbTaluka.DataValueField = "TM_bIntId";
                cmbTaluka.DataBind();
                cmbTaluka.SelectedIndex = 0;
            }
            else
            {
                cmbTaluka.AppendDataBoundItems = true;
                cmbTaluka.Items.Insert(0, new ListItem("<< Add Branch >>", "<< Add Branch >>"));
                cmbTaluka.SelectedIndex = 0;
            }
            #endregion
        }

        protected void btnLogin_ServerClick()
        {
            long lngentryCount;
            if (Convert.ToInt64(cmbTaluka.SelectedItem.Value) != 0)
            {
                string sql = "Select TM_iNtBuisnessEntryCount from Taluka_Master where TM_bIntCompId =@compID ";
                DataTable dtTable = SqlHelper.ReadTable(sql, false,
                    SqlHelper.AddInParam("@compID",SqlDbType.BigInt,Convert.ToInt64(((SysCompany)Session["SystemCompany"]).CompanyId)));
                if (dtTable.Rows.Count > 0)
                {
                    DataRow dtInfoRow = dtTable.Rows[0];
                    lngentryCount = Convert.ToInt64(dtInfoRow["TM_iNtBuisnessEntryCount"].ToString());
                    TalukaData objTaluka = new TalukaData(Convert.ToInt32(cmbTaluka.SelectedItem.Value), cmbTaluka.SelectedItem.Text, lngentryCount);
                    Session["TalukaDetails"] = (TalukaData)objTaluka;
                    SetMessage(true, " Support Center has been selected. Please Navigate through appropriate side menus !");
                    if (Convert.ToString(Session["UserType"]) != "A")
                    {
                        //fillChart();
                    }
                }
                else
                {
                    SetMessage(true, "Log In Error Entry Field not Found for Branch !!!");
                }
            }
            else
            {
                Session["TalukaDetails"] = null;
                SetMessage(false, "Select Support Center to Continue!!!!");
                if (Convert.ToString(Session["UserType"]) != "A")
                {
                    //fillChart();
                }
            }
        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            cmbTaluka.SelectedIndex = -1;
        }

        private void SetMessage(bool pBlnIsError, string pStrError)
        {
            //actionInfo.Attributes["class"] = "alert " + (pBlnIsError ? "alert-danger" : "alert-success");
            //actionInfo.InnerHtml = pStrError;
        }
    }
}