<%@ Page Title="Notifications Report" Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="NotificationReports.aspx.cs" Inherits="Admin_CommTrex.NotificationReports" %>
<asp:Content ID="contChild_Header" ContentPlaceHolderID="contHeadContent" runat="server">
    <!-- For grid stylings -->
    <link href="AdminExContent/js/advanced-datatable/css/demo_page.css" rel="stylesheet" />
    <link href="AdminExContent/js/advanced-datatable/css/demo_table.css" rel="stylesheet" />
    <link href="AdminExContent/js/data-tables/DT_bootstrap.css" rel="stylesheet" />

    <!-- For checkbox stylings -->
    <link href="AdminExContent/js/iCheck/skins/flat/flat.css" rel="stylesheet" />
    <link href="AdminExContent/js/iCheck/skins/flat/blue.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="contChild_ExHeader" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
    <div class="page-heading">
        <h3>Product notifications report</h3>
        <ul class="breadcrumb">
                <li>
                    <a href="#"> Reports </a>
                </li>
                <li class="active"> Product Notifications </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="contChild_ExBody" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <section class="panel">
                <header class="panel-heading">
                    Notification Report
                            <span class="tools pull-right">
                                <a href="javascript:;" class="fa fa-chevron-down"></a>
                                <span class="collapsible-server-hidden">
                                    <asp:HiddenField runat="server" ID="HiddenField1" EnableViewState="true" Value="o" />
                                </span>
                            </span>
                </header>
                <div class="panel-body">
                    <div class="adv-table">
                        <asp:GridView ID="grdReportData" runat="server" AutoGenerateColumns="false"
                            RowStyle-CssClass="gradeA"
                            class="dynamic-table-grid display table table-bordered table-striped">
                            <Columns>
                                <asp:BoundField HeaderText="Send Date" FooterText="Send Date" DataField="SendDate" />
                                <asp:BoundField HeaderText="Send Time" FooterText="Send Time" DataField="SendTime" />
                                <asp:BoundField HeaderText="Sent To" FooterText="Sent To" DataField="RecieverCnt" />
                                <asp:BoundField HeaderText="Notified Products" FooterText="Notified Products" DataField="ProdCnt" />
                                <asp:BoundField HeaderText="Recieved By" FooterText="Recieved By" DataField="RecievedCnt" />
                                <asp:BoundField HeaderText="Read By" FooterText="Read By" DataField="ReadCnt" />
                                <asp:BoundField DataField="PNL_bIntId" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <!--//adv table -->
                </div>
                <!--//panel-body -->
            </section>
            <!--//panel -->
        </div>
        <!--//col-sm-12 -->
    </div> <!--//row -->
</asp:Content>
<asp:Content ID="contChild_PageLevelScripts" ContentPlaceHolderID="Cnt_PageLevelScripts" runat="server">
    <!--icheck -->
    <script src="AdminExContent/js/iCheck/jquery.icheck.js"></script>
    <script src="AdminExContent/js/icheck-init.js"></script>

    <!--dynamic table-->
    <script type="text/javascript" src="AdminExContent/js/advanced-datatable/js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="AdminExContent/js/data-tables/DT_bootstrap.js"></script>

    <!--page js-->
    <script src="js/pagesjs/NotificationReports.aspx.js"></script>

</asp:Content>
