<%@ Page Title="Comm Trex Admin" Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="OrderApproval.aspx.cs" Inherits="Admin_CommTrex.OrderApproval" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contHeadContent" runat="server">
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/themes/start/jquery-ui.css" />
    <style>
        .hdrAlgnCntrStyle {
            padding: 10px 20px 10px 20px !important;
            text-align: center !important;
        }

        .itmAddressStyle {
            min-width: 300px !important;
            white-space: normal;
            text-transform: capitalize;
        }

        .itmEmailStyle {
            text-transform: lowercase;
        }

        .ui-widget-header {
            border: 1px solid #967777;
            background: rgb(150,119,119);
            background: linear-gradient(0deg, rgba(150,119,119,1) 0%, rgba(150,119,119,1) 35%, rgba(222,175,175,1) 100%);
            color: #eaf5f7;
            font-weight: bold;
        }

        .prod-img {
            display: block;
            width: 50px !important;
            height: auto;
            margin: 2px 0px;
            border: 2px solid #2042ae !important;
            border-radius: 13px;
        }

        .custom-input {
            width: 55.66667%;
            height: 26px;
            border: 1px solid #ccc;
            border-radius: 8px;
            float: right;
            padding-left: 8px;
            margin: 3px 0px;
        }

        .dwnldInvoiceCls {
            background: #efefef;
            text-decoration: none;
        }

        .acceptLnkCls {
            width: 77px;
        }

        .rejectLnkCls {
            width: 77px;
        }

        .align-left {
            text-align: left;
        }

        .align-right {
            text-align: right;
        }

        .align-center {
            text-align: center;
        }

        textarea {
            resize: none;
        }

        .mandate {
            color: red;
            font-weight: 600;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
    <div class="page-heading pt">
        <h3>Order Approval</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Master </a>
            </li>
            <li class="active">Order Approval</li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <asp:HiddenField ID="activeTab" EnableViewState="true" runat="server" Value="" />
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading custom-tab ">
                <ul class="nav nav-tabs" runat="server" >
                    <li class="active" id="Pending" runat="server">
                        <a href="#PendingOrder" data-toggle="tab">Pending Orders</a>
                    </li>
                    <li class="" id="Accepted" runat="server">
                        <a href="#AcceptedOrder" data-toggle="tab">Accepted Orders</a>
                    </li>
                    <li class="" id="Rejected">
                        <a href="#RejectedOrder" data-toggle="tab">Rejected Orders</a>
                    </li>
                    <li class="" id="Dispatched">
                        <a href="#DispatchOrder" data-toggle="tab">Dispatched Orders</a>
                    </li>
                </ul>
            </header>
            <div class="panel-body">
                <div class="tab-content">
                    <!-- 1st tab Pending-->
                    <div class="tab-pane active" id="PendingOrder">
                        <div class="form-group">
                            <div class="form-horizontal adminex-form">
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updatePanelPending">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnPendingExport" />
                                        <asp:PostBackTrigger ControlID="btnPendingSearch" />
                                        <asp:PostBackTrigger ControlID="btnPendingClear" />
                                    </Triggers>

                                    <ContentTemplate>
                                        <section class="panel">
                                            <div class="panel-body">
                                                <header class="panel-heading" id="updateReportText">Order Search Filters</header>

                                                <%--<div class="form-group">
                                            <label class="col-sm-2 control-label">User Name</label>
                                            <div class="col-sm-4 control-label">
                                                <input type="text" class="text form-control" id="txtorgid" runat="server" />
                                            </div>
                                            <label class="col-sm-2 control-label">Mobile No.</label>
                                            <div class="col-sm-4 control-label">
                                                <input type="text" class="form-control" id="txtusermob" runat="server" maxlength="10" oninput="onlyNum(this)" />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Order No.</label>
                                            <div class="col-sm-10 control-label">
                                                <input type="text" id="txtOrdNo" runat="server" maxlength="20" oninput="onlyNum(this)" class="form-control" />
                                            </div>
                                        </div>--%>

                                                <div class="row col-sm-4">
                                                    <label class="control-label">User Name</label>
                                                    <input type="text" id="txtUserName_Pend" class="custom-input" runat="server" />
                                                </div>
                                                <div class="row col-sm-4">
                                                    <label class="control-label">Mobile No.</label>
                                                    <input type="text" id="txtUserPhn_Pend" class="custom-input" runat="server" maxlength="10" oninput="onlyNum(this)" />
                                                </div>
                                                <div class="row col-sm-4">
                                                    <label class="control-label">Order No.</label>
                                                    <input type="text" id="txtOrdNo_Pend" class="custom-input" runat="server" maxlength="20" oninput="onlyNum(this)" />
                                                </div>

                                                <div class="row col-sm-4">
                                                    <label class="control-label">Order Date (From)</label>
                                                    <input type="date" id="ordPlcDtFrm_Pend" class="custom-input" runat="server" onchange="ordPlcDtSelected(event);" />
                                                </div>
                                                <div class="row col-sm-4">
                                                    <label class="control-label">Order Date (Till)</label>
                                                    <input type="date" id="ordPlcDtTill_Pend" class="custom-input" runat="server" disabled="disabled" />
                                                </div>
                                                <div class="row col-sm-4 align-right" style="padding-top: 2px;">
                                                    <button class="btn gbtn1" type="button" runat="server" onserverclick="btnReset_ServerClick" id="btnPendingClear" title="Reset" style="float: left">
                                                        Clear&nbsp;&nbsp;<i class="fa fa-undo"></i>
                                                    </button>
                                                    <button class="btn gbtn" type="button" runat="server" onserverclick="btnSearchPending_Click" id="btnPendingSearch" title="Search">
                                                        Search&nbsp;&nbsp;<i class="fa fa-search"></i>
                                                    </button>
                                                </div>
                                                  <div class="panel-body">
                                                      <%--alert-info--%> 
                                        <div class="alert " style="padding: 8px;" runat="server" id="actionInfo">
                                            Search Record 
                                        </div>
                                    </div>

                                            </div>

                                            <div class="alert toss" runat="server" id="pendingMsgDiv" style="display: none;">
                                            </div>

                                            <div>
                                                <div style="padding-bottom: 10px; text-align: center; display: none;" id="divPendingOrder" runat="server">
                                                    <button class="btn gbtn1" type="button" id="btnPendingExport" runat="server"
                                                        onserverclick="btnExportPending_ServerClick">
                                                        Download Report&nbsp;&nbsp;<i class="fa fa-download"></i>
                                                    </button>
                                                    <asp:HiddenField ID="hidOrderID" runat="server" />
                                                    <asp:HiddenField ID="hidOrderNo" runat="server" />
                                                    <asp:HiddenField ID="hidOrderprodID" runat="server" />
                                                    <asp:HiddenField ID="hidOrderQty" runat="server" />
                                                    <asp:HiddenField ID="hidClientPhn" runat="server" />
                                                </div>
                                                <div class="panel-body adv-table" style="overflow-x: scroll;">
                                                    <asp:GridView runat="server" ID="grdPending" DataKeyNames="DUWD_bIntWalletId"
                                                        AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grdPending_PageIndexChanging"
                                                        OnRowDataBound="grdPending_RowDataBound" OnRowCommand="grdPending_RowCommand"
                                                        HeaderStyle-Wrap="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                        RowStyle-Wrap="false" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"
                                                        class="dynamic-table-grid display table table-bordered table-striped">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr No" HeaderStyle-CssClass="hdrAlgnCntrStyle" FooterStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <%--<FooterTemplate>
                                                                    <b><asp:Label ID="FtrLblSum" runat="server" Text='Sum'></asp:Label></b>
                                                                </FooterTemplate>--%>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField Visible="false" HeaderText="ID" HeaderStyle-CssClass="hdrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("DUWD_bIntWalletId") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField Visible="false" HeaderText="ClientID" HeaderStyle-CssClass="hdrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClientID" runat="server" Text='<%# Eval("DUWD_nVarWalletNumber") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="User Name" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-Wrap="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("DUWD_decWallBal") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="User Mobile" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserMobile" runat="server" Text='<%# Eval("DUWD_vCharTransactionType") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delivery Contact Person" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-Wrap="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClientName" runat="server" Text='<%# Eval("DUWD_dtTransactionTime") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delivery Contact Number" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClientPhn" runat="server" Text='<%# Eval("DUWD_nVarExtraField1") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delivery Contact Email" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-CssClass="itmEmailStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClientEmail" runat="server" Text='<%# Eval("DUWD_nVarExtraField2") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delivery Address" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-CssClass="itmAddressStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("DUWD_vCharExtraField3") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                          

                                                          <%--  <asp:TemplateField HeaderText="Product Image" HeaderStyle-CssClass="hdrAlgnCntrStyle" HeaderStyle-Width="75px" ItemStyle-Width="75px">
                                                                <ItemTemplate>
                                                                    <asp:Image Visible="false" ID="Image1" runat="server" Style="width: inherit" data-toggle="modal" data-target="#enlargeImgMod"
                                                                        ImageUrl='<%# "https://www.goldifyapp.com/admin/"+Eval("PI_vCharImgPath").ToString().Trim() %>'
                                                                        data-imgurl='<%# "https://www.goldifyapp.com/admin/"+Eval("PI_vCharImgPath").ToString().Trim() %>' />
                                                                    <asp:Label Visible="false" runat="server" ID="lblProdImgs" Text='<%# Eval("PI_vCharImgPath") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>

                                                            <asp:TemplateField HeaderText="Product Name" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Literal runat="server" ID="lblProdName"
                                                                        Text='<%# string.Join("<br /><br />", Eval("DUWD_vCharExtraField4").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Product Quantity" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Literal runat="server" ID="lblOrdQty"
                                                                        Text='<%# string.Join("<br /><br />", Eval("DUWD_IntExtraField5").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Available Quantity" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Literal runat="server" ID="lblAvailability"
                                                                        Text='<%# string.Join("<br /><br />", Eval("DUWD_decAmtDebCred").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Product Weight (in gm)" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Literal runat="server" ID="lblProdWeight"
                                                                        Text='<%# string.Join("<br /><br />", Eval("DUWD_nCharTnxRefNo").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order Time" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderTime" runat="server" Text='<%# Eval("DUWD_charTransactedBY","{0:yyyy-MM-dd HH:mm:ss}") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order ID" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderNum" runat="server" Text='<%# Eval("DUWD_charReason") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                           <%-- <asp:TemplateField HeaderText="Order Status" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderStatus" runat="server" Text='<%# Eval("EM_vOrderStatus") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>

                                                          <%--  <asp:TemplateField HeaderText="User Total Recharge" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRechargelAmt" runat="server" Text='<%# Eval("Recharge","{0:n2}")%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="User Total Withdraw" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWithdrawllAmt" runat="server" Text='<%# Eval("Withdrawal","{0:n2}")%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="GBeans Redeemed" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGbeanlAmt" runat="server" Text='<%# Eval("EM_intGbeanUsed","{0:n2}")%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Wallet Amount Used" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWalletAmt" runat="server" Text='<%# Eval("EM_decFinalAmt","{0:n2}") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>

                                                          <%--  <asp:TemplateField HeaderText="Order Total Amount" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFinalAmt" runat="server" Text='<%# Eval("FinalAmount","{0:n2}") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>

                                                            <asp:TemplateField HeaderText="Accept Order" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <%--<asp:Button Visible="false" CssClass="btn gbtns" ID="btnAccept" Text="Accept" runat="server"
                                                                        CommandName="Accept" CommandArgument='<%# Eval("EM_bIntId")+","+ Eval("EM_vCharOrdNum")+","+Eval("EM_bIntClientPhNo") %>' />--%>
                                                                    <a id="acceptLnk" class="acceptLnkCls btn gbtns" runat="server"
                                                                        data-ord-id='<%# Eval("DUWD_bIntWalletId") %>' data-ord-num='<%# Eval("DUWD_nVarWalletNumber") %>'
                                                                        data-user-phn='<%# Eval("DUWD_nVarWalletNumber") %>' href="javascript:;">Accept</a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Reject Order" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <a id="rejectLnk" class="rejectLnkCls btn gbtns" runat="server"
                                                                        data-ord-id='<%# Eval("DUWD_bIntWalletId") %>' data-ord-num='<%# Eval("DUWD_nVarWalletNumber") %>'
                                                                        data-user-phn='<%# Eval("DUWD_nVarWalletNumber") %>' href="javascript:;">Reject</a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:CommandField ShowEditButton="True" HeaderText="Actions" HeaderStyle-CssClass="nosort" />--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <div style="padding: 8px;" runat="server" id="rowTotal"></div>
                                                </div>
                                            </div>
                                        </section>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                    <!-- 2nd tab Accepted-->
                    <div class="tab-pane" id="AcceptedOrder" >
                        <div class="form-group">
                            <div class="form-horizontal adminex-form">
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updatePanelAccepted">
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnAcceptExport" />
                                        <asp:PostBackTrigger ControlID="btnHidInvoiceDwnld" />
                                        <asp:PostBackTrigger ControlID="btnAcceptSearch" />
                                        <asp:PostBackTrigger ControlID="btnAcceptClear" />
                                    </Triggers>

                                    <ContentTemplate>
                                        <section class="panel">
                                            <div class="panel-body">
                                                <header class="panel-heading">Order Search Filters</header>                                       

                                                <div class="row col-sm-4">
                                                    <label class="control-label">User Name</label>
                                                    <input type="text" id="txtUserName_Accpt" class="custom-input" runat="server" />
                                                </div>
                                                <div class="row col-sm-4">
                                                    <label class="control-label">Mobile No.</label>
                                                    <input type="text" id="txtUserPhn_Accpt" class="custom-input" runat="server" maxlength="10" oninput="onlyNum(this)" />
                                                </div>
                                                <div class="row col-sm-4">
                                                    <label class="control-label">Order No.</label>
                                                    <input type="text" id="txtOrdNo_Accpt" class="custom-input" runat="server" maxlength="20" oninput="onlyNum(this)" />
                                                </div>

                                                <div class="row col-sm-4">
                                                    <label class="control-label">Order Date (From)</label>
                                                    <input type="date" id="ordPlcDtFrm_Accpt" class="custom-input" runat="server" onchange="acceptDtSelected(event);" />
                                                </div>
                                                <div class="row col-sm-4">
                                                    <label class="control-label">Order Date (Till)</label>
                                                    <input type="date" id="ordPlcDtTill_Accpt" class="custom-input" runat="server" disabled="disabled" />
                                                </div>
                                                <div class="row col-sm-4 align-right" style="padding-top: 2px;">
                                                    <button class="btn gbtn1" type="button" runat="server" onserverclick="btnAcceptClear_ServerClick" id="btnAcceptClear" title="Reset" style="float: left">
                                                        Clear&nbsp;&nbsp;<i class="fa fa-undo"></i>
                                                    </button>
                                                    <button class="btn gbtn" type="button" runat="server" onserverclick="btnAcceptSearch_ServerClick" id="btnAcceptSearch" title="Search">
                                                        Search&nbsp;&nbsp;<i class="fa fa-search"></i>
                                                    </button>
                                                </div>

                                            </div>

                                            <div class="alert toss" runat="server" id="acceptedMsgDiv" style="display: block;">
                                            </div>

                                            <div>
                                                <div style="padding-bottom: 10px; text-align: center; display: none;" id="divAcceptExport" runat="server">
                                                    <button class="btn gbtn1" type="button" onserverclick="btnExportAccept_ServerClick" runat="server" id="btnAcceptExport">
                                                        Download Report&nbsp;&nbsp;<i class="fa fa-download"></i>
                                                    </button>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    <div hidden="hidden">
                                                        <asp:Button runat="server" ID="btnHidInvoiceDwnld" OnClick="btnDwnldInvoiceClick" Style="display: none" />
                                                    </div>
                                                    <asp:HiddenField ID="hidRowIdForInvoice" runat="server" />
                                                </div>
                                                <div class="panel-body adv-table" style="overflow-x: scroll;">
                                                    <asp:GridView runat="server" ID="grdAccepted" DataKeyNames="EM_bIntId"
                                                        AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="grdAccepted_PageIndexChanging"
                                                        OnRowDataBound="grdAccepted_RowDataBound" OnRowCommand="grdAccepted_RowCommand"
                                                        HeaderStyle-Wrap="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                        RowStyle-Wrap="false" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"
                                                        class="dyna mic-table-grid display table table-bordered table-striped">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr No" HeaderStyle-CssClass="hdrAlgnCntrStyle" FooterStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <%-- <FooterTemplate>
                                                            <b>
                                                                <asp:Label ID="FtrLblSum" runat="server" Text='Sum'></asp:Label></b>
                                                        </FooterTemplate>--%>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="hdrStyle" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("EM_bIntId") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ClientID" HeaderStyle-CssClass="hdrStyle" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClientID" runat="server" Text='<%# Eval("EM_bIntUserId") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="User Name" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-Wrap="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UD_vCharName") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="User Mobile" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserMobile" runat="server" Text='<%# Eval("UD_vCharPhoneNumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delivery Contact Person" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-Wrap="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClientName" runat="server" Text='<%# Eval("EM_vCharClientName") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <%-- <asp:TemplateField HeaderText="Mobile" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClientMobile" runat="server" Text='<%# Eval("EM_bIntClientPhNo").ToString().Substring(0,3)+"*****"+Eval("EM_bIntClientPhNo").ToString().Substring(8) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                            <asp:TemplateField HeaderText="Delivery Contact Number" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClientPhn" runat="server" Text='<%# Eval("DUWD_nVarWalletNumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delivery Contact Email" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-CssClass="itmEmailStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClientEmail" runat="server" Text='<%# Eval("EM_vCharClientEmailId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delivery Address" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-CssClass="itmAddressStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("EM_vCharClientAddress1") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Product Image" HeaderStyle-CssClass="hdrAlgnCntrStyle" HeaderStyle-Width="75px" ItemStyle-Width="75px">
                                                                <%-- 7th --%>
                                                                <ItemTemplate>
                                                                    <asp:Image Visible="false" ID="Image1" runat="server" Style="width: inherit" data-toggle="modal" data-target="#enlargeImgMod"
                                                                        ImageUrl='<%# "https://www.goldifyapp.com/admin/"+Eval("PI_vCharImgPath").ToString().Trim() %>'
                                                                        data-imgurl='<%# "https://www.goldifyapp.com/admin/"+Eval("PI_vCharImgPath").ToString().Trim() %>' />

                                                                    <asp:Label Visible="false" runat="server" ID="lblProdImgs" Text='<%# Eval("PI_vCharImgPath") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Product Name" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <%-- <ItemTemplate>
                                                                        <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("PM_nVarProdName") %>'>
                                                                        </asp:Label>
                                                                    </ItemTemplate>--%>
                                                                <ItemTemplate>
                                                                    <asp:Literal runat="server" ID="lblProdName"
                                                                        Text='<%# string.Join("<br /><br />", Eval("PM_vCharProdName").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Product Quantity">
                                                                <ItemTemplate>
                                                                    <asp:Literal runat="server" ID="lblOrdQty"
                                                                        Text='<%# string.Join("<br /><br />", Eval("OrdSub_decQty").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Product Weight (in gm)" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Literal runat="server" ID="lblProdWeight"
                                                                        Text='<%# string.Join("<br /><br />", Eval("PM_decWeight").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Total Weight (in gm)" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblTotalWeight">
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField Visible="false" HeaderText="Product Price">
                                                                <ItemTemplate>
                                                                    <asp:Literal runat="server" ID="lblPordPrice"
                                                                        Text='<%# string.Join("<br /><br />", Eval("OrdSub_decBasicAmt").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order Time" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderTime" runat="server" Text='<%# Eval("EM_dtEnquiryTime","{0:yyyy-MM-dd HH:mm:ss}") %>'></asp:Label>
                                                                    <asp:Label ID="lblOrderDate" runat="server" Text='<%# Eval("EM_dtEnquiryTime","{0:yyyy-MM-dd}") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order ID" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderNum" runat="server" Text='<%# Eval("EM_vCharOrdNum") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order Status" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderStatus" runat="server" Text='<%# Eval("EM_vOrderStatus") %>' CssClass='<%# Eval("EM_vOrderStatus").Equals("failure")?"failed-style":"success-style" %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="User Total Recharge" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRechargelAmt" runat="server" Text='<%# Eval("Recharge","{0:n2}")%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="User Total Withdraw" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWithdrawllAmt" runat="server" Text='<%# Eval("Withdrawal","{0:n2}")%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Gbeans Redeemed" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGbeanlAmt" runat="server" Text='<%# Eval("EM_intGbeanUsed","{0:n2}")%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Wallet Amount Used" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWalletAmt" runat="server" Text='<%# Eval("EM_decFinalAmt","{0:n2}") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order Total Amount" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFinalAmt" runat="server" Text='<%# Eval("FinalAmount","{0:n2}") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField Visible="false" HeaderText="User Additional Details">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClientGST" runat="server" Text='<%# Eval("EM_vCharGstNo") %>' />
                                                                    <asp:Label ID="lblClientPAN" runat="server" Text='<%# Eval("EM_vCharClientPAN") %>' />
                                                                    <asp:Label ID="lblClientAadhaar" runat="server" Text='<%# Eval("EM_vCharClientAadhaar") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <a id="dwnldInvoiceLink" class="dwnldInvoiceCls btn gbtns" runat="server"
                                                                        data-order-rowid='<%# ((GridViewRow) Container).RowIndex %>'
                                                                        href="javascript:;">Generate Invoice</a>
                                                                    <%-- not triggering file download bcoz of update panel, using above link as alternative
                                                                        <asp:Button CssClass="btn gbtns" runat="server" ID="btnDwnldInvoice" Text="Generate Invoice"
                                                                        RowIndex='<%# ((GridViewRow) Container).RowIndex %>' OnClick="btnDwnldInvoiceClick" />--%>
                                                                    <asp:Button CssClass="btn gbtns" runat="server" ID="btnDispatch" Text="Dispatch"
                                                                        CommandName="Dispatch" CommandArgument='<%# Eval("EM_bIntId")+","+ Eval("EM_vCharOrdNum")%>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <a id="rejectLnk" class="rejectLnkCls btn gbtns" runat="server"
                                                                        data-ord-id='<%# Eval("EM_bIntId") %>' data-ord-num='<%# Eval("DUWD_nVarWalletNumber") %>'
                                                                        data-user-phn='<%# Eval("DUWD_nVarWalletNumber") %>' href="javascript:;">Reject</a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>

                                                    </asp:GridView>
                                                    <div style="padding: 8px;" runat="server" id="rowTotalAccept"></div>
                                                </div>
                                            </div>
                                        </section>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                    <!-- 3rd tab Rejected-->
                    <div class="tab-pane" id="RejectedOrder">
                        <div class="form-group">
                            <div class="form-horizontal adminex-form">
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updReject">
                                    <ContentTemplate>

                                        <section class="panel">
                                            <div class="panel-body">
                                                <header class="panel-heading">Order Search Filters</header>

                           

                                                <div class="row col-sm-4">
                                                    <label class="control-label">User Name</label>
                                                    <input type="text" id="txtUserName_Rjct" class="custom-input" runat="server" />
                                                </div>
                                                <div class="row col-sm-4">
                                                    <label class="control-label">Mobile No.</label>
                                                    <input type="text" id="txtUserPhn_Rjct" class="custom-input" runat="server" maxlength="10" oninput="onlyNum(this)" />
                                                </div>
                                                <div class="row col-sm-4">
                                                    <label class="control-label">Order No.</label>
                                                    <input type="text" id="txtOrdNo_Rjct" class="custom-input" runat="server" maxlength="20" oninput="onlyNum(this)" />
                                                </div>

                                                <div class="row col-sm-4">
                                                    <label class="control-label">Order Date (From)</label>
                                                    <input type="date" id="ordPlcDtFrm_Rjct" class="custom-input" runat="server" onchange="rejectDtSelected(event);" />
                                                </div>
                                                <div class="row col-sm-4">
                                                    <label class="control-label">Order Date (Till)</label>
                                                    <input type="date" id="ordPlcDtTill_Rjct" class="custom-input" runat="server" disabled="disabled" />
                                                </div>
                                                <div class="row col-sm-4 align-right" style="padding-top: 2px;">
                                                    <button class="btn gbtn1" type="button" runat="server" onserverclick="btnRejectClear_ServerClick" id="btnRejectClear" title="Reset" style="float: left">
                                                        Clear&nbsp;&nbsp;<i class="fa fa-undo"></i>
                                                    </button>
                                                    <button class="btn gbtn" type="button" runat="server" onserverclick="btnRejectSearch_ServerClick" id="btnRejectSearch" title="Search">
                                                        Search&nbsp;&nbsp;<i class="fa fa-search"></i>
                                                    </button>
                                                </div>


                                                <%--<tr style="margin-top: 15px;">
                            <td style="padding-right: 15px;">
                                <label class="control-label">
                                    Status
                                </label>
                            </td>
                            <td style="padding-right: 30px;">
                                <asp:DropDownList runat="server" ID="ddlStatus" Style="height: 34px;">
                                    <asp:ListItem Text="--Please Select --" />
                                    <asp:ListItem Text="Success" />
                                    <asp:ListItem Text="Failure" />
                                </asp:DropDownList>
                            </td>
                        </tr>--%>
                                            </div>

                                            <div class="alert toss" runat="server" id="rejectedMsgDiv" style="display: none;">
                                            </div>

                                            <div>
                                                <div style="padding-bottom: 10px; text-align: right; display: none;" id="divRejectExport" runat="server">
                                                    <button class="btn gbtn1" type="button" onserverclick="btnExportReject_ServerClick" runat="server" id="btnRejectExport">
                                                        Download Report&nbsp;&nbsp;<i class="fa fa-download"></i>
                                                    </button>
                                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                                </div>

                                                <div class="panel-body adv-table" style="overflow-x: scroll;">
                                                    <asp:GridView runat="server" ID="grdRejected" DataKeyNames="EM_bIntId"
                                                        AutoGenerateColumns="false" AllowPaging="true"
                                                        OnRowDataBound="grdRejected_RowDataBound" OnPageIndexChanging="grdRejected_PageIndexChanging"
                                                        HeaderStyle-Wrap="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                        RowStyle-Wrap="false" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"
                                                        class="dyna mic-table-grid display table table-bordered table-striped">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr No" HeaderStyle-CssClass="hdrAlgnCntrStyle" FooterStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <b>
                                                                        <asp:Label ID="FtrLblSum" runat="server" Text='Sum'></asp:Label></b>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="hdrStyle" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("EM_bIntId") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ClientID" HeaderStyle-CssClass="hdrStyle" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClientID" runat="server" Text='<%# Eval("EM_bIntUserId") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="User Name" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-Wrap="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UD_vCharName") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="User Mobile" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserMobile" runat="server" Text='<%# Eval("UD_vCharPhoneNumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delivery Contact Person" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-Wrap="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClientName" runat="server" Text='<%# Eval("EM_vCharClientName") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%-- <asp:TemplateField HeaderText="Mobile" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClientMobile" runat="server" Text='<%# Eval("EM_bIntClientPhNo").ToString().Substring(0,3)+"*****"+Eval("EM_bIntClientPhNo").ToString().Substring(8) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                            <asp:TemplateField HeaderText="Delivery Contact Number" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClientPhn" runat="server" Text='<%# Eval("EM_bIntClientPhNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delivery Contact Email" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-CssClass="itmEmailStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClientEmail" runat="server" Text='<%# Eval("EM_vCharClientEmailId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delivery Address" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-CssClass="itmAddressStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("EM_vCharClientAddress1") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Product Image" HeaderStyle-CssClass="hdrAlgnCntrStyle" HeaderStyle-Width="75px" ItemStyle-Width="75px">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblProdImgs_Rjct" Text='<%# Eval("PI_vCharImgPath") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Product Name" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Literal runat="server" ID="lblProdName"
                                                                        Text='<%# string.Join("<br /><br />", Eval("PM_vCharProdName").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Product Quantity" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Literal runat="server" ID="lblOrdQty"
                                                                        Text='<%# string.Join("<br /><br />", Eval("OrdSub_decQty").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Product Weight (in gm)" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Literal runat="server" ID="lblProdWeight"
                                                                        Text='<%# string.Join("<br /><br />", Eval("PM_decWeight").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order Time" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderTime" runat="server" Text='<%# Eval("EM_dtEnquiryTime","{0:yyyy-MM-dd HH:mm:ss}") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order ID" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderNum" runat="server" Text='<%# Eval("EM_vCharOrdNum") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order Status" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderStatus" runat="server" Text='<%# Eval("EM_vOrderStatus") %>' CssClass='<%# Eval("EM_vOrderStatus").Equals("failure")?"failed-style":"success-style" %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="User Total Recharge" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRechargelAmt" runat="server" Text='<%# Eval("Recharge","{0:n2}")%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="User Total Withdraw" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWithdrawllAmt" runat="server" Text='<%# Eval("Withdrawal","{0:n2}")%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gbeans Redeemed" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGbeanlAmt" runat="server" Text='<%# Eval("EM_intGbeanUsed","{0:n2}")%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order Total Amount" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFinalAmt" runat="server" Text='<%# Eval("FinalAmount","{0:n2}") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <div style="padding: 8px;" runat="server" id="rowTotalReject"></div>
                                                </div>
                                            </div>
                                        </section>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnRejectExport" />
                                        <asp:PostBackTrigger ControlID="btnRejectSearch" />
                                        <asp:PostBackTrigger ControlID="btnRejectClear" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                    <!-- 4th tab Dispatch-->
                    <div class="tab-pane" id="DispatchOrder">
                        <div class="form-group">
                            <div class="form-horizontal adminex-form">
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel2">
                                    <ContentTemplate>
                                        <section class="panel">
                                            <div class="panel-body">
                                                <header class="panel-heading">Order Search Filters</header>

                               

                                                <div class="row col-sm-4">
                                                    <label class="control-label">User Name</label>
                                                    <input type="text" id="txtUserName_Disp" class="custom-input" runat="server" />
                                                </div>
                                                <div class="row col-sm-4">
                                                    <label class="control-label">Mobile No.</label>
                                                    <input type="text" id="txtUserPhn_Disp" class="custom-input" runat="server" maxlength="10" oninput="onlyNum(this)" />
                                                </div>
                                                <div class="row col-sm-4">
                                                    <label class="control-label">Order No.</label>
                                                    <input type="text" id="txtOrdNo_Disp" class="custom-input" runat="server" maxlength="20" oninput="onlyNum(this)" />
                                                </div>

                                                <div class="row col-sm-4">
                                                    <label class="control-label">Order Date (From)</label>
                                                    <input type="date" id="ordPlcDtFrm_Disp" class="custom-input" runat="server" onchange="dispatchDtSelected(event);" />
                                                </div>
                                                <div class="row col-sm-4">
                                                    <label class="control-label">Order Date (Till)</label>
                                                    <input type="date" id="ordPlcDtTill_Disp" class="custom-input" runat="server" disabled="disabled" />
                                                </div>
                                                <div class="row col-sm-4 align-right" style="padding-top: 2px;">
                                                    <button class="btn gbtn1" type="button" runat="server" onserverclick="btnDisClear_ServerClick" id="btnDispatchClear" title="Reset" style="float: left">
                                                        Clear&nbsp;&nbsp;<i class="fa fa-undo"></i>
                                                    </button>
                                                    <button class="btn gbtn" type="button" runat="server" onserverclick="btnDisSearch_ServerClick" id="btnDispatchSearch" title="Search">
                                                        Search&nbsp;&nbsp;<i class="fa fa-search"></i>
                                                    </button>
                                                </div>

                                            </div>

                                            <div class="alert toss" runat="server" id="dispatchedMsgDiv" style="display: none;">
                                            </div>

                                            <div>
                                                <div style="padding-bottom: 10px; text-align: center; display: none;" id="divExportDisOrder" runat="server">
                                                    <button class="btn gbtn1" type="button" onserverclick="btnExportDisOrder_ServerClick" runat="server" id="btnExportDisOrder">
                                                        Download Report&nbsp;&nbsp;<i class="fa fa-download"></i>
                                                    </button>
                                                    <asp:HiddenField ID="HiddenField4" runat="server" />
                                                </div>

                                                <div class="panel-body adv-table" style="overflow-x: scroll;">
                                                    <asp:GridView runat="server" ID="grdDispatched" DataKeyNames="EM_bIntId"
                                                        AutoGenerateColumns="false" AllowPaging="true"
                                                        OnRowDataBound="grdDispatched_RowDataBound" OnPageIndexChanging="grdDispatched_PageIndexChanging"
                                                        HeaderStyle-Wrap="true" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"
                                                        RowStyle-Wrap="false" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle"
                                                        class="dyna mic-table-grid display table table-bordered table-striped">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sr No" HeaderStyle-CssClass="hdrAlgnCntrStyle" FooterStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="hdrStyle" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("EM_bIntId") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ClientID" HeaderStyle-CssClass="hdrStyle" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClientID" runat="server" Text='<%# Eval("EM_bIntUserId") %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="User Name" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-Wrap="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UD_vCharName") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="User Mobile" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserMobile" runat="server" Text='<%# Eval("UD_vCharPhoneNumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delivery Contact Person" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-Wrap="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClientName" runat="server" Text='<%# Eval("EM_vCharClientName") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%-- <asp:TemplateField HeaderText="Mobile" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClientMobile" runat="server" Text='<%# Eval("EM_bIntClientPhNo").ToString().Substring(0,3)+"*****"+Eval("EM_bIntClientPhNo").ToString().Substring(8) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Delivery Contact Number" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClientPhn" runat="server" Text='<%# Eval("EM_bIntClientPhNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delivery Contact Email" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-CssClass="itmEmailStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClientEmail" runat="server" Text='<%# Eval("EM_vCharClientEmailId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delivery Address" HeaderStyle-CssClass="hdrAlgnCntrStyle" ItemStyle-CssClass="itmAddressStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("EM_vCharClientAddress1") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Product Image" HeaderStyle-CssClass="hdrAlgnCntrStyle" HeaderStyle-Width="75px" ItemStyle-Width="75px">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblProdImgs_Disp" Text='<%# Eval("PI_vCharImgPath") %>' Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Product Name" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Literal runat="server" ID="lblProdName"
                                                                        Text='<%# string.Join("<br /><br />", Eval("PM_vCharProdName").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Product Quantity" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <%--  <ItemTemplate>
                                                        <asp:Label ID="lblOrdQty" runat="server" Text='<%# Eval("OrdSub_decQty") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>--%>
                                                                <ItemTemplate>
                                                                    <asp:Literal runat="server" ID="lblOrdQty"
                                                                        Text='<%# string.Join("<br /><br />", Eval("OrdSub_decQty").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Product Weight (in gm)" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Literal runat="server" ID="lblProdWeight"
                                                                        Text='<%# string.Join("<br /><br />", Eval("PM_decWeight").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order Time" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderTime" runat="server" Text='<%# Eval("EM_dtEnquiryTime","{0:yyyy-MM-dd HH:mm:ss}") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order ID" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderNum" runat="server" Text='<%# Eval("EM_vCharOrdNum") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <%--<asp:TemplateField HeaderText="Availability">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAvailability" runat="server" Text='<%# Eval("PM_intQuantityAvailable") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Order Status" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderStatus" runat="server" Text='<%# Eval("EM_vOrderStatus") %>' CssClass='<%# Eval("EM_vOrderStatus").Equals("failure")?"failed-style":"success-style" %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                                <%-- <EditItemTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlgridStatus" Style="height: 34px;">
                                                                <asp:ListItem Text="--Please Select --" />
                                                                <asp:ListItem Text="failure" />
                                                                <asp:ListItem Text="success" />
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>--%>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="User Total Recharge" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRechargelAmt" runat="server" Text='<%# Eval("Recharge","{0:n2}")%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="User Total Withdraw" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWithdrawllAmt" runat="server" Text='<%# Eval("Withdrawal","{0:n2}")%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gbeans Redeemed" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGbeanlAmt" runat="server" Text='<%# Eval("EM_intGbeanUsed","{0:n2}")%>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Wallet Amount Used" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWalletAmt" runat="server" Text='<%# Eval("EM_decFinalAmt","{0:n2}") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order Total Amount" HeaderStyle-CssClass="hdrAlgnCntrStyle">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFinalAmt" runat="server" Text='<%# Eval("FinalAmount","{0:n2}") %>'>
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                    <div style="padding: 8px;" runat="server" id="rowTotalDispatch"></div>
                                                </div>
                                            </div>
                                        </section>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnExportDisOrder" />
                                        <asp:PostBackTrigger ControlID="btnDispatchSearch" />
                                        <asp:PostBackTrigger ControlID="btnDispatchClear" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>


                    <!-- confirmation for Accept-->
                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="confirmAcceptOrder" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                    <h4 class="modal-title">Proceed Confirmation - Order Accept</h4>
                                </div>
                                <div class="modal-body">

                                    <div role="form">
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-4 control-label lbl-pad-top">Select mobile number<span class="mandate"> *</span></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList runat="server" ID="ddlPhn" CssClass="form-control" Style="height: 35px; width: inherit">
                                                        <asp:ListItem Value="0" Text="--Please Select --" />
                                                        <asp:ListItem Value="1" Text="8104644979" />
                                                        <asp:ListItem Value="2" Text="7506569695" />
                                                        <asp:ListItem Value="3" Text="9870680253" Enabled="false" />
                                                        <asp:ListItem Value="4" Text="8097481837" />
                                                        <asp:ListItem Value="5" Text="9892156202" />
                                                        <asp:ListItem Value="6" Text="9819353421" />
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="hidRowId" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="row" id="sendOtpRow" style="text-align: center; display: none;">
                                                <button class="btn gbtns" type="button" onclick="return sendOtpForAccept();"
                                                    runat="server" id="btnSendOtp">
                                                    Send OTP&nbsp;&nbsp;<i class="fa fa-send-o"></i>
                                                </button>
                                                <%--<asp:Button CssClass="btn btn-post fa" ID="aspbtnSendOtp" runat="server" OnClientClick="return sendClicked();" Text="SEND OTP &#xf1d8;" />--%>
                                                &nbsp;&nbsp;
                                                <label id="cntDwnLbl" class="retry-countdown"></label>
                                            </div>
                                            <div class="row">
                                                <label id="otpSentLbl" class="col-sm-12 control-label lbl-pad-top" style="text-align: center; display: none">OTP sent on selected mobile number.</label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row" id="otpDivRow" style="display: none">
                                                <label class="col-sm-4 control-label lbl-pad-top">Enter OTP<span class="mandate"> *</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtOtp" name="txtOtp" runat="server" CssClass="form-control" MaxLength="5" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;" Style="width: inherit"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row" style="text-align: center">
                                                <label id="lblError" class="col-sm-12 control-label lbl-pad-top" style="color: red"></label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="panel-body" style="text-align: center">
                                                    <%--<button class="btn btn-success" type="button" onclick="return validate();"
                                                        runat="server" id="btnCnfrmYes">
                                                        Yes <i class="fa fa-check-circle"></i>
                                                    </button>--%>
                                                    <asp:Button CssClass="btn gbtn fa" ID="aspbtnCnfrmYes" runat="server"
                                                        OnClientClick="return validateAccept();" OnClick="btnAcptSubmit_Click"
                                                        Text="Yes &#xf058;" />
                                                    &nbsp;&nbsp;&nbsp;<!-- for space between btns -->
                                                    <button class="btn gbtn1" type="button" data-dismiss="modal"
                                                        id="Button5">
                                                        Cancel <i class="fa fa-times-circle"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Action Buttons -->

                                    </div>
                                    <!-- //form -->
                                </div>
                                <!-- //modal body -->
                            </div>
                            <!-- //modal content -->
                        </div>
                        <!-- //modal dialog -->
                    </div>
                    <!-- // END confirmation for Accept -->

                    <!-- confirmation for Reject-->
                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="confirmRejectOrder" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                    <h4 class="modal-title">Proceed Confirmation - Order Reject</h4>
                                </div>
                                <div class="modal-body">

                                    <div role="form">
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-4 control-label lbl-pad-top">Select mobile number<span class="mandate"> *</span></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList runat="server" ID="ddlPhnRej" CssClass="form-control" Style="height: 35px; width: inherit">
                                                        <asp:ListItem Value="0" Text="--Please Select --" />
                                                        <asp:ListItem Value="1" Text="8104644979" />
                                                        <asp:ListItem Value="2" Text="7506569695" />
                                                        <asp:ListItem Value="3" Text="9870680253" Enabled="false" />
                                                        <asp:ListItem Value="4" Text="9892156202" Enabled="true" />
                                                        <asp:ListItem Value="6" Text="9819353421" Enabled="false" />
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row" id="sendRejOtpRow" style="text-align: center; display: none;">
                                                <button class="btn gbtns" type="button" onclick="return sendOtpForReject();"
                                                    runat="server" id="btnRejSendOtp">
                                                    Send OTP&nbsp;&nbsp;<i class="fa fa-send-o"></i>
                                                </button>
                                                <%--<asp:Button CssClass="btn btn-post fa" ID="aspbtnSendOtp" runat="server" OnClientClick="return sendClicked();" Text="SEND OTP &#xf1d8;" />--%>
                                                &nbsp;&nbsp;
                                                <label id="cntDwnLblRjct" class="retry-countdown"></label>
                                            </div>
                                            <div class="row">
                                                <label id="otpRejSentLbl" class="col-sm-12 control-label lbl-pad-top" style="text-align: center; display: none">OTP sent on selected mobile number.</label>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row" id="otpRejDivRow" style="display: none">
                                                <label class="col-sm-4 control-label lbl-pad-top">Enter OTP<span class="mandate"> *</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtRejOtp" name="txtRejOtp" runat="server" CssClass="form-control" MaxLength="5" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;" Style="width: inherit"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row" style="text-align: center">
                                                <label id="lblRejError" class="col-sm-12 control-label lbl-pad-top" style="color: red"></label>
                                            </div>
                                        </div>
                                        <%--<div class="form-group" style="text-align: center;">
                                            <h4>Do you really want to Reject?</h4>
                                        </div>--%>
                                        <div class="form-group">
                                            <div class="row">
                                                <label class="col-sm-4 control-label">Rejection Reason<span class="mandate"> *</span></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="txtRejectReason" name="txtReason" runat="server" CssClass="form-control" TextMode="MultiLine"
                                                        Rows="4" ToolTip="Order rejection reason" Style="width: inherit"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row" id="otpRejSubRow" style="display: none">
                                                <div class="panel-body" style="text-align: center">
                                                    <%--<div class="form-group" style="text-align: center;">
                                                        <h4 class="col-sm-4 control-label lbl-pad-top">Reason for reject</h4>
                                                        <asp:TextBox class="col-sm-8" runat="server" ID="txtRejectReason" TextMode="MultiLine"
                                                            Rows="3" Columns="5" ToolTip="Order rejection reason" />
                                                    </div>
                                                    --%>
                                                    <button class="btn gbtn" type="button" onserverclick="btnRejectYes_ServerClick"
                                                        runat="server" id="btnRejectYes">
                                                        Yes <i class="fa fa-check-circle"></i>
                                                    </button>
                                                    &nbsp;&nbsp;&nbsp;<!-- for space between btns -->
                                                    <button class="btn gbtn1" type="button" data-dismiss="modal"
                                                        id="Button2">
                                                        Cancel <i class="fa fa-times-circle"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Action Buttons -->

                                    </div>
                                    <!-- //form -->
                                </div>
                                <!-- //modal body -->
                            </div>
                            <!-- //modal content -->
                        </div>
                        <!-- //modal dialog -->
                    </div>
                    <!-- // END confirmation for Reject -->


                    <!-- modal for Enlarge Image -->
                    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="enlargeImgMod" class="modal fade">
                        <div class="modal-dialog" style="width: fit-content;">
                            <div class="modal-content">
                                <div class="modal-header" style="min-width: 300px;">
                                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">&times</button>
                                    <h4 class="modal-title">Enlarged Image</h4>
                                </div>
                                <div class="modal-body" style="padding: 0px">
                                    <!-- //form -->
                                    <div role="form">
                                        <div class="row" style="max-width: 450px;">
                                            <asp:Image ID="imgEnlarged" runat="server" Style="width: -webkit-fill-available;"
                                                onerror="this.onerror=null;this.src='images/noimage.jpg';" />
                                        </div>
                                    </div>
                                    <!-- //form -->
                                </div>
                                <!-- //modal body -->
                            </div>
                            <!-- //modal content -->
                        </div>
                        <!-- //modal dialog -->
                    </div>
                    <!-- // END Enlarge Image -->

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Cnt_PageLevelScripts" runat="server">
    <script src="js/pagesjs/OrderApproval.js"></script>
</asp:Content>
