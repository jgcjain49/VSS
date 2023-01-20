<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestingIPandMAC.aspx.cs" MasterPageFile="~/AdminEx.Master" Inherits="Admin_CommTrex.TestingIPandMAC" %>

<asp:Content ID="contChild_Header" ContentPlaceHolderID="contHeadContent" runat="server">
    <link href="AdminExContent/js/advanced-datatable/css/demo_page.css" rel="stylesheet" />
    <link href="AdminExContent/js/advanced-datatable/css/demo_table.css" rel="stylesheet" />
    <link href="AdminExContent/js/data-tables/DT_bootstrap.css" rel="stylesheet" />
    <!--file upload-->
    <link href="AdminExContent/css/bootstrap-fileupload.min.css" rel="stylesheet" />
    <!--tags input-->
    <link href="AdminExContent/js/jquery-tags-input/jquery.tagsinput.css" rel="stylesheet" />
</asp:Content>

    <asp:Content ID="contChild_ExHeader" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
         <div class="page-heading">
                <h3>Test IP address and MAC address.</h3>
                <ul class="breadcrumb">
                        <li>
                            <a href="#"> Master </a>
                        </li>
                        <li class="active"> Test Master </li>
                </ul>
        </div>

    </asp:Content>

    <asp:Content ID="contChild_ExBody" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
                   <div class="panel-body">
                    <div class="tab-content">
                        <div class="tab-pane active" id="addStaticad">
            <div class="row">
                                <section class="panel" id="pnlStaticAdbasic">
                                    <header class="panel-heading">
                                        Advertisements details
							                <span class="tools pull-right">
                                                <a href="javascript:;" class="fa fa-chevron-up"></a>
                                                <span class="collapsible-server-hidden">
                                                    <asp:HiddenField runat="server" ID="HiddenField1" EnableViewState="true" Value="c" />
                                                </span>
                                            </span>
                                    </header>
                                    <div class="panel-body collapse">
                                        <div class="form-horizontal adminex-form">

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Advertise ID </label>
                                                <div class="col-sm-10">
                                            <button class="pnl-opener btn btn-success" type="button"
                                                            onserverclick="btnTestAddress_ServerClick"
                                                            runat="server" id="btnTestAddress">
                                                            Test <i class="fa fa-plus-square"></i>
                                            </button>
                                                </div>
                                            </div>

                                              <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Advertise ID </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtmac" name="txtmac" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="false"
                                                        data-toggle="tooltip" title="" placeholder="MAC Address" data-original-title="Address"></asp:TextBox>
                                                </div>
                                            </div>

                                            
                                              <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Advertise ID </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtmacaddr" name="txtmacaddr" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="false"
                                                        data-toggle="tooltip" title="" placeholder="MAC Address" data-original-title="Address"></asp:TextBox>
                                                </div>
                                            </div>

                                                                                          <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Advertise ID </label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtlocalmac" name="txtlocalmac" runat="server" CssClass="form-control tooltips" data-trigger="hover" Enabled="false"
                                                        data-toggle="tooltip" title="" placeholder="MAC Address" data-original-title="Address"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </section>
                </div>
                        </div>
                   </div>
           </div>
    </asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Cnt_PageLevelScripts" runat="server">

    <!--file upload-->
    <script type="text/javascript" src="AdminExContent/js/bootstrap-fileupload.min.js"></script>
    <!--tags input-->
    <script src="AdminExContent/js/jquery-tags-input/jquery.tagsinput.js"></script>
    <script src="AdminExContent/js/tagsinput-init.js"></script>
    <!--bootstrap input mask-->
    <script type="text/javascript" src="AdminExContent/js/bootstrap-inputmask/bootstrap-inputmask.min.js"></script>


    <!--dynamic table-->
    <script type="text/javascript" src="AdminExContent/js/advanced-datatable/js/jquery.dataTables.js"></script>
    <script type="text/javascript" src="AdminExContent/js/data-tables/DT_bootstrap.js"></script>

</asp:Content>