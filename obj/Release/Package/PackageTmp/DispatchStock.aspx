<%@ Page Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="DispatchStock.aspx.cs" Inherits="Admin_CommTrex.DispatchStock" %>

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
        <h3>Add/Modify/Delete Dispatch Stock</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Dispatch Stock </a>
            </li>
            <li class="active">Dispatch Stock Details </li>
        </ul>
    </div>

</asp:Content>

<asp:Content ID="contChild_ExBody" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <asp:HiddenField ID="activeTab" EnableViewState="true" runat="server" Value="addproducts" />
    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading custom-tab ">
                <ul class="nav nav-tabs">
                    <li class="active" id="tab_adddispstocks">
                        <a href="#addNewdispStock" data-toggle="tab">Add Dispatch Stock</a>
                    </li>
                    <li class="" id="tab_modifystocks">
                        <a href="#modifydispStock" data-toggle="tab">View/Modify Dispatch Stock</a>
                    </li>
                </ul>
            </header>

            <div class="panel-body">
                <div class="tab-content">
                    <!-- //Add new user tab -->
                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <div class="tab-pane active" id="addNewdispStock">
                        <div class="row">
                            <section class="panel" id="pnlSecurity">
                                <header class="panel-heading">
                                    Dispatch Stock Details
							                <span class="tools pull-right">
                                                <a href="javascript:;" class="fa fa-chevron-down"></a>
                                                <span class="collapsible-server-hidden">
                                                    <asp:HiddenField runat="server" ID="HiddenField3" EnableViewState="true" Value="C" />
                                                </span>
                                            </span>
                                </header>


                                <div class="panel-body collapse">
                                    <div class="form-horizontal adminex-form">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                             <ContentTemplate>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">
                                                Commodity Name :<span style="color: red"> *</span>
                                            </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="selectcommname" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15" x>
                                                    <%--OnSelectedIndexChanged="cmbdrdSupCntrId_SelectedIndexChanged"--%>
                                                </asp:DropDownList>
                                            </div>

                                        </div>

                                        <%--<div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Commodity Name : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtCommodityName" name="txtCommodityName" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Commodity Name" data-original-title="Commodity Name"></asp:TextBox>

                                               <asp:RegularExpressionValidator ID="lettersvalidation" runat="server"
                                                    ErrorMessage="Only Letters Allowed" ControlToValidate="txtCommodityName"
                                                    ValidationExpression="[a-zA-Z ]*$">
                                               </asp:RegularExpressionValidator> 

                                            </div>
                                        </div>--%>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">
                                                Commodity Type :
                                              <span style="color: red">*</span>
                                            </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddlCommodityType" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select One --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="NCDEX"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Non-NCDEX"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Demate"></asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Dispatch Date : </label>
                                            <div class="col-sm-10">
                                                <input type="date" id="dtDispDate" class="custom-input" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Buyer/Seller Company: </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtBuyCompany" name="txtBuyCompany" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Buyer Company" data-original-title="Buyer Company"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Quantity : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtQuantity" TextMode="Number" name="txtQuantity" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Quantity" data-original-title="Quantity"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValueToCompare="0" ControlToValidate="txtQuantity" 
                                            ErrorMessage="Value must be Greater then 0 !!!" Operator="GreaterThan" Type="Integer"></asp:CompareValidator> 
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Warehouse Name : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddlWareName" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" data-original-title="Warehouse Name"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Warehouse Location : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtWareLocation" name="txtWareLocation" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Warehouse Location " data-original-title="Warehouse Location "></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">CDD No : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtCDDNo" name="txtCDDNo" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="CDD No" data-original-title="CDD No"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">CMSE Lot ID : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtLotId" name="txtLotId" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="CMSE Lot ID" data-original-title="CMSE Lot ID"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Discount : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtDiscount" TextMode="Number" name="txtDiscount" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Discount" data-original-title="Discount"></asp:TextBox>
                                            </div>
                                        </div>
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
                                            data-open-on="Save" data-open-panels="pnlSecurity"
                                            runat="server" id="btnSave" onserverclick="btnSave_ServerClick">
                                            New
                                        </button>
                                        <button class="btn btn-info" type="button"
                                            runat="server" id="btnClear" onserverclick="btnClear_ServerClick">
                                            Clear
                                        </button>
                                    </div>
                                    <div class="panel-body ">
                                        <div class="alert alert-info" style="padding: 8px;" runat="server" id="actionInfo">
                                            Press New to Add Dispatch Stock.
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

                    <div class="tab-pane" id="modifydispStock">
                        <div class="form-group">
                            <div class="alert alert-info" runat="server" id="updateActionDiv">
                                Click on respective buttons for Modify / Delete.
                            </div>
                            <div class="adv-table nice-scroll-grid">
                                <asp:GridView ID="grddispatch" runat="server"
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
                                                <asp:Label ID="DSID" runat="server" Text='<%# Eval("iDispatchId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Commodity Name">
                                            <ItemTemplate>
                                                <asp:Label ID="commodityname" runat="server" Text='<%# Eval("sCommodityName") %>'></asp:Label>
                                            </ItemTemplate>
                                           <%--  <EditItemTemplate>
                                                <asp:Label ID="edtcommodityname" runat="server" Text='<%# Bind("sCommodityName") %>'>
                                                </asp:Label>
                                            </EditItemTemplate>--%>
                                            
                                     
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Commodity Type">
                                            <ItemTemplate>
                                                <asp:Label ID="cmdtytype" runat="server" Text='<%# Eval("sCommodityType") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--<EditItemTemplate>
                                           
                                                <asp:DropDownList ID="drpdwncommoditytype" runat="server" Font-Bold="True" AutoPostBack="True" CssClass="form-control m-bot15" 
                                                       SelectedValue='<%# Bind("sCommodityType") %>' >
                                                    <asp:ListItem Value="0" Text="-- Select One --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="NCDEX"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Non-NCDEX"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Demate"></asp:ListItem>

                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dispatch Date">
                                            <ItemTemplate>
                                                <asp:Label ID="dispatchdate" runat="server" Text='<%# Eval("dDispatchDate") %>'></asp:Label>
                                            </ItemTemplate>

                                          <%--  <EditItemTemplate>
                                                <asp:TextBox ID="edtdispatchdate" TextMode="Date" runat="server" Text='<%# Bind("dDispatchDate","{0:yyyy-MM-dd}") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Buyer Company">
                                            <ItemTemplate>
                                                <asp:Label ID="buyercompany" runat="server" Text='<%# Eval("sSeller") %>'></asp:Label>
                                            </ItemTemplate>

                                           <%-- <EditItemTemplate>
                                                <asp:TextBox ID="edtbuyercompany" runat="server" Text='<%# Bind("sSeller") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="qty" runat="server" Text='<%# Eval("iQty") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--<EditItemTemplate>
                                                <asp:TextBox ID="edtqty" runat="server" TextMode="Number" Text='<%# Bind("iQty") %>'>
                                                </asp:TextBox>
                                               <asp:CompareValidator ID="CompareValidator2" runat="server" ValueToCompare="0" ControlToValidate="edtqty" 
                                                ErrorMessage="Value must be Greater then 0 !!!" Operator="GreaterThan" Type="Integer"></asp:CompareValidator> 

                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Warehouse Name">
                                            <ItemTemplate>
                                                <asp:Label ID="warehousename" runat="server" Text='<%# Eval("sWarehouseName") %>'></asp:Label>
                                            </ItemTemplate>

                                         <%--   <EditItemTemplate>
                                                  <asp:Label ID="lblsWarehouse" Visible="false" runat="server" Text='<%# Bind("sWarehouseName") %>'> </asp:Label>
                                                    <asp:DropDownList ID="edtddlsWarehouse" AutoPostBack="true" runat="server" Font-Bold="True" CssClass="form-control m-bot15">
                                                </asp:DropDownList>
                                             
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Warehouse Location">
                                            <ItemTemplate>
                                                <asp:Label ID="warehouselocation" runat="server" Text='<%# Eval("sWarhouseLocation") %>'></asp:Label>
                                            </ItemTemplate>

                                        <%--    <EditItemTemplate>
                                                <asp:TextBox ID="edtwarehouselocation" runat="server" Text='<%# Bind("sWarhouseLocation") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="CDD No">
                                            <ItemTemplate>
                                                <asp:Label ID="cddno" runat="server" Text='<%# Eval("sCDDNo") %>'></asp:Label>
                                            </ItemTemplate>

                                       <%--     <EditItemTemplate>
                                                <asp:TextBox ID="edtcddno" runat="server" Text='<%# Bind("sCDDNo") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="CMSE Lot ID">
                                            <ItemTemplate>
                                                <asp:Label ID="cmselotid" runat="server" Text='<%# Eval("sCMSELotId") %>'></asp:Label>
                                            </ItemTemplate>

                                          <%--  <EditItemTemplate>
                                                <asp:TextBox ID="edtcmselotid" runat="server" Text='<%# Bind("sCMSELotId") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Discount">
                                            <ItemTemplate>
                                                <asp:Label ID="discount" runat="server" Text='<%# Eval("nDiscount") %>'></asp:Label>
                                            </ItemTemplate>

                                          <%--  <EditItemTemplate>
                                                <asp:TextBox ID="edtdiscount" runat="server" Text='<%# Bind("nDiscount") %>'>
                                                </asp:TextBox>
                                                <asp:RegularExpressionValidator ID="numberswithdecimalvalidation" runat="server"
                                                    ErrorMessage="Only Numbers Allowed" ControlToValidate="edtdiscount"
                                                    ValidationExpression="((\d+)((\.\d{1,2})?))$">
                                               </asp:RegularExpressionValidator> 
                                              
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <%--<asp:CommandField ShowEditButton="True" HeaderText="Actions" HeaderStyle-CssClass="nosort" />--%>
                                            <asp:TemplateField  HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="editLink" class="edit"  runat="server" data-toggle="tab"  
                                                    data-dispatch-id='<%# Eval("iDispatchId") %>'
                                                    href="javascript:;#addNewdispStock"  >Edit <i class="fa fa-edit"></i>&nbsp;</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="A1" class="delete" runat="server"
                                                    data-dispatch-id='<%# Eval("iDispatchId") %>'
                                                    data-commodity-name='<%# Eval("sCommodityName") %>' href="javascript:;">Delete</a>
                                            </ItemTemplate> 
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                             <asp:HiddenField ID="HidEdit" runat="server" Value="" />
                        <asp:HiddenField ID="HidIsEditMode" runat="server" Value="" />
                        <asp:HiddenField ID="HidBnkId" runat="server" Value="" />

                        </div>
                    </div>
                 
                </div>
            </div>
        </section>
        <!-- Panel Body Main -->
    </div>
    <!-- Main Row -->

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modDeleteDispatchStock" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                    <h4 class="modal-title">Delete the Dispatch Stock.</h4>
                </div>
                <div class="modal-body">

                    <div role="form">
                        <div class="form-group">
                            <h3>Are you sure you want to delete Dispatch Stock?</h3>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2 col-sm-2 control-label"> ID </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDelDispatchID" name="txtDelDispatchID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="txtDelHidden" runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- Id -->
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2 col-sm-2 control-label">Commodity Name </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDelCommodityName" name="txtDelCommodityName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!-- Name -->

                        <div class="form-group">
                            <div class="row">
                                <div class="panel-body" style="text-align: center">
                                    <button class="btn btn-success" type="button"
                                        runat="server" id="btnDeleteDispatchStock" onserverclick="btnDeleteDispatchStock_ServerClick">
                                        <i class="fa fa-trash"></i>Delete Dispatch Stock
                                    </button>
                                    <button class="btn btn-danger" type="button"
                                        id="btnCancelDeleteDispatchStock">
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
    <script type="text/javascript" src="js/pagesjs/DispatchStock.js"></script>
</asp:Content>
