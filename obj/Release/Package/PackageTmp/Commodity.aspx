<%@ Page Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="Commodity.aspx.cs" Inherits="Admin_CommTrex.Commodity" %>

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
        <h3>Add/Modify/Delete Commodity details</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Master </a>
            </li>
            <li class="active">Commodity Master </li>
        </ul>
    </div>

</asp:Content>

<asp:Content ID="contChild_ExBody" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <asp:HiddenField ID="activeTab" EnableViewState="true" runat="server" Value="addproducts" />
    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading custom-tab ">
                <ul class="nav nav-tabs">
                    <li class="active" id="tab_addpayment">
                        <a href="#addproducts" data-toggle="tab">Add New Commodity</a>
                    </li>
                    
                    <li class="" id="tab_modifyPayment">
                        <a href="#modifyproducts" data-toggle="tab">View/Modify Commodity</a>
                    </li>
                </ul>
            </header>

            <div class="panel-body">
                <div class="tab-content">
                    <!-- //Add new user tab -->
                     <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <div class="tab-pane active" id="addproducts">
                        <div class="row">
                            <section class="panel" id="pnlSecurity">
                                <header class="panel-heading">
                                    Commodity Details
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
                                            <label class="col-sm-2 col-sm-2 control-label">Commodity Name : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtCommName" name="txtCommName" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Commodity Name" data-original-title="Commodity Name"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="lettersvalidation" runat="server"
                                                    ErrorMessage="Only Letters Allowed" ControlToValidate="txtCommName"
                                                    ValidationExpression="[a-zA-Z ]*$">
                                               </asp:RegularExpressionValidator> 
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">HSN : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtGST" name="txtGST" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="GST" data-original-title="GST"></asp:TextBox>

                                                  <asp:RegularExpressionValidator ID="rgxAadhaar" runat="server" ControlToValidate="txtGST"
                                                            ValidationExpression="[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$"
                                                            ErrorMessage="Invalid GST Number." ForeColor="Red"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>


                                       <%--    <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Upload Excel : </label>
                                            <div class="col-sm-10">
                                                  <asp:FileUpload ID="fileExcel" runat="server" class="default" />    
                                            <button class="btn gbtn" runat="server" onserverclick="btnUploadExcel_Click" type="button"
                                            id="btnUploadExcel" data-toggle="modal" data-target="#modReqProcessing" title="Upload Excel">
                                            Upload&nbsp;&nbsp;<i class="fa fa-upload"></i>
                                        </button>                          
                                            </div>
                                        </div>--%>







                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Packing : </label>
                                            <br />
                                            <%--<p></p>--%>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtPacking" name="txtPacking" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Packing (KGs)" data-original-title="Packing (in MTs i.e. 10MT, 50MT, 100MT etc.)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label"> Lot Size : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtDefaultQty" TextMode="Number" name="txtDefaultQty" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Default Qty" data-original-title="Default Qty"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValueToCompare="0" ControlToValidate="txtDefaultQty" 
	                                                ErrorMessage="Value must be Greater then 0 !!!" Operator="GreaterThan" Type="Integer"></asp:CompareValidator> 
                                            </div>
                                        </div>
                                          <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">
                                                Commodity Type :
                                              <span style="color: red">*</span>
                                            </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddlCommodityType" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select One --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Food Grains"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Cerials"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Spices"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="OilSeed"></asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                      <%--  <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label"> Quantity Type : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtQtyType" name="txtQtyType" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder=" Quantity Type" data-original-title=" Quantity Type"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="qtyinletters" runat="server"
                                                    ErrorMessage="Only Letters Allowed" ControlToValidate="txtQtyType"
                                                    ValidationExpression="[a-zA-Z ]*$">
                                               </asp:RegularExpressionValidator> 
                                            </div>
                                        </div>--%>
                                       <%-- <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label"> Type of commodity : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtTypofcomm" name="txtTypofcomm" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Type of commodity" data-original-title="Type of commodity"></asp:TextBox>
                                            </div>
                                        </div>--%>
                                        <%--<div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Default Futures Qty : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtDefFuturesQty" name="txtDefFuturesQty" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Default Futures Qty" data-original-title="Default Futures Qty"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Default Options Qty : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtDefOptionsQty" name="txtDefOptionsQty" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Default Options Qty" data-original-title="Default Options Qty"></asp:TextBox>
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
                                        <button class="btn btn-info" type="button"
                                            runat="server" id="btnClear" onserverclick="btnClear_ServerClick">
                                            Clear
                                        </button>
                                    </div>
                                    <div class="panel-body">
                                        <div class="alert alert-info" style="padding: 8px;" runat="server" id="actionInfo">
                                            Press New to Add Commodity.
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

                  

                    <div class="tab-pane" id="modifyproducts">
                        <div class="form-group">
                            <div class="alert alert-info" runat="server" id="updateActionDiv">
                                Click on respective buttons for Modify / Delete.
                            </div>
                            <div class="adv-table nice-scroll-grid">
                                <asp:GridView ID="grdcmdty" runat="server"
                                    EnableModelValidation="True" AutoGenerateColumns="False"
                                    DataKeyNames=""
                                     OnRowEditing="GridView1_RowEditing" 
                                    OnRowUpdating="GridView1_RowUpdating1" 
                                    OnRowCancelingEdit="GridView1_RowCancelingEdit" 
                                    RowStyle-CssClass="gradeA"
                                    class="dynamic-table-grid display table table-bordered table-striped">
                                      <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="CID" runat="server" Text='<%# Eval("iComID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Commodity Name">
                                            <ItemTemplate>
                                                <asp:Label ID="cmdtyname" runat="server" Text='<%# Eval("sCommodityName") %>'></asp:Label>
                                            </ItemTemplate>

                                           <%-- <EditItemTemplate>
                                                <asp:TextBox ID="edtcmdtyname" runat="server" Text='<%# Bind("sCommodityName") %>'>
                                                </asp:TextBox>
                                                <asp:RegularExpressionValidator ID="lettersvalidation" runat="server"
                                                    ErrorMessage="Only Letters Allowed" ControlToValidate="edtcmdtyname"
                                                    ValidationExpression="[a-zA-Z ]*$">
                                               </asp:RegularExpressionValidator> 
                                            </EditItemTemplate>--%>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="GST/HSN">
                                            <ItemTemplate>
                                                <asp:Label ID="gst" runat="server" Text='<%# Eval("sGst") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="edtgst" runat="server" Text='<%# Bind("sGst") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Packing">
                                            <ItemTemplate>
                                                <asp:Label ID="packing" runat="server" Text='<%# Eval("sPacking") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="edtpacking" runat="server" Text='<%# Bind("sPacking") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lot Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lotsize" runat="server" Text='<%# Eval("iLotSize") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="edtlotsize" TextMode="Number" runat="server" Text='<%# Bind("iLotSize") %>'>
                                                </asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValueToCompare="0" ControlToValidate="edtlotsize" 
	                                                ErrorMessage="Value must be Greater then 0 !!!" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

<%--                                           <asp:TemplateField HeaderText="Quantity Type">
                                            <ItemTemplate>
                                                <asp:Label ID="quantitytype" runat="server" Text='<%# Eval("sQtyType") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="edtquantitytype" runat="server" Text='<%# Bind("sQtyType") %>'>
                                                </asp:TextBox>
                                                 <asp:RegularExpressionValidator ID="edtqtyinletters" runat="server"
                                                    ErrorMessage="Only Letters Allowed" ControlToValidate="edtquantitytype"
                                                    ValidationExpression="[a-zA-Z ]*$">
                                               </asp:RegularExpressionValidator> 
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

                                           <asp:TemplateField HeaderText="Type of Commodity">
                                            <ItemTemplate>
                                                <asp:Label ID="commoditytype" runat="server" Text='<%# Bind("sTypeOfCommodity") %>'></asp:Label>
                                            </ItemTemplate>

                                          <%--  <EditItemTemplate>
                                                <asp:TextBox ID="edtcommoditytype" runat="server" Text='<%# Bind("sTypeOfCommodity") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <%--<asp:CommandField ShowEditButton="True" HeaderText="Actions" HeaderStyle-CssClass="nosort" />--%>
                                            <asp:TemplateField  HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="editLink" class="edit"  runat="server" data-toggle="tab"  
                                                    data-commodity-id='<%# Eval("iComID") %>'
                                                    href="javascript:;#addproducts"  >Edit <i class="fa fa-edit"></i>&nbsp;</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="A1" class="delete" runat="server"
                                                    data-commodity-id='<%# Eval("iComID") %>'
                                                    data-commodity-name='<%# Eval("sCommodityName") %>' href="javascript:;">Delete</a>
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

                        <%-- not used <button class="btn btn-success" type="button" onserverclick="btnUpdate_ServerClick"
                            runat="server" id="update">
                            Update
                        </button>--%>
                    </div>
                </div>
            </div>
        </section>
        <!-- Panel Body Main -->
    </div>
    <!-- Main Row -->

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modDeleteCommodity" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                    <h4 class="modal-title">Delete the Commodity.</h4>
                </div>
                <div class="modal-body">

                    <div role="form">
                        <div class="form-group">
                            <h3>Are you sure you want to delete Commodity?</h3>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2 col-sm-2 control-label">Commodity ID </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDelCommID" name="txtDelCommID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="txtDelHidden" runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- Id -->
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2 col-sm-2 control-label">Commodity Name </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDelCommName" name="txtDelCommName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!-- Name -->

                        <div class="form-group">
                            <div class="row">
                                <div class="panel-body" style="text-align: center">
                                    <button class="btn btn-success" type="button"
                                        runat="server" id="btnDeleteCommodity" onserverclick="btnDeleteCommodity_ServerClick">
                                        <i class="fa fa-trash"></i>Delete Commodity
                                    </button>
                                    <button class="btn btn-danger" type="button"
                                        id="btnCancelDeleteCommodity">
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
    <script type="text/javascript" src="js/pagesjs/CommodityCreate.js"></script>
</asp:Content>
