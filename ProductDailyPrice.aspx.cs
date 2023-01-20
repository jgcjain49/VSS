using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin_CommTrex
{
    public partial class ProductDailyPrice : System.Web.UI.Page
    {
        public static DataTable dtstProductPrice;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TalukaDetails"] != null)
            {
                if (!IsPostBack)
                {
                    Productdata();
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            bool updatedvalue = false;
            DataTable dt = SqlHelper.ReadTable("Select top 1 PDP_decGST from [Dove_ProductDaily_Price_17]", false);
            string goldC22k = "";
            string goldC24k = "";
            string SilverC95_8K = "";
            string SilverC99_9k = "";
            string goldB22k = "";
            string goldB24k = "";
            string SilverB95_8K = "";
            string SilverB99_9k = "";
            string goldJ18k = "";
            string goldJ22k = "";
            string SilverJ95_8K = "";
            string SilverJ99_9k = "";


            decimal OldGSTvalue = Convert.ToDecimal(dt.Rows[0][0].ToString());

            if (txtGC22K.Text != "")
            {
                updatedvalue = true;
                update("Gold Coin", 22M, Convert.ToDecimal(txtGC22K.Text), 0.00M, txtGST.Text != "" ? Convert.ToDecimal(txtGST.Text) : OldGSTvalue);
                goldC22k = "Price:" + Convert.ToDecimal(txtGC22K.Text) + ", Making Charges:0.00";
            }
            if (txtGC24K.Text != "")
            {
                updatedvalue = true;
                update("Gold Coin", 24M, Convert.ToDecimal(txtGC24K.Text), 0.00M, txtGST.Text != "" ? Convert.ToDecimal(txtGST.Text) : OldGSTvalue);
                goldC24k = "Price:" + Convert.ToDecimal(txtGC24K.Text) + ", Making Charges:0.00";

            }
            if (txtSC95_8.Text != "")
            {
                updatedvalue = true;
                update("Silver Coin", 95.8M, Convert.ToDecimal(txtSC95_8.Text), 0.00M, txtGST.Text != "" ? Convert.ToDecimal(txtGST.Text) : OldGSTvalue);
                SilverC95_8K = "Price:" + Convert.ToDecimal(txtSC95_8.Text) + ", Making Charges:0.00";

            }
            if (txtSC99_9.Text != "")
            {
                updatedvalue = true;
                update("Silver Coin", 99.9M, Convert.ToDecimal(txtSC99_9.Text), 0.00M, txtGST.Text != "" ? Convert.ToDecimal(txtGST.Text) : OldGSTvalue);
                SilverC99_9k = "Price:" + Convert.ToDecimal(txtSC99_9.Text) + ", Making Charges:0.00";
            }
            if (txtGB22K.Text != "")
            {
                updatedvalue = true;
                update("Gold Bar", 22M, Convert.ToDecimal(txtGB22K.Text), 0.00M, txtGST.Text != "" ? Convert.ToDecimal(txtGST.Text) : OldGSTvalue);
                goldB22k = "Price:" + Convert.ToDecimal(txtGB22K.Text) + ", Making Charges:0.00";
            }
            if (txtGB24K.Text != "")
            {
                updatedvalue = true;
                update("Gold Bar", 24M, Convert.ToDecimal(txtGB24K.Text), 0.00M, txtGST.Text != "" ? Convert.ToDecimal(txtGST.Text) : OldGSTvalue);
                goldB24k = "Price:" + Convert.ToDecimal(txtGB24K.Text) + ", Making Charges:0.00";
            }
            if (txtSB95_8.Text != "")
            {
                updatedvalue = true;
                update("Silver Bar", 95.8M, Convert.ToDecimal(txtSB95_8.Text), 0.00M, txtGST.Text != "" ? Convert.ToDecimal(txtGST.Text) : OldGSTvalue);
                SilverB95_8K = "Price:" + Convert.ToDecimal(txtSB95_8.Text) + ", Making Charges:0.00";
            }
            if (txtSB99_9.Text != "")
            {
                updatedvalue = true;
                update("Silver Bar", 99.9M, Convert.ToDecimal(txtSB99_9.Text), 0.00M, txtGST.Text != "" ? Convert.ToDecimal(txtGST.Text) : OldGSTvalue);
                SilverB99_9k = "Price:" + Convert.ToDecimal(txtSB99_9.Text) + ", Making Charges:0.00";
            }
            if (txtGJ18K.Text != "")
            {
                updatedvalue = true;
                decimal MakingCharge = 0;
                if (txtGJMC.Text == "")
                {
                    DataTable dtmc = SqlHelper.ReadTable("Select PDP_decMakingChrg from [dbo].[Dove_ProductDaily_Price_17] where PDP_vCharProdName = @prodname and PDP_decPurity =@purity ", false,
                         SqlHelper.AddInParam("@prodname", SqlDbType.VarChar, "Gold Jewellery"),
                         SqlHelper.AddInParam("@purity", SqlDbType.Decimal, 18M));

                    if (dtmc.Rows.Count > 0)
                    {
                        MakingCharge = Convert.ToDecimal(dtmc.Rows[0][0].ToString());
                    }
                }
                update("Gold Jewellery", 18M, Convert.ToDecimal(txtGJ18K.Text), txtGJMC.Text != "" ? Convert.ToDecimal(txtGJMC.Text) : MakingCharge, txtGST.Text != "" ? Convert.ToDecimal(txtGST.Text) : OldGSTvalue);
                goldJ18k = "Price:" + Convert.ToDecimal(txtGJ18K.Text) + ", Making Charges:" + (txtGJMC.Text != "" ? Convert.ToDecimal(txtGJMC.Text) : MakingCharge).ToString();
            }
            if (txtGJ22K.Text != "")
            {
                updatedvalue = true;
                decimal MakingCharge = 0;
                if (txtGJMC.Text == "")
                {
                    DataTable dtmc = SqlHelper.ReadTable("Select PDP_decMakingChrg from [dbo].[Dove_ProductDaily_Price_17] where PDP_vCharProdName = @prodname and PDP_decPurity =@purity ", false,
                         SqlHelper.AddInParam("@prodname", SqlDbType.VarChar, "Gold Jewellery"),
                         SqlHelper.AddInParam("@purity", SqlDbType.Decimal, 22M));

                    if (dtmc.Rows.Count > 0)
                    {
                        MakingCharge = Convert.ToDecimal(dtmc.Rows[0][0].ToString());
                    }
                }
                update("Gold Jewellery", 22M, Convert.ToDecimal(txtGJ22K.Text), txtGJMC.Text != "" ? Convert.ToDecimal(txtGJMC.Text) : MakingCharge, txtGST.Text != "" ? Convert.ToDecimal(txtGST.Text) : OldGSTvalue);
                goldJ22k = "Price:" + Convert.ToDecimal(txtGJ22K.Text) + ", Making Charges:" + (txtGJMC.Text != "" ? Convert.ToDecimal(txtGJMC.Text) : MakingCharge).ToString();
            }
            if (txtSJ95_8.Text != "")
            {
                updatedvalue = true;
                decimal MakingCharge = 0;
                if (txtSJMC.Text == "")
                {
                    DataTable dtmc = SqlHelper.ReadTable("Select PDP_decMakingChrg from [dbo].[Dove_ProductDaily_Price_17] where PDP_vCharProdName = @prodname and PDP_decPurity =@purity ", false,
                         SqlHelper.AddInParam("@prodname", SqlDbType.VarChar, "Silver Jewellery"),
                         SqlHelper.AddInParam("@purity", SqlDbType.Decimal, 95.8M));

                    if (dtmc.Rows.Count > 0)
                    {
                        MakingCharge = Convert.ToDecimal(dtmc.Rows[0][0].ToString());
                    }
                }
                update("Silver Jewellery", 95.8M, Convert.ToDecimal(txtSJ95_8.Text), txtSJMC.Text != "" ? Convert.ToDecimal(txtSJMC.Text) : MakingCharge, txtGST.Text != "" ? Convert.ToDecimal(txtGST.Text) : OldGSTvalue);
                SilverJ95_8K = "Price:" + Convert.ToDecimal(txtSJ95_8.Text) + ", Making Charges:" + (txtSJMC.Text != "" ? Convert.ToDecimal(txtSJMC.Text) : MakingCharge).ToString();
            }
            if (txtSJ99_9.Text != "")
            {
                updatedvalue = true;
                decimal MakingCharge = 0;
                if (txtSJMC.Text == "")
                {
                    DataTable dtmc = SqlHelper.ReadTable("Select PDP_decMakingChrg from [dbo].[Dove_ProductDaily_Price_17] where PDP_vCharProdName = @prodname and PDP_decPurity =@purity ", false,
                         SqlHelper.AddInParam("@prodname", SqlDbType.VarChar, "Silver Jewellery"),
                         SqlHelper.AddInParam("@purity", SqlDbType.Decimal, 99.9M));

                    if (dtmc.Rows.Count > 0)
                    {
                        MakingCharge = Convert.ToDecimal(dtmc.Rows[0][0].ToString());
                    }
                }
                update("Silver Jewellery", 99.9M, Convert.ToDecimal(txtSJ99_9.Text), txtSJMC.Text != "" ? Convert.ToDecimal(txtSJMC.Text) : MakingCharge, txtGST.Text != "" ? Convert.ToDecimal(txtGST.Text) : OldGSTvalue);
                SilverJ99_9k = "Price:" + Convert.ToDecimal(txtSJ99_9.Text) + ", Making Charges:" + (txtSJMC.Text != "" ? Convert.ToDecimal(txtSJMC.Text) : MakingCharge).ToString();
            }

            if (updatedvalue == true)
            {

                SqlHelper.ReadTable("sp_DoveDailyPriceUpdate", true);


                foreach (Control item in Page.Form.FindControl("CntAdminEx_Body").Controls)
                {
                    if (item is TextBox)
                    {
                        ((TextBox)item).Text = string.Empty;
                    }
                }

                Productdata();

                actionInfo.InnerHtml = "Daily Price has been updated.";

                updateLog();


            }
            else
            {
                actionInfo.InnerHtml = "Please enter atleast One value to proceed to update.";
            }


        }

        protected void btnClear_ServerClick(object sender, EventArgs e)
        {
            foreach (Control item in Page.Form.FindControl("CntAdminEx_Body").Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Text = string.Empty;
                }
            }
        }

        protected void update(string ProductName, decimal Purity, decimal Price, decimal Makingcharge, decimal GST)
        {
            try
            {
                SqlHelper.ReadTable("sp_DoveUpdateProductDaily_Price", true,
                SqlHelper.AddInParam("@PDP_vCharProdName", System.Data.SqlDbType.VarChar, ProductName),
                SqlHelper.AddInParam("@PDP_decPurity", System.Data.SqlDbType.Decimal, Purity),
                SqlHelper.AddInParam("@PDP_decPrice", System.Data.SqlDbType.Decimal, Price),
                SqlHelper.AddInParam("@PDP_decMakingChrg", System.Data.SqlDbType.Decimal, Makingcharge),
                SqlHelper.AddInParam("@PDP_decGST", System.Data.SqlDbType.Decimal, GST));
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("ProductDailyPrice", "PriceUpdate", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

            }


        }
        protected void updateLog()
        {
            try
            {
                string GoldC22k, GoldC24k, SilverC95_8k, SilverC99_9k, GoldB22k, GoldB24k,
                                SilverB95_8k, SilverB99_9k, GoldJ18K, GoldJ22k, SilverJ95_8k, SilverJ99_9k;
                DataTable dtDailyPriceData = (DataTable)ViewState["ProductPrice"];
                GoldC22k = "Price:" + dtDailyPriceData.Rows[0]["PDP_decPrice"] + ", Making Charges: " + dtDailyPriceData.Rows[0]["PDP_decMakingChrg"];
                GoldC24k = "Price:" + dtDailyPriceData.Rows[1]["PDP_decPrice"] + ", Making Charges: " + dtDailyPriceData.Rows[0]["PDP_decMakingChrg"];
                SilverC95_8k = "Price:" + dtDailyPriceData.Rows[2]["PDP_decPrice"] + ", Making Charges: " + dtDailyPriceData.Rows[0]["PDP_decMakingChrg"];
                SilverC99_9k = "Price:" + dtDailyPriceData.Rows[3]["PDP_decPrice"] + ", Making Charges: " + dtDailyPriceData.Rows[0]["PDP_decMakingChrg"];
                GoldB22k = "Price:" + dtDailyPriceData.Rows[4]["PDP_decPrice"] + ", Making Charges: " + dtDailyPriceData.Rows[0]["PDP_decMakingChrg"];
                GoldB24k = "Price:" + dtDailyPriceData.Rows[5]["PDP_decPrice"] + ", Making Charges: " + dtDailyPriceData.Rows[0]["PDP_decMakingChrg"];
                SilverB95_8k = "Price:" + dtDailyPriceData.Rows[6]["PDP_decPrice"] + ", Making Charges: " + dtDailyPriceData.Rows[0]["PDP_decMakingChrg"];
                SilverB99_9k = "Price:" + dtDailyPriceData.Rows[7]["PDP_decPrice"] + ", Making Charges: " + dtDailyPriceData.Rows[0]["PDP_decMakingChrg"];
                GoldJ18K = "Price:" + dtDailyPriceData.Rows[8]["PDP_decPrice"] + ", Making Charges: " + dtDailyPriceData.Rows[0]["PDP_decMakingChrg"];
                GoldJ22k = "Price:" + dtDailyPriceData.Rows[9]["PDP_decPrice"] + ", Making Charges: " + dtDailyPriceData.Rows[0]["PDP_decMakingChrg"];
                SilverJ95_8k = "Price:" + dtDailyPriceData.Rows[10]["PDP_decPrice"] + ", Making Charges: " + dtDailyPriceData.Rows[0]["PDP_decMakingChrg"];
                SilverJ99_9k = "Price:" + dtDailyPriceData.Rows[11]["PDP_decPrice"] + ", Making Charges: " + dtDailyPriceData.Rows[0]["PDP_decMakingChrg"];

                SqlHelper.ReadTable("sp_DoveUpdateProductDaily_Price_Log", true,
                SqlHelper.AddInParam("@vCharGoldC22K", System.Data.SqlDbType.VarChar, GoldC22k),
                SqlHelper.AddInParam("@vCharGoldC24K", System.Data.SqlDbType.VarChar, GoldC24k),
                SqlHelper.AddInParam("@vCharSilverC95_8K", System.Data.SqlDbType.VarChar, SilverC95_8k),
                SqlHelper.AddInParam("@vCharSilverC99_9K", System.Data.SqlDbType.VarChar, SilverC99_9k),
                SqlHelper.AddInParam("@vCharGoldB22K", System.Data.SqlDbType.VarChar, GoldB22k),
                SqlHelper.AddInParam("@vCharGoldB24K", System.Data.SqlDbType.VarChar, GoldB24k),
                SqlHelper.AddInParam("@vCharSilverB95_8K", System.Data.SqlDbType.VarChar, SilverB95_8k),
                SqlHelper.AddInParam("@vCharSilverB99_9K", System.Data.SqlDbType.VarChar, SilverB99_9k),
                SqlHelper.AddInParam("@vCharGoldJ18K", System.Data.SqlDbType.VarChar, GoldJ18K),
                SqlHelper.AddInParam("@vCharGoldJ22K", System.Data.SqlDbType.VarChar, GoldJ22k),
                SqlHelper.AddInParam("@vCharSilverJ95_8K", System.Data.SqlDbType.VarChar, SilverJ95_8k),
                SqlHelper.AddInParam("@vCharSilverJ99_9K", System.Data.SqlDbType.VarChar, SilverJ99_9k),
                SqlHelper.AddInParam("@decGST", System.Data.SqlDbType.Decimal, Convert.ToDecimal(dtDailyPriceData.Rows[0]["PDP_decGST"])));

            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("ProductDailyPrice", "updateLog", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);

            }


        }

        protected void grdViewProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdViewProduct.PageIndex = e.NewPageIndex;
            grdViewProduct.DataSource = dtstProductPrice;
            grdViewProduct.DataBind();
        }
        public void Productdata()
        {

            try
            {
                DataTable dtProductPrice = SqlHelper.ReadTable("spGetProductDailyPriceDetails", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                             SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                             SqlHelper.AddInParam("@vCharCatName", SqlDbType.VarChar, ""));

                dtstProductPrice = dtProductPrice;
                if (dtProductPrice.Rows.Count > 0)
                {
                    Div1.Attributes["style"] = "padding-bottom: 10px; text-align: right; display: none;";
                }
                else
                {
                    Div1.Attributes["style"] = "padding-bottom: 10px; text-align: right; display: block;";
                    Div1.InnerHtml = "No records found.";
                }
                grdViewProduct.DataSource = dtProductPrice;
                grdViewProduct.DataBind();
                ViewState["ProductPrice"] = dtProductPrice;
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("showgrid", "Productdata", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
            }
        }

        protected void btnLogSearch_ServerClick(object sender, EventArgs e)
        {
            try
            {
                DataTable dtProductPrice = SqlHelper.ReadTable("spGetProductDailyPriceLogDetails", Convert.ToString(Session["SystemUserSqlConnectionString"]), true,
                                             SqlHelper.AddInParam("@bintTalukaId", SqlDbType.BigInt, Convert.ToInt64(((TalukaData)Session["TalukaDetails"]).TalukaID)),
                                             SqlHelper.AddInParam("@dtLogDateTime", SqlDbType.Date, dtLogDate.Value));
                if (dtProductPrice.Rows.Count > 0)
                {
                    string GoldC22KPrice = "", GoldC22KMaking = "", GoldC24KPrice = "", GoldC24KMaking = "", SilverC95_8KPrice = "", SilverC95_8KMaking = "",
                    SilverC99_9KPrice = "", SilverC99_9KMaking = "", GoldJ18KPrice = "", GoldJ18KMaking = "", GoldJ22KPrice = "", GoldJ22KMaking = "",
                    SilverJ95_8KPrice = "", SilverJ95_8KMaking = "", SilverJ99_9KPrice = "", SilverJ99_9KMaking = "", GoldB22KPrice = "", GoldB22KMaking = "",
                    GoldB24KPrice = "", GoldB24KMaking = "", SilverB95_8KPrice = "", SilverB95_8KMaking = "", SilverB99_9KPrice = "", SilverB99_9KMaking = "";

                    string[] gold22kTemp = dtProductPrice.Rows[0]["PDPL_vCharGoldC22K"].ToString().Split(',');
                    GoldC22KPrice = gold22kTemp[0].Trim().Replace("Price:", "");
                    GoldC22KMaking = gold22kTemp[1].Trim().Replace("Making Charges:", "");

                    string[] gold24kTemp = dtProductPrice.Rows[0]["PDPL_vCharGoldC24K"].ToString().Split(',');
                    GoldC24KPrice = gold24kTemp[0].Trim().Replace("Price:", "");
                    GoldC24KMaking = gold24kTemp[1].Trim().Replace("Making Charges:", "");

                    string[] silverc95_8kTemp = dtProductPrice.Rows[0]["PDPL_vCharSilverC95.8K"].ToString().Split(',');
                    SilverC95_8KPrice = silverc95_8kTemp[0].Trim().Replace("Price:", "");
                    SilverC95_8KMaking = silverc95_8kTemp[1].Trim().Replace("Making Charges:", "");

                    string[] silverc99_9kTemp = dtProductPrice.Rows[0]["PDPL_vCharSilverC99.9K"].ToString().Split(',');
                    SilverC99_9KPrice = silverc99_9kTemp[0].Trim().Replace("Price:", "");
                    SilverC99_9KMaking = silverc99_9kTemp[1].Trim().Replace("Making Charges:", "");

                    string[] goldj18kTemp = dtProductPrice.Rows[0]["PDPL_vCharGoldJ18K"].ToString().Split(',');
                    GoldJ18KPrice = goldj18kTemp[0].Trim().Replace("Price:", "");
                    GoldJ18KMaking = goldj18kTemp[1].Trim().Replace("Making Charges:", "");

                    string[] goldj22kTemp = dtProductPrice.Rows[0]["PDPL_vCharGoldJ22K"].ToString().Split(',');
                    GoldJ22KPrice = goldj22kTemp[0].Trim().Replace("Price:", "");
                    GoldJ22KMaking = goldj22kTemp[1].Trim().Replace("Making Charges:", "");

                    string[] silverj95_8kTemp = dtProductPrice.Rows[0]["PDPL_vCharSilverJ95.8K"].ToString().Split(',');
                    SilverJ95_8KPrice = silverj95_8kTemp[0].Trim().Replace("Price:", "");
                    SilverJ95_8KMaking = silverj95_8kTemp[1].Trim().Replace("Making Charges:", "");

                    string[] silverj99_9kTemp = dtProductPrice.Rows[0]["PDPL_vCharSilverJ99.9K"].ToString().Split(',');
                    SilverJ99_9KPrice = silverj99_9kTemp[0].Trim().Replace("Price:", "");
                    SilverJ99_9KMaking = silverj99_9kTemp[1].Trim().Replace("Making Charges:", "");

                    string[] goldb22kTemp = dtProductPrice.Rows[0]["PDPL_vCharGoldB22K"].ToString().Split(',');
                    GoldB22KPrice = goldb22kTemp[0].Trim().Replace("Price:", "");
                    GoldB22KMaking = goldb22kTemp[1].Trim().Replace("Making Charges:", "");

                    string[] goldb24kTemp = dtProductPrice.Rows[0]["PDPL_vCharGoldB24K"].ToString().Split(',');
                    GoldB24KPrice = goldb24kTemp[0].Trim().Replace("Price:", "");
                    GoldB24KMaking = goldb24kTemp[1].Trim().Replace("Making Charges:", "");

                    string[] silverb95_8kTemp = dtProductPrice.Rows[0]["PDPL_vCharSilverB95.8K"].ToString().Split(',');
                    SilverB95_8KPrice = silverb95_8kTemp[0].Trim().Replace("Price:", "");
                    SilverB95_8KMaking = silverb95_8kTemp[1].Trim().Replace("Making Charges:", "");

                    string[] silverb99_9kTemp = dtProductPrice.Rows[0]["PDPL_vCharSilverB99.9K"].ToString().Split(',');
                    SilverB99_9KPrice = silverb99_9kTemp[0].Trim().Replace("Price:", "");
                    SilverB99_9KMaking = silverb99_9kTemp[1].Trim().Replace("Making Charges:", "");



                    DataTable historicPriceData = new DataTable();
                    historicPriceData.Columns.AddRange(new DataColumn[7] { new DataColumn("Sr No"),
                                                            new DataColumn("PDP_bIntId"),
                                                            new DataColumn("PDP_vCharProdName"),
                                                            new DataColumn("PDP_decPurity"),
                                                            new DataColumn("PDP_decPrice"),
                                                            new DataColumn("PDP_decMakingChrg"),
                                                            new DataColumn("PDP_decGST")
                                                        }
                                    );
                    historicPriceData.Rows.Add(1, 0, "Gold Coin", 22 + ".00", GoldC22KPrice, GoldC22KMaking, dtProductPrice.Rows[0]["PDPL_decGST"]);
                    historicPriceData.Rows.Add(2, 0, "Gold Coin", 24 + ".00", GoldC24KPrice, GoldC24KMaking, dtProductPrice.Rows[0]["PDPL_decGST"]);
                    historicPriceData.Rows.Add(3, 0, "Silver Coin", 95.8 + "0", SilverC95_8KPrice, SilverC95_8KMaking, dtProductPrice.Rows[0]["PDPL_decGST"]);
                    historicPriceData.Rows.Add(4, 0, "Silver Coin", 99.9 + "0", SilverC99_9KPrice, SilverC99_9KMaking, dtProductPrice.Rows[0]["PDPL_decGST"]);
                    historicPriceData.Rows.Add(5, 0, "Gold Jewellery", 18 + ".00", GoldJ18KPrice, GoldJ18KMaking, dtProductPrice.Rows[0]["PDPL_decGST"]);
                    historicPriceData.Rows.Add(6, 0, "Gold Jewellery", 22 + ".00", GoldJ22KPrice, GoldJ22KMaking, dtProductPrice.Rows[0]["PDPL_decGST"]);
                    historicPriceData.Rows.Add(7, 0, "Silver Bar", 95.8 + "0", SilverB95_8KPrice, SilverB95_8KMaking, dtProductPrice.Rows[0]["PDPL_decGST"]);
                    historicPriceData.Rows.Add(8, 0, "Silver Bar", 99.9 + "0", SilverB99_9KPrice, SilverB99_9KMaking, dtProductPrice.Rows[0]["PDPL_decGST"]);
                    historicPriceData.Rows.Add(9, 0, "Gold Bar", 22 + ".00", GoldB22KPrice, GoldB22KMaking, dtProductPrice.Rows[0]["PDPL_decGST"]);
                    historicPriceData.Rows.Add(10, 0, "Gold Bar", 24 + ".00", GoldB24KPrice, GoldB24KMaking, dtProductPrice.Rows[0]["PDPL_decGST"]);
                    historicPriceData.Rows.Add(11, 0, "Silver Jewellery", 95.8 + "0", SilverJ95_8KPrice, SilverJ95_8KMaking, dtProductPrice.Rows[0]["PDPL_decGST"]);
                    historicPriceData.Rows.Add(12, 0, "Silver Jewellery", 99.9 + "0", SilverJ99_9KPrice, SilverJ99_9KMaking, dtProductPrice.Rows[0]["PDPL_decGST"]);

                    grdViewProduct.DataSource = historicPriceData;
                    grdViewProduct.DataBind();
                    Div1.Attributes["style"] = "display:none;";
                    Div1.InnerHtml = "";

                }
                else
                {
                    grdViewProduct.DataSource = null;
                    grdViewProduct.DataBind();
                    Div1.Attributes["style"] = "display:block;";
                    Div1.InnerHtml = "No records found.";
                }
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("showgrid", "Productdata", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
            }
        }

        protected void btnLogClear_ServerClick(object sender, EventArgs e)
        {
            try
            {
                Productdata();
                dtLogDate.Value = "";
            }
            catch (Exception exError)
            {
                long pLngErr = -1;
                if (exError.GetBaseException() is System.Data.SqlClient.SqlException)
                    pLngErr = ((System.Data.SqlClient.SqlException)exError.GetBaseException()).Number;
                pLngErr = GlobalFunctions.ReportError("showgrid", "Productdata", pLngErr, exError.GetBaseException().GetType().ToString(), exError.Message, exError.StackTrace);
            }
        }

    }
}