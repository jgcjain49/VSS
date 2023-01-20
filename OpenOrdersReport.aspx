<%@ Page Title="" Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="OpenOrdersReport.aspx.cs" Inherits="Admin_CommTrex.OpenOrdersReport"  EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="contHeadContent" runat="server">
    <link href="AdminExContent/js/advanced-datatable/css/demo_page.css" rel="stylesheet" />
    <link href="AdminExContent/js/advanced-datatable/css/demo_table.css" rel="stylesheet" />
    <link href="AdminExContent/js/data-tables/DT_bootstrap.css" rel="stylesheet" />
    <!--file upload-->
    <link href="AdminExContent/css/bootstrap-fileupload.min.css" rel="stylesheet" />
    <!--tags input-->
    <link href="AdminExContent/js/jquery-tags-input/jquery.tagsinput.css" rel="stylesheet" />

    <style>
        .goldLabel {
            color: gold;
            font-weight: 900;
            font-size: 23px;
            text-shadow: 2px 1px 4px #000000;
            padding-right: 1.5%;
        }

        .silverLabel {
            color: silver;
            font-weight: 900;
            font-size: 23px;
            text-shadow: 2px 1px 4px #000000;
            padding-right: 1.5%;
            padding-left: 5%;
        }

        .hdrStyle {
            padding: 10px 20px 10px 20px !important;
            text-align: center;
        }

        .dataTables_length label, .dataTables_filter label {
            font-weight: 600;
            BACKGROUND: transparent !important;
            color: grey !important;
            margin-bottom: 0 !important;
        }

        .dataTables_wrapper .dataTables_filter {
            width: 450px;
        }

        .hdrAlgnCntrStyle {
            padding: 10px 20px 10px 20px !important;
            text-align: center !important;
        }

        .line-break-text {
            text-align: left !important;
            white-space: pre-line;
        }

        .failed-style {
            background-color: red;
            padding: 8px 15px;
            border-radius: 14px;
            font-weight: 600;
            color: white;
        }

        .success-style {
            background-color: forestgreen;
            padding: 8px 15px;
            border-radius: 14px;
            font-weight: 600;
            color: white;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
    <div class="page-heading">
        <%--<h3>Report</h3>--%>
        <ul class="breadcrumb">
            <%--<li>
                <a href="#">Master </a>
            </li>--%>
            <li class="active">SMARTBUY OPEN ORDERS</li>
        </ul>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CntAdminEx_Body" runat="server">

    <div class="tab-pane" id="modifyUser">
        <div class="form-group">
            <%--  <div class="alert alert-info" runat="server" id="updateActionDiv">
                Select user to Generate  the REPORT.
            </div>--%>

            <div style="padding: 5px;">
                <div class="form-horizontal adminex-form">
                    <section class="panel">
                        <div class="panel-body">

                            <header class="panel-heading">Orders Search Filters</header>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: right; padding-right: 15px;">
                                        <label class="control-label">
                                            Organization ID
                                        </label>
                                    </td>
                                    <td style="padding-right: 30px;">
                                        <input type="text" class="text" id="txtorgid" runat="server" />
                                    </td>

                                    <td style="text-align: right; padding-right: 15px;">
                                        <label class="control-label">
                                            Mobile
                                        </label>
                                    </td>
                                    <td style="padding-right: 30px;">
                                        <input type="number" class="numbers" id="txtusermob" runat="server" />
                                    </td>

                                    <td style="text-align: right; padding-right: 15px;">
                                        <label class="control-label">
                                            Order No.
                                        </label>
                                    </td>
                                    <td style="padding-right: 30px;">
                                        <input type="number" class="numbers" id="txtOrdNum" runat="server" />
                                    </td>

                                    <td style="padding-right: 20px;">
                                        <button class="btn btn-success" type="button" runat="server" onserverclick="btnFetchReportClick" id="btnLogin" title="Search" style="width: 100px;">
                                            Search&nbsp;&nbsp;&nbsp;<i class="fa fa-search"></i>
                                        </button>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; padding-right: 15px;">
                                        <label class="control-label">
                                            Order Placed Date (From)
                                        </label>
                                    </td>
                                    <td style="padding-right: 30px;">
                                        <input type="date" id="ordPlcDt" runat="server" onchange="ordPlcDtSelected(event);" />
                                    </td>

                                    <td style="text-align: right; padding-right: 15px;">
                                        <label class="control-label">
                                            Order Placed Date (Till)
                                        </label>
                                    </td>
                                    <td style="padding-right: 30px;">
                                        <input type="date" id="ordPlcDtTill" runat="server" disabled="disabled" />
                                    </td>
                                    <td style="display:none">
                                        <asp:DropDownList ID="ddUserLevelSearch" runat="server" Style="height: 34px;">
                                            <%--  <asp:ListItem Text="--Please Select--"/>--%>
                                        </asp:DropDownList>
                                    </td>

                                    <td>
                                        <button class="btn btn-success" type="button" runat="server" onserverclick="btnresetclick" id="btnReset" title="Reset" style="width: 100px;">
                                            Clear&nbsp;&nbsp;&nbsp;<i class="fa fa-undo"></i>
                                        </button>
                                    </td>
                                </tr>
                                <tr>

                                    <%-- <td style="padding-right: 15px;">
                                        <label class="control-label">
                                            Order Closed Date
                                        </label>
                                    </td>
                                    <td style="padding-right: 30px;">
                                        <input type="date" id="ordCnclDt" runat="server" />
                                    </td>--%>
                                </tr>
                            </table>

                        </div>


                        <%--  <div class="form-group">
                        <label class="col-sm-2 col-sm-2 control-label">Select Distributor
                            <p style="color: red">*</p>
                        </label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="cmbDistributer" runat="server" Font-Bold="True" AutoPostBack="True"
                                CssClass="form-control m-bot15" OnSelectedIndexChanged="cmbDistributer_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--OnSelectedIndexChanged="cmbDistributer_SelectedIndexChanged"--%>
                    </section>
                </div>
            </div>
            <%-- <div class="form-group">
                        <label class="col-sm-2 col-sm-2 control-label">Select User
                            <p style="color: red">*</p>
                        </label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="cmbUserList" runat="server" Font-Bold="True" AutoPostBack="True"
                                CssClass="form-control m-bot15" OnSelectedIndexChanged="cmbUserList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>--%>
            <%--   <div class="panel-body" style="text-align: center">
                        <button class="btn btn-success" type="button"
                            onserverclick="btnFetchUserReportClick" runat="server" id="btnLogin">
                            PROCEED FURTHER
                                    <i class="fa fa-arrow-right"></i>
                        </button>

                    </div>--%>


            <%--   <div class="panel-body" style="text-align: center">
                        <button class="btn btn-success" type="button"
                            onserverclick="btnExpotClick" runat="server" id="btnExpot">
                            Export to Excel
                                    <i class="fa fa-arrow-right"></i>
                        </button>

                    </div>--%>
            <%--<input type="text" id="liveGold" runat="server" value="5100.83" onserverchange="btnFetchUserReportClick" />
                    <input type="text" id="liveSilv" runat="server" value="" />
                    <button class="btn btn-success" type="button" id="getRate" runat="server" onserverclick="calculateDiff">RATES</button>
                    <asp:TextBox ID="liveGoldASP" runat="server" Text="5150.83" >
                    </asp:TextBox>--%>
            <%-- div for live rate display; --%>
            <%--  <div class="panel-body" style="text-align: center;">
                        <%--<label class="col-sm-2 col-sm-2 control-label">Live Gold</label>--%>
            <%--<label class="col-sm-2 col-sm-2 control-label">Live Silver</label>--%>
            <%--<span class="goldLabel">Live Gold</span>
                        <input type="text" id="liveGold" value="" readonly="true" />
                        &nbsp;&nbsp;&nbsp;
                        <span class="silverLabel">Live Silver</span>
                        <input type="text" id="liveSilv" value="" readonly="true" />
                    </div>--%>
            <%--<asp:Timer ID="myTimer" runat="server" Interval="3600" ontick="calculateDiff"></asp:Timer>--%>
            <div class="panel-body adv-table" style="overflow-x: scroll;">
                <div style="display: none;" id="downloadBtnDiv" runat="server">
                    <button class="btn btn-success" type="button" onclick="getRateAndCalDIff()" id="btnReCalculate" style="float: left;">
                        Calculate Value Difference&nbsp;&nbsp;<i class="fa fa-refresh"></i>
                    </button>
                    <button class="btn btn-success" type="button" onserverclick="btnExpotClick" runat="server" id="btnExpot" style="float: right;">
                        Download&nbsp;&nbsp;<i class="fa fa-download"></i>
                    </button>
                </div>
                <asp:GridView ID="grdReport" runat="server"
                    EnableModelValidation="True" AutoGenerateColumns="False"
                    DataKeyNames="DUOH_bIntOrderHistoryId" AllowPaging="true" AllowSorting="true"
                    RowStyle-CssClass="gradeA" ShowFooter="true" OnRowDataBound="grdReport_RowDataBound"
                    OnPageIndexChanging="grdReport_PageIndexChanging"
                    HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                    RowStyle-Wrap="false" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"
                    class="dynamic-table-grid display table table-bordered table-striped" PagerSettings-Mode="NumericFirstLast"
                    PageSize="10" PagerStyle-HorizontalAlign="Left" PagerSettings-PageButtonCount="5" PagerSettings-Visible="True">
                    <%--https://docs.microsoft.com/en-us/dotnet/api/system.data.datacolumn.expression?view=netcore-3.1--%>
                    <%--<Columns>
                                <asp:TemplateField HeaderText="Order Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderHisID" runat="server" Text='<%# Eval("DUOH_bIntOrderHistoryId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserId" runat="server" Text='<%# Eval("DUOH_bIntUserId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 
                                <asp:TemplateField HeaderText="Product Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdName" runat="server" Text='<%# Eval("DUOH_nVarProductName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Product Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdWeight" runat="server" Text='<%# Eval("DUOH_decProdWeight") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Product Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdQty" runat="server" Text='<%# Eval("DUOH_decProdQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Product Buy Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdBuyPrice" runat="server" Text='<%# Eval("DUOH_decProdBuyPrice") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Product Order Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdOrdNo" runat="server" Text='<%# Eval("DUOH_nVarProdOrderNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Product Order Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdOrderTime" runat="server" Text='<%# Eval("DUOH_nVarProdOrderTime") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Product Order Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdOrderTyp" runat="server" Text='<%# Eval("DUOH_vCharProdOrderType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <%--  <asp:TemplateField HeaderText="Product Cancel Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdCanclTime" runat="server" Text='<%# Eval("DUOH_nVarProdCancelTime") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                    <%--  <asp:TemplateField HeaderText="Live Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdCanclPrice" runat="server" Text='<%# Eval("DUOH_decProdCancelPrice") %>' Style="font-weight: bold;"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                    <%--  <asp:TemplateField HeaderText="Product Diff Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProdDiffPrice" runat="server" Text='<%# Eval("DUOH_decProdDiffPrice") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                    <%--<asp:TemplateField HeaderText="Live Profit and Loss">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("DUOH_decProdDiffPrice") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Balance Difference Price">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("DUOH_decProdDiffPrice") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                    <%-- </Columns>--%>
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="Label1" Text='Sum' runat="server"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <%--  <asp:TemplateField HeaderText="Order Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderHisID" runat="server" Text='<%# Eval("DUOH_bIntOrderHistoryId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="User Name" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UD_vCharName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Mobile" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblUserMobile" runat="server" Text='<%# Eval("UD_vCharPhoneNumber").ToString().Substring(0,3)+"*****"+Eval("UD_vCharPhoneNumber").ToString().Substring(8) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Value Difference" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblProdDiffPrice" runat="server" Text='<%# Eval("DUOH_decProdDiffPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblProdDiffPriceTot" runat="server" Text=''></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Order Placed Date" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblProdOrderTime" runat="server" Text='<%# Eval("DUOH_nVarProdOrderTime").ToString().Substring(0,10) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Order Cancelled Date" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblProdCanclTime" runat="server" Text='<%# Eval("DUOH_nVarProdCancelTime").ToString().Length>0?Eval("DUOH_nVarProdCancelTime").ToString().Substring(0,10):Eval("DUOH_nVarProdCancelTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Product" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblProdName" runat="server" Text='<%# Eval("DUOH_nVarProductName").ToString().Contains("Gold Bar ")? "Gold Bar" : "Silver Bar" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Order Number" ItemStyle-CssClass="ordNum" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblProdOrdNo" runat="server" Text='<%# Eval("DUOH_nVarProdOrderNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Order Price" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblProdBuyPrice" runat="server" Text='<%# Eval("DUOH_decProdBuyPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Upper Limit Price" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdStatus" runat="server" Text='<%# Eval("DUOH_decUpperlmtPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Lower Limit Price" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblPrePayAmt" runat="server" Text='<%# Eval("DUOH_decLowerlmtPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Upper Limit Ratio (%)" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdStatus" runat="server" Text='<%# Eval("DUOH_vCharUppLmtRatio") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Lower Limit Ratio (%)" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblPrePayAmt" runat="server" Text='<%# Eval("DUOH_vCharLowLmtRatio") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Purity" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblPurity" runat="server" Text='<%# Eval("DUOH_nVarProductName").ToString().Contains("Gold Bar")? "24KT" : "S999T" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Purity ID" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblPurityID" runat="server" Text='<%# Eval("DUOH_nVarProductName").ToString().Contains("Gold Bar")? "AU_24" : "AG_999" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Weight" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblProdWeight" runat="server" Text='<%# Eval("DUOH_decProdWeight")+" gm" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Weight" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalWeight" runat="server" Text='<%# Eval("Total_Weight", "{0:0.00}")+" gm" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Quantity" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblProdQty" runat="server" Text='<%# Eval("DUOH_decProdQty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Order Type" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblProdOrderTyp" runat="server" Text='<%# Eval("DUOH_vCharProdOrderType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Sub-supplier ID" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblsubsuppid" runat="server" Text='G2'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Support Center ID" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblsuppcntr" runat="server" Text='M1'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Organization ID" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblDistID" runat="server" Text='<%# Eval("UD_SubUserId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                                    <asp:TemplateField HeaderText="User Level" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblul" runat="server" Text='<%# ddUserLevelSearch.Items[ Convert.ToInt32(Eval("DUM_bIntMemberType")) ].Text %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="Service Fee" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblSrvcfee" runat="server" Text='<%# Eval("Srvc_Fee", "{0:0.00}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblSrvcChrgTot" runat="server" Text=''></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="GST" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblgst" runat="server" Width="70px" Text='<%# Eval("GST", "{0:0.00}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblgstTot" runat="server" Text=''></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Pre-payment Amount" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblPrePayAmt" runat="server" Text='<%# Eval("Prepayment", "{0:0.00}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblPrePayAmtTot" runat="server" Text=''></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="GBean" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblgbean" runat="server" Text='<%# Eval("DUOH_intGbeanRcvd", "{0:0.00}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblgbeanTot" runat="server" Text=''></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Total Pre-payment" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblTotPrePay" runat="server" Text='<%# Eval("Total_Prepay", "{0:0.00}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotPrePayTot" runat="server" Text=''></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cancel Price" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblProdCanclPrice" runat="server" Text='<%# Eval("DUOH_decProdCancelPrice") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Order Status" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdStatus" runat="server" Text='<%# Eval("DUOH_nVarOrderStatus") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Booking Time" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdBookTime" runat="server" Text='<%# Eval("DUOH_nVarProdOrderTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Order Cancel Time" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblOrdCnclTime" runat="server" Text='<%# Eval("DUOH_nVarProdCancelTime") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Volume" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblVolume" runat="server" Text='<%# Eval("Total_Weight", "{0:0.00}")+" gm" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="State" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                            <ItemTemplate>
                                <asp:Label ID="lblState" runat="server" Text='<%# Eval("UD_vCharCity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
            <div class="panel-body">
                <div class="alert alert-info" style="padding: 8px;" runat="server" id="actionInfo">
                    <%-- Select user to Continue..--%>
                </div>
            </div>


        </div>

    </div>

    <%--<center><asp:DropDownList ID="ddlOrderstatus" visible="true" Enabled="false" runat="server"></asp:DropDownList></center>--%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Cnt_PageLevelScripts" runat="server">
    <!--file upload-->
    <%-- <script type="text/javascript" src="AdminExContent/js/bootstrap-fileupload.min.js"></script>--%>
    <!--tags input-->
    <script src="AdminExContent/js/jquery-tags-input/jquery.tagsinput.js"></script>
    <script src="AdminExContent/js/tagsinput-init.js"></script>
    <!--bootstrap input mask-->
    <script type="text/javascript" src="AdminExContent/js/bootstrap-inputmask/bootstrap-inputmask.min.js"></script>
    <!--dynamic table-->
    <script type="text/javascript" src="AdminExContent/js/advanced-datatable/js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="AdminExContent/js/data-tables/DT_bootstrap.js"></script>
    <!-- grid scroll-->
    <%--  <script type="text/javascript" src="https://code.jquery.com/jquery-3.5.1.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.22/js/jquery.dataTables.min.js"></script>--%>
    <!--dynamic table initialization -->
    <script src="https://multiapi.multiicon.in/socket.io/socket.io.js"></script>
    <script src="js/pagesjs/OpenOrdersReport.js"></script>
</asp:Content>
