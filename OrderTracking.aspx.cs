using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin_CommTrex
{
    public partial class OrderTracking : System.Web.UI.Page
    {
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            dt = SqlHelper.ReadTable("Select * from [OrderTrackingQueue_17] where OTQ_vcharorderId = @ID", false,
               SqlHelper.AddInParam("@ID", System.Data.SqlDbType.VarChar, txtorderNumber.Value));

            if (dt.Rows.Count == 0)
            {
                updateActionDiv.InnerHtml = "No Records Found.";
            }
            if (dt.Rows.Count > 0)
            {
                ddlCourierCompany.SelectedItem.Text = dt.Rows[0]["OTQ_vcharCourierCompany"].ToString();
                txtStatus1.Value = dt.Rows[0]["OTQ_vcharCourierCompany"].ToString();
                if (dt.Rows[0]["OTQ_vcharStatus1"].ToString()!="")
                {
                    txtStatus1.Value = dt.Rows[0]["OTQ_vcharStatus1"].ToString();
                    txtStatus1.Disabled = true;
                }
                if (dt.Rows[0]["OTQ_vcharStatus2"].ToString() != "")
                {
                    txtStatus2.Value = dt.Rows[0]["OTQ_vcharStatus2"].ToString();
                    txtStatus2.Disabled = true;
                }

                if (dt.Rows[0]["OTQ_vcharStatus3"].ToString() != "")
                {
                    txtStatus3.Value = dt.Rows[0]["OTQ_vcharStatus3"].ToString();
                    txtStatus3.Disabled = true;
                }
                if (dt.Rows[0]["OTQ_vcharStatus4"].ToString() != "")
                {
                    txtStatus4.Value = dt.Rows[0]["OTQ_vcharStatus4"].ToString();
                    txtStatus4.Disabled = true;
                }

            }

        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            dt = SqlHelper.ReadTable("Select * from [OrderTrackingQueue_17] where OTQ_vcharorderId = @ID", false,
               SqlHelper.AddInParam("@ID", System.Data.SqlDbType.VarChar, txtorderNumber.Value));
            try
            {
                if (dt==null)
                {
                    SqlHelper.ReadTable("INSERT INTO [dbo].[OrderTrackingQueue_17] ([OTQ_vcharorderId],[OTQ_vcharCourierCompany],[OTQ_vcharStatus1] " +
               " ,[OTQ_dtDatetime1],[OTQ_vcharStatus2],[OTQ_dtDatetime2],[OTQ_vcharStatus3],[OTQ_dtDatetime3],[OTQ_vcharStatus4],[OTQ_dtDatetime4]) " +
               "     VALUES " +
               " (@Ordernum,@CourierCompany,@Status1,@Datetime1,@Status2,@Datetime2,@Status3,@Datetime3,@Status4,@Datetime4)", false,
               SqlHelper.AddInParam("@Ordernum", SqlDbType.VarChar, txtorderNumber.Value),
               SqlHelper.AddInParam("@CourierCompany", SqlDbType.VarChar, ddlCourierCompany.SelectedItem.Text),
               SqlHelper.AddInParam("@Status1", SqlDbType.VarChar, txtStatus1.Value),
               SqlHelper.AddInParam("@Datetime1", SqlDbType.DateTime, DateTime.UtcNow.AddHours(5.5)),
               SqlHelper.AddInParam("@Status2", SqlDbType.VarChar, txtStatus2.Value != " " ? txtStatus2.Value : null),
               SqlHelper.AddInParam("@Datetime2", SqlDbType.DateTime, txtStatus2.Value != " " ? DateTime.UtcNow.AddHours(5.5) : (DateTime?)null),
               SqlHelper.AddInParam("@Status3", SqlDbType.VarChar, txtStatus3.Value != " " ? txtStatus3.Value : null),
               SqlHelper.AddInParam("@Datetime3", SqlDbType.DateTime, txtStatus3.Value != " " ? DateTime.UtcNow.AddHours(5.5) : (DateTime?)null),
               SqlHelper.AddInParam("@Status4", SqlDbType.VarChar, txtStatus4.Value != " " ? txtStatus4.Value : null),
               SqlHelper.AddInParam("@Datetime4", SqlDbType.DateTime, txtStatus4.Value != " " ? DateTime.UtcNow.AddHours(5.5) : (DateTime?)null));

                    updateActionDiv.InnerHtml = "Status Updated";
                }
                else if (dt.Rows.Count > 0)
                {
                    SqlHelper.ReadTable("UPDATE [dbo].[OrderTrackingQueue_17] " +
                    "  SET [OTQ_vcharStatus1] = @status1 ,[OTQ_dtDatetime1] = @Date1 " +
                    "     ,[OTQ_vcharStatus2] = @status2 ,[OTQ_dtDatetime2] = @Date2 " +
                    "     ,[OTQ_vcharStatus3] = @status3 ,[OTQ_dtDatetime3] = @Date3 " +
                    "     ,[OTQ_vcharStatus4] = @status4 ,[OTQ_dtDatetime4] = @Date4 " +
                    " WHERE [OTQ_vcharorderId] = @Ordernum", false,
                    SqlHelper.AddInParam("@Status1", SqlDbType.VarChar, txtStatus1.Value),
                    SqlHelper.AddInParam("@Date1", SqlDbType.DateTime, DateTime.UtcNow.AddHours(5.5)),
                    SqlHelper.AddInParam("@Status2", SqlDbType.VarChar, txtStatus2.Value != " " ? txtStatus2.Value : null),
                    SqlHelper.AddInParam("@Date2", SqlDbType.DateTime, txtStatus2.Value != " " ? DateTime.UtcNow.AddHours(5.5) : (DateTime?)null),
                    SqlHelper.AddInParam("@Status3", SqlDbType.VarChar, txtStatus3.Value != " " ? txtStatus3.Value : null),
                    SqlHelper.AddInParam("@Date3", SqlDbType.DateTime, txtStatus3.Value != " " ? DateTime.UtcNow.AddHours(5.5) : (DateTime?)null),
                    SqlHelper.AddInParam("@Status4", SqlDbType.VarChar, txtStatus4.Value != " " ? txtStatus4.Value : null),
                    SqlHelper.AddInParam("@Date4", SqlDbType.DateTime, txtStatus4.Value != " " ? DateTime.UtcNow.AddHours(5.5) : (DateTime?)null),
                    SqlHelper.AddInParam("@Ordernum", SqlDbType.VarChar, txtorderNumber.Value));

                    updateActionDiv.InnerHtml = "Status Updated";
                }
            }
            catch (Exception exError)
            {
                updateActionDiv.InnerHtml = "Error due to " + exError.InnerException.Message;
            }



        }
    }
}