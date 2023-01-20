<%@ Page Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="Admin_CommTrex.Payment" %>

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
        <h3>Add/Modify/Delete Payment</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Payment </a>
            </li>
            <li class="active">Payment Details </li>
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
                        <a href="#addNewPayment" data-toggle="tab">Add Payment</a>
                    </li>
                    <li class="" id="tab_modifyPayment">
                        <a href="#modifyPayment" data-toggle="tab">View/Modify Payment</a>
                    </li>
                </ul>
            </header>

            <div class="panel-body">
                <div class="tab-content">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <!-- //Add new user tab -->
                    <div class="tab-pane active" id="addNewPayment">
                        <div class="row">
                            <section class="panel" id="pnlSecurity">
                                <header class="panel-heading">
                                    Payment Details
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
                                                 <label class="col-sm-2 col-sm-2 control-label">Client Name : </label>
                                                 <%--<div class="col-sm-10">
                                                <asp:TextBox ID="txtClientName" name="txtClientName" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Client Name" data-original-title="Client Name"></asp:TextBox>
                                            </div>--%>
                                                 <div class="col-sm-10">
                                                     <asp:DropDownList ID="addClientName"  runat="server" Font-Bold="True" AutoPostBack="True"
                                                         CssClass="form-control m-bot15">
                                                       
                                                     </asp:DropDownList>
                                                 </div>
                                             </div>
                                     <%--   <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">
                                                Transaction Type :
                                              <span style="color: red">*</span>
                                            </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddlTransactionType" runat="server" Font-Bold="True" AutoPostBack="false" CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Please Select --" />
                                                    <asp:ListItem Value="1" Text="Credit" />
                                                    <asp:ListItem Value="2" Text="Debit" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>--%>
                                      <%--  <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Transaction Type1 : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtTxnTyp" name="txtTxnTyp" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Transaction Type" data-original-title="Transaction Type"></asp:TextBox>
                                            </div>
                                        </div>--%>
                                     

                                          <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Type Of Payment : </label>
                                            <div class="col-sm-10">
                                             
                                                <asp:DropDownList ID="ddlTypeOfPayment"   runat="server" Font-Bold="True"  AutoPostBack="true" CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Please Select --" />
                                                    <asp:ListItem Value="1" Text="Debit" />
                                                    <asp:ListItem Value="2" Text="Creadit" />                                           

                                                </asp:DropDownList>
                                            </div>
                                        </div> 

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Mode Of Payment : </label>
                                            <div class="col-sm-10">
                                                <%--OnSelectedIndexChanged="ddlModeOfPayment_SelectedIndexChanged"--%>
                                                <asp:DropDownList ID="ddlModeOfPayment" OnSelectedIndexChanged="paymentMethod_SelectedIndexChanged"  runat="server" Font-Bold="True"  AutoPostBack="true" CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Please Select --" />
                                                    <asp:ListItem Value="1" Text="Online" />
                                                    <asp:ListItem Value="2" Text="Cash" />
                                                    <asp:ListItem Value="3" Text="Cheque" />
                                                    <asp:ListItem Value="4" Text="Combined Payment" />

                                                </asp:DropDownList>
                                            </div>
                                        </div> 

                                         <asp:Panel runat="server" ID="RadioSelectPanel" class="form-group">
                                          <div>
                                            <label class="col-sm-2 col-sm-2 control-label">Payment Method Document Type : </label>
                                            <div class="col-sm-10">
                                                <%--OnSelectedIndexChanged="ddlModeOfPayment_SelectedIndexChanged"--%>
                                                <asp:RadioButtonList ID="DropDownList1"   RepeatDirection="Horizontal" OnSelectedIndexChanged="drdCommType_SelectedIndexChanged" runat="server" CssClass="radBtnShoppingBag" Font-Bold="True"  AutoPostBack="true" >
                                                    <asp:ListItem Value="0"   Text="Image" />

                                                    <asp:ListItem Value="1"  Text="Cheque Number" />
                                                  
                                                </asp:RadioButtonList>
                                            </div>
                                        </div></asp:Panel>
                                   
                                        <asp:Panel runat="server" ID="Browser" class="form-group">
                                                        <div>
                                            <label class="col-sm-2 control-label" id="data1">
                                                Cheque Image Upload
                                                       <span style="color: red">*</span>
                                            </label>
                                            <div class="col-sm-2">
                                                <button class="btn gbtns" type="button" 
                                                    data-toggle="modal" data-target="#modReport" btn-action-image="Class" 
                                                    runat="server" id="btnReport">
                                                    <i class="fa fa-folder-open"></i>Browse
                                                </button>
                                            </div>
                                            <label class="col-sm-2 control-label">Path </label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="DocImgPath" name="DocImgPath" runat="server"
                                                    CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Display an Image Path"
                                                    data-original-title="Display an Image Path " Enabled="false"></asp:TextBox>
                                            </div>
                                        </div></asp:Panel>
                                    
                                              <asp:Panel runat="server" ID="ChequePanel" class="form-group">          
                                                     <div  >
                                            <label class="col-sm-2 col-sm-2 control-label">Cheque Number : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="ChequeBox" TextMode="Number" name="ChequeBox"  runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="TransactionNumtooltip" title="" placeholder="cheque Number" data-original-title="Check Number"></asp:TextBox>
                                            </div>
                                        </div>
                                              </asp:Panel>

                                
                                           <asp:Panel runat="server" ID="TransactionPanel" class="form-group">
                                                     <div  >
                                            <label class="col-sm-2 col-sm-2 control-label">transaction Number : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="TransactionNum" TextMode="Number" name="ChequeBox" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="transaction Number" data-original-title="Check Number"></asp:TextBox>
                                            </div>
                                        </div></asp:Panel>
                                     <%--   <div class="form-group" runat="server" visible="false" id="chequedetail">
                                            <label class="col-sm-2 col-sm-2 control-label">Cheque No : </label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtChequeNo" name="txtChequeNo" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Cheque No" data-original-title="Cheque No"></asp:TextBox>
                                            </div>
                                            <label class="col-sm-2 col-sm-2 control-label">Bank Name : </label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtBankName" name="txtBankName" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Bank Name" data-original-title="Bank Name"></asp:TextBox>
                                            </div>

                                        </div>--%>
                                     <%--   <div class="form-group" visible="false" runat="server" id="chequedetail1">
                                            <label class="col-sm-2 col-sm-2 control-label">Amount : </label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtAmount" name="txtAmount" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Amount" data-original-title="Amount"></asp:TextBox>
                                            </div>
                                            <label class="col-sm-2 col-sm-2 control-label">Image Upload : </label>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtImage" name="txtImage" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Bank Name" data-original-title="Bank Name"></asp:TextBox>
                                            </div>

                                        </div>--%>
                                        
                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Against Invoice No : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtAgainstInvNo"  AutoPostBack="true" OnTextChanged="ChangeEventForAmount" TextMode="Number" name="txtAgainstInvNo" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Against Invoice No" data-original-title="Against Invoice No"></asp:TextBox>
                                            </div>
                                        </div>

                                           <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Amount : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtAmt" ReadOnly="true" TextMode="Number" name="txtAmount" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Amount" data-original-title="Amount"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group" style="visibility: hidden;">
                                            <label class="col-sm-2 col-sm-2 control-label">dropdown : </label>
                                            <div class="col-sm-10">
                                                 <asp:DropDownList ID="dllForAmount"   runat="server" Font-Bold="True"  AutoPostBack="true" CssClass="form-control m-bot15">
                                               
                                                 

                                                </asp:DropDownList>
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
                                            data-open-on="Save"
                                               data-open-panels="pnlSecurity"
                                            runat="server" id="btnSave" onserverclick="btnSave_ServerClick">
                                            New 
                                        </button>

                                        <button class="btn btn-info" type="button" data-open-panels="pnlSecurity" onserverclick="btnClear_ServerClick"
                                          data-open-on="Cancel"  btn-action="Cancel" runat="server" id="btnClear">
                                            Clear
                                        </button>
                                    </div>
                                    <div class="panel-body ">
                                        <div class="alert alert-info" style="padding: 8px;" runat="server" id="actionInfo">
                                            Press New to Add Payment.
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

                    <div class="tab-pane" id="modifyPayment">
                        <div class="form-group">
                            <div class="alert alert-info" runat="server" id="updateActionDiv">
                                Click on respective buttons for Modify / Delete.
                            </div>
                            <div class="adv-table nice-scroll-grid">
                                <asp:GridView ID="grdUser" runat="server"
                                    EnableModelValidation="True" AutoGenerateColumns="False"
                                    DataKeyNames=""
                                    OnRowEditing="GridView_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating"
                                    RowStyle-CssClass="gradeA"
                                    class="dynamic-table-grid display table table-bordered table-striped">
                                      <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="paymentId" runat="server" Text='<%# Eval("iPayId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="sClientName">
                                            <ItemTemplate>
                                                <asp:Label ID="ClientName" runat="server" Text='<%# Eval("sClientName") %>'></asp:Label>
                                            </ItemTemplate>

                                       

                                        </asp:TemplateField>


                              

                                        <asp:TemplateField HeaderText="nAmount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnAmount" runat="server" Text='<%# Eval("nAmount") %>'></asp:Label>
                                            </ItemTemplate>

                                           <%-- <EditItemTemplate>
                                                <asp:TextBox ID="Amount" runat="server" Text='<%# Bind("nAmount") %>'>
                                                </asp:TextBox>
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                ControlToValidate="Amount" runat="server"
                                                ErrorMessage="Only Numbers allowed"
                                                ValidationExpression="((\d+)((\.\d{1,2})?))$">
                                                </asp:RegularExpressionValidator>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                   

                                             <asp:TemplateField HeaderText="Type Of Payment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstypeOfPayment" runat="server" Text='<%# Eval("TypeOfPayment") %>'></asp:Label>
                                            </ItemTemplate>

                                          <%--  <EditItemTemplate>                                     
                                                  <asp:DropDownList ID="TypeOfPayment" SelectedItem='<%# Eval("TypeOfPayment") %>' runat="server" Font-Bold="True"  AutoPostBack="true" CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Please Select --" />
                                                    <asp:ListItem Value="1" Text="Credit" />
                                                    <asp:ListItem Value="2" Text="dabit" />
                                                 

                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="ModeOfPayment">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsModeOfPayment" runat="server" Text='<%# Eval("sModeOfPayment") %>'></asp:Label>
                                            </ItemTemplate>

                                            <%--<EditItemTemplate>                                     
                                                  <asp:DropDownList ID="ModeOfPayment" SelectedItem='<%# Eval("sModeOfPayment") %>' runat="server" Font-Bold="True"  AutoPostBack="true" CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Please Select --" />
                                                    <asp:ListItem Value="1" Text="Online" />
                                                    <asp:ListItem Value="2" Text="Cash" />
                                                    <asp:ListItem Value="3" Text="Cheque" />
                                                    <asp:ListItem Value="4" Text="Combined Payment" />

                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="AgainstInvNo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsAgainstInvNo" runat="server" Text='<%# Eval("sAgainstInvNo") %>'></asp:Label>
                                            </ItemTemplate>

                                        <%--    <EditItemTemplate>
                                                <asp:TextBox  ID="AgainstInvNo" AutoPostBack="true" OnSelectedIndexChanged="EditChangeEventForAmount" runat="server" Text='<%# Bind("sAgainstInvNo") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderStyle-CssClass="hdrAlgnCntrStyle" HeaderText="View Cheque Documents">
                                            <ItemTemplate>
                                                <asp:Button ID="btnShowGallery1" RowIndex='<%# ((GridViewRow) Container).RowIndex %>'
                                                    runat="server" Text="View Documents" OnClick="btnShowGallery_Click" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                           <%--   <asp:TemplateField HeaderText="Hide Amount" >
                                                                              <EditItemTemplate>                                     
                                                  <asp:DropDownList ID="editdllhideamount" runat="server" Font-Bold="True"  AutoPostBack="true" CssClass="form-control m-bot15">
                                                  
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>--%>
                                           <asp:TemplateField HeaderStyle-CssClass="nosort" Visible="false"  HeaderText="Selection gst"   ControlStyle-CssClass="column-full-width" >
                                                <ItemTemplate>
                                                    <div class="action-buttons-group">
                                                     
                                                        <br /><br>
                                                        <button class="btn gbtns" type="button"
                                                            data-toggle="modal"  data-target="#modReport" 
                                                            runat="server" id="btnImage">
                                                            Browse gst <i class="fa fa-eye-slash"></i>
                                                        </button>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                       <asp:TemplateField HeaderText="Cheque Number">
                                            <ItemTemplate>
                                                <asp:Label ID="Cheque" runat="server" Text='<%# Eval("ChequeNum") %>'></asp:Label>
                                            </ItemTemplate>

                                           <%-- <EditItemTemplate>
                                                <asp:TextBox ID="Chequegb" runat="server" Text='<%# Bind("ChequeNum") %>'>
                                                </asp:TextBox>
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                ControlToValidate="Chequegb" runat="server"
                                                ErrorMessage="Only Numbers allowed"
                                                ValidationExpression="((\d+)((\.\d{1,2})?))$">
                                                </asp:RegularExpressionValidator>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Transaction Number">
                                            <ItemTemplate>
                                                <asp:Label ID="Transaction" runat="server" Text='<%# Eval("TransactionNum") %>'></asp:Label>
                                            </ItemTemplate>

                                           <%-- <EditItemTemplate>
                                                <asp:TextBox ID="TransactionGb" runat="server" Text='<%# Bind("TransactionNum") %>'>
                                                </asp:TextBox>
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                                ControlToValidate="TransactionGb" runat="server"
                                                ErrorMessage="Only Numbers allowed"
                                                ValidationExpression="((\d+)((\.\d{1,2})?))$">
                                                </asp:RegularExpressionValidator>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                           

                                        <%--<asp:CommandField ShowEditButton="True" HeaderText="Actions" HeaderStyle-CssClass="nosort" />--%>
                                            <asp:TemplateField  HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="editLink" class="edit"  runat="server" data-toggle="tab"  
                                                    data-user-id='<%# Eval("iPayId") %>'
                                                    href="javascript:;#addNewPayment"  >Edit <i class="fa fa-edit"></i>&nbsp;</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="A1" class="delete" runat="server"
                                                    data-user-id='<%# Eval("iPayId") %>'
                                                    data-user-name='<%# Eval("sClientName") %>' href="javascript:;">Delete</a>
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
                                    <button class="btn btn-success" type="button" onserverclick="btnDeleteClient_ServerClick"
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
     <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modReport" class="modal fade">
                        <div class="modal-dialog" style="width: 280px;">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                    <h4 class="modal-title">
                                        <asp:Label ID="lblimgnm" runat="server" Text="Select Image Gst "></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <div role="form">
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-9">
                                                    <div class="fileupload fileupload-new"  data-provides="fileupload" data-caption="Cover Image">
                                                        <div class="fileupload-new thumbnail prerview-img-thumb" style="width: 75px; height: 75px;">
                                                            <asp:Image ID="MainImage" runat="server" ImageUrl="http://www.placehold.it/300x250/EFEFEF/AAAAAA&amp;text=no+image" AlternateText="No Image" />
                                                        </div>
                                                        <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 75px; max-height: 75px; line-height: 20px;"></div>
                                                        <div>
                                                            <span class="btn btn-default btn-file">
                                                                <span class="fileupload-new"><i class="fa fa-picture-o"></i>Select image</span>
                                                                <span class="fileupload-exists"><i class="fa fa-undo"></i>Change</span>
                                                                <asp:HiddenField ID="txtImgPathMain" runat="server"></asp:HiddenField>
                                                                <asp:FileUpload ID="FileMainImage1" Height="26px" accept=".pdf .jpg .png" runat="server" class="default" />
                                                            </span>
                                                            <a href="#" class="btn btn-danger fileupload-exists" data-dismiss="fileupload"><i class="fa fa-trash"></i>Remove</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-9">
                                                    <button class="btn gbtn" type="button"
                                                        runat="server" onserverclick="btnSaveImgUpload" id="b1">
                                                        <i class='fa fa-floppy-o'></i>Save 
                                                    </button>
                                                    <%-- <button class="btn btn-success" type="button"
                                                        runat="server" id="btnSaveFileCancel">
                                                        <i class="fa fa-times"></i>Cancel 
                                                    </button>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

     <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="modUserDocuments" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
                                    <h4 class="modal-title">Document Gallery</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="col-md-6 form-group">
                                        <div class="alert toss alert-block fade in">
                                            <%--  <div class="row">--%>
                                            <label class="control-label">PAN Image</label>
                                            <%-- <div class="col-md-9">--%>
                                            <div class="fileupload fileupload-new" data-provides="fileupload" data-caption="Cover Image">
                                                <div class="fileupload-new thumbnail prerview-img-thumb" style="width: 150px; height: 100px;">

                                                    <asp:Image ID="ImgCoverImage" runat="server"  AlternateText="No Image" />
                                                </div>
                                                <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 200px; max-height: 150px; line-height: 20px;"></div>
                                            </div>
                                            <!-- //file upload-->
                                            <div class="">
                                                <span id="enLargeImg"><a id="enlargeImgLnk1" class="btn gbtn1" href="#" target="_blank" runat="server" style="text-decoration: none;">View</a></span>
                                            </div>
                                        </div>
                                        <!-- //alert-success div-->
                                    </div>
                                    <!-- //form-group -->


                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-9">
                                                <%--   <button id="btnImageModify" type="submit" class="btn btn-primary" runat="server" data-toggle="modal" data-target="#modSendingMessage" onserverclick="btnImageModify_ServerClick">Upload</button>--%>
                                                <button id="btnImageCancel" type="button" class="btn gbtn1" data-dismiss="modal">Cancel</button>
                                            </div>
                                        </div>
                                    </div>
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

    <!--dynamic table-->
    <script type="text/javascript" src="js/pagesjs/payment.js"></script>
</asp:Content>
