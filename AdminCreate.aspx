<%@ Page Language="C#" MasterPageFile="~/AdminEx.Master" AutoEventWireup="true" CodeBehind="AdminCreate.aspx.cs" Inherits="Admin_CommTrex.AdminCreate" %>

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
        <h3>Add/Modify/Delete Admin details</h3>
        <ul class="breadcrumb">
            <li>
                <a href="#">Master </a>
            </li>
            <li class="active">Admin Master </li>
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
                        <a href="#addNewUser" data-toggle="tab">Add New Admin</a>
                    </li>
                    <li class="" id="tab_modifyproducts">
                        <a href="#modifyUser" data-toggle="tab">View/Modify Admin</a>
                    </li>
                    <%-- <li class="" id="PassChange">
                        <a href="#ChangePass" data-toggle="tab">Change Password</a>
                    </li>--%>
                </ul>
            </header>

            <div class="panel-body">
                <div class="tab-content">
                        <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <!-- //Add new user tab -->
                    <div class="tab-pane active" id="addNewUser">
                        <div class="row">
                            <section class="panel" id="pnlSecurity">
                                <header class="panel-heading">
                                    Admin Details
							                <span class="tools pull-right">
                                                <a href="javascript:;" class="fa fa-chevron-down"></a>
                                                <span class="collapsible-server-hidden">
                                                    <asp:HiddenField runat="server" ID="HiddenField3" EnableViewState="true" Value="c" />
                                                </span>
                                            </span>
                                </header>
                                <div class="panel-body">
                                    <div class="form-horizontal adminex-form">
                                   
                                    
                                        <div class="form-group1">
                                            <label class="col-sm-2 col-sm-2 control-label">User Name : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtUserName" name="txtUserName" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="User Name" data-original-title="User Name"></asp:TextBox>

                                                <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                            ControlToValidate="txtUserName" ErrorMessage="Please Enter Valid User Name!!!"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                     ></asp:RegularExpressionValidator>--%>
                                                <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                            ControlToValidate="txtUserName" ErrorMessage="Please Enter Valid User Name!!!"     
                                                      ValidationExpression1="[6-9]{1}[0-9]{9}"></asp:RegularExpressionValidator>--%>
                                            </div>
                                        </div>
                                                
                                                 
                                        <div class="form-group1">
                                            <label class="col-sm-2 col-sm-2 control-label">
                                                Password
                                              <span style="color: red">*</span>
                                            </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtPass" name="txtPass" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" TextMode="Password" placeholder="Enter Password" data-original-title="Enter Password"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group1">
                                            <label class="col-sm-2 col-sm-2 control-label">Email id :  <span style="color: red">*</span></label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtEmail" name="txtEmail" TextMode="Email" runat="server" CssClass="form-control tooltips"
                                                    data-toggle="tooltip" title="" placeholder="Email" data-original-title="Email"></asp:TextBox>

                                               <asp:RegularExpressionValidator ID="revEmailID" runat="server"
                                                            ControlToValidate="txtEmail" ErrorMessage="Please Enter Valid Email Id!!!"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            </div>
                                        </div> 
                                        <div class="form-group1">
                                            <label class="col-sm-2 col-sm-2 control-label">Contact number :  <span style="color: red">*</span></label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtphnno" name="txtphnno"  runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    MaxLength="10" data-toggle="tooltip" title="" placeholder="Contact number" data-original-title="Contact number"></asp:TextBox>
                                                   
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  
                                                            ControlToValidate="txtphnno" ErrorMessage="Invalid Contact Number !!! "  
                                                             ValidationExpression="[6-9]{1}[0-9]{9}"></asp:RegularExpressionValidator> 
                                             <%--   <button style="float: right;" runat="server" class="pnl-opener btn gbtn" type="button" onserverclick="btnSendOTP_Click">Send Otp </button>--%>
                                            </div>
                                        </div>

                                 

                                        <div class="form-group1">
                                            <label class="col-sm-2 col-sm-2 control-label">Employee Code :  <span style="color: red">*</span></label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtEmpCode" name="txtEmpCode" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="Employee Code" data-original-title="Employee Code"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group1">
                                            <label class="col-sm-2 col-sm-2 control-label">Admin Role : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddl_AdminRole" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select Admin Role --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Super Admin"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Admin"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Accounts"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Contract approver"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group1">
                                            <label class="col-sm-2 col-sm-2 control-label">Action : </label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="ddl_Action" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select Action --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Suspend"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Delete"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
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
                                            Press New to Add Admin.
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

                    <div class="tab-pane" id="ChangePass">
                        <div class="form-group">
                            <div class="alert alert-info" runat="server" id="Div2">
                                Click here to change password. 
                            </div>

                              <section class="panel" id="Section1">
                                <header class="panel-heading">
                                    Admin Password Details
							              <%--  <span class="tools pull-right">
                                                <a href="javascript:;" class="fa fa-chevron-down"></a>
                                                <span class="collapsible-server-hidden">
                                                    <asp:HiddenField runat="server" ID="HiddenField1" EnableViewState="true" Value="c" />
                                                </span>
                                            </span>--%>
                                </header>
                                <div class="panel-body">
                                    <div class="form-horizontal adminex-form">

                                        <div class="form-group">
                                            <label class="col-sm-2 col-sm-2 control-label">Enter Registered Number : </label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="VerifyNumText" name="txtNumber" runat="server" CssClass="form-control tooltips" data-trigger="hover"
                                                    data-toggle="tooltip" title="" placeholder="User Number" data-original-title="User Number"></asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="form-group">
                            <div class="row" id="sendOtpRow" style="text-align: center;">
                                <button class="btn gbtn" type="button" onclick="return sendOtpForAcceptPrev();"
                                    runat="server" id="btnSendOtp">
                                    Send OTP <i class="fa fa-send-o"></i>
                                </button>
                                                &nbsp;&nbsp;
                                                <label id="cntDwnLbl" class="retry-countdown"></label>
                            </div>
                            <div class="row">
                                <label id="otpSentLbl" class="col-sm-12 control-label lbl-pad-top" style="text-align: center;font-size: 16px;margin-top:40px; display: none">OTP sent on selected mobile number.</label>
                            </div>
                        </div>


                                           <div class="form-group" id="PassPanel" style="display:none">
                            <div  id="otpDivRow" style="display: none">
                                <label class="col-sm-1 control-label lbl-pad-top">Enter OTP<span class="mandate"> *</span></label>
                                <div class="col-sm-2">
                                    <asp:TextBox ID="txtOtp" name="txtOtp" runat="server" CssClass="form-control" MaxLength="5" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;" ></asp:TextBox>
                                </div>
                            </div>
                                                 
                       
                        
                                                <div id="Div1" class="row"  runat="server" >
                                               <label class="col-sm-2 mt1"> Enter New Password </label>
                                          <div class="col-sm-3 ">
                                    <asp:TextBox ID="NewPassword" name="txtNewPass" runat="server" CssClass="form-control"  placeholder="New Password" ></asp:TextBox>
                                       </div>
                                       <div class="col-sm-2" runat="server" id="Div3">
                                           <button class="btn gbtn" type="button" style="margin-left: 70px;" runat="server" id="Btn_ChangePass" onserverclick="btnSubmit_click">Save</button>

                                         </div>
                                         </div>                                                     
                                                </div>
                                        
                                        
                                        </div>
                                    </div>
                                  </section>





                            </div>
                        </div>

                    <div class="tab-pane" id="modifyUser">
                        <div class="form-group">
                            <div class="alert alert-info" runat="server" id="updateActionDiv">
                                Click on respective buttons for Modify / Delete.
                            </div>
                            <div class="adv-table nice-scroll-grid">
                                <asp:GridView ID="grdAdmin" runat="server"
                                    EnableModelValidation="True" AutoGenerateColumns="False"
                                    DataKeyNames="iAdminId"
                                     OnRowEditing="GridView1_RowEditing" 
                                    OnRowUpdating="GridView1_RowUpdating1" 
                                    OnRowCancelingEdit="GridView1_RowCancelingEdit" 
                                    RowStyle-CssClass="gradeA"
                                    class="dynamic-table-grid display table table-bordered table-striped">
                                      <Columns>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="UID" runat="server" Text='<%# Eval("iAdminId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="username" runat="server" Text='<%# Eval("sUserName") %>'></asp:Label>
                                            </ItemTemplate>
                                             <%--  <EditItemTemplate>
                                                <asp:TextBox ID="gusername" runat="server" Text='<%# Bind("sUserName") %>'>
                                                </asp:TextBox>
                                                 
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                           <%-- <asp:TemplateField HeaderText="Password">
                                            <ItemTemplate>
                                                <asp:Label ID="password" runat="server" Text='<%# Eval("sPassword") %>'></asp:Label>
                                            </ItemTemplate>
                                           
                                        </asp:TemplateField>--%>

                                          <asp:TemplateField HeaderText="EmailID">
                                            <ItemTemplate>
                                                <asp:Label ID="emailid" runat="server" Text='<%# Eval("sEmailId") %>'></asp:Label>
                                            </ItemTemplate>
                                             <%--  <EditItemTemplate>

                                                <asp:TextBox ID="gemailid" runat="server" Text='<%# Bind("sEmailId") %>'>
                                                </asp:TextBox>
                                                   <asp:RegularExpressionValidator ID="revEmailID" runat="server"
                                                            ControlToValidate="gemailid" ErrorMessage="Please Enter Valid Email ID !!!" 
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ContactNumber">
                                           <ItemTemplate>
                                                <asp:Label ID="contactnumber" runat="server" Text='<%# Eval("sContactNumber") %>'></asp:Label>
                                            </ItemTemplate>
                                             <%--<EditItemTemplate>

                                                <asp:TextBox ID="gcontactnumber" runat="server" MaxLength="10" Text='<%# Bind("sContactNumber") %>'>
                                                </asp:TextBox>
                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                            ControlToValidate="gcontactnumber" ErrorMessage="Invalid Contact Number !!!"  
                                                             ValidationExpression="[6-9]{1}[0-9]{9}"></asp:RegularExpressionValidator> 
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                   <asp:TemplateField HeaderText="EmployeeCode">
                                            <ItemTemplate>
                                                <asp:Label ID="employeecode" runat="server" Text='<%# Eval("sEmployeeCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <%-- <EditItemTemplate>

                                                <asp:TextBox ID="gemployeecode" runat="server" Text='<%# Bind("sEmployeeCode") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="AdminRole">
                                            <ItemTemplate>
                                                <asp:Label ID="adminrole" runat="server" Text='<%# Eval("sAdminRole") %>'></asp:Label>
                                            </ItemTemplate>
                                            <%-- <EditItemTemplate>

                                                  <asp:DropDownList ID="gadminrole" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select Admin Role --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Super Admin"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Admin"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Accounts"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Contract approver"></asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>

                                        </asp:TemplateField>

                                                <asp:TemplateField HeaderText="UserAction">
                                            <ItemTemplate>
                                                <asp:Label ID="useraction" runat="server" Text='<%# Eval("sAction") %>'></asp:Label>
                                            </ItemTemplate>
                                                    <%-- <EditItemTemplate>
                                                          <asp:DropDownList ID="guseraction" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select Action --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Suspend"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Delete"></asp:ListItem>
                                                </asp:DropDownList>
                                             
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Action" Visible="false">
                                              
                                               <%-- <asp:LinkButton   data-user-id='<%# Eval("iAdminId") %>'
                                                    data-user-name='<%# Eval("sUserName") %>' ID="lnkDeleteItem" OnClick="btnDeleteAdmin_ServerClick" class="deleteItem" runat="server">&nbsp;Delete</asp:LinkButton>--%>
                                             <%-- <EditItemTemplate>
                                                          <asp:DropDownList ID="ddlapproval" runat="server" Font-Bold="True" AutoPostBack="True"
                                                    CssClass="form-control m-bot15">
                                                    <asp:ListItem Value="0" Text="-- Select Action --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Approved"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Pending"></asp:ListItem>
                                                   
                                                </asp:DropDownList>
                                            </EditItemTemplate>--%>
                                        </asp:TemplateField>

                                       <%-- <asp:CommandField ShowEditButton="True" HeaderText="Actions" HeaderStyle-CssClass="nosort" />--%>

                                          <asp:TemplateField  HeaderText="Actions">
                                            <ItemTemplate>
                                                <a id="editLink" class="edit"  runat="server" data-toggle="tab"  
                                                    data-admin-id='<%# Eval("iAdminId") %>'
                                                    href="javascript:;#addNewUser">Edit <i class="fa fa-edit"></i>&nbsp;</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="nosort" HeaderText="Actions">
                                            <ItemTemplate> 
                                               <%-- <asp:LinkButton   data-user-id='<%# Eval("iAdminId") %>'
                                                    data-user-name='<%# Eval("sUserName") %>' ID="lnkDeleteItem" OnClick="btnDeleteAdmin_ServerClick" class="deleteItem" runat="server">&nbsp;Delete</asp:LinkButton>--%>
                                                <a id="A1" class="delete" runat="server"
                                                    data-admin-id='<%# Eval("iAdminId") %>'          
                                                    data-admin-name='<%# Eval("sUserName") %>' href="javascript:;">Delete</a>

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
                                    <button class="btn btn-success" type="button"
                                        runat="server" id="btnDeleteAdmin" onserverclick="btnDeleteAdmin_ServerClick">
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
    <script type="text/javascript" src="js/pagesjs/AdminCreate.js"></script>
</asp:Content>
