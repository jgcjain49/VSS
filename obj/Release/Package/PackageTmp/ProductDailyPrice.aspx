<%@ Page Title="" Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="ProductDailyPrice.aspx.cs" Inherits="Admin_CommTrex.ProductDailyPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contHeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
    <div class="page-heading pt">
        <h3>Daily Product Price</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Master </a>
            </li>
            <li class="active">Daily  Product Price</li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <asp:HiddenField ID="activeTab" EnableViewState="true" runat="server" Value="" />

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading custom-tab ">
                <ul class="nav nav-tabs">
                    <li class="active" id="tab_addproducts">
                        <a href="#updateprices" data-toggle="tab">Update Product Price</a>
                    </li>
                    <li class="" id="tab_viewproducts">
                        <a href="#viewprices" data-toggle="tab">View Product Price</a>
                    </li>
                </ul>
            </header>
            <div class="panel-body">
                <div class="tab-content">
                    <div class="tab-pane active" id="updateprices">
                        <div class="row">
                            <section class="panel" id="pnlProductPrices">
                                <header class="panel-heading">
                                    Daily Product Prices
                                    <span class="tools pull-right">
                                        <a href="javascript:;" class="fa fa-chevron-up"></a>
                                        <span class="collapsible-server-hidden">
                                            <asp:HiddenField runat="server" ID="HiddenField1" EnableViewState="true" Value="o" />
                                        </span>
                                    </span>
                                </header>
                                <div class="panel-body collapse">
                                    <div class="form-horizontal adminex-form">
                                        <div class="form-group">
                                            <label class="col-sm-6 col-sm-6 control-label" style="text-align: left">22K Gold Coin Price/gm</label>
                                            <label class="col-sm-6 col-sm-6 control-label" style="text-align: left">24k Gold Coin Price/gm </label>
                                            <div class="col-sm-6 numbers">
                                                <asp:TextBox ID="txtGC22K" name="txtGC22K" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="true"
                                                    data-toggle="tooltip" title="" placeholder="Price for 22K Gold Coin" data-original-title="Price for 22K Gold Coin">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-6 numbers">
                                                <asp:TextBox ID="txtGC24K" name="txtGC24K" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="true"
                                                    data-toggle="tooltip" title="" placeholder="Price for 24K Gold Coin" data-original-title="Price for 24K Gold Coin">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 col-sm-6 control-label" style="text-align: left">95.8 Silver Coin Price/gm </label>
                                            <label class="col-sm-6 col-sm-6 control-label" style="text-align: left">99.9 Silver Coin Price/gm </label>
                                            <div class="col-sm-6 numbers">
                                                <asp:TextBox ID="txtSC95_8" name="txtSC95_8" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="true"
                                                    data-toggle="tooltip" title="" placeholder="Price for 95.8 Silver Coin" data-original-title="Price for 95.8 Silver Coin">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-6 numbers">
                                                <asp:TextBox ID="txtSC99_9" name="txtSC99_9" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="true"
                                                    data-toggle="tooltip" title="" placeholder="Price for 99.9 Silver Coin" data-original-title="Price for 99.9 Silver Coin">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 col-sm-6 control-label" style="text-align: left">22K Gold Biscuit Price/gm </label>
                                            <label class="col-sm-6 col-sm-6 control-label" style="text-align: left">24K Gold Biscuit Price/gm </label>
                                            <div class="col-sm-6 numbers">
                                                <asp:TextBox ID="txtGB22K" name="txtGB22K" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="true"
                                                    data-toggle="tooltip" title="" placeholder="Price for 22k Gold Biscuit" data-original-title="Price for 22k Gold Biscuit">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-6 numbers">
                                                <asp:TextBox ID="txtGB24K" name="txtGB24K" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="true"
                                                    data-toggle="tooltip" title="" placeholder="Price for 24k Gold Biscuit" data-original-title="Price for 24k Gold Biscuit">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-6 col-sm-6 control-label" style="text-align: left">95.8 Silver Biscuit Price/gm </label>
                                            <label class="col-sm-6 col-sm-6 control-label" style="text-align: left">99.9 Silver Biscuit Price/gm </label>
                                            <div class="col-sm-6 numbers">
                                                <asp:TextBox ID="txtSB95_8" name="txtSB95_8" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="true"
                                                    data-toggle="tooltip" title="" placeholder="Price for 95.8 Silver Biscuit" data-original-title="Price for 95.8 Silver Biscuit">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-6 numbers">
                                                <asp:TextBox ID="txtSB99_9" name="txtSB99_9" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="true"
                                                    data-toggle="tooltip" title="" placeholder="Price for 99.9 Silver Biscuit" data-original-title="Price for 99.9 Silver Biscuit">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-sm-4 control-label" style="text-align: left">18K Gold Jewellery Price/gm </label>
                                            <label class="col-sm-4 col-sm-4 control-label" style="text-align: left">22K Gold Jewellery Price/gm </label>
                                            <label class="col-sm-4 col-sm-4 control-label" style="text-align: left">Making Charges for Gold Jewellery</label>
                                            <div class="col-sm-4 numbers">
                                                <asp:TextBox ID="txtGJ18K" name="txtGJ18K" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="true"
                                                    data-toggle="tooltip" title="" placeholder="Price for Gold Jewellery 18K" data-original-title="Price for Gold Jewellery 18K">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 numbers">
                                                <asp:TextBox ID="txtGJ22K" name="txtGJ22K" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="true"
                                                    data-toggle="tooltip" title="" placeholder="Price for Gold Jewellery 22K" data-original-title="Price for Gold Jewellery 22K">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 numbers ">
                                                <asp:TextBox ID="txtGJMC" name="txtGJMC" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="true"
                                                    data-toggle="tooltip" title="" placeholder="Making Charge for Gold Jewellery" data-original-title="Making Charge for Gold Jewellery">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 col-sm-4 control-label" style="text-align: left">95.8 Silver Jewellery Price/gm </label>
                                            <label class="col-sm-4 col-sm-4 control-label" style="text-align: left">99.9 Silver Jewellery Price/gm </label>
                                            <label class="col-sm-4 col-sm-4 control-label" style="text-align: left">Making Charges for Silver Jewellery</label>
                                            <div class="col-sm-4 numbers">
                                                <asp:TextBox ID="txtSJ95_8" name="txtSJ95_8" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="true"
                                                    data-toggle="tooltip" title="" placeholder="Price for Silver Jewellery 95.8" data-original-title="Price for Silver Jewellery 95.8">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 numbers">
                                                <asp:TextBox ID="txtSJ99_9" name="txtSJ99_9" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="true"
                                                    data-toggle="tooltip" title="" placeholder="Price for Silver Jewellery 99.9" data-original-title="Price for Silver Jewellery 99.9">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4 numbers ">
                                                <asp:TextBox ID="txtSJMC" name="txtSJMC" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="true"
                                                    data-toggle="tooltip" title="" placeholder="Making Charge for Silver Jewellery" data-original-title="Making Charge for Silver Jewellery">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label" style="text-align: left">GST in Percentage</label>
                                            <div class="col-sm-2 numbers ">
                                                <asp:TextBox ID="txtGST" name="txtGST" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="true"
                                                    MaxLength="4" data-toggle="tooltip" title="" placeholder="GST in Percentage" data-original-title="GST in Percentage">
                                                </asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-lg-12">
                                                <section class="panel">
                                                    <div class="panel-body" style="text-align: center">
                                                        <button class="pnl-opener btn gbtn" type="button"
                                                            btn-action="Save" data-open-on="Save" data-open-panels="pnlProductMaster"
                                                            onserverclick="btnSave_ServerClick"
                                                            runat="server" id="btnSave">
                                                            <i class="fa fa-plus-square"></i>Save
                                                        </button>
                                                        <button class="btn gbtn1" type="button"
                                                            runat="server" id="btnClear" onserverclick="btnClear_ServerClick">
                                                            Clear <i class="fa fa-refresh"></i>
                                                        </button>
                                                        <%--  <button>Save</button>
                                                        <button>Clear</button>--%>
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="alert toss" style="padding: 8px;" runat="server" id="actionInfo">
                                                            Press new to add Product.
                                                        </div>
                                                    </div>
                                                </section>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </section>
                        </div>
                    </div>

                    <div class="tab-pane" id="viewprices">
                        <div class="form-horizontal adminex-form">
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel1">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnLogSearch" />
                                    <asp:PostBackTrigger ControlID="btnLogClear" />
                                </Triggers>

                                <ContentTemplate>

                                    <div class="alert toss" runat="server" id="Div1" style="display: none;">
                                    </div>
                                    <div class="panel-body">
                                        <header class="panel-heading"></header>



                                        <div class="row col-sm-4">
                                            <label class="control-label">Date </label>
                                            <input type="date" id="dtLogDate" class="custom-input" runat="server" />
                                        </div>

                                        <div class="row col-sm-4 align-right" style="padding-top: 2px;">
                                            <button class="btn gbtn" type="button" runat="server" onserverclick="btnLogSearch_ServerClick" id="btnLogSearch" title="Search">
                                                Search&nbsp;&nbsp;<i class="fa fa-search"></i>
                                            </button>
                                            <button class="btn gbtn1" type="button" onserverclick="btnLogClear_ServerClick" runat="server" id="btnLogClear" title="Reset" style="float: left">
                                                Clear&nbsp;&nbsp;<i class="fa fa-undo"></i>
                                            </button>

                                        </div>





                                        <%--<button class="btn btn-success" type="button" onserverclick="btnExpotClick" runat="server" id="btnExpot">
                            Download Report <i class="fa fa-download"></i>
                        </button>--%>
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                    </div>

                                    <div class="panel-body adv-table" style="overflow-x: scroll;">
                                        <asp:GridView runat="server" ID="grdViewProduct" DataKeyNames="PDP_bIntId"
                                            AutoGenerateColumns="false" AllowPaging="false" OnPageIndexChanging="grdViewProduct_PageIndexChanging"
                                            HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                            RowStyle-Wrap="false" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"
                                            class="dyna mic-table-grid display table table-bordered table-striped">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No" HeaderStyle-CssClass="hdrAlgnCntrStyle" FooterStyle-CssClass="hdrAlgnCntrStyle">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="hdrAlgnCntrStyle" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("PDP_bIntId") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product Name" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("PDP_vCharProdName") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Purity" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPurity" runat="server" Text='<%# Eval("PDP_decPurity") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("PDP_decPrice") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Making Charge ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMakingCharge" runat="server" Text='<%# Eval("PDP_decMakingChrg") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GST" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGST" runat="server" Text='<%# Eval("PDP_decGST") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>

                    </div>
                </div>

            </div>
        </section>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Cnt_PageLevelScripts" runat="server">
    <script src="js/pagesjs/ProductDailyPrice.js"></script>
</asp:Content>
