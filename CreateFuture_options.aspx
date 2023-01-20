<%@ Page Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="CreateFuture_options.aspx.cs" Inherits="Admin_CommTrex.CreateFuture_options" %>

<asp:Content ID="contChild_Header" ContentPlaceHolderID="contHeadContent" runat="server">
    <link href="AdminExContent/js/advanced-datatable/css/demo_page.css" rel="stylesheet" />
    <link href="AdminExContent/js/advanced-datatable/css/demo_table.css" rel="stylesheet" />
    <link href="AdminExContent/js/data-tables/DT_bootstrap.css" rel="stylesheet" />
    <!--file upload-->
    <link href="AdminExContent/css/bootstrap-fileupload.min.css" rel="stylesheet" />
    <!--tags input-->
    <link href="AdminExContent/js/jquery-tags-input/jquery.tagsinput.css" rel="stylesheet" />

    <link href="js/iCheck/skins/flat/grey.css" rel="stylesheet" />
    <link href="js/iCheck/skins/flat/red.css" rel="stylesheet" />
    <link href="js/iCheck/skins/flat/green.css" rel="stylesheet" />
    <link href="js/iCheck/skins/flat/blue.css" rel="stylesheet" />
    <link href="js/iCheck/skins/flat/yellow.css" rel="stylesheet" />
    <link href="js/iCheck/skins/flat/purple.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="contChild_ExHeader" ContentPlaceHolderID="CntAdminEx_Header" runat="server">
    <div class="page-heading">
        <h3>Add/Modify/Delete Future/Options details</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Master </a>
            </li>
            <li class="active">Future/Options Master </li>
        </ul>
    </div>

</asp:Content>

<asp:Content ID="contChild_ExBody" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <asp:HiddenField ID="activeTab" EnableViewState="true" runat="server" Value="addproducts" />
    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading custom-tab ">
                <ul class="nav nav-tabs">
                    <li class="active" id="tab_addproducts">
                        <a href="#addNewUser" data-toggle="tab">Add New Future/Options</a>
                    </li>
                    <li class="" id="tab_modifyproducts">
                        <a href="#modifyUser" data-toggle="tab">View/Modify Future/Options</a>
                    </li>
                </ul>
            </header>

            <div class="panel-body">
                <div class="tab-content">
                    <!-- //Add new user tab -->
                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <div class="tab-pane active" id="addNewUser">
                        <div class="row">
                            <section class="panel" id="pnlSecurity">
                                <header class="panel-heading">
                                    Future/Options Details
							                <span class="tools pull-right">
                                                <a href="javascript:;" class="fa fa-chevron-down"></a>
                                                <span class="collapsible-server-hidden">
                                                    <asp:HiddenField runat="server" ID="HiddenField3" EnableViewState="true" Value="c" />
                                                </span>
                                            </span>
                                </header>
                                <div class="panel-body">
                                    <div class="form-horizontal adminex-form">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                             <ContentTemplate>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Symbol : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtSymbol" name="txtSymbol" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Symbol" data-original-title="Symbol"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">
                                                Lot Size
                                              <span style="color: red">*</span>
                                            </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtQuantity" name="txtQuantity" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="LotSize" data-original-title="Quantity"></asp:TextBox>
                                                  <asp:RegularExpressionValidator ID="revContNo" runat="server"
                                                            ControlToValidate="txtQuantity" ErrorMessage="Please Enter Correct Lot Size!!!"
                                                            ValidationExpression="^(0|[1-9]\d*)$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                                 <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Expiry Date : </label>
                                            <div class="col-sm-10">
                                                <input type="date" id="expdate" class="custom-input" runat="server" />
                                            </div>
                                        </div>

                                       <%-- <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Type : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddl_Type" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select Type --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Future"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="CE"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="PE"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>--%>
                                                    </ContentTemplate>
                                              </asp:UpdatePanel>
                                    </div>
                                </div>

                            </section>
                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                <section class="panel">
                                    <div class="panel-body" style="text-align: center">
                                        <button class="pnl-opener btn gbtn" type="button"
                                            btn-action="New"
                                            data-open-on="Save"
                                            runat="server" id="btnSave" onserverclick="btnSave_ServerClick">
                                            New 
                                        </button>
                                        <button class="btn btn-info" type="button" onserverclick="btnClear_ServerClick"
                                            runat="server" id="btnClear">
                                            Clear
                                        </button>
                                    </div>
                                    <div class="panel-body">
                                        <div class="alert alert-info" style="padding: 8px;" runat="server" id="actionInfo">
                                            Press New to Add Future/Options.
                                        </div>
                                    </div>

                                </section>
                                <!-- //panel -->
                            </div>
                            <!-- //Grid 12 -->
                        </div>
                        <!--//row buttons -->
                        <!-- end of Add New User Code Here -->
                    </div>

                    <div class="tab-pane" id="modifyUser">
                        <div class="form-group">
                            <div class="alert alert-info" runat="server" id="updateActionDiv">
                                Click on respective buttons for Modify / Delete.
                            </div>
                            <div class="adv-table nice-scroll-grid">
                                <asp:GridView ID="grdFuture" runat="server"
                                    EnableModelValidation="True" AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    
                                   OnRowEditing="GridView_RowEditing"
                                     OnRowUpdating="GridView1_RowUpdating"
                                     OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                    RowStyle-CssClass="gradeA"
                                    class="dynamic-table-grid display table table-bordered table-striped">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="futureId" runat="server" Text='<%# Eval("iFutureId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Symbol">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("sSymbol") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUserName" runat="server" Text='<%# Bind("sSymbol") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="LotSize">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("iLotSize") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="TxtSize" runat="server" Text='<%# Bind("iLotSize") %>'>
                                                </asp:TextBox>
                                                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                ControlToValidate="TxtSize" runat="server"
                                                ErrorMessage="Only Numbers allowed"
                                                       ValidationExpression="\d+">
                                               <%-- ValidationExpression="^(0 |[1-9]\d*)$">--%>
                                                </asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Expiry Date">
                                            <ItemTemplate>
                                                <asp:Label ID="Expirydate" runat="server" Text='<%# Eval("ExpiryDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                                            </ItemTemplate>

                                          <%--  <EditItemTemplate>
                                                <asp:TextBox ID="edtdispatchdate" TextMode="Date" runat="server" Text='<%# Bind("dDispatchDate","{0:yyyy-MM-dd}") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <%--<asp:CommandField ShowEditButton="True" HeaderText="Actions" HeaderStyle-CssClass="nosort" />--%>
                                         <asp:TemplateField  HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="editLink" class="edit"  runat="server" data-toggle="tab"  
                                                    data-user-id='<%# Eval("iFutureId") %>'
                                                    href="javascript:;#addNewUser"  >Edit <i class="fa fa-edit"></i>&nbsp;</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="A1" class="delete" runat="server"
                                                    data-user-id='<%# Eval("iFutureId") %>'
                                                    data-user-name='<%# Eval("sSymbol") %>' href="javascript:;">Delete &nbsp;</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         

                                    </Columns>
                                </asp:GridView>
                            </div>


                        </div>
                    </div>
                     <div runat="server" id="editHidDiv" visible="true">
                        <asp:HiddenField ID="HidEdit" runat="server" Value="" />
                        <asp:HiddenField ID="HidIsEditMode" runat="server" Value="" />
                        <asp:HiddenField ID="HidBnkId" runat="server" Value="" />
                </div>
            </div>
        </section>
        <!-- Panel Body Main -->
    </div>
    <!-- Main Row -->

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modDeleteFuture_opt" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                    <h4 class="modal-title">Delete the Future/Options.</h4>
                </div>
                <div class="modal-body">

                    <div role="form">
                        <div class="form-group">
                            <h3>Are you sure you want to delete Future/Options?</h3>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2 col-sm-2 control-label">Future/Options ID </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDelFut_OptID" name="txtDelFut_OptID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="txtDelHidden" runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- Id -->
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2 col-sm-2 control-label">Future/Options Name </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDelFut_OptName" name="txtDelFut_OptName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!-- Name -->

                        <div class="form-group">
                            <div class="row">
                                <div class="panel-body" style="text-align: center">
                                    <button class="btn btn-success" type="button"
                                        runat="server" id="btnDeleteFuture_opt" onserverclick="btnDeleteFuture_opt_ServerClick">
                                        <i class="fa fa-trash"></i>Delete Future/Options
                                    </button>
                                    <button class="btn btn-danger" type="button"
                                        id="btnCancelDeleteFuture_opt">
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
    <!-- //dialog -->

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

    <!--dynamic table-->
    <script type="text/javascript" src="js/pagesjs/CreateFuture.js"></script>
</asp:Content>
