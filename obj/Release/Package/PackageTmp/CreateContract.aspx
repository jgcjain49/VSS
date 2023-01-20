<%@ Page  MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="CreateContract.aspx.cs" Inherits="Admin_CommTrex.CreateContract" %>
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
        <h3>Add/Modify/Delete Contract details</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Contract </a>
            </li>
            <li class="active">Contract Details </li>
        </ul>
    </div>

</asp:Content>

<asp:Content ID="contChild_ExBody" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <asp:HiddenField ID="activeTab" EnableViewState="true" runat="server" Value="addproducts" />
    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading custom-tab ">
                <ul class="nav nav-tabs">
                    <li class="active" id="tab_addcontract">
                        <a href="#addNewContract" data-toggle="tab">Add New Contract</a>
                    </li>
                    <li class="" id="tab_modifycontracts">
                        <a href="#modifyContract" data-toggle="tab">View/Modify Contract</a>
                    </li>
                </ul>
            </header>

            <div class="panel-body">
                <div class="tab-content">
                    <!-- //Add new user tab -->
                     <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <div class="tab-pane active" id="addNewContract">
                        <div class="row">
                            <section class="panel" id="pnlSecurity">
                                <header class="panel-heading">
                                    Contract Details
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
                                            <label class="col-sm-2 col-sm-2 control-label">Date : </label>
                                            <div class="col-sm-10">
                                                <input type="date" id="dtDate" class="custom-input" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">
                                                Seller :
                                              <span style="color: red">*</span>
                                            </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtSeller" name="txtSeller" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Seller" data-original-title="Seller"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">
                                                Commodity :
                                              <span style="color: red">*</span>
                                            </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtCommodity" name="txtCommodity" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Enter Commodity" data-original-title="Enter Commodity"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="lettersvalidation" runat="server"
                                                    ErrorMessage="Only Letters Allowed" ControlToValidate="txtCommodity"
                                                    ValidationExpression="[a-zA-Z ]*$">
                                               </asp:RegularExpressionValidator> 
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Contract Mode : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtContractMode" name="txtContractMode" runat="server" CssClass="form-control tooltips"
                                                    data-toggle="tooltip" title="" placeholder="Contract Mode" data-original-title="Contract Mode"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Broker : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtBroker" name="txtBroker" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Broker" data-original-title="Broker"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Select : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="drpSelect" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select One --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Buyer"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Seller"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Contract Condition : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddlContCondition" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select Condition --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="NCDEX"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="NON-NCDEX"></asp:ListItem>
                                                </asp:DropDownList>
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
                                            <label class="col-sm-2 col-sm-2 control-label">Contract Mode : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddlContMode" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select Mode --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Buy"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Sell"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Square Off"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Total Rate : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtTotalRate" TextMode="Number" name="txtTotalRate" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Total Rate" data-original-title="Total Rate"></asp:TextBox>
                                            </div>
                                        </div>

                                       <%-- <div class="form-group" style="display:none;">
                                            <label class="col-sm-2 col-sm-2 control-label">Expiry : </label>
                                            <div class="col-sm-10">
                                                <input type="date" id="dtExpiry" class="custom-input" runat="server" />
                                            </div>
                                        </div>--%>
                                       <%--Expiry only show if square off selected--%>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Delivery Location : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddlDeliveryLocation" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select delivery Location --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="ABC"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="PQR"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="XYZ"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Delivery Condition : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddlDeliveryCondition" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select Delivery Condition --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="ABC"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="PQR"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="XYZ"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Packing : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtPacking" name="txtPacking" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Packing" data-original-title="Packing"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Brokerage : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtBrokerage" name="txtBrokerage" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Brokerage" data-original-title="Brokerage"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Payment Days : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtPaymentDays" name="txtPaymentDays" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Payment Days" data-original-title="Payment Days"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Again CIS : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtAgainCIS" name="txtAgainCIS" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Again CIS" data-original-title="Again CIS"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Payment Condition : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddlPayCondition" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select Payment Condition --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="ABC"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="PQR"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="XYZ"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                       <%-- autofill drpdwn like txt--%>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Discount/Kapat (%) : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtDiscKepat" TextMode="Number" name="txtDiscKepat" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Discount/Kapat (%)" data-original-title="Discount/Kapat (%)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Contract Terms : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtContractTerms" name="txtContractTerms" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Contract Terms" data-original-title="Contract Terms"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Internal Comment : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtInternalComment" name="txtInternalComment" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Internal Comment" data-original-title="Internal Comment"></asp:TextBox>
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
                                            Press New to Add Contract.
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

                    <div class="tab-pane" id="modifyContract">
                        <div class="form-group">
                            <div class="alert alert-info" runat="server" id="updateActionDiv">
                                Click on respective buttons for Modify / Delete.
                            </div>
                            <div class="adv-table nice-scroll-grid">
                                <asp:GridView ID="grdUser" runat="server"
                                    EnableModelValidation="True"  AutoGenerateColumns="False"
                                    DataKeyNames="" OnRowEditing="GridView_RowEditing" OnRowUpdating="GridView1_RowUpdating"
                                      OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                    RowStyle-CssClass="gradeA"
                                    class="dynamic-table-grid display table table-bordered table-striped">
                                      <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>                                                      
                                                <asp:Label ID="ContractID" runat="server" Text='<%# Eval("iContractId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="userDatelb" runat="server"  Text='<%# Eval("dDate") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="date"  TextMode="Date"  runat="server" Text='<%# Bind("dDate","{0:yyyy-MM-dd}") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Seller">
                                            <ItemTemplate>
                                                <asp:Label ID="Sellerlb" runat="server" Text='<%# Eval("sSeller") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="Seller" runat="server" Text='<%# Bind("sSeller") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Commodity">
                                            <ItemTemplate>
                                                <asp:Label ID="Commoditylb" runat="server" Text='<%# Eval("sCommodity") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="Commodity" runat="server" Text='<%# Bind("sCommodity") %>'>
                                                </asp:TextBox>
                                                <asp:RegularExpressionValidator ID="lettersvalidation" runat="server"
                                                    ErrorMessage="Only Letters Allowed" ControlToValidate="Commodity"
                                                    ValidationExpression="[a-zA-Z ]*$">
                                               </asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ContractMode">
                                            <ItemTemplate>
                                                <asp:Label ID="ContractModelb" runat="server" Text='<%# Eval("sContractMode") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="ContractMode" runat="server" Text='<%# Bind("sContractMode") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Broker">
                                            <ItemTemplate>
                                                <asp:Label ID="lbBroker" runat="server" Text='<%# Eval("sBroker") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="Broker" runat="server" Text='<%# Bind("sBroker") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:Label ID="lbSelect" runat="server" Text='<%# Eval("sSelect") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                               
                                                  <asp:DropDownList ID="drpSelect"  selectedValue='<%# Eval("sSelect") %>' runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select One --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Buyer"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Seller"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="ContractCondition">
                                            <ItemTemplate>
                                                <asp:Label ID="lbContractCondition" runat="server" Text='<%# Eval("sContractCondition") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                  <asp:DropDownList ID="ContCondition" selectedValue='<%# Eval("sContractCondition") %>' runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select Condition --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="NCDEX"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="NON-NCDEX"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Qnt">
                                            <ItemTemplate>
                                                <asp:Label ID="lbQty" runat="server" Text='<%# Eval("iQty") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="Qty" runat="server" TextMode="Number" Text='<%# Bind("iQty") %>'>
                                                </asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValueToCompare="0" ControlToValidate="Qty" 
                                    ErrorMessage="Value must be Greater then 0 !!!" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="ContarctMode1">
                                            <ItemTemplate>
                                                <asp:Label ID="lbContarctMode1" runat="server" Text='<%# Eval("sContarctMode1") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                 <asp:DropDownList ID="ContMode" selectedValue='<%# Eval("sContarctMode1") %>' runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select Mode --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Buy"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Sell"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Square Off"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="TotalRate">
                                            <ItemTemplate>
                                                <asp:Label ID="lbTotalRate" runat="server" Text='<%# Eval("nTotalRate") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="TotalRate" runat="server" Text='<%# Bind("nTotalRate") %>'>
                                                </asp:TextBox>
                                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                ControlToValidate="TotalRate" runat="server"
                                                ErrorMessage="Only Numbers allowed"
                                                       ValidationExpression="((\d+)((\.\d{1,2})?))$">
                                               <%-- ValidationExpression="^(0 |[1-9]\d*)$">--%>
                                                </asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="DeliveryLocation">
                                            <ItemTemplate>
                                                <asp:Label ID="lbDeliveryLocation" runat="server"  Text='<%# Eval("sDeliveryLocation") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DeliveryLoc" selectedValue='<%# Eval("sDeliveryLocation") %>' runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select delivery Location --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="ABC"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="PQR"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="XYZ"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="DeliveryCondition">
                                            <ItemTemplate>
                                                <asp:Label ID="lbDeliveryCondition" runat="server" Text='<%# Eval("sDeliveryCondition") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                               <asp:DropDownList ID="DeliveryCondition" selectedValue='<%# Eval("sDeliveryCondition") %>' runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select Delivery Condition --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="ABC"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="PQR"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="XYZ"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Packing">
                                            <ItemTemplate>
                                                <asp:Label ID="lbPacking" runat="server" Text='<%# Eval("sPacking") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="Packing" runat="server" Text='<%# Bind("sPacking") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Brokerage">
                                            <ItemTemplate>
                                                <asp:Label ID="lbBrokerage" runat="server" Text='<%# Eval("sBrokerage") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="Brokerage" runat="server" Text='<%# Bind("sBrokerage") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="PaymentDays">
                                            <ItemTemplate>
                                                <asp:Label ID="lbPaymentDays" runat="server" Text='<%# Eval("sPaymentDays") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="PaymentDays" runat="server" Text='<%# Bind("sPaymentDays") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="AgainCIS">
                                            <ItemTemplate>
                                                <asp:Label ID="lbAgainCIS" runat="server" Text='<%# Eval("sAgainCIS") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="AgainCIS" runat="server" Text='<%# Bind("sAgainCIS") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="PaymentCondition">
                                            <ItemTemplate>
                                                <asp:Label ID="lbPaymentCondition" runat="server" Text='<%# Eval("sPaymentCondition") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                     <asp:DropDownList ID="PayCondition" selectedValue='<%# Eval("sPaymentCondition") %>' runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select Payment Condition --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="ABC"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="PQR"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="XYZ"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Discount">
                                            <ItemTemplate>
                                                <asp:Label ID="lbDiscount" runat="server" Text='<%# Eval("nDiscount") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="Discount" runat="server" Text='<%# Bind("nDiscount") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="ContractTerms">
                                            <ItemTemplate>
                                                <asp:Label ID="lbContractTerms" runat="server" Text='<%# Eval("sContractTerms") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="ContractTerms" runat="server" Text='<%# Bind("sContractTerms") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="InternalComment">
                                            <ItemTemplate>
                                                <asp:Label ID="lbInternalComment" runat="server" Text='<%# Eval("sInternalComment") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="InternalComment" runat="server" Text='<%# Bind("sInternalComment") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:CommandField ShowEditButton="True" HeaderText="Actions" HeaderStyle-CssClass="nosort" />--%>
                                           <asp:TemplateField  HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="editLink" class="edit"  runat="server" data-toggle="tab"  
                                                    data-user-id='<%# Eval("iContractId")%>'
                                                    href="javascript:;#addNewContract"  >Edit <i class="fa fa-edit"></i>&nbsp;</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="A1" class="delete" runat="server"
                                                    data-user-id='<%# Eval("iContractId") %>'
                                                    data-user-name='<%# Eval("dDate") %>' href="javascript:;">Delete</a>
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
                            <h3>Are you sure you want to delete Admin?</h3>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2 col-sm-2 control-label">Admin ID </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDelAdminID" name="txtDelAdminID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="txtDelHidden" runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- Id -->
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2 col-sm-2 control-label">Admin Name </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDelAdminName" name="txtDelAdminName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!-- Name -->

                        <div class="form-group">
                            <div class="row">
                                <div class="panel-body" style="text-align: center">
                                    <button class="btn btn-success" type="button"  onserverclick="btnDeleteClient_ServerClick"
                                        runat="server" id="btnDeleteAdmin">
                                        <i class="fa fa-trash"></i>Delete Admin
                                    </button>
                                    <button class="btn btn-danger" type="button"
                                        id="btnCancelDeleteAdmin">
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
    <script type="text/javascript" src="js/pagesjs/payment.js"></script>
</asp:Content>
