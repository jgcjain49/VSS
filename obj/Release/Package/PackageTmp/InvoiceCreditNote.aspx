<%@ Page Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="InvoiceCreditNote.aspx.cs" Inherits="Admin_CommTrex.InvoiceCreditNote" %>

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
        <h3>Add/Modify/Delete Invoice/Credit Note</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Invoice/Credit Note </a>
            </li>
            <li class="active">Invoice/Credit Note Details </li>
        </ul>
    </div>

</asp:Content>

<asp:Content ID="contChild_ExBody" ContentPlaceHolderID="CntAdminEx_Body" runat="server">
    <asp:HiddenField ID="activeTab" EnableViewState="true" runat="server" Value="addproducts" />
    <div class="col-lg-12">
        <section class="panel">
            <header class="panel-heading custom-tab ">
                <ul class="nav nav-tabs">
                    <li class="active" id="tab_addinvcrednote">
                        <a href="#addNewinvcrednote" data-toggle="tab">Add Invoice/Credit Note</a>
                    </li>
                    <li class="" id="tab_modifyinvcrednote">
                        <a href="#modifyinvcrednote" data-toggle="tab">View/Modify Invoice/Credit Note</a>
                    </li>
                </ul>
            </header>

            <div class="panel-body">
                <div class="tab-content">
                    <!-- //Add new user tab -->
                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <div class="tab-pane active" id="addNewinvcrednote">
                        <div class="row">
                            <section class="panel" id="pnlSecurity">
                                <header class="panel-heading">
                                    Invoice/Credit Note Details
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
                                            <label class="col-sm-2 col-sm-2 control-label">Invoice Number : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtInvoiceNumber" name="txtInvoiceNumber" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Invoice Number" data-original-title="Invoice Number"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                ControlToValidate="txtInvoiceNumber" runat="server"
                                                ErrorMessage="Only Numbers allowed"
                                                ValidationExpression="\d+">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Date : </label>
                                            <div class="col-sm-10">
                                                <input type="date" id="dtDate" class="custom-input" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Client Name : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="selectclientname" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15" x>
                                                </asp:DropDownList>
                                               <%-- <asp:TextBox ID="txtClientName" name="txtClientName" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Client Name" data-original-title="Client Name"></asp:TextBox>--%>

                                               <%-- <asp:RegularExpressionValidator ID="lettersvalidation" runat="server"
                                                    ErrorMessage="Only Letters Allowed" ControlToValidate="txtClientName"
                                                    ValidationExpression="[a-zA-Z ]*$">
                                               </asp:RegularExpressionValidator> --%>
                                            </div>
                                        </div>

                                     <%--   <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Type Of Invoice : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtTypeOfInvoice" name="txtTypeOfInvoice" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Type Of Invoice" data-original-title="Type Of Invoice"></asp:TextBox>

                                            </div>
                                        </div>--%>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Contract Name : </label>
                                            <div class="col-sm-10">
                                                  <asp:DropDownList ID="selectcontractname" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15" x>
                                                </asp:DropDownList>
                                              <%--  <asp:TextBox ID="txtContName" name="txtContName" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Contract Name" data-original-title="Contract Name"></asp:TextBox>--%>
                                                <%-- <asp:RegularExpressionValidator ID="lettersvalidation1" runat="server"
                                                    ErrorMessage="Only Letters Allowed" ControlToValidate="txtContName"
                                                    ValidationExpression="[a-zA-Z ]*$">
                                               </asp:RegularExpressionValidator> --%>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Commodity Name : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="selectCommodityName" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15" x>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">HSN Code : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtHSNCode" name="txtHSNCode" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="HSN Code" data-original-title="HSN Code"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Value For Lot Size : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtValueForLotSize"  AutoPostBack="true" OnTextChanged="changeEventOnLotSIze" TextMode="Number" name="txtValueForLotSize" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Value For Lot Size" data-original-title="Value For Lot Size"></asp:TextBox>

                                             <asp:CompareValidator ID="CompareValidator3" runat="server" ValueToCompare="0" ControlToValidate="txtValueForLotSize" 
                                            ErrorMessage="Value must be Greater then 0 !!!" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Quantity : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtQuantity"   TextMode="Number" AutoPostBack="true" OnTextChanged="ChangeEventOnQuality" name="txtQuantity" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Quantity" data-original-title="Quantity"></asp:TextBox>

                                                      <asp:CompareValidator ID="CompareValidator1" runat="server" ValueToCompare="0" ControlToValidate="txtQuantity" 
                                                            ErrorMessage="Value must be Greater then 0 !!!" Operator="GreaterThan" Type="Integer">
                                                      </asp:CompareValidator>
                                               
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Total Value : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtTotalValue" ReadOnly="true" TextMode="Number" name="txtTotalValue" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Total Value" data-original-title="Total Value"></asp:TextBox>

                                                 <asp:RegularExpressionValidator ID="numberswithdecimalvalidation1" runat="server"
                                                    ErrorMessage="Only Numbers Allowed" ControlToValidate="txtTotalValue"
                                                    ValidationExpression="((\d+)((\.\d{1,2})?))$">
                                               </asp:RegularExpressionValidator> 
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">GST : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtGST"  TextMode="Number" name="txtGST" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="GST" data-original-title="GST"></asp:TextBox>

                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                    ErrorMessage="Only Numbers Allowed" ControlToValidate="txtGST"
                                                    ValidationExpression="((\d+)((\.\d{1,2})?))$">
                                               </asp:RegularExpressionValidator> 
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Other Tax : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtOtherTax"  TextMode="Number" name="txtOtherTax" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Other Tax" data-original-title="Other Tax"></asp:TextBox>

                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                    ErrorMessage="Only Numbers Allowed" ControlToValidate="txtOtherTax"
                                                    ValidationExpression="((\d+)((\.\d{1,2})?))$">
                                               </asp:RegularExpressionValidator> 
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Discount : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtDiscount" MaxLength="100" TextMode="Number" name="txtDiscount" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Discount" data-original-title="Discount"></asp:TextBox>

                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                                    ErrorMessage="Only Numbers Allowed" ControlToValidate="txtDiscount"
                                                   ValidationExpression="((\d+)((\.\d{1,2})?))$">
                                               </asp:RegularExpressionValidator> 
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Rounding : </label>
                                            <div class="col-sm-10">
                                                <asp:RadioButtonList ID="radlRounding" RepeatDirection="Horizontal" runat="server" >
                                                    <asp:ListItem Text="Yes" Value="true" ></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="false"></asp:ListItem>
                                                </asp:RadioButtonList>


                                              <%--  <asp:RequiredFieldValidator runat="server" ID="genderRequired" Display="Dynamic"
                                                     ControlToValidate="radlRounding" ErrorMessage="This is an Error"
                                                     ValidationGroup="signUp">*</asp:RequiredFieldValidator>--%>

                                               <%-- <asp:RequiredFieldValidator runat="server" ID="RFV123" ValidationGroup="VG1" ControlToValidate="radlRounding" 
                                                    ErrorMessage="PLEASE SELECT YES/NO"/>--%>
                                              
                                           
<%--                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1"
                                                    ControlToValidate="radlRounding" Text="Required" ErrorMessage="PLEASE SELECT YES/NO"/>--%>

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
                                            runat="server" id="btnSave" onserverclick="btnSave_ServerClick">New                                   
                                        </button>
                                        <button class="btn btn-info" type="button"
                                            runat="server" id="btnClear" onserverclick="btnClear_ServerClick">
                                            Clear <i class="fa fa-refresh"></i>
                                        </button>
                                    </div>
                                    <div class="panel-body ">
                                        <div class="alert alert-info" style="padding: 8px;" runat="server" id="actionInfo">
                                            Press New to Add Invoice/Credit Note.
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

                    <div class="tab-pane" id="modifyinvcrednote">
                        <div class="form-group">
                            <div class="alert alert-info" runat="server" id="updateActionDiv">
                                Click on respective buttons for Modify / Delete.
                            </div>
                            <div class="adv-table nice-scroll-grid">
                                <asp:GridView ID="grdinvcrnote" runat="server"
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
                                                <asp:Label ID="InvID" runat="server" Text='<%# Eval("iInvoiceId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Invoice Number">
                                            <ItemTemplate>
                                                <asp:Label ID="invoiceno" runat="server" Text='<%# Eval("sInvoiceNo") %>'></asp:Label>
                                            </ItemTemplate>

<%--                                            <EditItemTemplate>
                                                <asp:label ID="edtinvoiceno" runat="server" Text='<%# Bind("sInvoiceNo") %>'>
                                                </asp:label>

                                            </EditItemTemplate>--%>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="InvDate" runat="server" Text='<%# Eval("dDate") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--<EditItemTemplate>
                                                <asp:TextBox ID="edtInvDate" TextMode="Date" runat="server" Text='<%# Bind("dDate","{0:yyyy-MM-dd}") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Client Name">
                                            <ItemTemplate>
                                                <asp:Label ID="clientname" runat="server" Text='<%# Eval("sClientName") %>'></asp:Label>
                                            </ItemTemplate>

                                           <%-- <EditItemTemplate>
                                                <asp:label ID="edtclientname" runat="server" Text='<%# Bind("sClientName") %>'>
                                                </asp:label>

                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

            <%--                            <asp:TemplateField HeaderText="Type of Invoice">
                                            <ItemTemplate>
                                                <asp:Label ID="typeofinvoice" runat="server" Text='<%# Eval("sTypeOfInvoice") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="edttypeofinvoice" runat="server" Text='<%# Bind("sTypeOfInvoice") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>

                                          
                                        <asp:TemplateField HeaderText="Contract Name">
                                            <ItemTemplate>
                                                <asp:Label ID="contractname" runat="server" Text='<%# Eval("sContractName") %>'></asp:Label>
                                            </ItemTemplate>

                                           <%-- <EditItemTemplate>
                                                <asp:Label ID="edtcontractname" runat="server" Text='<%# Bind("sContractName") %>'>
                                                </asp:Label>
                                              
                                            </EditItemTemplate>--%>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Commodity Name">
                                            <ItemTemplate>
                                                <asp:Label ID="commodityname" runat="server" Text='<%# Eval("sCommodityName") %>'></asp:Label>
                                            </ItemTemplate>

                                         <%--   <EditItemTemplate>
                                                <asp:Label ID="edtcommodityname" runat="server" Text='<%# Bind("sCommodityName") %>'>
                                                </asp:Label>
                                                
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="HSN Code">
                                            <ItemTemplate>
                                                <asp:Label ID="hsncode" runat="server" Text='<%# Eval("sHsnCode") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--<EditItemTemplate>
                                                <asp:TextBox ID="edthsncode" runat="server" Text='<%# Bind("sHsnCode") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Value for Lot Size">
                                            <ItemTemplate>
                                                <asp:Label ID="valueforlotsize" runat="server" Text='<%# Eval("iValueLotSize") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--<EditItemTemplate>
                                                <asp:TextBox ID="edtvalueforlotsize" TextMode="Number" runat="server" Text='<%# Bind("iValueLotSize") %>'>
                                                </asp:TextBox>

                                             <asp:CompareValidator ID="CompareValidator4" runat="server" ValueToCompare="0" ControlToValidate="edtvalueforlotsize" 
                                            ErrorMessage="Value must be Greater then 0 !!!" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>

                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>
                                          
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="qty" runat="server" Text='<%# Eval("iQty") %>'></asp:Label>
                                            </ItemTemplate>

                                         <%--   <EditItemTemplate>
                                                <asp:TextBox ID="edtqty" TextMode="Number" runat="server" Text='<%# Bind("iQty") %>'>
                                                </asp:TextBox>

                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ValueToCompare="0" ControlToValidate="edtqty" 
                                                ErrorMessage="Value must be Greater then 0 !!!" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>
                                            </EditItemTemplate>--%>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Value">
                                            <ItemTemplate>
                                                <asp:Label ID="totalvalue" runat="server" Text='<%# Eval("nTotalValue") %>'></asp:Label>
                                            </ItemTemplate>

                                           <%-- <EditItemTemplate>
                                                <asp:TextBox ID="edttotalvalue" runat="server" Text='<%# Bind("nTotalValue") %>'>
                                                </asp:TextBox>
                                                <asp:RegularExpressionValidator ID="numberswithdecimalvalidation4" runat="server"
                                                    ErrorMessage="Only Positive Numbers Allowed" ControlToValidate="edttotalvalue"
                                                    ValidationExpression="((\d+)((\.\d{1,2})?))$">
                                               </asp:RegularExpressionValidator> 

                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="GST">
                                            <ItemTemplate>
                                                <asp:Label ID="gst" runat="server" Text='<%# Eval("nGst") %>'></asp:Label>
                                            </ItemTemplate>

                                          <%--  <EditItemTemplate>
                                                <asp:TextBox ID="edtgst" runat="server" Text='<%# Bind("nGst") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Other Tax">
                                            <ItemTemplate>
                                                <asp:Label ID="othertax" runat="server" Text='<%# Eval("nOtherTax") %>'></asp:Label>
                                            </ItemTemplate>

<%--                                            <EditItemTemplate>
                                                <asp:TextBox ID="edtothertax" runat="server" Text='<%# Bind("nOtherTax") %>'>
                                                </asp:TextBox>

                                                <asp:RegularExpressionValidator ID="numberswithdecimalvalidation2" runat="server"
                                                    ErrorMessage="Only Numbers Allowed" ControlToValidate="edtothertax"
                                                    ValidationExpression="((\d+)((\.\d{1,2})?))$">
                                               </asp:RegularExpressionValidator> 

                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Discount">
                                            <ItemTemplate>
                                                <asp:Label ID="discount" runat="server" Text='<%# Eval("nDiscount") %>'></asp:Label>
                                            </ItemTemplate>

                                          <%--  <EditItemTemplate>
                                                <asp:TextBox ID="edtdiscount" MaxLength="100" runat="server" Text='<%# Bind("nDiscount") %>'>
                                                </asp:TextBox>

                                                <asp:RegularExpressionValidator ID="numberswithdecimalvalidation3" runat="server"
                                                    ErrorMessage="Only Numbers Allowed" ControlToValidate="edtdiscount"
                                                    ValidationExpression="^(0 |[1-9]\d*)$">
                                               </asp:RegularExpressionValidator> 

                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Rounding">
                                            <ItemTemplate>
                                                <asp:Label ID="rounding" runat="server" Text='<%# Eval("bIsRoundOff") %>'></asp:Label>
                                            </ItemTemplate>                                            
                                          <%--  <EditItemTemplate>
                                               
                                                
                                                 <asp:RadioButtonList ID="edtrounding" RepeatDirection="Horizontal" runat="server" >
                                                    <asp:ListItem Text="Yes" Value="true"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="false"></asp:ListItem>
                                                </asp:RadioButtonList>

                                         

                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <%--<asp:CommandField ShowEditButton="True" HeaderText="Actions" HeaderStyle-CssClass="nosort" />--%>
                                                <asp:TemplateField  HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="editLink" class="edit"  runat="server" data-toggle="tab"  
                                                    data-user-id='<%# Eval("iInvoiceId") %>'
                                                    href="javascript:;#addNewinvcrednote"  >Edit <i class="fa fa-edit"></i>&nbsp;</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="A1" class="delete" runat="server"
                                                    data-invoice-id='<%# Eval("iInvoiceId") %>'
                                                    data-client-name='<%# Eval("sClientName") %>' href="javascript:;">Delete</a>
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

    <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modDeleteinvcrnote" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                    <h4 class="modal-title">Delete Invoice Credit Note.</h4>
                </div>
                <div class="modal-body">

                    <div role="form">
                        <div class="form-group">
                            <h3>Are you sure you want to delete Invoice Credit Note?</h3>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2 col-sm-2 control-label">Invoice ID </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDelinvcrnoteID" name="txtDelinvcrnoteID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="txtDelHidden" runat="server" />
                                </div>
                            </div>
                        </div>
                        <!-- Id -->
                        <div class="form-group">
                            <div class="row">
                                <label class="col-sm-2 col-sm-2 control-label">Client Name </label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDelinvcrnoteName" name="txtDelinvcrnoteName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <!-- Name -->

                        <div class="form-group">
                            <div class="row">
                                <div class="panel-body" style="text-align: center">
                                    <button class="btn btn-success" type="button"
                                        runat="server" id="btnDeleteInvoiceCreditNt" onserverclick="btnDeleteInvoiceCreditNt_ServerClick">
                                        <i class="fa fa-trash"></i>Delete Invoice
                                    </button>
                                    <button class="btn btn-danger" type="button"
                                        id="btnCancelDeleteinvcrnote">
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
    <script type="text/javascript" src="js/pagesjs/InvoiceCreditNote.js"></script>
</asp:Content>
