<%@ Page Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="AddStock.aspx.cs" Inherits="Admin_CommTrex.AddStock" %>

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
        <h3>Add/Modify/Delete Stock</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Stock </a>
            </li>
            <li class="active">Stock Details </li>
        </ul>
    </div>

</asp:Content>

<asp:Content ID="contChild_ExBody" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <asp:HiddenField ID="activeTab" EnableViewState="true" runat="server" Value="addproducts" />
    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading custom-tab ">
                <ul class="nav nav-tabs">
                    <li class="active" id="tab_addstocks">
                        <a href="#addNewStock" data-toggle="tab">Add New Stock</a>
                    </li>
                      <li class="" id="AddExcel">
                        <a href="#ExcelImport" data-toggle="tab">Import Warehouse from Excel</a>
                    </li>
                    <li class="" id="Ware_Manual">
                        <a href="#WareHosuseImport" data-toggle="tab">Import Warehouse Manually</a>
                    </li>
                    <li class="" id="tab_modifystocks">
                        <a href="#modifyStock" data-toggle="tab">View/Modify Stock</a>
                    </li>
                </ul>
            </header>

            <div class="panel-body">
                <div class="tab-content">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <!-- //Add new user tab -->
                    <div class="tab-pane active" id="addNewStock">
                        <div class="row">
                            <section class="panel" id="pnlSecurity">
                                <header class="panel-heading">
                                    Stock Details
							                <span class="tools pull-right">
                                                <a href="javascript:;" class="fa fa-chevron-down"></a>
                                                <span class="collapsible-server-hidden">
                                                    <asp:HiddenField runat="server" ID="HiddenField3" EnableViewState="true" Value="c" />
                                                </span>
                                            </span>
                                </header>


                                <div class="panel-body collapse">
                                    <div class="form-horizontal adminex-form">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                             <ContentTemplate>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Commodity Name : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddl_CommNm" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control tooltips">
                                                   
                                                </asp:DropDownList>
                                                <%--  <asp:TextBox ID="txtCommodityName" name="txtCommodityName" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Commodity Name" data-original-title="Commodity Name"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">
                                                Commodity Exchange(MKT) :
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
                                            <label class="col-sm-2 col-sm-2 control-label">Deposite Date : </label>
                                            <div class="col-sm-10">
                                                <input type="date" id="dtDepDate" class="custom-input" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Depositor Company (Seller) : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtDepCompany" name="txtDepCompany" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Depositor Company" data-original-title="Depositor Company"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Quantity : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtQuantity" TextMode="Number" name="txtQuantity" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="in KGs" data-original-title="Quantity"></asp:TextBox>
                                              <asp:CompareValidator ID="CompareValidator1" runat="server" ValueToCompare="0" ControlToValidate="txtQuantity" 
                                                ErrorMessage="Value must be Greater then 0 !!!" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>
                                            </div>
                                        </div> <%--relation with stock qty, commodity--%>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Warehouse Location : </label>
                                            <div class="col-sm-10">


                                              <%--  <asp:TextBox ID="txtWareLocation" name="txtWareLocation" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Warehouse Location " data-original-title="Warehouse Location "></asp:TextBox>--%>
                                                    
                                                <asp:DropDownList ID="ddl_WareLocation" runat="server" Font-Bold="True" AutoPostBack="True"
                                                     CssClass="form-control tooltips">
                                                   <asp:ListItem Value="0" Text="--Select Warehouse--"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="NCDEX"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Non-NCDEX"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Demate"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                      <%--  location base warehouse name fill--%>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Warehouse Name : </label>
                                            <div class="col-sm-10">
                                                
                                             <%--   <asp:TextBox ID="txtWareName" name="txtWareName" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Warehouse Name" data-original-title="Warehouse Name"></asp:TextBox>--%>
                                                  <asp:DropDownList ID="ddl_WareName" runat="server" Font-Bold="True" 
                                                    CssClass="form-control tooltips"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">CDD No : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtCDDNo" TextMode="Number" name="txtCDDNo" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="CDD No" data-original-title="CDD No"></asp:TextBox>
                                               
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">CMSE Lot ID : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtLotId" TextMode="Number" name="txtLotId" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="CMSE Lot ID" data-original-title="CMSE Lot ID"></asp:TextBox>
                                                
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Discount : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtDiscount" TextMode="Number" name="txtDiscount" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Discount in %" data-original-title="Discount"></asp:TextBox>
                                               
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">FED : </label>
                                            <div class="col-sm-10">
                                                <input type="date" id="dtFEDDate" class="custom-input" runat="server" />
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
                                            runat="server" onserverclick="btnClear_ServerClick" id="btnClear">
                                            Clear 
                                        </button>
                                    </div>
                                    <div class="panel-body ">

                                        <div class="alert alert-info" style="padding: 8px;" runat="server" id="actionInfo">
                                            Press New to Add Stock.
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

                     <div class="tab-pane" id="WareHosuseImport">
                        <div class="form-group">
                            <div class="alert alert-info" runat="server" id="Warehouse_alert">
                               Import Warehouse Manually
                            </div>

                             <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Warehouse Name : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="WareName" name="txtWareName" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="WareName" data-original-title="WareName"></asp:TextBox>
                                            </div>

                                 <div style="text-align:center;">                                                               
                                 <button id="Ware_btn" style="margin-top: 30px;"  class="pnl-opener btn gbtn" type="button" runat="server" onserverclick="InserWarehouse">Save </button>
                                    </div>

                                        </div>

                            </div>
                         </div>

                       <div class="tab-pane" id="ExcelImport">
                        <div class="form-group">
                            <div class="alert alert-info" runat="server" id="Excell_Info">
                               Import Warehouse From Excel
                            </div>
                               <div >
                                   <asp:FileUpload  ID="fileExcel" Height="26px" accept=".pdf .jpg .png" runat="server" class="default" />                                      
                                       </div>
                            <div class="panel-body" >                                     
                                        <button class="btn btn-info" type="button"
                                            runat="server" id="Button2" onserverclick="btnUploadExcel_Click">
                                            Import <i class="fa fa-refresh"></i>
                                        </button>                              
                                    </div>
                             <div style="float:right" > 
                                 <asp:Label runat="server">Sample File for Warehouse Upload: </asp:Label>   
                                    
                                 <button  class="pnl-opener btn gbtn" type="button" runat="server" onserverclick="DownloadFile">Download </button>
                                    </div>
                            </div>
                           </div>
                    <div class="tab-pane" id="modifyStock">
                        <div class="form-group">
                            <div class="alert alert-info" runat="server" id="updateActionDiv">
                                Click on respective buttons for Modify / Delete.
                            </div>
                            <div class="adv-table nice-scroll-grid">
                                <asp:GridView ID="grdUser" runat="server"
                                    EnableModelValidation="True" AutoGenerateColumns="False"
                                    
                                    DataKeyNames="" OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                    RowStyle-CssClass="gradeA"
                                    class="dynamic-table-grid display table table-bordered table-striped">
                                      <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="stockId" runat="server" Text='<%# Eval("iStockId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Commodity Name">
                                            <ItemTemplate>
                                                <asp:Label ID="sCommodityName" runat="server" Text='<%# Eval("sCommodityName") %>'></asp:Label>
                                            </ItemTemplate>
                                          <%--  <EditItemTemplate>                                       
                                                <asp:DropDownList ID="CommodityName" selectedValue='<%# Eval("sCommodityName") %>' runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <%--<asp:ListItem Value="0" Text="-- Select Commodity --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="ABC"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="PQR"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="XYZ"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Commodity Market">
                                            <ItemTemplate>
                                                <asp:Label ID="sCommodityMarket" runat="server" Text='<%# Eval("sCommodityMarket") %>'></asp:Label>
                                            </ItemTemplate>

                                           <%-- <EditItemTemplate>
                                              
                                                <asp:DropDownList ID="CommodityMarket" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Enabled="true" Value="0" Text="-- Select One --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="NCDEX"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Non-NCDEX"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Demate"></asp:ListItem>

                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Deposit Date">
                                            <ItemTemplate>
                                                <asp:Label ID="dDepositDate" runat="server" Text='<%# Eval("dDepositDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                                            </ItemTemplate>

                                          <%--  <EditItemTemplate>
                                                <asp:TextBox ID="DepositDate" TextMode="Date" runat="server" Text='<%# Bind("dDepositDate","{0:yyyy-MM-dd}") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seller">
                                            <ItemTemplate>
                                                <asp:Label ID="sSeller" runat="server" Text='<%# Eval("sSeller") %>'></asp:Label>
                                            </ItemTemplate>

                                        <%--    <EditItemTemplate>
                                                <asp:TextBox ID="Seller" runat="server" Text='<%# Bind("sSeller") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="Qnt" runat="server" Text='<%# Eval("iQty") %>'></asp:Label>
                                            </ItemTemplate>

                                           <%-- <EditItemTemplate>
                                                <asp:TextBox ID="qnt" TextMode="Number" runat="server" Text='<%# Bind("iQty") %>'>
                                                </asp:TextBox>
                                                   <asp:CompareValidator ID="CompareValidator2" runat="server" ValueToCompare="0" ControlToValidate="qnt" 
                                                ErrorMessage="Value must be Greater then 0 !!!" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Warehouse Location">
                                            <ItemTemplate>
                                                <asp:Label ID="sWarehouse" runat="server" Text='<%# Eval("sWarehouseLocation") %>'></asp:Label>
                                            </ItemTemplate>

                                         <%--   <EditItemTemplate>
                                                <asp:TextBox ID="WarehouseLocation" runat="server" Text='<%# Bind("sWarehouseLocation") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="WarehouseName">
                                            <ItemTemplate>
                                                <asp:Label ID="WarehouseName" runat="server" Text='<%# Eval("sWarehouseName") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--<EditItemTemplate>
                                            
                                                <asp:Label ID="lbWarehouseName" Visible="false" runat="server" Text='<%# Eval("sWarehouseName") %>'></asp:Label>
                                                 <asp:DropDownList ID="sWarehouseName"  runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                               
                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CDDNo">
                                            <ItemTemplate>
                                                <asp:Label ID="sCDDNo" runat="server" Text='<%# Eval("sCDDNo") %>'></asp:Label>
                                            </ItemTemplate>

                                         <%--   <EditItemTemplate>
                                                <asp:TextBox ID="CDDNo" runat="server" Text='<%# Bind("sCDDNo") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CMSE Lot Id">
                                            <ItemTemplate>
                                                <asp:Label ID="sCMSELotId" runat="server" Text='<%# Eval("sCMSELotId") %>'></asp:Label>
                                            </ItemTemplate>

                                           <%-- <EditItemTemplate>
                                                <asp:TextBox ID="CMSELotId" runat="server" Text='<%# Bind("sCMSELotId") %>'>
                                                </asp:TextBox>
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                ControlToValidate="CMSELotId" runat="server"
                                                ErrorMessage="Only Numbers allowed"
                                                       ValidationExpression="((\d+)((\.\d{1,2})?))$">
                                         
                                                </asp:RegularExpressionValidator>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Discount">
                                            <ItemTemplate>
                                                <asp:Label ID="nDiscount" runat="server" Text='<%# Eval("nDiscount") %>'></asp:Label>
                                            </ItemTemplate>

                                           <%--<EditItemTemplate>
                                                <asp:TextBox ID="Discount" runat="server" Text='<%# Bind("nDiscount") %>'>
                                                </asp:TextBox>
                                                   <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                                ControlToValidate="Discount" runat="server"
                                                ErrorMessage="Only Numbers allowed"
                                                       ValidationExpression="((\d+)((\.\d{1,2})?))$">
                                              
                                                </asp:RegularExpressionValidator>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FED">
                                            <ItemTemplate>
                                                <asp:Label ID="dFED" runat="server" Text='<%# Eval("dFED","{0:yyyy-MM-dd}") %>'></asp:Label>
                                            </ItemTemplate>

                                        <%--    <EditItemTemplate>
                                                <asp:TextBox ID="FED" TextMode="Date" runat="server" Text='<%# Bind("dFED","{0:yyyy-MM-dd}") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                      <%--  <asp:CommandField ShowEditButton="True" HeaderText="Actions" HeaderStyle-CssClass="nosort" />--%>
                                           <asp:TemplateField  HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="editLink" class="edit"  runat="server" data-toggle="tab"  
                                                    data-user-id='<%# Eval("iStockId") %>'
                                                    href="javascript:;#addNewStock"  >Edit <i class="fa fa-edit"></i>&nbsp;</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="A1" class="delete" runat="server"
                                                    data-user-id='<%# Eval("iStockId") %>'
                                                    data-user-name='<%# Eval("sCommodityName") %>' href="javascript:;">Delete</a>
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

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modDeleteAdmin" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                    <h4 class="modal-title">Delete the Admin.</h4>
                </div>
                <div class="modal-body">

                    <div role="form">
                        <div class="form-group">
                            <h3>Are you sure you want to delete Stock Detail?</h3>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2 col-sm-2 control-label">Stock ID </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDelAdminID" name="txtDelAdminID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="txtDelHidden" runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- Id -->
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2 col-sm-2 control-label">Commodity Name </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDelAdminName" name="txtDelAdminName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!-- Name -->

                        <div class="form-group">
                            <div class="row">
                                <div class="panel-body" style="text-align: center">
                                    <button class="btn btn-success" type="button" onserverclick="btnDeleteClient_ServerClick"
                                        runat="server" id="btnDeleteAdmin">
                                        <i class="fa fa-trash"></i>Delete Stock Detail 
                                    </button>
                                    <button class="btn btn-danger" type="button" 
                                        id="btnCancelDeleteAdmin">
                                        Cancel 
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
    <script type="text/javascript" src="js/pagesjs/AddStock.js"></script>
        
 <script type="text/javascript" src="js/websitejs/jquery.sumoselect.min.js"></script>

</asp:Content>
