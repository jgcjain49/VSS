<%@ Page Title="New products notifications" Language="C#" MasterPageFile="~/AdminEx.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="NewProductNofication.aspx.cs" Inherits="Admin_CommTrex.NewProductNofication" %>
<asp:Content ID="contChild_Header" ContentPlaceHolderID="contHeadContent" runat="server">
    
    <!-- For grid stylings as dynamic table-->
    <link href="AdminExContent/js/advanced-datatable/css/demo_page.css" rel="stylesheet" />
    <link href="AdminExContent/js/advanced-datatable/css/demo_table.css" rel="stylesheet" />
    <link href="AdminExContent/js/data-tables/DT_bootstrap.css" rel="stylesheet" />

    <!-- For checkbox stylings -->
    <link href="AdminExContent/js/iCheck/skins/flat/flat.css" rel="stylesheet" />
    <link href="AdminExContent/js/iCheck/skins/flat/blue.css" rel="stylesheet" />

    <link href="AdminExContent/js/iCheck/skins/minimal/minimal.css" rel="stylesheet" />
    <link href="AdminExContent/js/iCheck/skins/minimal/blue.css" rel="stylesheet" />
    

</asp:Content>
<asp:Content ID="contChild_ExHeader" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
    <div class="page-heading">
        <h3>New product notifications</h3>
        <ul class="breadcrumb">
                <li>
                    <a href="#"> Announcement </a>
                </li>
                <li class="active"> Product Notifications </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="contChild_ExBody" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <%--<form runat="server">--%>
        <!-- Users panel -->
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <header class="panel-heading">
                            Users to be notified
                            <span class="tools pull-right">
                                <a href="javascript:;" class="fa fa-chevron-down"></a>
                                <!-- To enable close button uncomment following -->
                                <%--<a href="javascript:;" class="fa fa-times"></a>--%>
                            </span>
                        </header>
                        <div class="panel-body">
                            <div class="icheck">
                                <div class="minimal-blue">
                                    <div class="checkbox ">
                                        <input type="checkbox" id="chkAllUsers"/>
                                        <label>Select All Users </label>
                                    </div>
                                </div>
                            </div>
                            <div class="adv-table">
                                <asp:GridView  ID="grdUsers" runat="server" AutoGenerateColumns="false"
                                    RowStyle-CssClass="gradeA"
                                    data-sorting="2,1" data-add-printing-function="false"
                                    class="dynamic-table-grid display table table-bordered table-striped">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="center">
                                            <ItemTemplate>
                                                <div class="flat-blue">
                                                    <div class="radio">
                                                        <asp:CheckBox id="cbSelectUser" runat="server" Checked='<%# Bind("isUserSelected") %>'/>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Name" FooterText="Name" DataField="Usr_Name" />
                                        <asp:BoundField HeaderText="Role" FooterText="Role" DataField = "vwUsr_RoleTxt" />
                                        <asp:BoundField HeaderText="Address" FooterText="Address" DataField = "UCon_nVcharAddressPrimary" />
                                        <asp:BoundField DataField="Usr_Id" />
                                        <asp:BoundField DataField="Usr_DeviceRegId" />
                                    </Columns>
                                    </asp:GridView>
                            </div> <!--//adv table -->
                        </div> <!--//panel-body -->
                    </section> <!--//panel -->
                </div> <!--//col-sm-12 -->
            </div> <!--//row -->

        <!-- Products panel -->
        <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <header class="panel-heading">
                            Products for notification
                            <span class="tools pull-right">
                                <a href="javascript:;" class="fa fa-chevron-down"></a>
                                <!-- To enable close button uncomment following -->
                                <%--<a href="javascript:;" class="fa fa-times"></a>--%>
                            </span>
                        </header>
                        <div class="panel-body">
                            <div class="adv-table">
                                <asp:GridView  ID="grdProducts" runat="server" AutoGenerateColumns="false"
                                    RowStyle-CssClass="gradeA"
                                    data-sorting="2,1" data-add-printing-function="true"
                                    class="dynamic-table-grid display table table-bordered table-striped">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-CssClass="center">
                                            <ItemTemplate>
                                                <div class="flat-blue">
                                                    <div class="radio">
                                                        <asp:CheckBox id="cbSelectProduct" runat="server"/>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Name" FooterText="Name" DataField="PM_vCharProdName" />
                                        <asp:BoundField HeaderText="Type" FooterText="Type" DataField = "PM_vCharProdType" />
                                        <asp:BoundField HeaderText="Price" FooterText="Price" DataField = "PM_decProdPrice" />
                                        <asp:BoundField HeaderText="Description" FooterText="Description" DataField="PM_vCharProdDesc" />
                                        <asp:BoundField HeaderText="Status" FooterText="Status" DataField="vwvCharProdStatus" />
                                        <asp:BoundField DataField="PM_bIntProdId" />
                                    </Columns>
                                    </asp:GridView>
                            </div> <!--//adv table -->
                        </div> <!--//panel-body -->
                    </section> <!--//panel -->
                </div> <!--//col-sm-12 -->
            </div> <!--//row -->
        <div class="row">
            <div class="col-lg-6">
                <section class="panel">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-6" style="width:20%;">
                                <button class="btn btn-success" type="button" data-toggle="modal" data-target="#modSendingMessage"
                                        runat="server" id="btnSendNotification" onserverclick="btnSendNotification_ServerClick">
                                     Send Now <i class="fa fa-arrow-circle-right"></i>
                                </button>
                            </div>
                            <div class="col-xs-6" style="width:80%;">
                                <div class="alert alert-info" style="padding:8px;" runat="server" id="progInfo">
                                    No action taken yet.
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </div><!--//row -->
        <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modSendingMessage" class="modal fade" style="width:100%;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Notifying selected user(s)</h4>
                    </div>
                    <div class="modal-body">
                        Please wait for few minutes...
                                        <br />
                        <div style="text-align: center">
                            <img src="images/loading-blue.gif" alt="Please wait" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <%--</form>--%>
</asp:Content>
<asp:Content ID="contChild_PageLevelScripts" ContentPlaceHolderID="Cnt_PageLevelScripts" runat="server">
    
    <!--icheck -->
    <script src="AdminExContent/js/iCheck/jquery.icheck.js"></script>
    <script src="AdminExContent/js/icheck-init.js"></script>

    <!--dynamic table-->
    <script type="text/javascript" src="AdminExContent/js/advanced-datatable/js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="AdminExContent/js/data-tables/DT_bootstrap.js"></script>

    <!--dynamic table initialization -->
    <script src="js/pagesjs/NewProductNotification.aspx.js"></script>

</asp:Content>
